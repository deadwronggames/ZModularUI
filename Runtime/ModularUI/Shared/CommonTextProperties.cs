using System;
using System.Threading.Tasks;
using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class CommonTextProperties : BaseModularUIProperty
    {
        [SerializeField] AssetReferenceFontAssetSO _fontAssetReference;
        [SerializeField] int _fontSize;
        [SerializeField] ModularColorSO _textColor;
        [HideInInspector] [SerializeField] TMP_FontAsset _font; // Serialization is necessary so that value is carried over into build
        
        public TMP_FontAsset Font => _font;
        public int FontSize => _fontSize;
        public ModularColorSO TextColor => _textColor;
        
        protected override async Task ReloadAddressablesAssets()
        {
            if (_fontAssetReference != null)
                _font = await _fontAssetReference.LoadAssetSafeAsync();
        }
        
        public virtual void ApplyTo(TMP_Text target, float tweenTime = 0f, Ease ease = Ease.OutQuad) => ApplyToInternal(target, tweenTime, ease);
        
        /// <summary>
        /// Internal helper that applies the properties immediately if assets are ready.
        /// Returns true if applied immediately, false if deferred until assets are loaded.
        /// This allows derived classes to apply additional properties only after the base properties
        /// (like fonts or sprites) are guaranteed to exist, avoiding flicker or invalid state.
        /// </summary>
        protected bool ApplyToInternal(TMP_Text target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            // Make sure to check all objects that are loaded from Addressables
            target.enabled = ((_font != null) && (_fontSize > 0));
            if (!EnsureAssetsLoadedOrInvokeAfter(() => ApplyToInternal(target, tweenTime, ease), _font)) return false;
            
            target.font = _font;
            target.fontSize = _fontSize;
            if (tweenTime == 0f) target.color = _textColor;
            else
            {
                DOTween.Kill(target);
                target.DOColor(_textColor, tweenTime).SetEase(ease);
            }

            return true;
        }
    }
}
