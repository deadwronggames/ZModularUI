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
        [SerializeField] ModularImageProperties _frontImage;
        [SerializeField] ModularImageProperties _middleImage;
        [SerializeField] ModularImageProperties _backImage;
        [SerializeField] UIBorderProperties _border;

        public Vector2 ButtonSize => _buttonSize ;
        public CommonTextProperties Text => _text ;
        public ModularImageProperties FrontImage => _frontImage ;
        public ModularImageProperties MiddleImage => _middleImage ;
        public ModularImageProperties BackImage => _backImage ;
        public UIBorderProperties Border => _border ;

        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;

        public void ApplyTo(RectTransform buttonRectTransform, TMP_Text text, Image frontImage, Image middleImage, Image backImage, Image borderImage, RectTransform buttonVisualsRectTransform, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            buttonRectTransform.SetSize(_buttonSize);
            _text.ApplyTo(text, tweenTime, ease);
            _frontImage.ApplyTo(frontImage, tweenTime, ease);
            _middleImage.ApplyTo(middleImage, tweenTime, ease);
            _backImage.ApplyTo(backImage, tweenTime, ease);
            _border.ApplyTo(borderImage, buttonVisualsRectTransform, tweenTime, ease);
        }
    }
}