using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularFillProperties : BaseModularUIProperty
    {
        [SerializeField] ImageProperties _backgroundProperties;
        [SerializeField] ImageProperties _changeIndicatorProperties;
        [SerializeField] ImageProperties _fillProperties;
        [SerializeField] UIBorderProperties _borderProperties;
        [SerializeField] RectOffset _fillPadding;
        [SerializeField] BaseModularFillChangeEffectSO _changeEffect;
        
        public ImageProperties BackgroundProperties => _backgroundProperties;
        public ImageProperties ChangeIndicatorProperties => _changeIndicatorProperties;
        public ImageProperties FillProperties => _fillProperties;
        public UIBorderProperties BorderProperties => _borderProperties;
        public BaseModularFillChangeEffectSO ChangeEffect => _changeEffect;

        
        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;
        
        public void ApplyTo(Image backgroundImage, Image changeIndicatorImage, Image fillImage, Image borderImage, out BaseModularFillChangeEffectSO changeEffect, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            _backgroundProperties.ApplyTo(backgroundImage, tweenTime, ease);
            _changeIndicatorProperties.ApplyTo(changeIndicatorImage, tweenTime, ease);
            _fillProperties.ApplyTo(fillImage, tweenTime, ease);

            _borderProperties.ApplyTo(borderImage, backgroundImage.rectTransform, tweenTime, ease);
            
            changeIndicatorImage.rectTransform.SetPadding(_fillPadding);
            fillImage.rectTransform.SetPadding(_fillPadding);

            changeEffect = _changeEffect;
        }
    }
}
