using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularViewProperties : BaseModularUIProperty
    {
        [SerializeField] ModularImageProperties _backgroundProperties;
        [SerializeField] UIBorderProperties _borderProperties;
        [SerializeField] RectOffset _contentPadding;
        
        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;

        public void ApplyTo(Image backgroundImage, Image borderImage, RectTransform contentRectTransform, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            _backgroundProperties.ApplyTo(backgroundImage, tweenTime, ease);
            _borderProperties.ApplyTo(borderImage, backgroundImage.rectTransform, tweenTime, ease);
            contentRectTransform.SetPadding(_contentPadding);
        }
    }
}