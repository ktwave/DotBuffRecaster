using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DotBuffRecaster {
    internal class Constants{
        public const string Name = "DotBuffRecaster";
        public static readonly string[] HotbarAddonName = {
            "_ActionBar",
            "_ActionBar01",
            "_ActionBar02",
            "_ActionBar03",
            "_ActionBar04",
            "_ActionBar05",
            "_ActionBar06",
            "_ActionBar07",
            "_ActionBar08",
            "_ActionBar09",
            "_ActionBarEx",
        };
        public static Vector4 White { get; set; } = new Vector4(1, 1, 1, 1);
        public static Vector4 Black { get; set; } = new Vector4(0, 0, 0, 1);
        public static Vector4 Red { get; set; } = new Vector4(1, 0, 0, 1);
        public static string HarfSpace { get; internal set; } = " ";
    }
}
