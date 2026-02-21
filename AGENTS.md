# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

OkoshiMAX is a Windows Forms VB.NET application for video playback with advanced features like global hotkeys, bookmark management, and clipboard image viewing. It uses mpv (libmpv-2.dll) as the media backend.

## Build & Test

### Build
```bash
# Use Visual Studio MSBuild for .NET Framework 4.8
msbuild OkoshiMAX.vbproj /p:Configuration=Debug /p:Platform=x64
msbuild OkoshiMAX.vbproj /p:Configuration=Release /p:Platform=x64
```

### Run
```bash
# Run from Visual Studio or execute
bin\Debug\OkoshiMAX.exe
```

### Test
```bash
# Build test project
msbuild tests\OkoshiMAX.Tests.vbproj

# Run tests (requires VSTest.Console or Visual Studio)
vstest.console.exe tests\bin\Debug\OkoshiMAX.Tests.dll
```

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

## Git Commit Guidelines

When creating commits, write detailed commit messages that include:

1. **Summary line** - Brief description of the change (Japanese)
2. **Changes section** - List each modified file and specific changes

Example:
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
