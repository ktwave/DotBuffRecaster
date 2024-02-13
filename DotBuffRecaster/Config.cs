using Dalamud.Configuration;
using Dalamud.Interface.GameFonts;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBuffRecaster {
    [Serializable]
    class Config : IPluginConfiguration {
        
        [NonSerialized]
        private DalamudPluginInterface? PluginInterface;

        public int Version { get; set; } = 0;
        public bool IsEnabled { get; internal set; } = false;
        public bool IsPreview { get; internal set; } = false;
        public float OffsetX { get; internal set; } = 0f;
        public float OffsetY { get; internal set; } = 0f;
        public float Scale { get; internal set; } = 100f;
        public GameFontFamilyAndSize? Font = null;

        public void Save() {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
