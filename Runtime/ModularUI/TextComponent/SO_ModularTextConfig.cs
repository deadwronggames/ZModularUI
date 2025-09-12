using DeadWrongGames.ZCommon;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/ModularTextConfig", fileName = "ModularTextConfig")]
    public class ModularTextConfig : BaseModularUIComponentConfig
    {
        // ReSharper disable StringLiteralTypo
        private static readonly string[] DEFAULT_TEXTS = { "Example Header", "Example Subheader", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."}; // ReSharper restore StringLiteralTypo

        public static string GetDefaultText(Tier tier) => ModularUIHelpers.GetProperty(tier, DEFAULT_TEXTS);
    }
}