using System;
using System.Threading.Tasks;
using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularTextProperties : BaseModularUIProperty
    {
        [SerializeField] AssetReferenceFontAssetSO _fontAssetReference;
        [SerializeField] int _fontSize;
        [SerializeField] FontStyles _fontStyle;
        [SerializeField] ModularColorSO _textColor;
        [SerializeField] TextAlignmentOptions _textAlignment;
        private TMP_FontAsset _font;
        
        public TMP_FontAsset Font => _font;
        public int FontSize => _fontSize;
        public FontStyles FontStyle => _fontStyle;
        public ModularColorSO TextColor => _textColor;
        public TextAlignmentOptions TextAlignment => _textAlignment;

        protected override async Task ReloadAddressablesAssets()
        {
            if (_fontAssetReference != null)
                _font = await _fontAssetReference.LoadAssetSafeAsync();
        }
        
        public void ApplyTo(TMP_Text target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            // Make sure to check all objects that are loaded from Addressables
            if (!EnsureAssetsLoadedOrInvokeAfter(() => ApplyTo(target, tweenTime, ease), _font)) return;
    
            target.font = _font;
            target.fontStyle = _fontStyle;
            target.alignment = _textAlignment;
            if (tweenTime == 0f)
            {
                target.fontSize = _fontSize;
                target.color = _textColor;
            }
            else
            {
                DOTween.Kill(target);
                DOTween.To(getter: () => target.fontSize, setter: x => target.fontSize = x, endValue: _fontSize, duration: tweenTime).SetEase(ease);
                target.DOColor(_textColor, tweenTime).SetEase(ease);
            }
            
            target.gameObject.SetActive((_font != null) && (_fontSize > 0));
        }
    }
}