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
    public class UIBorderProperties : BaseModularUIProperty
    {
        [SerializeField] AssetReferenceSpriteSO _spriteAssetReference;
        [SerializeField] ModularColorSO _imageColor;
        [SerializeField] RectOffset _contentPadding;
        [HideInInspector] [SerializeField] Sprite _sprite; // Serialization is necessary so that value is carried over into build
        public Sprite Sprite => _sprite;
        public ModularColorSO ImageColor => _imageColor;
        public RectOffset ContentPadding => _contentPadding;


        protected override async Task ReloadAddressablesAssets()
        {
            if (_spriteAssetReference != null)
                _sprite = await _spriteAssetReference.LoadAssetSafeAsync();
        }
        
        public void ApplyTo(Image target, RectTransform targetPadding, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            // Make sure to check all objects that are loaded from Addressables
            target.enabled = (_sprite != null);
            if (!EnsureAssetsLoadedOrInvokeAfter(() => ApplyTo(target, targetPadding, tweenTime, ease), _sprite)) return;
            
            target.sprite = _sprite;
            
            if (tweenTime == 0f) target.color = _imageColor;
            else
            {
                DOTween.Kill(target);
                target.DOColor(_imageColor, tweenTime).SetEase(ease);
            }
            
            targetPadding.SetPadding(_contentPadding);
            
            
        }
    }
}