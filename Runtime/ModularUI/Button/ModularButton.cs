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
        [SerializeField] RectTransform _visualsRectTransform;
        [SerializeField] TMP_Text _text;
        [SerializeField] Image _frontImage;
        [SerializeField] Image _middleImage;
        [SerializeField] Image _backImage;
        [SerializeField] Image _borderImage;

        private RectTransform _buttonRectTransform;
        
        protected override void Setup()
        {
            _buttonRectTransform = GetComponent<RectTransform>();

            // make sure the visual elements have the correct properties
            _text.fontStyle = FontStyles.Bold | FontStyles.UpperCase;
            _text.alignment = TextAlignmentOptions.Center;
            _frontImage.preserveAspect = true;
            _middleImage.preserveAspect = true;
            _backImage.preserveAspect = false;
        }

        protected override void Apply()
        {
            ModularButtonProperties properties = _theme.GetButtonProperties(_componentTier);
            properties.ApplyTo(_buttonRectTransform, _text, _frontImage, _middleImage, _backImage, _borderImage, _visualsRectTransform);
        }
        
        public void SetText(string newText)
        {
            _text.text = newText;
        }
        
        public void SetFrontImage(Sprite newSprite)
        {
            _frontImage.sprite = newSprite;
            _frontImage.gameObject.SetActive(newSprite != null);
        }
        
        public void SetMiddleImage(Sprite newSprite)
        {
            _middleImage.sprite = newSprite;
            _middleImage.gameObject.SetActive(newSprite != null);
        }
        
        public void DoFeedback(ButtonInteractionFeedback feedback, bool doOneshots) => feedback.DoFeedback(_theme.GetButtonProperties(_componentTier), doOneshots, _text);
        
        public void EndFeedback()
        {
            // apply the default properties again
            ModularButtonProperties properties = _theme.GetButtonProperties(_componentTier);
            properties.ApplyTo(_buttonRectTransform, _text, _frontImage, _middleImage, _backImage, _borderImage, _visualsRectTransform, ButtonInteractionFeedback.TWEEN_TIME, ButtonInteractionFeedback.TWEEN_EASE); 
        }
    }
}