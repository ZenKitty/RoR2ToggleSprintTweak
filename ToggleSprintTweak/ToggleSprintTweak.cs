using BepInEx;
using Rewired;
using RoR2;

namespace ToggleSprintTweak
{

    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class ToggleSprintPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Zen_Kitty";
        public const string PluginName = "ToggleSprintTweak";
        public const string PluginVersion = "1.0.0";

        public void Awake()
        {
            bool isSprinting = false;

            On.RoR2.PlayerCharacterMasterController.FixedUpdate += (orig, self) => {
                orig(self);
                InputBankTest instanceFieldBodyInputs = self.GetInstanceField<InputBankTest>("bodyInputs");
                if (instanceFieldBodyInputs)
                {
                    if (self.networkUser && self.networkUser.localUser != null && !self.networkUser.localUser.isUIFocused)
                    {
                        CharacterBody instanceFieldBody = self.GetInstanceField<CharacterBody>("body");
                        if (instanceFieldBody)
                        {
                            Player inputPlayer = self.networkUser.localUser.inputPlayer;
                            isSprinting = instanceFieldBody.isSprinting;

                            if (inputPlayer.GetButtonDown("Sprint")) {
                                if (!isSprinting) {
                                    isSprinting = true;
                                }
                            }

                            instanceFieldBodyInputs.sprint.PushState(isSprinting);
                        }
                    }
                }
            };
        }
    }
}
