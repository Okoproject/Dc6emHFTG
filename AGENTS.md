# AGENTS.md

このファイルは、このリポジトリを扱うAIコーディングエージェントとエンジニア向けの共通ガイドです。ツール固有の指示は `CLAUDE.md` などを参照してください。

## プロジェクト

OkoshiMAXは、VB.NET / Windows Forms製の動画プレーヤーです。

- .NET Framework 4.8
- x64専用
- 再生バックエンド: `dll/libmpv-2.dll`（libmpv）
- 主な機能: メディア再生、プレイリスト、しおり、グローバルホットキー、クリップボード画像表示、URL再生

## アーキテクチャ

| 変更対象 | 主なファイル | 関連ファイル |
|---|---|---|
| 再生、プレイリスト、しおり、メインUI | `src/MainPlayerForm.vb` | `MainPlayerForm.Designer.vb`、フォームリソース |
| libmpv連携、再生イベント | `src/MpvPlayerWrapper.vb` | `OkoshiMAX.vbproj` |
| グローバルホットキー | `src/HotKeyManager.vb` | `src/SettingsForm.vb`、`My Project/Settings.settings` |
| 設定画面 | `src/SettingsForm.vb` | `SettingsForm.Designer.vb`、`My Project/Settings.settings` |
| クリップボード画像表示 | `src/ClipboardImageViewer.vb` | 対応するDesignerとリソース |
| プレイリスト項目 | `src/PlaylistItem.vb` | `src/MainPlayerForm.vb` |
| 永続設定 | `My Project/Settings.settings` | `My.Settings`の利用箇所 |
| UI文言 | `My Project/Resources.resx`、`Resources.ja.resx` | `My.Resources`の利用箇所 |

## 変更時の確認事項

- UI変更時は、フォーム本体、`*.Designer.vb`、対応する`.resx`の整合性を確認する。
- 設定を追加・改名する場合は、`Settings.settings`と`My.Settings`、設定名文字列、`CallByName`の利用箇所を同期する。
- ホットキーを追加・削除する場合は、`HotKeyType`、設定名マッピング、`Settings.settings`、`SettingsForm`、登録処理を同期する。
- リソース文字列を追加・変更する場合は、英語版と日本語版で同じキーを維持する。
- libmpv連携を変更する場合は、P/Invoke宣言、イベントスレッド、アンマネージド資源の解放を確認する。
- 生成ファイルは直接変更せず、正本となる設定、リソース、フォーム定義から更新する。

## エンジニアリング原則

- KISS
- YAGNI
- SOLID
- DRY
- Separation of Concerns
- Composition over Inheritance
- Principle of Least Surprise
- Fail Fast
- Guard Clauses
- Rule of Three
- Principle of Least Privilege

KISSとYAGNIを優先し、SOLIDやDRYを理由に要求外の抽象化、拡張、リファクタリングを行わないでください。

## プロジェクト規約

- 整形と命名は `.editorconfig` を正本とする。
- 変更箇所周辺の既存パターンに合わせる。
- 変更は要求された範囲に限定し、無関係な修正を混ぜない。
- コメントとドキュメントは日本語で記述する。
- 公開メソッドと関数には日本語のXMLドキュメントを付ける。
- コメントは処理内容ではなく、その実装が必要な理由を説明する。
- プロジェクト既定は `Option Explicit On`、`Option Strict Off`。既存のlate bindingを維持しつつ、変更コードでは可能な範囲で型を明示する。
- 非推奨または廃止されたAPIを新たに使用しない。
- 空の`Catch`で例外を握りつぶさない。

## 外部依存と検証

- `libmpv-2.dll`は必須で、x64環境を前提とする。
- URL / YouTube再生には、PATH上の`yt-dlp`が必要。アプリには同梱されていない。
- `.doc` / `.docx`の読込には、late bindingで起動可能なMicrosoft Wordが必要。
- 自動テストは未整備。変更対象はVisual Studioで手動確認する。
- このリポジトリではコマンドラインビルドを実行せず、変更後はユーザーにVisual Studioでのビルドと動作確認を依頼する。

## Gitワークフロー

- 作業開始前に、新しいブランチを作成するかユーザーへ確認する。
- 1つのタスクまたは変更につき、正確に1コミットとする。
- 無関係な変更を同じコミットに含めない。
- コミットはユーザーから依頼された場合のみ作成する。
- コミットメッセージは日本語で記述する。
- コミット本文に `## 変更内容` を設け、変更ファイルごとの要点を記載する。

## 正本

- プロジェクト設定、参照、ターゲット、出力: `OkoshiMAX.vbproj`
- 整形、命名: `.editorconfig`
- 永続設定: `My Project/Settings.settings`
- 多言語リソース: `My Project/Resources.resx`、`My Project/Resources.ja.resx`
- `DEPENDENCIES.md`は補助資料とし、内容が異なる場合は`OkoshiMAX.vbproj`を優先する。
