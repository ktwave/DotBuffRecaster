using Dalamud.Interface.GameFonts;
using Dalamud.IoC;
using Dalamud.Plugin;
using DotBuffRecaster.Service;
using ImGuiNET;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace DotBuffRecaster {
    unsafe class DotBuffRecaster : IDalamudPlugin {
        public string Name => "DotBuffRecaster";
        bool isConfigOpen = false;

        internal Config config;
        internal DotBuffRecaster D;

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
                if (isConfigOpen) {
                    if (ImGui.Begin(Name + " Config", ref isConfigOpen, ImGuiWindowFlags.NoResize)) {
                        ImGui.SetWindowSize(new Vector2(350, 500));

                        var isEnabled = config.IsEnabled;
                        if (ImGui.Checkbox("プラグインを有効にする(Enable Plugin)", ref isEnabled)) {
                            config.IsEnabled = isEnabled;
                        }

                        ImGui.Spacing();

                        var isPreview = config.IsPreview;
                        if (ImGui.Checkbox("プレビュー(Preview)", ref isPreview)) {
                            config.IsPreview = isPreview;
                        }
                        ImGui.Spacing();
                        ImGui.Separator();
                        ImGui.Spacing();

                        var isLeftAlighn = config.IsLeftAlighn;
                        if (ImGui.Checkbox("アイコンを左揃えにする(Left Align Icon)", ref isLeftAlighn)) {
                            config.IsLeftAlighn = isLeftAlighn;
                        }
                        ImGui.Spacing();

                        ImGui.Text("X座標のオフセット(X Offset)");
                        var offsetX = (int)config.OffsetX;
                        ImGui.SetNextItemWidth(200f);
                        if (ImGui.DragInt(" ", ref offsetX, 0.5f)) {
                            config.OffsetX = offsetX;
                        }
                        ImGui.Spacing();

                        ImGui.Text("アイコンの拡大率(Icon Scale)");
                        var size = (int)config.Size;
                        ImGui.SetNextItemWidth(200f);
                        if (ImGui.DragInt("   ", ref size, 1, 1, 300)) {
                            config.Size = size;
                        }
                        ImGui.Spacing();

                        ImGui.Text("アイコンの間隔(Icon Padding)");
                        var padding = config.Padding;
                        ImGui.SetNextItemWidth(200f);
                        if (ImGui.DragInt("     ", ref padding, 1f, 0, 100)) {
                            config.Padding = padding;
                        }
                        ImGui.Spacing();
                        ImGui.Separator();
                        ImGui.Spacing();

                        config.Font = GameFontFamilyAndSize.Axis36;
                    }
                }
            } catch (Exception e) {

            } finally {
            }
        }
    }
}