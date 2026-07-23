# AGENTS.md

このファイルは、このリポジトリでコードを扱うAIコーディングエージェント（Claude Code、OpenCode等）へ向けた共通ガイダンスを提供します。ツール固有の追加事項は、各ツール専用のファイル（`CLAUDE.md` 等）を参照してください。

## プロジェクト概要

OkoshiMAXは、グローバルホットキー、しおり（ブックマーク）管理、クリップボード画像ビューアなどの高度な機能を備えた、動画再生用のWindows Forms VB.NETアプリケーションです。メディアバックエンドにはmpv（libmpv-2.dll）を使用しています。

COM/アセンブリ参照の全リストやビルド・発行設定については `DEPENDENCIES.md` を参照してください。

## アーキテクチャ

### コアコンポーネント

- **MainPlayerForm.vb** - メインUI、メディア再生制御、ホットキー処理、プレイリストおよびYouTube/URL再生
- **MpvPlayerWrapper.vb** - libmpv-2.dllのP/Invokeラッパー
- **HotKeyManager.vb** - Win32 APIによるグローバルホットキー登録
- **SettingsForm.vb** - ホットキーカスタマイズ用の設定UI
- **ClipboardImageViewer.vb** - クリップボード画像ビューア用の独立ウィンドウ
- **PlaylistItem.vb** - プレイリストの1項目を表すデータクラス（ファイルパス、再生時間、メモ、再生位置）

### モジュール構成

```
src/
├── ApplicationEvents.vb      # My.Applicationの部分クラス
├── HotKeyManager.vb          # モジュール: ホットキー登録、enumマッピング
├── MainPlayerForm.vb          # プレーヤーロジックを持つメインフォーム
├── MpvPlayerWrapper.vb       # libmpvのP/Invokeラッパー
├── PlaylistItem.vb            # プレイリスト項目のデータクラス
├── SettingsForm.vb            # 設定フォーム
├── ClipboardImageViewer.vb    # クリップボードビューア
├── Settings.vb                # My.Settingsの部分クラス
└── *.Designer.vb              # フォームデザイナー
```

### HotKeyManagerのパターン

HotKeyManagerはモジュール（静的クラス）で、以下を提供します。
- 30種類以上のホットキータイプを持つ `HotKeyType` enum
- Win32アトムIDのための `HotKeyAtoms` ディクショナリ
- 設定名マッピングのための `GetSettingModifierProperty()` / `GetSettingKeyProperty()`
- 設定値をWin32モディファイアに変換する `GetModifierValue()`

### i18nリソース

文字列は `My Project\Resources.resx`（英語デフォルト）と `My Project\Resources.ja.resx`（日本語）に格納されています。`My.Resources.*` 経由でアクセスします。

### 設定の保存

アプリケーション設定は `My.Settings` に、以下のようなカスタムプロパティで保存されます。
- `SKAA`、`SKA` - ホットキーのモディファイアとキー
- `SC1`、`SC2` 等 - 速度制御値
- `Onryou` - 音量
- `autoBM` - 自動しおりモード

### シングルインスタンスパターン

`MainPlayerForm.Instance` はロード時に設定され、`SettingsForm` がホットキー登録のためにメインフォームへアクセスする際に使用されます。

### 再生機能に関する補足

- **YouTube / URL再生** - MainPlayerForm.vbの `TextBox1_KeyDown` が `http(s)://` 形式の入力を検出し、mpvに渡します。mpv側はPATH上に `yt-dlp` が存在することを前提としており（`IsYtDlpAvailable()` で確認）、存在しない場合は無言で失敗するのではなくメッセージダイアログで再生をブロックします。
- **タイムストレッチ / ピッチ補正** - `MpvPlayerWrapper.PitchCorrection` はmpvの `audio-pitch-correction` プロパティをラップしており、`InitializePitchCorrectionCheckbox()` で動的に生成されるチェックボックスから切り替え、`My.Settings("PitchCorrection")` に永続化されます。

### 重要な制約

1. **libmpv-2.dll依存** - 出力ディレクトリに配置必須、64ビット限定
2. **COM参照** - 文書出力用にMicrosoft.Office.Interop.Wordを使用
3. **ターゲットフレームワーク** - .NET Framework 4.8、x64専用
4. **VB.NET固有パターン** - `My.Settings`、`My.Resources`、動的プロパティアクセス用の `CallByName`
5. **コマンドラインビルド不可** - このプロジェクトはコマンドライン（MSBuild/dotnet CLIなし）ではビルドできません。コード変更後は必ずVisual Studioでの手動ビルド・テストをユーザーに依頼してください。ビルドコマンドを実行しようとしないでください。
6. **自動テストスイートなし** - `tests/` ディレクトリにはプロジェクトの `bin`/`obj` 出力のみが存在し、追跡対象のソースはありません。現時点で実行・拡張すべきユニットテストはありません。
7. **`yt-dlp` 外部依存** - URL/YouTube再生にはPATH上の `yt-dlp` が必要です。アプリには同梱されていません。

