using DeadWrongGames.ZCommon;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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
    /// <item>Modular properties are hierarchical. Nested properties (like <see cref="ModularImageProperties"/> inside <see cref="ModularViewProperties"/>) are recursively loaded and applied.</item>
    /// </list>
    /// </para>
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/UITheme", fileName = "UITheme", order = 0)]
    public class ModularUIThemeSO : ScriptableObject
    {
        [Header("Modular Buttons")]
        [SerializeField] ModularButtonProperties[] _buttonProperties;
        public ModularButtonProperties GetButtonProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _buttonProperties);
        
        
        [Header("Modular Fills")]
        [SerializeField] ModularFillProperties[] _fillProperties;
        [SerializeField] ModularFillProperties[] _fillCircularProperties;
        public ModularFillProperties GetFillProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _fillProperties);
        public ModularFillProperties GetFillCircularProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _fillCircularProperties);
        
        
        [Header("Modular Scroll Views")]
        [SerializeField] ModularScrollViewProperties[] _scrollViewProperties;
        public ModularScrollViewProperties GetScrollViewProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _scrollViewProperties);
        
        
        [Header("Modular Text")]
        [SerializeField] ModularTextProperties[] _textProperties;
        public ModularTextProperties GetTextProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _textProperties);
        
        
        [Header("Modular Views")]
        [SerializeField] ModularViewProperties[] _viewProperties;
        public ModularViewProperties GetViewProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _viewProperties);
        
        
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