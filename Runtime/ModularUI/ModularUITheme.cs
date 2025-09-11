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
        
        public Sprite GetWindowBackgroundSprite(Tier tier)
        {
            int index = Mathf.Min((int)tier, _windowBackgroundSprites.Length - 1);
            return _windowBackgroundSprites[index];
        }
        
        public Sprite GetWindowBorderSprite(Tier tier)
        {
            int index = Mathf.Min((int)tier, _windowBorderSprites.Length - 1);
            return _windowBorderSprites[index];
        }
        
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
        
        public int GetFontSize(Tier tier)
        {
            int index = Mathf.Min((int)tier, _fontSizes.Length - 1);
            return _fontSizes[index];
        }
        
        public FontStyles GetFontStyle(Tier tier)
        {
            int index = Mathf.Min((int)tier, _fontStyles.Length - 1);
            return _fontStyles[index];
        }
        
        public Color GetFontColor(Tier tier)
        {
            int index = Mathf.Min((int)tier, _textColors.Length - 1);
            return _textColors[index];
        }
        
        public HorizontalAlignmentOptions GetTextAlignment(Tier tier)
        {
            int index = Mathf.Min((int)tier, _textAlignment.Length - 1);
            return _textAlignment[index];
        }
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