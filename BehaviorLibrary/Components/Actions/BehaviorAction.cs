using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Actions
{
    public class BehaviorAction : BehaviorComponent
    {

        private Func<BehaviorReturnCode> _Action;

        public BehaviorAction() { }

        public BehaviorAction(Func<BehaviorReturnCode> action)
        {
            _Action = action;
        }

        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (_Action.Invoke())
                {
                    case BehaviorReturnCode.Success:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Failure:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    default:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                ReturnCode = BehaviorReturnCode.Failure;
                return ReturnCode;
            }
        }

    }
}
