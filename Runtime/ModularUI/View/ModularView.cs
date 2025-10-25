using DeadWrongGames.ZCommon;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularView : BaseModularUIComponent<ModularViewConfigSO>
    {
        [SerializeField] Tier _componentTier;
        
        [Header("Setup")]
        [SerializeField] Image _backgroundImage;
        [SerializeField] Image _borderImage;
        [SerializeField] RectTransform _contentRectTransform;
        
        protected override void Setup()
        {
        }
        
        protected override void Apply()
        {
            ModularViewProperties properties = _theme.GetViewProperties(_componentTier);
            properties.ApplyTo(_backgroundImage, _borderImage, _contentRectTransform);
        }
    }
}   