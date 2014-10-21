using System;
using BehaviorLibrary;
using BehaviorLibrary.Components;
using BehaviorLibrary.Components.Composites;
using BehaviorLibrary.Components.Actions;
using BehaviorLibrary.Components.Conditionals;
using BehaviorLibrary.Components.Decorators;
using BehaviorLibrary.Components.Utility;

namespace BehaviorLibrary.Components.Decorators
{
    public class Succeeder : BehaviorComponent
    {
        private BehaviorComponent _Behavior;
        
        /// <summary>
        /// returns a success even when the decorated component failed
        /// </summary>
        /// <param name="behavior">behavior to run</param>
        public Succeeder(BehaviorComponent behavior)
        {
            _Behavior = behavior;
        }
        
        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            ReturnCode = _Behavior.Behave();
            if (ReturnCode == BehaviorReturnCode.Failure) {
                ReturnCode = BehaviorReturnCode.Success;
            }
            return ReturnCode;
        }
    }
}
