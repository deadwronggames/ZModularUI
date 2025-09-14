using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public struct ModularButtonProperties
    {
        [SerializeField] Vector2 _buttonSize;
        [SerializeField] ButtonTextProperties _text;
        [SerializeField] ImageProperties _frontImage;
        [SerializeField] ImageProperties _middleImage;
        [SerializeField] ImageProperties _backImage;
        [SerializeField] UIBorderProperties _border;

        public void ApplyTo(RectTransform buttonRectTransform, TMP_Text text, Image frontImage, Image middleImage, Image backImage, Image borderImage, RectTransform buttonVisualsRectTransform)
        {
            buttonRectTransform.sizeDelta = _buttonSize;
            _text.ApplyTo(text);
            _frontImage.ApplyTo(frontImage);
            _middleImage.ApplyTo(middleImage);
            _backImage.ApplyTo(backImage);
            _border.ApplyTo(borderImage, buttonVisualsRectTransform);
        }
    }
}