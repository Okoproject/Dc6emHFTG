# OkoshiMAX プロジェクト依存関係

## プロジェクト概要

| 項目 | 内容 |
|------|------|
| プロジェクト名 | OkoshiMAX |
| プロジェクトタイプ | VB.NET Windows Forms アプリケーション |
| ターゲットフレームワーク | .NET Framework 4.8 |
| 出力タイプ | WinExe (Windows 実行ファイル) |
| Visual Studio バージョン | Visual Studio 2022 (17.0+) |

---

## 1. .NET Framework 参照

### 標準ライブラリ

| アセンブリ名 | 用途 |
|-------------|------|
| System | 基本システム機能 |
| System.Core | LINQ およびコア機能 |
| System.Data | データアクセス |
| System.Deployment | 配置機能 |
| System.Drawing | グラフィックス描画 |
| System.Windows.Forms | Windows Forms UI |
| System.Xml | XML 処理 |
| System.Xml.Linq | LINQ to XML |
| System.Data.DataSetExtensions | DataSet 拡張 |
| System.Net.Http | HTTP クライアント |

---

## 2. COM 参照 (COM Interop)

| コンポーネント名 | GUID | バージョン | 用途 |
|----------------|------|-----------|------|
| Microsoft.Office.Core | {2DF8D04C-5BFA-101B-BDE5-00AA0044DE52} | 2.8 | Office コア機能 |
| Microsoft.Office.Interop.Word | {00020905-0000-0000-C000-000000000046} | 8.7 | Word 自動化・文書出力 |
| VBIDE | {0002E157-0000-0000-C000-000000000046} | 5.3 | VBA IDE 統合 |
| WMPLib | {6BF52A50-394A-11D3-B153-00C04F79FAA6} | 1.0 | Windows Media Player コントロール |

> **注意**: これらの COM コンポーネントは、対応するアプリケーションがインストールされている必要があります。

---

## 3. ブートストラッパーパッケージ

| パッケージ名 | 状態 | 説明 |
|-------------|------|------|
| Microsoft.Net.Framework.3.5.SP1 | インストール無効 | .NET Framework 3.5 SP1 |

---

## 4. 外部 DLL

| ファイル名 | サイズ | 用途 |
|-----------|--------|------|
| wmp.dll | ~12.4 MB | Windows Media Player ライブラリ（ローカル配置） |

---

## 5. リソースファイル

### アイコン・画像リソース

- `OkoshiMAX1_4_1.ico` - アプリケーションアイコン
- `okoshimax.ico` - 代替アイコン
- `Resources/OkoshiMAX1_5_0.ico` - リソース内アイコン
- 各種 UI アイコン（Visual Studio イメージライブラリ）
  - DownloadDocument_16x.png
  - DestinationAssistant_16x.png
  - Cancel_16x.png
  - Add_16x.png
  - など

---

## 6. システム要件

### 必要条件

- Windows OS (Windows 7 SP1 以降推奨)
- .NET Framework 4.8
- Microsoft Word (COM Interop 機能を使用する場合)
- Windows Media Player (WMP 機能を使用する場合)

### 開発環境

- Visual Studio 2017/2019/2022
- VB.NET 開発ツール

---

## 7. ビルド構成

| 構成 | プラットフォーム | 最適化 | 出力パス |
|------|----------------|--------|----------|
| Debug | AnyCPU | 無効 | bin\Debug\ |
| Release | AnyCPU | 有効 | bin\Release\ |

---

## 8. 発行設定

| 設定項目 | 値 |
|---------|-----|
| 発行先 | bin\Debug\app.publish\ |
| インストール元 | ディスク |
| デスクトップショートカット | 作成する |
| アプリケーションのバージョン | 1.5.3.* |
| 発行者名 | Teruhisa Yoshioka |

---

## 9. コンパイル設定

| オプション | 設定値 |
|-----------|--------|
| Option Explicit | On |
| Option Compare | Binary |
| Option Strict | Off |
| Option Infer | On |
| 自動バインディングリダイレクト | 有効 |

---

## 更新履歴

| 日付 | 更新内容 |
|------|----------|
| 2026-02-15 | 依存関係ドキュメント作成 |

---

*このドキュメントは自動生成されました。`
