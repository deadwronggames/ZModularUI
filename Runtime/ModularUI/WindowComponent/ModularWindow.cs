using DeadWrongGames.ZCommon;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularWindow : BaseModularUIComponent<ModularTextConfig>
    {
        [SerializeField] Tier _componentTier;
        
        [Header("Setup")]
        [SerializeField] GameObject _backgroundGO;
        [SerializeField] GameObject _borderGO;
        [SerializeField] GameObject _contentGO;
        
        private Image _imageBackground;
        private Image _imageBorder;
        private VerticalLayoutGroup _contentVerticalLayoutGroup;
        
        protected override void Setup()
        {
            _imageBackground = _backgroundGO.GetComponent<Image>();
            _imageBorder = _borderGO.GetComponent<Image>();
            _contentVerticalLayoutGroup = _contentGO.GetComponent<VerticalLayoutGroup>();
        }
        
        protected override void Apply()
        {
            _imageBackground.sprite = _theme.GetWindowBackgroundSprite(_componentTier);
            _imageBorder.sprite = _theme.GetWindowBorderSprite(_componentTier);
            _contentVerticalLayoutGroup.padding = _theme.GetWindowPadding(_componentTier);
        }
    }
}   