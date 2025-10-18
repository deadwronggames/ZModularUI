using DeadWrongGames.ZCommon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularButton : BaseModularUIComponent<ModularButtonConfigSO>
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
        private ModularButtonProperties _defaultProperties;
        
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
#if UNITY_EDITOR
            if (!ModularUIThemeSO.JustRecompiled) // Changing RectTransform throws warnings otherwise
#endif
            {
                _defaultProperties = _theme.GetButtonProperties(_componentTier);
                _defaultProperties.ApplyTo(_buttonRectTransform, _text, _frontImage, _middleImage, _backImage, _borderImage, _visualsRectTransform);
            }
        }
        
        public void DoFeedback(ButtonInteractionFeedbackSO feedback, bool doOneshots) => feedback.DoFeedback(_theme.GetButtonProperties(_componentTier), doOneshots, _text);
        
        public void EndFeedback()
        {
            _defaultProperties.ApplyTo(_buttonRectTransform, _text, _frontImage, _middleImage, _backImage, _borderImage, _visualsRectTransform, ButtonInteractionFeedbackSO.TWEEN_TIME, ButtonInteractionFeedbackSO.TWEEN_EASE, test: true); 
        }
    }
}