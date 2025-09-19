using DeadWrongGames.ZCommon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    /// <summary>
    /// A ScriptableObject containing all modular UI properties for a theme.
    /// <para>
    /// This class organizes UI properties into logical groups: windows, text, and buttons.
    /// Each property type is represented by a class derived from <see cref="BaseModularUIProperty"/>,
    /// which handles caching and loading of Addressable assets used by that property.
    /// </para>
    /// <para>
    /// Example usage:
    /// Notes:
    /// <list type="bullet">
    /// <item>Addressable assets in property classes are automatically cached in the property instances.</item>
    /// <item>Properties that do not use Addressables can simply return Task.CompletedTask from <see cref="BaseModularUIProperty.ReloadAddressablesAssets"/>.</item>
    /// <item>Modular properties are hierarchical. Nested properties (like <see cref="ImageProperties"/> inside <see cref="ModularWindowProperties"/>) are recursively loaded and applied.</item>
    /// </list>
    /// </para>
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/UITheme", fileName = "UITheme", order = 0)]
    public class ModularUIThemeSO : ScriptableObject
    {
        [Header("Modular Windows")]
        [SerializeField] ModularWindowProperties[] _windowProperties;
        public ModularWindowProperties GetWindowProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _windowProperties);
        
        
        [Header("Modular Text")]
        [SerializeField] ModularTextProperties[] _textProperties;
        public ModularTextProperties GetTextProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _textProperties);
        
        
        [Header("Modular Buttons")]
        [SerializeField] ModularButtonProperties[] _buttonProperties;
        public ModularButtonProperties GetButtonProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _buttonProperties);

        
#if UNITY_EDITOR
        // Calling Addressables or changing RectTransforms after recompile causes warnings
        public static bool JustRecompiled { get; private set; } = true;
        private void OnValidate()
        {
            if (JustRecompiled) JustRecompiled = false;
        }
#endif


#if UNITY_EDITOR
        [Button("Reconfigure all Modular UI")]
        public void ReconfigureAll()
        {
            _ = BaseModularUIProperty.ReloadAddressablesAssetsRecursive(this); // Should already be reloaded from OnValidate but just to be sure. reload is async so will not affect this call but at least the next.
            ModularUIHelpers.ReconfigureAllModularUI();
        }
#endif
    }
}