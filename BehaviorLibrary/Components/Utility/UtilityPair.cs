using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Utility
{
    public class UtilityPair
    {
        public UtilityPair(UtilityVector vector, BehaviorComponent behavior)
        {
			this.vector = vector;
			this.behavior = behavior;
        }

        public UtilityVector vector { get; set; }
        public BehaviorComponent behavior { get; set; }
    }
}
