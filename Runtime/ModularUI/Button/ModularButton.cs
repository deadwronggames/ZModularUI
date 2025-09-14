using DeadWrongGames.ZCommon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularButton : BaseModularUIComponent<ModularButtonConfig>
    {
        [SerializeField] Tier _componentTier;
        
        [Header("Setup")]
        [SerializeField] RectTransform _contentRectTransform;
        [SerializeField] TMP_Text _text;
        [SerializeField] Image _frontImage;
        [SerializeField] Image _middleImage;
        [SerializeField] Image _backImage;
        [SerializeField] Image _borderImage;

        private RectTransform _rectTransform;
        
        protected override void Setup()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        protected override void Apply()
        {
            ButtonProperties properties = _theme.GetButtonProperties(_componentTier);
            _rectTransform.sizeDelta = properties.ButtonSize;
            properties.Text.ApplyTo(_text);
            properties.FrontImage.ApplyTo(_frontImage);
            properties.MiddleImage.ApplyTo(_middleImage);
            properties.BackImage.ApplyTo(_backImage);
            properties.Border.ApplyTo(_borderImage);
            properties.Border.ApplyPadding(_contentRectTransform);
        }
    }
}