using DeadWrongGames.ZCommon;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularFill : BaseModularUIComponent<ModularFillConfigSO>
    {
        [SerializeField] Tier _componentTier;
        
        [Header("Setup")]
        [SerializeField] Image _backgroundImage;
        [SerializeField] Image _changeIndicatorImage;
        [SerializeField] Image _fillImage;
        [SerializeField] Image _borderImage;
        
        protected override void Setup()
        {
        }
        
        protected override void Apply()
        {
            ModularFillProperties properties = _theme.GetFillProperties(_componentTier);
            properties.ApplyTo(_backgroundImage, _changeIndicatorImage, _fillImage, _borderImage);
        }

    }
}