## コードスタイルルール

### 簡潔に書く
- 可能な場合は複数行の `If...Then...Else` ではなく `If()` 演算子を使い、式を1行で記述する
- 重複したオブジェクト参照を減らすために `With` ブロックを使う
- 明示的な行継続文字（`_`）は避け、暗黙の行継続を使う
- `&` による連結ではなく文字列補間（`$"..."`）を使う

### ネストを浅く保つ
- ガード節（早期リターン）を使い、深いネストを避ける
- 最大ネスト深度: 2階層（早期の `Return` / `Continue` / `Exit` でフラット化することを優先）
- 深くネストしたロジックは別メソッドに抽出する

### 命名規則
- クラス、メソッド、プロパティ、イベント、公開メンバーには **PascalCase**
- ローカル変数と引数には **camelCase**
- プライベートフィールドには `.editorconfig` に従い **_camelCase**（アンダースコア接頭辞）を使用（例: `_mediaPlayer`、`_currentPlaybackSpeed`）
- わかりやすい名前を使用し、ループカウンタ以外では単一文字の変数を避ける

### 型安全性
- 常に `Option Explicit On` と `Option Strict On` を有効にする
- `IIf()` 関数ではなく `If()` 演算子（短絡評価）を使う
- 非推奨または廃止されたメソッド・APIは使用しない。常に推奨される最新の代替手段を使用する

### エラーハンドリング
- 汎用的な `Exception` ではなく、具体的な例外型をキャッチする
- 空の `Catch` ブロックで例外を握りつぶさない

### 構造
- 1行につき1文、1行につき1宣言
- メソッドは短く焦点を絞った状態に保つ（単一責任）
- フィールドは `Private` にし、必要に応じてプロパティ経由で公開する

### ドキュメント・コメント
- **すべてのコメント・ドキュメントは日本語で記述すること**
- すべての公開メソッド・関数にはXMLドキュメントのサマリー（`''' <summary>`）を付けること
- 非自明なロジック、回避策、扱いにくい計算にはインラインコメントを追加する
- コメントは「何を」ではなく「なぜ」を説明すること

例 - 関数サマリー:
```vb
''' <summary>
''' メディアファイルの指定位置にシークする。
''' mpvコマンドキューの溢れを防ぐため、保留中のシーク要求を破棄する。
''' </summary>
''' <param name="positionSeconds">シーク先の秒数</param>
Public Sub SeekTo(positionSeconds As Double)
```

例 - 複雑なロジックへのインラインコメント:
```vb
' mpvは初期化後100ms待たないとコマンドを受け付けない
' 待機しないと最初のコマンドが無視される
Await Task.Delay(100)
RaiseEvent Initialized(Me, EventArgs.Empty)
```

## Gitワークフロー

### ブランチ戦略
- **作業を開始する前に、必ず新しいブランチを作成するかどうかをユーザーに確認する**
- ユーザーが「はい」と答えた場合 → 新しいブランチを作成し、そこで作業する
- ユーザーが「いいえ」と答えた場合、または特定のブランチを指定した場合 → そのブランチを使用する
- コードにバグが含まれていてもレビュー可能な状態を保つため、ブランチを使用する

### タスクごとに1コミット
- 常に1つのタスク・変更につき**正確に1コミット**とする
- 1つのタスクに対して複数のコミットを作成しない
- 複数の無関係な変更を1つのコミットにまとめない

## Gitコミットガイドライン

**すべてのコミットメッセージは日本語で記述すること。**

コミットを作成する際は、以下を含む詳細なコミットメッセージを記述してください。

1. **サマリー行** - 変更内容の簡潔な説明（日本語）
2. **変更内容セクション（`## 変更内容`）** - 変更したファイルごとに、日本語で具体的な変更点を列挙

### フォーマット

```
<変更内容の日本語サマリー>

## 変更内容

### <ファイル名1>
- <変更内容1の説明（日本語）>
- <変更内容2の説明（日本語）>

### <ファイル名2>
- <変更内容1の説明（日本語）>
- <変更内容2の説明（日本語）>
```

### 例

```
MpvPlayer初期化完了イベントとファイル読込時自動再生を追加

## 変更内容

### MpvPlayerWrapper.vb
- Initializedイベントを追加
- コンストラクタ末尾で100ms遅延後にInitializedイベントを発火する処理を追加

### MainPlayerForm.vb
- OnMpvReadyイベントハンドラを追加
- InitializeMediaPlayerでInitializedイベントハンドラを登録
- OnMediaChangedでファイル読込時に自動再生する処理を追加
```
