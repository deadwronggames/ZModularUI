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
        
        public ImageProperties BackgroundProperties => _backgroundProperties;
        public ImageProperties ChangeIndicatorProperties => _changeIndicatorProperties;
        public ImageProperties FillProperties => _fillProperties;
        public UIBorderProperties BorderProperties => _borderProperties;

        
        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;
        
        public void ApplyTo(Image backgroundImage, Image changeIndicatorImage, Image fillImage, Image borderImage, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            // Apply Image properties
            _backgroundProperties.ApplyTo(backgroundImage, tweenTime, ease);
            _changeIndicatorProperties.ApplyTo(changeIndicatorImage, tweenTime, ease);
            _fillProperties.ApplyTo(fillImage, tweenTime, ease);

            // Apply padding
            RectTransform[] paddingTargets = { backgroundImage.rectTransform, changeIndicatorImage.rectTransform, fillImage.rectTransform };
            _borderProperties.ApplyTo(borderImage, paddingTargets, tweenTime, ease);
        }
    }
}
