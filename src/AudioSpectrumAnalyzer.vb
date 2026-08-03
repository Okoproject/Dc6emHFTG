Imports NAudio.Wave
Imports NAudio.Dsp

''' <summary>
'''     WASAPIループバック録音とFFTにより、実際にシステムから再生されている音声の帯域別スペクトラムを解析するクラス。
'''     既定の再生デバイス全体をキャプチャするため、OkoshiMAX以外の音（ブラウザの別タブの動画、通知音など）も
'''     混ざって解析される点に注意。OkoshiMAXのみの音声を分離するには、Windows 10の比較的新しいビルドが必要な
'''     プロセス指定ループバック（ActivateAudioInterfaceAsync等）の実装が要るが、対応コストが大きいため見送っている。
''' </summary>
Public Class AudioSpectrumAnalyzer
    Implements IDisposable

#Region "定数"

    ' FFTのサンプル数（2のべき乗）。値が大きいほど低域の分解能は上がるが応答が遅くなる。
    Private Const FftLength As Integer = 2048

    ' FftLength = 2^FftPow
    Private Const FftPow As Integer = 11

    ' 直前の解析に使うサンプルバッファの最大保持数（FFT窓の数倍程度で十分）
    Private Const MaxBufferedSamples As Integer = FftLength * 4

    ' 無音とみなす音量下限（dB）。WASAPIループバックの実測値は聴感よりかなり小さいため、
    ' EqualizerForm.RmsFloorDbと同じ値まで広げ、静かな帯域の変化がここで潰れないようにする。
    Private Const FloorDb As Double = -90.0

