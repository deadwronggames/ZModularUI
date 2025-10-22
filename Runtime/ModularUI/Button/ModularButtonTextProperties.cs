// using System;
// using System.Threading.Tasks;
// using DeadWrongGames.ZCommon;
// using DeadWrongGames.ZUtils;
// using TMPro;
// using UnityEngine;
// using DG.Tweening;
// 
// namespace DeadWrongGames.ZModularUI
// {
//     [Serializable]
//     public class ModularButtonTextProperties : BaseModularUIProperty
//     {
//         [SerializeField] AssetReferenceFontAssetSO _fontAssetReference;
//         [SerializeField] int _fontSize;
//         [SerializeField] ModularColorSO _textColor;
//         [HideInInspector] [SerializeField] TMP_FontAsset _font; // Serialization is necessary so that value is carried over into build
//         
//         public TMP_FontAsset Font => _font;
//         public int FontSize => _fontSize;
//         public ModularColorSO TextColor => _textColor;
//         
//         protected override async Task ReloadAddressablesAssets()
//         {
//             if (_fontAssetReference != null)
//                 _font = await _fontAssetReference.LoadAssetSafeAsync();
//         }
//         
//         public void ApplyTo(TMP_Text target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
//         {
//             // Make sure to check all objects that are loaded from Addressables
//             target.enabled = ((_font != null) && (_fontSize > 0));
//             if (!EnsureAssetsLoadedOrInvokeAfter(() => ApplyTo(target, tweenTime, ease), _font)) return;
//             
//             target.font = _font;
//             target.fontSize = _fontSize;
//             if (tweenTime == 0f) target.color = _textColor;
//             else
//             {
//                 DOTween.Kill(target);
//                 target.DOColor(_textColor, tweenTime).SetEase(ease);
//             }
//         }
//     }
// }