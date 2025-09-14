using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public struct ImageProperties
    {
        [SerializeField] Sprite _sprite;
        [SerializeField] ModularColor _imageColor;
        
        public void ApplyTo(Image target)
        {
            target.sprite = _sprite;
            target.color = _imageColor;
            
            target.gameObject.SetActive(_sprite != null);
        }
    }
}