#End Region

    Private ReadOnly _bandFrequenciesHz() As Integer
    Private ReadOnly _capture As WasapiLoopbackCapture
    Private ReadOnly _sampleBuffer As New List(Of Single)
    Private ReadOnly _bufferLock As New Object()
    Private ReadOnly _levelsLock As New Object()
    Private _latestBandLevelsDb() As Double
    Private _disposed As Boolean = False

    ''' <summary>
    '''     指定した中心周波数の帯域それぞれについて解析するアナライザーを作成し、録音を開始する。
    '''     録音デバイスが取得できない環境では、以降<see cref="GetBandLevelsDb"/>が無音値を返し続ける。
    ''' </summary>
    ''' <param name="bandFrequenciesHz">解析する各帯域の中心周波数(Hz)。低い順に並んでいること。</param>
    Public Sub New(bandFrequenciesHz() As Integer)
        If bandFrequenciesHz Is Nothing OrElse bandFrequenciesHz.Length = 0 Then
            Throw New ArgumentException("bandFrequenciesHzは1件以上指定してください。", "bandFrequenciesHz")
        End If

        _bandFrequenciesHz = bandFrequenciesHz
        ReDim _latestBandLevelsDb(_bandFrequenciesHz.Length - 1)
        For i As Integer = 0 To _latestBandLevelsDb.Length - 1
            _latestBandLevelsDb(i) = FloorDb
        Next

        Try
            _capture = New WasapiLoopbackCapture()
            AddHandler _capture.DataAvailable, AddressOf OnDataAvailable
            _capture.StartRecording()
        Catch ex As Exception
            ' 既定の再生デバイスが取得できない等の環境依存の失敗。以降は無音値を返す。
            Debug.WriteLine("AudioSpectrumAnalyzer: WASAPIループバック録音を開始できませんでした。" & ex.Message)
            _capture = Nothing
        End Try
    End Sub

    ''' <summary>
    '''     各帯域の直近の実測音量（RMS、dB）を取得する。録音が開始できていない場合は全帯域が無音値。
    ''' </summary>
    Public Function GetBandLevelsDb() As Double()
        SyncLock _levelsLock
            Return CType(_latestBandLevelsDb.Clone(), Double())
        End SyncLock
    End Function

    Private Sub OnDataAvailable(sender As Object, e As WaveInEventArgs)
        Dim newSamples = ConvertToMonoFloatSamples(e.Buffer, e.BytesRecorded, _capture.WaveFormat)
        If newSamples.Count = 0 Then Return

        SyncLock _bufferLock
            _sampleBuffer.AddRange(newSamples)

            Dim excess = _sampleBuffer.Count - MaxBufferedSamples
            If excess > 0 Then
                _sampleBuffer.RemoveRange(0, excess)
            End If

            If _sampleBuffer.Count >= FftLength Then
                AnalyzeLatestWindow(_capture.WaveFormat.SampleRate)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    '''     バッファ末尾のFFT窓を解析し、各帯域の音量(dB)を更新する。呼び出し元で_bufferLockを保持していること。
    ''' </summary>
    Private Sub AnalyzeLatestWindow(sampleRate As Integer)
        Try
            Dim startIndex = _sampleBuffer.Count - FftLength
            Dim fftBuffer(FftLength - 1) As Complex

            For i As Integer = 0 To FftLength - 1
                Dim windowMultiplier = FastFourierTransform.HammingWindow(i, FftLength)
                fftBuffer(i).X = CSng(_sampleBuffer(startIndex + i) * windowMultiplier)
                fftBuffer(i).Y = 0
            Next

            FastFourierTransform.FFT(True, FftPow, fftBuffer)

            Dim binHz = sampleRate / CDbl(FftLength)
            Dim nyquistBin = FftLength \ 2 - 1
            Dim newLevels(_bandFrequenciesHz.Length - 1) As Double

            For b As Integer = 0 To _bandFrequenciesHz.Length - 1
                ' 帯域の境界は、隣接する中心周波数同士の幾何平均（両端は1オクターブ分を仮定）
                Dim lowHz As Double = If(b = 0,
                    _bandFrequenciesHz(b) / Math.Sqrt(2),
                    Math.Sqrt(_bandFrequenciesHz(b - 1) * CDbl(_bandFrequenciesHz(b))))
                Dim highHz As Double = If(b = _bandFrequenciesHz.Length - 1,
                    _bandFrequenciesHz(b) * Math.Sqrt(2),
                    Math.Sqrt(_bandFrequenciesHz(b) * CDbl(_bandFrequenciesHz(b + 1))))

                Dim lowBin = Math.Max(1, CInt(Math.Floor(lowHz / binHz)))
                Dim highBin = Math.Min(nyquistBin, CInt(Math.Ceiling(highHz / binHz)))

                Dim sumSquares As Double = 0
                Dim binCount As Integer = 0
                For bin = lowBin To highBin
                    Dim magnitude = Math.Sqrt(fftBuffer(bin).X * fftBuffer(bin).X + fftBuffer(bin).Y * fftBuffer(bin).Y)
                    sumSquares += magnitude * magnitude
                    binCount += 1
                Next

                Dim rms = If(binCount > 0, Math.Sqrt(sumSquares / binCount), 0.0)
                Dim db = If(rms > 0, 20.0 * Math.Log10(rms), FloorDb)
                newLevels(b) = Math.Max(FloorDb, db)
            Next

            SyncLock _levelsLock
                Array.Copy(newLevels, _latestBandLevelsDb, newLevels.Length)
            End SyncLock
        Catch ex As Exception
            ' 音声処理中の予期しない例外はキャプチャスレッドを壊さないよう握りつぶさず記録のみ行う
            Debug.WriteLine("AudioSpectrumAnalyzer: 解析中に例外が発生しました。" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    '''     WASAPIから渡されるバイト列を、モノラルのfloatサンプル列に変換する。
    '''     WASAPIの共有モードのミックスフォーマットは、実体がIEEE FloatやPCMでも
    '''     Encodingが"Extensible"（WAVEFORMATEXTENSIBLE）として報告されることが多いため、
    '''     Encodingの種別ではなくBitsPerSampleを主な判定材料にする（32bit=float、16bit=整数PCMとして扱う）。
    '''     それ以外のビット深度には対応しない（対応外の場合は空リストを返す）。
    ''' </summary>
    Private Shared Function ConvertToMonoFloatSamples(rawBuffer() As Byte, bytesRecorded As Integer, format As WaveFormat) As List(Of Single)
        Dim result As New List(Of Single)
        Dim channels = format.Channels
        If channels <= 0 Then Return result

        If format.BitsPerSample = 32 Then
            Dim sampleCount = bytesRecorded \ 4
            Dim floatBuffer(sampleCount - 1) As Single
            System.Buffer.BlockCopy(rawBuffer, 0, floatBuffer, 0, sampleCount * 4)

            Dim frameCount = sampleCount \ channels
            For frame = 0 To frameCount - 1
                Dim sum As Single = 0
                For ch = 0 To channels - 1
                    sum += floatBuffer(frame * channels + ch)
                Next
                result.Add(sum / channels)
            Next
        ElseIf format.BitsPerSample = 16 Then
            Dim frameCount = bytesRecorded \ (2 * channels)
            For frame = 0 To frameCount - 1
                Dim sum As Integer = 0
                For ch = 0 To channels - 1
                    Dim offset = (frame * channels + ch) * 2
                    sum += BitConverter.ToInt16(rawBuffer, offset)
                Next
                result.Add((sum / channels) / 32768.0F)
            Next
        End If

        Return result
    End Function

#Region "IDisposable"

    Public Sub Dispose() Implements IDisposable.Dispose
        If _disposed Then Return
        _disposed = True

        If _capture IsNot Nothing Then
            RemoveHandler _capture.DataAvailable, AddressOf OnDataAvailable
            Try
                _capture.StopRecording()
            Catch ex As Exception
                Debug.WriteLine("AudioSpectrumAnalyzer: 録音停止時にエラーが発生しました。" & ex.Message)
            End Try
            _capture.Dispose()
        End If
    End Sub

#End Region

End Class
