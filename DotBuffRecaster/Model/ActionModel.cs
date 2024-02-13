using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBuffRecaster.Model {
    public class ActionModel {
        public struct Action {
            public int JobId { get; set; }
            public int ActiionId { get; set; }
            public string ActiionName { get; set; }
            public int StatusId { get; set; }
            public bool IsBuff { get; set; }
            public float PosX { get; set; }
            public float PosY { get; set; }
        }
    }
}
