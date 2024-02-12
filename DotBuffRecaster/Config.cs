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
        public bool IsEnabled { get; internal set; }
        public bool IsPreview { get; internal set; }
        public int OffsetX { get; internal set; }
        public bool IsLeftAlighn { get; internal set; }
        public int Size { get; internal set; }
        public int Padding { get; internal set; }
        public GameFontFamilyAndSize? Font = null;

        public void Save() {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
