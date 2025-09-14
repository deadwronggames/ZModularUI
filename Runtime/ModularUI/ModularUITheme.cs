using DeadWrongGames.ZCommon;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/UITheme", fileName = "UITheme", order = 0)]
    public class ModularUITheme : ScriptableObject
    {
        #region Modular Windows
        [Header("Modular Windows")]
        [SerializeField] Sprite[] _windowBackgroundSprites;
        [SerializeField] ModularColor[] _windowBackgroundColors;
        [SerializeField] UIBorderProperties[] _windowBorders;
        [SerializeField] RectOffset[] _windowContentPadding;
        
        public Sprite GetWindowBackgroundSprite(Tier tier) => ModularUIHelpers.GetProperty(tier, _windowBackgroundSprites);
        public Color GetWindowBackgroundColor(Tier tier) => ModularUIHelpers.GetProperty(tier, _windowBackgroundColors);
        public UIBorderProperties GetWindowBorderProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _windowBorders);
        public RectOffset GetWindowContentPadding(Tier tier)
        {
            return ((int)tier > _windowContentPadding.Length - 1) ? new RectOffset() : _windowContentPadding[(int)tier];
        }
        #endregion
        
        #region Modular Text
        [Header("Modular Text")]
        [SerializeField] TMP_FontAsset _fontOrnate;
        [SerializeField] TMP_FontAsset _fontPlain;
        [SerializeField] TMP_FontAsset _fontMonoSpace;
        [SerializeField] int[] _fontSizes;
        [SerializeField] FontStyles[] _fontStyles;
        [SerializeField] ModularColor[] _textColors;
        [SerializeField] HorizontalAlignmentOptions[] _textAlignment;
        
        public enum FontType{ Ornate, Plain, MonoSpace }
        public TMP_FontAsset GetFont(FontType type)
        {
            return type switch
                 {
                     FontType.Ornate => _fontOrnate,
                     FontType.Plain => _fontPlain,
                     FontType.MonoSpace => _fontMonoSpace,
                     _ => Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF")
                 };
        }
        public int GetFontSize(Tier tier) => ModularUIHelpers.GetProperty(tier, _fontSizes);
        public FontStyles GetFontStyle(Tier tier) => ModularUIHelpers.GetProperty(tier, _fontStyles);
        public Color GetFontColor(Tier tier) => ModularUIHelpers.GetProperty(tier, _textColors);
        public HorizontalAlignmentOptions GetTextAlignment(Tier tier) => ModularUIHelpers.GetProperty(tier, _textAlignment);
        #endregion
        
        #region Modular Buttons
        [Header("Modular Buttons")]
        [SerializeField] ButtonProperties[] _buttonProperties;
        public ButtonProperties GetButtonProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _buttonProperties);
        #endregion


#if UNITY_EDITOR
        [Button("Reconfigure all Modular UI")]
        public void ReconfigureAll() => ModularUIHelpers.ReconfigureAllModularUI();
#endif
    }
}