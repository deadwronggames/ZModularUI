using DeadWrongGames.ZCommon;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/UITheme", fileName = "UITheme", order = 0)]
    public class ModularUITheme : ScriptableObject
    {
        #region Modular Windows
        [Header("Modular Windows")]
        [SerializeField] ModularWindowProperties[] _windowProperties;
        public ModularWindowProperties GetWindowProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _windowProperties);
        #endregion
        
        
        #region Modular Text
        [Header("Modular Text")]
        [SerializeField] ModularTextProperties[] _textProperties;
        public ModularTextProperties GetTextProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _textProperties);
        #endregion
        
        
        #region Modular Buttons
        [Header("Modular Buttons")]
        [SerializeField] ModularButtonProperties[] _buttonProperties;
        public ModularButtonProperties GetButtonProperties(Tier tier) => ModularUIHelpers.GetProperty(tier, _buttonProperties);
        #endregion


#if UNITY_EDITOR
        [Button("Reconfigure all Modular UI")]
        public void ReconfigureAll() => ModularUIHelpers.ReconfigureAllModularUI();
#endif
    }
}