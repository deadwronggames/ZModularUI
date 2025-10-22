using DeadWrongGames.ZCommon;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularFill : BaseModularUIComponent<ModularFillConfigSO>
    {
        [SerializeField] bool _isCircularFill;
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
            // Apply theme properties for either circular or normal fill
            ModularFillProperties properties = (_isCircularFill) ? _theme.GetFillCircularProperties(_componentTier) : _theme.GetFillProperties(_componentTier);
            properties.ApplyTo(_backgroundImage, _changeIndicatorImage, _fillImage, _borderImage);
            
            // Set the image fill behaviour
            _changeIndicatorImage.fillMethod = (_isCircularFill) ? Image.FillMethod.Radial360 : Image.FillMethod.Horizontal;
            _fillImage.fillMethod            = (_isCircularFill) ? Image.FillMethod.Radial360 : Image.FillMethod.Horizontal;
            _changeIndicatorImage.fillOrigin = (_isCircularFill) ? (int)Image.Origin360.Top : (int)Image.OriginHorizontal.Left;
            _fillImage.fillOrigin            = (_isCircularFill) ? (int)Image.Origin360.Top : (int)Image.OriginHorizontal.Left;
        }
    }
}
