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
    public class ImageProperties : BaseModularUIProperty
    {
        [SerializeField] AssetReferenceSpriteSO _spriteAssetReference;
        [SerializeField] ModularColorSO _imageColor;
        private Sprite _sprite;
        
        public Sprite Sprite => _sprite;
        public ModularColorSO ImageColor => _imageColor;

        protected override async Task ReloadAddressablesAssets()
        {
            if (_spriteAssetReference != null)
            {
                if (_spriteAssetReference != null)
                    _sprite = await _spriteAssetReference.LoadAssetSafeAsync();
            }
        }
        
        
        public void ApplyTo(Image target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            // Make sure to check all objects that are loaded from Addressables
            if (!EnsureAssetsLoadedOrInvokeAfter(() => ApplyTo(target, tweenTime, ease), _sprite)) return;
            
            target.sprite = _sprite;
            if (tweenTime == 0f) target.color = _imageColor;
            else
            {
                DOTween.Kill(target);
                target.DOColor(_imageColor, tweenTime).SetEase(ease);
            }
            
            target.gameObject.SetActive(_sprite != null);
        }
    }
}