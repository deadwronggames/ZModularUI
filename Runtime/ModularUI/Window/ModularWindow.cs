using DeadWrongGames.ZCommon;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularWindow : BaseModularUIComponent<ModularTextConfig>
    {
        [SerializeField] Tier _componentTier;
        
        [Header("Setup")]
        [SerializeField] Image _backgroundImage;
        [SerializeField] Image _borderImage;
        [SerializeField] VerticalLayoutGroup _contentVerticalLayoutGroup;
        
        protected override void Setup()
        {
        }
        
        protected override void Apply()
        {
            _backgroundImage.sprite = _theme.GetWindowBackgroundSprite(_componentTier);
            _backgroundImage.color = _theme.GetWindowBackgroundColor(_componentTier);
            _theme.GetWindowBorderProperties(_componentTier).ApplyTo(_borderImage);
            _theme.GetWindowBorderProperties(_componentTier).ApplyPadding(_backgroundImage.rectTransform);
            _contentVerticalLayoutGroup.padding = _theme.GetWindowContentPadding(_componentTier);
        }
    }
}   