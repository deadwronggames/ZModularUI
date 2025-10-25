using System;
using System.Threading.Tasks;
using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularImageProperties : BaseModularUIProperty
    {
        [SerializeField] AssetReferenceSpriteSO _spriteAssetReference;
        [SerializeField] ModularColorSO _imageColor;
        [HideInInspector] [SerializeField] Sprite _sprite; // Serialization is necessary so that value is carried over into build
        
        protected override async Task ReloadAddressablesAssets()
        {
            if (_spriteAssetReference != null)
                _sprite = await _spriteAssetReference.LoadAssetSafeAsync();
        }
        
        
        public void ApplyTo(Image target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            // Do this regardless of weather _sprite is null
            if (_spriteAssetReference == null) _sprite = null;
            target.sprite = _sprite;
            target.enabled = (_sprite != null);
            
            // Make sure to check all objects that are loaded from Addressables
            if (!EnsureAssetsLoadedOrInvokeAfter(() => ApplyTo(target, tweenTime, ease), _sprite)) return;
            
            if (tweenTime == 0f) target.color = _imageColor;
            else
            {
                DOTween.Kill(target);
                target.DOColor(_imageColor, tweenTime).SetEase(ease);
            }
        }
    }
}