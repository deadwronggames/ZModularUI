using System.Collections.Generic;
using System.Linq;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/ButtonInteractionFeedback", fileName = "ButtonInteractionFeedback")]
    public class ButtonInteractionFeedback : SerializedScriptableObject
    {
        public const float TWEEN_TIME = 0.2f;
        public const Ease TWEEN_EASE = Ease.OutQuad;
        
        // ReSharper disable CollectionNeverUpdated.Local
        [SerializeField] AudioClip _audio;
        [SerializeField] Dictionary<ModularColor, ModularColor> _textColorDict;
        [SerializeField] ModularColor _textColorFallback;

        public void DoFeedback(ModularButtonProperties defaultProperties, bool doOneshots, TMP_Text text)
        {
            if (doOneshots)
            {
                if (_audio != null) "TODO playing audio clip not implemented yet".Log(level: ZMethodsDebug.LogLevel.Warning);
            }

            if (TryGetNewColor(defaultProperties.Text.TextColor, _textColorDict, _textColorFallback, out ModularColor newTextColor))
            {
                DOTween.Kill(text);
                text.DOColor(newTextColor, TWEEN_TIME).SetEase(TWEEN_EASE);
            }
            
            // can add more as needed
        }
        

        private static bool TryGetNewColor(ModularColor defaultColor, IReadOnlyDictionary<ModularColor, ModularColor> colorDict, ModularColor colorFallback, out ModularColor newColor)
        {
            if (colorDict.TryGetValue(defaultColor, out newColor)) return true;
       
            newColor = colorFallback;
            return (newColor != null);
        }
    }
}