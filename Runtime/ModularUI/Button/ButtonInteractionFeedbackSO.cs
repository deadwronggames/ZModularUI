using System;
using System.Collections.Generic;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/ButtonInteractionFeedback", fileName = "ButtonInteractionFeedback")]
    public class ButtonInteractionFeedbackSO : ScriptableObject
    {
        public const float TWEEN_TIME = 0.2f;
        public const Ease TWEEN_EASE = Ease.OutQuad;
        
        [SerializeField] AudioClip _audio;
        [SerializeField] ColorPair[] _textColorMap;
        [SerializeField] ModularColorSO _textColorFallback;

        public void DoFeedback(ModularButtonProperties defaultProperties, bool doOneshots, TMP_Text text)
        {
            if (doOneshots)
            {
                if (_audio != null) "TODO playing audio clip not implemented yet".Log(level: ZMethodsDebug.LogLevel.Warning);
            }

            if (TryGetNewColor(defaultProperties.Text.TextColor, _textColorMap, _textColorFallback, out ModularColorSO newTextColor))
            {
                DOTween.Kill(text);
                text.DOColor(newTextColor, TWEEN_TIME).SetEase(TWEEN_EASE);
            }
            
            // Can add more as needed
        }

        private static bool TryGetNewColor(ModularColorSO defaultColor, IEnumerable<ColorPair> colorMap, ModularColorSO colorFallback, out ModularColorSO newColor)
        {
            // Try to find new color from color map
            foreach (ColorPair colorPair in colorMap)
            {
                if (defaultColor.Color == colorPair.Key.Color)
                {
                    newColor = colorPair.Value;
                    return true;
                }
            }
       
            // otherwise fallback or just null
            newColor = colorFallback;
            return (newColor != null);
        }

        [Serializable]
        private struct ColorPair
        {
            public ModularColorSO Key;
            public ModularColorSO Value;
        }
    }
}