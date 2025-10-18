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
        [SerializeField] ButtonTextProperties _text;
        [SerializeField] ImageProperties _frontImage;
        [SerializeField] ImageProperties _middleImage;
        [SerializeField] ImageProperties _backImage;
        [SerializeField] UIBorderProperties _border;

        public Vector2 ButtonSize => _buttonSize ;
        public ButtonTextProperties Text => _text ;
        public ImageProperties FrontImage => _frontImage ;
        public ImageProperties MiddleImage => _middleImage ;
        public ImageProperties BackImage => _backImage ;
        public UIBorderProperties Border => _border ;

        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;

        public void ApplyTo(RectTransform buttonRectTransform, TMP_Text text, Image frontImage, Image middleImage, Image backImage, Image borderImage, RectTransform buttonVisualsRectTransform, float tweenTime = 0f, Ease ease = Ease.OutQuad, bool test = false)
        {
            
            buttonRectTransform.SetSize(_buttonSize);
            _text.ApplyTo(text, tweenTime, ease);
            if (test) return;
            _frontImage.ApplyTo(frontImage, tweenTime, ease);
            _middleImage.ApplyTo(middleImage, tweenTime, ease);
            // crash
            _backImage.ApplyTo(backImage, tweenTime, ease);
            _border.ApplyTo(borderImage, buttonVisualsRectTransform, tweenTime, ease);
        }
    }
}