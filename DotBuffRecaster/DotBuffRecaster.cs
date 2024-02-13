using Dalamud.Interface.GameFonts;
using Dalamud.IoC;
using Dalamud.Plugin;
using DotBuffRecaster.Service;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.Interop;
using ImGuiNET;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace DotBuffRecaster {
    unsafe class DotBuffRecaster : IDalamudPlugin {

        bool isConfigOpen = false;

        internal Config config;
        internal DotBuffRecaster D;

        bool isDebug = true;

        private DalamudPluginInterface PluginInterface { get; init; }

        public DotBuffRecaster([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface) {
            PluginInterface = pluginInterface;

            D = this;
            PluginInterface.Create<DalamudService>();
            DalamudService.PluginInterface.UiBuilder.Draw += Draw;
            config = DalamudService.PluginInterface.GetPluginConfig() as Config ?? new Config();

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
                var localPlayer = DalamudService.ClientState.LocalPlayer;
                if (localPlayer == null) return;

                if (isConfigOpen) {
                    if (ImGui.Begin(Constants.Name + " Config", ref isConfigOpen, ImGuiWindowFlags.NoResize)) {
                        ImGui.SetWindowSize(new Vector2(350, 500));
                        MainService.DrawConfigWindow(config, isConfigOpen);
                    }
                    if (!isConfigOpen) {
                        DalamudService.PluginInterface.SavePluginConfig(config);
                    }
                }

                if (!config.IsEnabled) 
                    return;

                if (DalamudService.ClientState.IsPvP) 
                    return;

                if (isDebug)
                    MainService.DrawDebugWindow(localPlayer);

            } catch (Exception e) {

            } finally {

            }
        }
    }
}