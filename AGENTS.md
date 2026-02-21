# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

OkoshiMAX is a Windows Forms VB.NET application for video playback with advanced features like global hotkeys, bookmark management, and clipboard image viewing. It uses mpv (libmpv-2.dll) as the media backend.



## Architecture

### Core Components

- **MainPlayerForm.vb** - Main UI, media playback control, hotkey handling
- **MpvPlayerWrapper.vb** - P/Invoke wrapper for libmpv-2.dll
- **HotKeyManager.vb** - Global hotkey registration via Win32 API
- **SettingsForm.vb** - Settings UI for hotkey customization
- **ClipboardImageViewer.vb** - Clipboard image viewer standalone window

### Module Structure

```
src/
├── ApplicationEvents.vb      # My.Application partial class
├── HotKeyManager.vb          # Module: hotkey registration, enum mappings
├── MainPlayerForm.vb          # Main form with player logic
├── MpvPlayerWrapper.vb       # libmpv P/Invoke wrapper
├── SettingsForm.vb            # Settings form
├── ClipboardImageViewer.vb    # Clipboard viewer
├── Settings.vb                # My.Settings partial class
└── *.Designer.vb              # Form designers
```

### HotKeyManager Pattern

HotKeyManager is a Module (static class) that provides:
- `HotKeyType` enum with 30+ hotkey types
- `HotKeyAtoms` dictionary for Win32 atom IDs
- `GetSettingModifierProperty()` / `GetSettingKeyProperty()` for settings name mapping
- `GetModifierValue()` for converting settings to Win32 modifiers

### i18n Resources

Strings are stored in `My Project\Resources.resx` (English default) and `My Project\Resources.ja.resx` (Japanese). Access via `My.Resources.*`.

### Settings Storage

Application settings are stored in `My.Settings` with custom properties like:
- `SKAA`, `SKA` - Modifier and key for hotkeys
- `SC1`, `SC2`, etc. - Speed control values
- `Onryou` - Volume
- `autoBM` - Auto bookmark mode

### Single Instance Pattern

`MainPlayerForm.Instance` is set on load and used by `SettingsForm` to access the main form for hotkey registration.

### Important Constraints

1. **libmpv-2.dll dependency** - Must be in output directory, 64-bit only
2. **COM references** - Microsoft.Office.Interop.Word for document import
3. **Target framework** - .NET Framework 4.8, x64 only
4. **VB.NET specific patterns** - `My.Settings`, `My.Resources`, `CallByName` for dynamic property access
5. **No command-line build available** - This project cannot be built via command line (no MSBuild/dotnet CLI). After making code changes, always ask the user to build and test manually in Visual Studio. Do NOT attempt to run build commands.

## Code Style Rules

### Keep It Concise
- Write expressions in one line when possible using the `If()` operator instead of multi-line `If...Then...Else`
- Use `With` blocks to reduce repetitive object references
- Use implicit line continuation; avoid the explicit line continuation character (`_`)
- Use string interpolation (`$"..."`) instead of `&` concatenation

### Shallow Nesting
- Use guard clauses (early return) to avoid deep nesting
- Maximum nesting depth: 2 levels (prefer flattening with early `Return` / `Continue` / `Exit`)
- Extract deeply nested logic into separate methods

### Naming Conventions
- **PascalCase** for classes, methods, properties, events, and public members
- **camelCase** for local variables and parameters
- Use descriptive names; avoid single-letter variables except loop counters

### Type Safety
- Always enable `Option Explicit On` and `Option Strict On`
- Use `If()` operator (short-circuit) instead of `IIf()` function

### Error Handling
- Catch specific exception types, not generic `Exception`
- Never silently swallow exceptions with empty `Catch` blocks

### Structure
- One statement per line, one declaration per line
- Keep methods short and focused (Single Responsibility)
- Fields should be `Private`; expose via properties when needed

### Documentation & Comments
- **All comments and documentation MUST be written in Japanese**
- All public methods and functions MUST have XML documentation summary (`''' <summary>`)
- Add inline comments to explain non-obvious logic, workarounds, or tricky calculations
- Comments should explain **why**, not **what**

Example - Function summary:
```vb
''' <summary>
''' メディアファイルの指定位置にシークする。
''' mpvコマンドキューの溢れを防ぐため、保留中のシーク要求を破棄する。
''' </summary>
''' <param name="positionSeconds">シーク先の秒数</param>
Public Sub SeekTo(positionSeconds As Double)
```

Example - Inline comment for complex logic:
```vb
' mpvは初期化後100ms待たないとコマンドを受け付けない
' 待機しないと最初のコマンドが無視される
Await Task.Delay(100)
RaiseEvent Initialized(Me, EventArgs.Empty)
```

## Git Commit Guidelines

**All commit messages MUST be written in Japanese.**

When creating commits, write detailed commit messages that include:

1. **Summary line** - A brief description of the change (in Japanese)
2. **Changes section (`## 変更内容`)** - List each modified file with specific changes described in Japanese

### Format

```
<Summary of the change in Japanese>

## 変更内容

### <FileName1>
- <Description of change 1 in Japanese>
- <Description of change 2 in Japanese>

### <FileName2>
- <Description of change 1 in Japanese>
- <Description of change 2 in Japanese>
```

### Example

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
