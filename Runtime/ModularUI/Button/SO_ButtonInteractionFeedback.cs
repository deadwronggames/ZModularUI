using System;
using System.Collections.Generic;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/ButtonInteractionFeedback", fileName = "ButtonInteractionFeedback")]
    public class ButtonInteractionFeedback : ScriptableObject
    {
        public const float TWEEN_TIME = 0.2f;
        public const Ease TWEEN_EASE = Ease.OutQuad;
        
        [SerializeField] AudioClip _audio;
        [SerializeField] ColorPair[] _textColorMap;
        [SerializeField] ModularColor _textColorFallback;

        public void DoFeedback(ModularButtonProperties defaultProperties, bool doOneshots, TMP_Text text)
        {
            if (doOneshots)
            {
                if (_audio != null) "TODO playing audio clip not implemented yet".Log(level: ZMethodsDebug.LogLevel.Warning);
            }

            if (TryGetNewColor(defaultProperties.Text.TextColor, _textColorMap, _textColorFallback, out ModularColor newTextColor))
            {
                DOTween.Kill(text);
                text.DOColor(newTextColor, TWEEN_TIME).SetEase(TWEEN_EASE);
            }
            
            // Can add more as needed
        }

        private static bool TryGetNewColor(ModularColor defaultColor, IEnumerable<ColorPair> colorMap, ModularColor colorFallback, out ModularColor newColor)
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
            public ModularColor Key;
            public ModularColor Value;
        }
    }
}