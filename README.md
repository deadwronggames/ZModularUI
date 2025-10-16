<p align="center">
  <img src="https://raw.githubusercontent.com/deadwronggames/ZSharedAssets/main/Banner_Zombie.jpg" alt="ZCommon Banner" style="width: 100%; max-width: 1200px; height: auto;">
</p>


# ZModularUI
Modular and theme-based UI framework for Unity. Build flexible and easily configurable UIs *Lego style*, using reusable building block components and data-driven configuration.

## Installation
- Install via Unity Package Manager using the Git URL: https://github.com/deadwronggames/ZModularUI
- Include in your code (when needed) via the namespace: 
```csharp 
using DeadWrongGames.ZModularUI;
```

## Overview
- Each UI element (like buttons, texts, windows) derives from a base class that links it to a configuration ScriptableObject, defining its visual and functional behavior.
- UIs can be assembled *lego-style* using modular prefabs included in the package.  
- These prefabs are customizable. You can copy them into your project, edit or extend them, and still benefit from the modular system.


## Core Concepts

### Modular Components

Each UI element derives from `BaseModularUIComponent<TConfig>`.  
This base class handles setup, configuration, and theme application.

Example:
```csharp
public class ModularText : BaseModularUIComponent<ModularTextConfigSO>
{
    // Automatically configured text component with tier-based theming. See code for details.
}
```

The base class ensures that:
- Each component references a TConfig ScriptableObject containing its settings
- It can automatically apply those settings and reconfigure in-editor
- Configuration changes propagate through the entire UI (via editor buttons)

### Themes & Configs
- ModularUIThemeSO: The root theme asset containing properties for all tiers of windows, buttons, and text elements.
- BaseModularUIComponentConfigSO: A per-component configuration that references the theme and optionally defines overrides.
- Property Classes: Each UI type (e.g. text, button) has a corresponding property set (e.g. ModularTextProperties) that defines settings like font, color, and layout.
- All property classes support Addressable assets, which are cached automatically.

### Prefabs
The package includes modular prefabs for:
- Windows
- Buttons
- Text elements
- Images
- Basic layout elements

More prefabs (scroll views, tabs, etc.) will be added soon.
Each prefab follows the modular structure and can be easily customized or extended.

### Configuration Lifecycle
- The component calls `Setup()` to link internal references.
- Then `Apply()` applies theme data from the referenced config.
- Reconfiguration can be triggered manually or automatically after recompilation.

### Current Status
- ZModularUI is in active development.
- The core system (configuration, themes, prefabs) is functional and stable for internal use.
- the async Addressables handling has still some quirks and is very usntable when not setup carefully.


### Planned additions:
- More UI element prefabs and functionality (scroll views, tabs, sliders)
- Page management with animation & transition helpers

### Notes
- Developed for use with Odin Inspector
- Designed to integrate with other ZPackages (ZCommon, ZServices, etc.)
- **Work in progress**
