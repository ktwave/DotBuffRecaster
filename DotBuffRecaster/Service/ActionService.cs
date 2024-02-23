using DotBuffRecaster.Enums;
using DotBuffRecaster.Model;
using FFXIVClientStructs.FFXIV.Client.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DotBuffRecaster.Model.ActionModel;

namespace DotBuffRecaster.Service {
    internal class ActionService {
        internal static List<StrAction> SetActions() {
            var result = new List<StrAction>();

            // PLD
            var a = new StrAction();
            a.JobId = (int)JobIds.PLD;
            a.Action = (int)Actions.FightOrFlight;
            a.StatusId = (int)Statuses.Buff.FightOrFlight;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            // WAR
            a = new StrAction();
            a.JobId = (int)JobIds.WAR;
            a.Action = (int)Actions.StormsEye;
            a.StatusId = (int)Statuses.Buff.StormsEye;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            // GNB
            a = new StrAction();
            a.JobId = (int)JobIds.GNB;
            a.Action = (int)Actions.NoMercy;
            a.StatusId = (int)Statuses.Buff.NoMercy;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            // WHM
            a = new StrAction();
            a.JobId = (int)JobIds.WHM;
            a.Action = (int)Actions.Dia;
            a.StatusId = (int)Statuses.DeBuff.Dia;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            // SCH
            a = new StrAction();
            a.JobId = (int)JobIds.SCH;
            a.Action = (int)Actions.Biolysis;
            a.StatusId = (int)Statuses.DeBuff.Biolysis;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            // AST
            a = new StrAction();
            a.JobId = (int)JobIds.AST;
            a.Action = (int)Actions.Combust3;
            a.StatusId = (int)Statuses.DeBuff.Combust3;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            // SGE
            a = new StrAction();
            a.JobId = (int)JobIds.SGE;
            a.Action = (int)Actions.Dosis3;
            a.StatusId = (int)Statuses.DeBuff.Dosis3;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            // Melee
            a = new StrAction();
            a.Action = (int)Actions.TrueNorth;
            a.StatusId = (int)Statuses.Buff.TrueNorth;
            a.ActionType = ActionModel.ActionType.BUFF;

            a.JobId = (int)JobIds.MNK;
            result.Add(a);

            a.JobId = (int)JobIds.DRG;
            result.Add(a);

            a.JobId = (int)JobIds.NIN;
            result.Add(a);

            a.JobId = (int)JobIds.SAM;
            result.Add(a);

            a.JobId = (int)JobIds.RPR;
            result.Add(a);

            // MNK
            a = new StrAction();
            a.JobId = (int)JobIds.MNK;
            a.Action = (int)Actions.TwinSnakes;
            a.StatusId = (int)Statuses.Buff.GrantsDisciplinedFist;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.MNK;
            a.Action = (int)Actions.FourpointFury;
            a.StatusId = (int)Statuses.Buff.GrantsDisciplinedFist;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.MNK;
            a.Action = (int)Actions.Demolish;
            a.StatusId = (int)Statuses.DeBuff.Demolish;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            // DRG
            a = new StrAction();
            a.JobId = (int)JobIds.DRG;
            a.Action = (int)Actions.Disembowel;
            a.StatusId = (int)Statuses.Buff.Disembowel;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.DRG;
            a.Action = (int)Actions.ChaoticSpring;
            a.StatusId = (int)Statuses.DeBuff.ChaoticSpring;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.DRG;
            a.Action = (int)Actions.LanceCharge;
            a.StatusId = (int)Statuses.Buff.LanceCharge;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.DRG;
            a.Action = (int)Actions.DragonSight;
            a.StatusId = (int)Statuses.Buff.DragonSight;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            // SAM
            a = new StrAction();
            a.JobId = (int)JobIds.SAM;
            a.Action = (int)Actions.Higanbana;
            a.StatusId = (int)Statuses.DeBuff.Higanbana;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.SAM;
            a.Action = (int)Actions.Gekko;
            a.StatusId = (int)Statuses.Buff.Gekko;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.SAM;
            a.Action = (int)Actions.Kasha;
            a.StatusId = (int)Statuses.Buff.Kasha;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.SAM;
            a.Action = (int)Actions.MeikyoShisui;
            a.StatusId = (int)Statuses.Buff.MeikyoShisui;
            a.ActionType = ActionModel.ActionType.BUFF;
            result.Add(a);

            // RPR
            a = new StrAction();
            a.JobId = (int)JobIds.RPR;
            a.Action = (int)Actions.ShadowOfDeath;
            a.StatusId = (int)Statuses.DeBuff.DeathDesign;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.RPR;
            a.Action = (int)Actions.WhorlOfDeath;
            a.StatusId = (int)Statuses.DeBuff.DeathDesign;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            // BRD
            a = new StrAction();
            a.JobId = (int)JobIds.BRD;
            a.Action = (int)Actions.CausticBite;
            a.StatusId = (int)Statuses.DeBuff.CausticBite;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.BRD;
            a.Action = (int)Actions.Stormbite;
            a.StatusId = (int)Statuses.DeBuff.Stormbite;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            a = new StrAction();
            a.JobId = (int)JobIds.BRD;
            a.Action = (int)Actions.IronJaws;
            a.StatusId = (int)Statuses.DeBuff.CausticBite;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            // BLM
            a = new StrAction();
            a.JobId = (int)JobIds.BLM;
            a.Action = (int)Actions.Thunder3;
            a.StatusId = (int)Statuses.DeBuff.Thunder3;
            a.ActionType = ActionModel.ActionType.DEBUFF;
            result.Add(a);

            return result;
        }
    }
}

