using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DeadWrongGames.ZServices.Task;
using DeadWrongGames.ZUtils;

namespace DeadWrongGames.ZModularUI
{
    public abstract class BaseModularUIProperty
    {
        /// <summary>
        /// Modular properties can be hierarchical. Nested properties (like <see cref="ImageProperties"/> inside <see cref="ModularWindowProperties"/>) are recursively loaded and applied.
        /// </summary>
        /// <param name="rootAssetUser">Instance of inheritor of this class or any other class that hold references to modular UI properties</param>
        public static async Task ReloadAddressablesAssetsRecursive(object rootAssetUser)
        {
            List<Task> tasks = new();
            CollectTasks(rootAssetUser);
            await Task.WhenAll(tasks);
            
            return;
            void CollectTasks(object assetUser)
            {
                if (assetUser is BaseModularUIProperty property)
                    tasks.Add(property.ReloadAddressablesAssets());

                FieldInfo[] fields = assetUser.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(assetUser);

                    // Case 1: Single BaseModularUIProperty
                    if (fieldValue != null && typeof(BaseModularUIProperty).IsAssignableFrom(field.FieldType))
                        CollectTasks(fieldValue);
                    
                    // Case 2: IEnumerable of BaseModularUIProperty
                    else if (fieldValue is IEnumerable fieldEnumerable and not string)
                    {
                        foreach (object entry in fieldEnumerable)
                        {
                            if (entry is BaseModularUIProperty)
                                CollectTasks(entry);
                        }
                    }
                }
            }
        }
        
        // Inheritors need to define how their Addressable assets are cached from references.
        protected abstract Task ReloadAddressablesAssets();

        
        // Inheritors should make sure to use this before using Addressable assets
        protected bool EnsureAssetsLoadedOrInvokeAfter(Action actionWhenReady, params object[] objectsToCheck)
        {
            // If nothing is null we are good
            if (objectsToCheck.AllNotNull()) return true;
            
            // Otherwise reload assets, check if now nothing is null and then invoke to action
            ReloadAddressablesAssets().ContinueWith(_ =>
            {
                if (objectsToCheck.AllNotNull())
                    MainThreadDispatcher.Enqueue(actionWhenReady);
            });
            return false;  
        }
    }
}