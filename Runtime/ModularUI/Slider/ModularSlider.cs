using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularSlider : BaseModularUIComponent<ModularSliderConfigSO>, IPointerClickHandler
    {
        [SerializeField] Tier _componentTier;
        
        [Header("Setup")]
        [SerializeField] Image _backgroundImage;
        [SerializeField] RectTransform _fillAreaRectTransform;
        [SerializeField] Image _fillImage;
        [SerializeField] Image _borderImage;
        [SerializeField] RectTransform _slideAreaRectTransform;
        [SerializeField] Image _handleImage;

        [Header("Slider Responses")]
        [SerializeField] UnityEvent<float> _onValueChanged;
        [SerializeField] UnityEvent<float> _onSliderClick;
        
        private Slider _slider;
        
        protected override void Setup()
        {
            _slider = GetComponentInChildren<Slider>(includeInactive: true);
        }

        protected override void Apply()
        {
            // apply text properties
            ModularSliderProperties properties = _theme.GetSliderProperties(_componentTier);
            properties.ApplyTo(_backgroundImage, _fillAreaRectTransform, _fillImage, _borderImage, _slideAreaRectTransform, _handleImage);
        }

        public void OnValueChanged() => _onValueChanged?.Invoke(_slider.value);
        public void OnPointerClick(PointerEventData _) => _onSliderClick?.Invoke(_slider.value);

        public float GetValue()
        {
            return _slider.value;
        }
        
        public void SetValue(float newValue)
        {
            _slider.value = newValue;
        }
        
        public void SetValueWithoutNotify(float newValue)
        {
            _slider.SetValueWithoutNotify(newValue);
        }

        public void Test()
        {
            "Hello".Print();
        }

    }
}