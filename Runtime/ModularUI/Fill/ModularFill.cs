using System;
using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    // Handles a UI fill component with optional circular fill and tweened change effects
    // TODO add data binding functionality
    public class ModularFill : BaseModularUIComponent<ModularFillConfigSO>
    {
        [SerializeField] bool _isCircularFill;
        [SerializeField] Tier _componentTier;
        [SerializeField] bool _doAllowTweening;
        [SerializeField] BaseModularFillChangeEffectSO _changeEffectSO;
        private bool _doNonDefaultChangeEffect => (_changeEffectSO != null);
        
        [Header("Setup")]
        [SerializeField] Image _backgroundImage;
        [SerializeField] Image _changeIndicatorImage;
        [SerializeField] Image _fillImage;
        [SerializeField] Image _borderImage;

        private float _currentValue;
        
        protected override void Setup()
        {
        }
        
        protected override void Apply()
        {
            _changeIndicatorImage.gameObject.SetActive(_doNonDefaultChangeEffect);
            
            // Apply theme properties for either circular or normal fill
            ModularFillProperties properties = (_isCircularFill) ? _theme.GetFillCircularProperties(_componentTier) : _theme.GetFillProperties(_componentTier);
            properties.ApplyTo(_backgroundImage, _changeIndicatorImage, _fillImage, _borderImage);
            
            // Configure fill method and origin for main and change indicator images
            _changeIndicatorImage.fillMethod = (_isCircularFill) ? Image.FillMethod.Radial360 : Image.FillMethod.Horizontal;
            _fillImage.fillMethod            = (_isCircularFill) ? Image.FillMethod.Radial360 : Image.FillMethod.Horizontal;
            _changeIndicatorImage.fillOrigin = (_isCircularFill) ? (int)Image.Origin360.Top : (int)Image.OriginHorizontal.Left;
            _fillImage.fillOrigin            = (_isCircularFill) ? (int)Image.Origin360.Top : (int)Image.OriginHorizontal.Left;
        }

        private void OnDestroy()
        {
            transform.KillTweensRecursively(); // Avoid warnings
        }

        // Change fill amount, either tween or instant 
        // Also play change feedback
        public void TweenFillAmount(float newFillAmount, bool useUnscaledTime = false, Action onCompleteAction = null)
            => HandleFillAmountChange(newFillAmount, doTweening: true, useUnscaledTime, onCompleteAction);
        public void SetFillAmount(float newFillAmount) 
            => HandleFillAmountChange(newFillAmount, doTweening: false);
        private void HandleFillAmountChange(float newFillAmount, bool doTweening, bool useUnscaledTime = false, Action onCompleteAction = null)
        {
            EnsureConfigured(); 
            transform.KillTweensRecursively();

            // Play custom change effect if assigned, otherwise default effect
            doTweening = (doTweening && _doAllowTweening);
            if (_doNonDefaultChangeEffect) _changeEffectSO.Play(_fillImage, _changeIndicatorImage, _currentValue, newFillAmount, doTweening, useUnscaledTime, onCompleteAction);
            else BaseModularFillChangeEffectSO.PlayChange(_fillImage, newFillAmount, doTweening, useUnscaledTime, onCompleteAction);

            _currentValue = newFillAmount;
        }
    }
}
