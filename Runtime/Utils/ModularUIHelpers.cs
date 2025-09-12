using System.Linq;
using DeadWrongGames.ZCommon;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public static class ModularUIHelpers
    {
        public static TProperty GetProperty<TProperty>(Tier tier, TProperty[] array)
        {
            int index = Mathf.Min((int)tier, array.Length - 1);
            return array[index];
        }
        
        public static void ReconfigureAllModularUI()
        {
#if UNITY_EDITOR
            // Configure all modular UI components
            IModularUIComponent[] modularUIComponents = Object.FindObjectsOfType<MonoBehaviour>().OfType<IModularUIComponent>().ToArray();
            foreach (IModularUIComponent modularUIComponent in modularUIComponents)
                modularUIComponent.Configure();

            // Collect root GOs
            Transform[] rootTransforms = modularUIComponents
                .Select(component => ((MonoBehaviour)component).transform.root)
                .Distinct()
                .ToArray();

            // Force layout rebuild
            foreach (Transform transform in rootTransforms)
            {
                foreach (RectTransform rectTransform in transform.GetComponentsInChildren<RectTransform>())
                    LayoutRebuilder.MarkLayoutForRebuild(rectTransform); 
            }

            // Save scene
            if (!EditorApplication.isPlaying) 
                UnityEditor.SceneManagement.EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
#endif
        }
    }
}