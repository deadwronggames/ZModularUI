using System;
using DeadWrongGames.ZCommon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    /// <summary>
    /// Class controlling the visuals of themed buttons.
    /// Can also represent toggles if <see cref="_isToggle"/> is set to true.
    /// </summary>
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
        
        private ModularButtonProperties _defaultProperties;
        private ModularButtonProperties.InteractionFeedbackContainer _interactionFeedbacks;
        
        protected override void Setup()
        {
            // Making sure the visual elements have the correct properties
            _text.fontStyle = FontStyles.Bold | FontStyles.UpperCase;
            _text.alignment = TextAlignmentOptions.Center;
            _frontImage.preserveAspect = true;
            _middleImage.preserveAspect = true;
            _backImage.preserveAspect = false;
        }
        
        protected override void Apply()
        {
            // Load properties based on tier and toggle mode
            _defaultProperties = (_isToggle) ? _theme.GetToggleProperties(_componentTier) : _theme.GetButtonProperties(_componentTier);
            _interactionFeedbacks = _defaultProperties.InteractionFeedbacks;

            // Then apply theme visuals and text / sprite content
            _defaultProperties.ApplyTo(_text, _backImage, _borderImage, _visualsRectTransform);
            ApplyContent();
        }

        public ModularButtonProperties.InteractionFeedbackContainer GetInteractionFeedbacks() => _defaultProperties.InteractionFeedbacks;
        
        /// <summary>
        /// Plays feedback animation/effects defined in <see cref="ButtonInteractionFeedbackSO"/>.
        /// </summary>
        public void DoFeedback(ButtonInteractionFeedbackSO feedback, bool doOneshots)
        {
            EnsureConfigured();
            feedback.DoFeedback(_theme.GetButtonProperties(_componentTier), doOneshots, _text, _frontImage, _middleImage, _backImage);
        }
        
        /// <summary>
        /// Resets visuals back to their default state.
        /// </summary>
        public void EndFeedback()
        {
            _defaultProperties.ApplyTo(_text, _backImage, _borderImage, _visualsRectTransform, ButtonInteractionFeedbackSO.TWEEN_TIME, ButtonInteractionFeedbackSO.TWEEN_EASE);
            ApplyContent();
        }

        // Sets text and sprite button content.
        private void ApplyContent()
        {
            _text.text = _textString;
            _text.enabled = (!string.IsNullOrEmpty(_textString));
            _frontImage.SetSpriteAndToggleEnabled(_frontSprite);
            _middleImage.SetSpriteAndToggleEnabled(_middleSprite);
        }
    }
}