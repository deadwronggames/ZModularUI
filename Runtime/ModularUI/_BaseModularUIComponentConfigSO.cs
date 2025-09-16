using Sirenix.OdinInspector;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    public abstract class BaseModularUIComponentConfigSO : ScriptableObject
    {
        public ModularUIThemeSO Theme => _theme;
        [SerializeField] [Required] ModularUIThemeSO _theme;
        
#if UNITY_EDITOR
        [Button("Reconfigure all Modular UI")]
        public void ReconfigureAll() => ModularUIHelpers.ReconfigureAllModularUI();
#endif
    }
}