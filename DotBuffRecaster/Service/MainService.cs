using Dalamud.Interface.GameFonts;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;
using Dalamud.Game.ClientState.Objects.SubKinds;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotBuffRecaster.Service {
    public static class MainService {
        internal static void DrawConfigWindow(Config config, bool isConfigOpen) {
            {
                config.Font = GameFontFamilyAndSize.Axis36;

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

                ImGui.Text("X座標のオフセット(X Offset)");
                var offsetX = config.OffsetX;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat(" ", ref offsetX, 0.5f)) {
                    config.OffsetX = offsetX;
                }
                ImGui.Spacing();

                ImGui.Text("Y座標のオフセット(X Offset)");
                var offsetY = config.OffsetY;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat(" ", ref offsetY, 0.5f)) {
                    config.OffsetY = offsetY;
                }
                ImGui.Spacing();

                ImGui.Text("フォントの拡大率(Font Scale)");
                var scale = config.Scale;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("   ", ref scale, 1, 1, 300)) {
                    config.Scale = scale;
                }

                ImGui.Spacing();
                ImGui.Separator();
                ImGui.Spacing();
                /*
                if(ImGui.BeginTabBar("ジョブ設定", ImGuiTabBarFlags.)) {

                }*/
            }
        }

        internal static void DrawDebugWindow(PlayerCharacter localPlayer) {
            DrawDebugHotbarInfo();
            DrawDebugStatusInfo(localPlayer);
        }

        private static void DrawDebugStatusInfo(PlayerCharacter localPlayer) {
            if (ImGui.Begin("[DBG]Statuses", ImGuiWindowFlags.AlwaysAutoResize)) {
                ImGui.Text("ObjectId: " + localPlayer.ObjectId.ToString());
                ImGui.Text("ClassJobId: " + localPlayer.ClassJob.Id.ToString());
                ImGui.Text("ClassJob: " + Enum.GetName(typeof(JobIds), localPlayer.ClassJob.Id));
                ImGui.Separator();
                var statuses = localPlayer.StatusList.Where(s => s.StatusId != 0).ToList();
                foreach (var i in Enumerable.Range(0, statuses.Count)) {
                    ImGui.Text("StatusId[" + i.ToString() + "]: " + statuses[0].StatusId.ToString());
                    ImGui.Text("ObjectId[" + i.ToString() + "]: " + statuses[0].SourceObject.ObjectId.ToString());
                    ImGui.Text("ObjectId[" + i.ToString() + "]: " + statuses[0].SourceObject.OwnerId.ToString());
                    ImGui.Text("RemainingTime[" + i.ToString() + "]: " + statuses[0].RemainingTime.ToString("#"));
                    ImGui.Text("");
                }

                ImGui.End();
            }


        }

        private static unsafe void DrawDebugHotbarInfo() {
            foreach (var i in Enumerable.Range(0, Constants.HotbarAddonName.Length)) {
                var hotbarNo = (i + 1).ToString();
                var addonName = Constants.HotbarAddonName[i];
                var a = (AddonActionBarBase*)Service.DalamudService.GameGui.GetAddonByName(addonName);
                if (!IsEnabledHotbar(a)) continue;
                if (ImGui.Begin("[DBG]Hotbar" + hotbarNo, ImGuiWindowFlags.AlwaysAutoResize)) {
                    foreach (var index in Enumerable.Range(0, a->Slot.Length)) {
                        var slot = a->Slot.GetPointer(index);
                        var positonX = slot->ComponentDragDrop->AtkComponentBase.OwnerNode->AtkResNode.ScreenX;
                        var positonY = slot->ComponentDragDrop->AtkComponentBase.OwnerNode->AtkResNode.ScreenY;
                        var actionId = slot->ActionId;
                        ImGui.Text("Hotbar" + hotbarNo + "-" + (index + 1).ToString());
                        ImGui.Text("ActionId: " + actionId.ToString());
                        ImGui.Text("X: " + positonX.ToString());
                        ImGui.SameLine();
                        ImGui.Text("Y: " + positonY.ToString());
                        ImGui.Separator();
                    }
                }
                ImGui.End();
            }
        }

        private static unsafe bool IsEnabledHotbar(AddonActionBarBase* a) {
            if (a is null) return false;
            if (!((AtkUnitBase*)a)->IsVisible) return false;
            return true;
        }
    }
}
