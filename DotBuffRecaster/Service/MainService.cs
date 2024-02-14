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
using DotBuffRecaster.Enums;
using static DotBuffRecaster.Model.ActionModel;
using System.ComponentModel.DataAnnotations;
using Dalamud.Interface.Utility;
using Dalamud.Logging;
using Dalamud.Game.ClientState.Objects.Types;
using static System.Net.Mime.MediaTypeNames;

namespace DotBuffRecaster.Service {
    public static class MainService {
        internal static void DrawConfigWindow(ref Config config, ref bool isConfigOpen) {
            {
                if (ImGui.Begin(Constants.Name + " Config", ref isConfigOpen, ImGuiWindowFlags.NoResize | ImGuiWindowFlags.AlwaysAutoResize)) {
                    // ImGui.SetWindowSize(new Vector2(350, 300));

                    config.Font = GameFontFamilyAndSize.Axis36;

                    var isEnabled = config.IsEnabled;
                    if (ImGui.Checkbox("プラグインを有効にする(Enable Plugin)", ref isEnabled)) {
                        config.IsEnabled = isEnabled;
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
                    if (ImGui.DragFloat("  ", ref offsetY, 0.5f)) {
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
                    ImGui.Spacing();

                    ImGui.SetCursorPosX(180f);
                    if(ImGui.Button("閉じる(Close)")) {
                        isConfigOpen = false;
                        DalamudService.PluginInterface.SavePluginConfig(config);
                    }
                    ImGui.End();

                    if (!isConfigOpen) {
                        DalamudService.PluginInterface.SavePluginConfig(config);
                    }
                }
            }
        }

        internal static void DrawDebugWindow() {
            DrawDebugHotbarInfo();
            DrawDebugStatusInfo();
        }

        private static void DrawDebugStatusInfo() {
            PlayerCharacter localPlayer = DalamudService.ClientState.LocalPlayer;
            if (ImGui.Begin("[DBG]Statuses", ImGuiWindowFlags.AlwaysAutoResize)) {
                var playerStatuses = localPlayer.StatusList.Where(s => s.StatusId != 0).ToList();
                ImGui.Text("ObjectId: " + localPlayer.ObjectId.ToString());
                ImGui.Text("ClassJobId: " + localPlayer.ClassJob.Id.ToString());
                ImGui.Text("ClassJob: " + Enum.GetName(typeof(JobIds), localPlayer.ClassJob.Id));
                ImGui.Text("statuses.count: " + playerStatuses.Count);
                ImGui.Separator();
                foreach (var i in Enumerable.Range(0, playerStatuses.Count)) {
                    ImGui.Text("StatusId[" + i.ToString() + "]: " + playerStatuses[i].StatusId.ToString());
                    ImGui.Text("RemainingTime[" + i.ToString() + "]: " + playerStatuses[i].RemainingTime.ToString("#"));
                    ImGui.Text("");
                }
                ImGui.Separator();

                var targetStatus = GetTargetStatuses();
                ImGui.Text("statuses.count: " + targetStatus.Count);
                foreach (var i in Enumerable.Range(0, targetStatus.Count)) {
                    ImGui.Text("StatusId[" + i.ToString() + "]: " + targetStatus[i].StatusId.ToString());
                    ImGui.Text("RemainingTime[" + i.ToString() + "]: " + targetStatus[i].RemainingTime.ToString("#"));
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

        internal static unsafe void Draw(List<StrAction> actions, Config config) {
            try {
                PlayerCharacter localPlayer = DalamudService.ClientState.LocalPlayer;
                actions.Where(action => action.JobId == localPlayer.ClassJob.Id).ToList().ForEach(action => {
                    foreach (var i in Enumerable.Range(0, Constants.HotbarAddonName.Length)) {
                        var hotbarNo = (i + 1).ToString();
                        var addonName = Constants.HotbarAddonName[i];
                        var addon = (AddonActionBarBase*)Service.DalamudService.GameGui.GetAddonByName(addonName);
                        if (!IsEnabledHotbar(addon)) continue;
                        foreach (var index in Enumerable.Range(0, addon->Slot.Length)) {
                            var hotbarAction = addon->Slot.GetPointer(index);
                            if (hotbarAction->ActionId == action.Action) {
                                var positonX = hotbarAction->ComponentDragDrop->AtkComponentBase.OwnerNode->AtkResNode.ScreenX + config.OffsetX;
                                var positonY = hotbarAction->ComponentDragDrop->AtkComponentBase.OwnerNode->AtkResNode.ScreenY + config.OffsetY;
                                ImGuiHelpers.ForceNextWindowMainViewport();
                                ImGuiHelpers.SetNextWindowPosRelativeMainViewport(new Vector2(positonX, positonY));
                                if (ImGui.Begin("Draw" + action.Action.ToString()
                                    , ImGuiWindowFlags.NoInputs
                                    | ImGuiWindowFlags.NoMove
                                    | ImGuiWindowFlags.NoScrollbar
                                    | ImGuiWindowFlags.NoBackground
                                    | ImGuiWindowFlags.NoTitleBar
                                    )) {
                                    
                                    float time = 0f;
                                    if(action.ActionType == ActionType.BUFF) {
                                        var playerStatuses = localPlayer.StatusList.Where(s => s.StatusId != 0).ToList();
                                        time = playerStatuses.Where(s => s.StatusId == action.StatusId).Select(s => s.RemainingTime).FirstOrDefault();
                                    } else {
                                        var targetStatuses = GetTargetStatuses();
                                        time = targetStatuses.Where(s => s.StatusId == action.StatusId).Select(s => s.RemainingTime).FirstOrDefault();
                                    }
                                    
                                    var dispTime = time.ToString("#");
                                    
                                    var textOffsetX = 3f;
                                    var textOffsetY = 0f;
                                    if (dispTime.Length == 1)
                                        textOffsetX = ImGui.CalcTextSize("99").X / 3;
                                    
                                    ImGui.SetWindowFontScale(1f * config.Scale / 100);

                                    ImGui.PushStyleColor(ImGuiCol.Text, Constants.Black);

                                    ImGui.SetCursorPos(new Vector2(textOffsetX + 1.5f, textOffsetY));
                                    ImGui.Text(dispTime);
                                    
                                    ImGui.SetCursorPos(new Vector2(textOffsetX - 1.5f, textOffsetY));
                                    ImGui.Text(dispTime);

                                    ImGui.SetCursorPos(new Vector2(textOffsetX, textOffsetY + 1.5f));
                                    ImGui.Text(dispTime);

                                    ImGui.SetCursorPos(new Vector2(textOffsetX, textOffsetY - 1.5f));
                                    ImGui.Text(dispTime);
                                    ImGui.PopStyleColor();

                                    ImGui.SetCursorPos(new Vector2(textOffsetX, textOffsetY));
                                    if (time < 4) ImGui.PushStyleColor(ImGuiCol.Text, Constants.Red);
                                    ImGui.Text(dispTime);
                                    if (time < 4) ImGui.PopStyleColor();

                                    ImGui.End();
                                }
                            }
                        }
                    }
                });
            } catch (Exception e) {
                PluginLog.Error(e.Message + "\n" + e.StackTrace);
            } finally {
                
            }
        }

        private static List<Dalamud.Game.ClientState.Statuses.Status> GetTargetStatuses() {
            GameObject target = DalamudService.TargetManager.Target;
            if (target is Dalamud.Game.ClientState.Objects.Types.BattleChara b) {
                return b.StatusList.Where(s => s.StatusId != 0).ToList();
            }
            return null;
        }
    }
}
