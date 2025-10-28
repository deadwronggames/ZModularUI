using System;
using System.Threading.Tasks;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularSliderProperties : BaseModularUIProperty
    {
        [SerializeField] ModularImageProperties _backgroundProperties;
        [SerializeField] ModularImageProperties _fillProperties;
        [SerializeField] RectOffset _fillPadding;
        [SerializeField] UIBorderProperties _borderProperties;
        [SerializeField] ModularImageProperties _handleProperties;
        
        
        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;
        
        public void ApplyTo(Image backgroundImage, RectTransform fillAreaRectTransform, Image fillImage, Image borderImage, RectTransform slideAreaRectTransform, Image handleImage, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            _backgroundProperties.ApplyTo(backgroundImage, tweenTime, ease);
            _fillProperties.ApplyTo(fillImage, tweenTime, ease);
            fillAreaRectTransform.SetPadding(_fillPadding);
            _borderProperties.ApplyTo(borderImage, backgroundImage.rectTransform, tweenTime, ease);
            SetSlideAreaPadding(slideAreaRectTransform);
            _handleProperties.ApplyTo(handleImage, tweenTime, ease);
        }

        private void SetSlideAreaPadding(RectTransform slideAreaRectTransform)
        {
            // TODO make awake-safe?
            slideAreaRectTransform.offsetMin = slideAreaRectTransform.offsetMin.With(x: _fillPadding.left);
            slideAreaRectTransform.offsetMax = slideAreaRectTransform.offsetMin.With(x: -_fillPadding.right);
        }
    }
}