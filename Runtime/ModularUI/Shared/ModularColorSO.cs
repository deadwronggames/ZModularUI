using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/Color", fileName = "Color")]
    public class ModularColorSO : ScriptableObject
    {
        public Color Color => _color;
        [SerializeField] Color _color;
        
        // Implicit conversion to Color
        public static implicit operator Color(ModularColorSO modularColor)
            => (modularColor != null) ? modularColor._color : Color.white;
    }
}