using DotBuffRecaster.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBuffRecaster.Model {
    public class ActionModel {
        public struct StrAction {
            public int JobId { get; set; }
            public int Action { get; set; }
            public int StatusId { get; set; }
            public ActionType ActionType { get; set; }
        }

        public enum ActionType {
            BUFF = 0,
            DEBUFF = 1
        }
    }
}
