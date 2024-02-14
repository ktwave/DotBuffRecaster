using Dalamud.Game.ClientState.Conditions;
using Dalamud.Interface.GameFonts;
using Dalamud.IoC;
using Dalamud.Logging;
using Dalamud.Plugin;
using DotBuffRecaster.Model;
using DotBuffRecaster.Service;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.Interop;
using ImGuiNET;
using Newtonsoft.Json.Linq;
using System.Numerics;
using static DotBuffRecaster.Model.ActionModel;

namespace DotBuffRecaster {
    public unsafe class DotBuffRecaster : IDalamudPlugin {
        bool isConfigOpen = false;
        bool isDebug = false;
        // bool isDebug = true;
        internal Config config;
        internal DotBuffRecaster D;
        List<StrAction> actions;

        private DalamudPluginInterface PluginInterface { get; init; }

        public DotBuffRecaster([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface) {
            PluginInterface = pluginInterface;

            D = this;
            PluginInterface.Create<DalamudService>();
            DalamudService.PluginInterface.UiBuilder.Draw += Draw;
            config = DalamudService.PluginInterface.GetPluginConfig() as Config ?? new Config();
            actions = ActionService.SetActions();

            DalamudService.PluginInterface.UiBuilder.OpenConfigUi += delegate { isConfigOpen = true; };
            DalamudService.Framework.RunOnFrameworkThread(() => {
                if (config.Font != null) {
                    _ = DalamudService.PluginInterface.UiBuilder.GetGameFontHandle(new GameFontStyle(config.Font.Value));
                }
            });
        }

        public void Dispose() {
            DalamudService.PluginInterface.UiBuilder.Draw -= Draw;
            D = null;
        }

        private void Draw() {
            try {
                // config
                if (isConfigOpen) MainService.DrawConfigWindow(ref config, ref isConfigOpen);

                // check not login
                if (DalamudService.ClientState.LocalPlayer == null) return;

                // check disabled
                if (!config.IsEnabled) return;

                // not supported pvp
                if (DalamudService.ClientState.IsPvP) return;

                // debug mode
                if (isDebug) MainService.DrawDebugWindow();

                // main method
                MainService.Draw(actions, config);

            } catch (Exception e) {
                // error
                PluginLog.Error(e.Message + "\n" + e.StackTrace);
            } finally {

            }
        }
    }
}