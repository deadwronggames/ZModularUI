using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public struct ModularWindowProperties
    {
        [SerializeField] ImageProperties _backgroundProperties;
        [SerializeField] UIBorderProperties _borderProperties;
        [SerializeField] RectOffset _windowContentPadding;

        public void ApplyTo(Image backgroundImage, Image borderImage, RectTransform contentRectTransform)
        {
            _backgroundProperties.ApplyTo(backgroundImage);
            _borderProperties.ApplyTo(borderImage, backgroundImage.rectTransform);
            contentRectTransform.SetPadding(_windowContentPadding);
        }
    }
}