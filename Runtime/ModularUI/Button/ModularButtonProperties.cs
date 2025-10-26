using System;
using System.Threading.Tasks;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularButtonProperties : BaseModularUIProperty
    {
        [SerializeField] Vector2 _buttonSize;
        [SerializeField] CommonTextProperties _text;
        [SerializeField] ModularImageProperties _backImage;
        [SerializeField] UIBorderProperties _border;
        
        public CommonTextProperties Text => _text ;

        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;

        public void ApplyTo(RectTransform buttonRectTransform, TMP_Text text, Image backImage, Image borderImage, RectTransform buttonVisualsRectTransform, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            buttonRectTransform.SetSize(_buttonSize);
            _text.ApplyTo(text, tweenTime, ease);
            _backImage.ApplyTo(backImage, tweenTime, ease);
            _border.ApplyTo(borderImage, buttonVisualsRectTransform, tweenTime, ease);
        }
    }
}