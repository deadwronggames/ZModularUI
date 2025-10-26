using DeadWrongGames.ZCommon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularButton : BaseModularUIComponent<ModularButtonConfigSO>
    {
        [Tooltip("Toggles are basically just a special form of ModulaButtons. If checked, the theme will provide different ModularButtonProperties.")]
        [SerializeField] bool _isToggle;
        [SerializeField] Tier _componentTier;
        [SerializeField] string _textString;
        [SerializeField] Sprite _frontSprite;
        [SerializeField] Sprite _middleSprite;
        
        [Header("Setup")]
        [SerializeField] RectTransform _visualsRectTransform;
        [SerializeField] TMP_Text _text;
        [SerializeField] Image _frontImage;
        [SerializeField] Image _middleImage;
        [SerializeField] Image _backImage;
        [SerializeField] Image _borderImage;

        private RectTransform _buttonRectTransform;
        private ModularButtonProperties _defaultProperties;
        
        protected override void Setup()
        {
            _buttonRectTransform = GetComponent<RectTransform>();

            // Making sure the visual elements have the correct properties
            _text.fontStyle = FontStyles.Bold | FontStyles.UpperCase;
            _text.alignment = TextAlignmentOptions.Center;
            _frontImage.preserveAspect = true;
            _middleImage.preserveAspect = true;
            _backImage.preserveAspect = false;
        }
        
        protected override void Apply()
        {
            _defaultProperties = (_isToggle) ? _theme.GetToggleProperties(_componentTier) : _theme.GetButtonProperties(_componentTier);
            _defaultProperties.ApplyTo(_buttonRectTransform, _text, _backImage, _borderImage, _visualsRectTransform);
            ApplyContent();
        }
        
        public void DoFeedback(ButtonInteractionFeedbackSO feedback, bool doOneshots) => feedback.DoFeedback(_theme.GetButtonProperties(_componentTier), doOneshots, _text, _frontImage, _middleImage, _backImage);
        
        public void EndFeedback()
        {
            _defaultProperties.ApplyTo(_buttonRectTransform, _text, _backImage, _borderImage, _visualsRectTransform, ButtonInteractionFeedbackSO.TWEEN_TIME, ButtonInteractionFeedbackSO.TWEEN_EASE);
            ApplyContent();
        }

        private void ApplyContent()
        {
            _text.text = _textString;
            _text.enabled = (!string.IsNullOrEmpty(_textString));
            _frontImage.SetSpriteAndToggleEnabled(_frontSprite);
            _middleImage.SetSpriteAndToggleEnabled(_middleSprite);
        }
    }
}