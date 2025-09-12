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
        [SerializeField] Sprite[] _windowBorderSprites;
        [SerializeField] RectOffset[] _windowPadding;
        
        public Sprite GetWindowBackgroundSprite(Tier tier) => ModularUIHelpers.GetProperty(tier, _windowBackgroundSprites);
        public Sprite GetWindowBorderSprite(Tier tier) => ModularUIHelpers.GetProperty(tier, _windowBorderSprites);
        public RectOffset GetWindowPadding(Tier tier)
        {
            RectOffset defaultPadding = new(50, 50, 50, 50);
            return ((int)tier > _windowPadding.Length - 1) ? defaultPadding : _windowPadding[(int)tier];
        }
        #endregion
        
        #region Modular Text
        [Header("Modular Text")]
        [SerializeField] TMP_FontAsset _fontOrnate;
        [SerializeField] TMP_FontAsset _fontPlain;
        [SerializeField] TMP_FontAsset _fontMonoSpace;
        [SerializeField] int[] _fontSizes;
        [SerializeField] FontStyles[] _fontStyles;
        [SerializeField] Color[]_textColors; // TODO use SO color definitions
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

        // [Header("Tween parameters")] 
        // [SerializeField] float _uiColorFadeDuration;
        // public float UIColorFadeDuration => _uiColorFadeDuration;
        
#if UNITY_EDITOR
        [Button("Reconfigure all Modular UI")]
        public void ReconfigureAll() => ModularUIHelpers.ReconfigureAllModularUI();
#endif
    }
}