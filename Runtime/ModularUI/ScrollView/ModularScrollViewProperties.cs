using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularScrollViewProperties : BaseModularUIProperty
    {
        [SerializeField] ModularImageProperties _backgroundProperties;
        [SerializeField] ModularImageProperties _handleProperties;
        [SerializeField] UIBorderProperties _borderProperties;
        [SerializeField] RectOffset _handlePadding;
        
        public ModularImageProperties BackgroundProperties => _backgroundProperties;
        public ModularImageProperties HandleProperties => _handleProperties;
        public UIBorderProperties BorderProperties => _borderProperties;

        
        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;
        
        public void ApplyTo(Image backgroundImage, Image handleImage, Image borderImage, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            _backgroundProperties.ApplyTo(backgroundImage, tweenTime, ease);
            _handleProperties.ApplyTo(handleImage, tweenTime, ease);
            _borderProperties.ApplyTo(borderImage, backgroundImage.rectTransform, tweenTime, ease);
            
            handleImage.rectTransform.SetPadding(_handlePadding);
        }
    }
}
