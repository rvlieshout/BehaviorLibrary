using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Utility
{
    public class UtilitySelector : BehaviorComponent
    {
        private UtilityPair[] _utility_pairs;
        
        public UtilitySelector(params UtilityPair[] pairs)
        {
            this._utility_pairs = pairs;
        }
    
        public override BehaviorReturnCode Behave()
        {
            return BehaviorReturnCode.Success;
        }
    }
}
