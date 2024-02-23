using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBuffRecaster.Enums {
    public static class Statuses {
        public enum Buff {
            // PLD
            FightOrFlight = 76, // FOF

            // WAR
            StormsEye = 2677,// シュトルムブレハ

            // GNB
            NoMercy = 1831, // ノー・マーシー

            // MNK
            GrantsDisciplinedFist = 3001, // 抗力(双掌打 四面脚)

            // DRG
            LanceCharge = 1864, // ランスチャージ
            DragonSight = 1910, // ドラゴンサイト
            Disembowel = 2720,

            // SAM
            Gekko = 1298, // 月光
            Kasha = 1299, // 花車
            MeikyoShisui = 1233, // 明鏡止水

            // Melee
            TrueNorth = 1250,
        }

        public enum DeBuff {
            // WHM
            // Aero = // エアロ
            // Aero2 = // エアロラ
            Dia = 1871, // ディア

            // SCH
            // bio // バイオ
            // bio2 // バイオラ
            Biolysis = 1895, // 蠱毒法

            // AST
            // Combust1 // コンバス
            // Combust2 // コンバラ
            Combust3 = 1881,// コンバガ

            // SGE
            // Dosis // ドシス
            // Dosis2 // ドシスII
            Dosis3 = 2616,// ドシスIII

            // MNK
            Demolish = 246, // 破砕拳DoT

            // DRG
            // ChaosThrust = , // 桜華狂咲
            ChaoticSpring = 2719, // 桜華繚乱

            // SAM
            Higanbana = 7867, // 彼岸花

            // RPR
            DeathDesign = 2586, // デスデザイン(シャドウ・オブ・デス ワーラル・オブ・デス)

            // BRD
            // Venomous Bite // ベノムバイト
            CausticBite = 1200, // コースティックバイト
            // Windbite // ウィンドバイト
            Stormbite = 1201,// ストームバイト

            // IronJaws = 1200 // アイアンジョー

            // BLM
            Thunder3 = 163, // サンダガ
            
        }
    }
}
