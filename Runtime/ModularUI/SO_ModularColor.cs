using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/Color", fileName = "Color")]
    public class ModularColor : ScriptableObject
    {
        public Color Get() => _color;
        [SerializeField] Color _color;
        
        // Implicit conversion to Color
        public static implicit operator Color(ModularColor modularColor)
            => (modularColor != null) ? modularColor._color : Color.white;
    }
}