using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Decorators
{
    public class Timer : BehaviorComponent
    {

        private Func<int> _ElapsedTimeFunction;

        private BehaviorComponent _Behavior;

        private int _TimeElapsed = 0;

        private int _WaitTime;

        /// <summary>
        /// executes the behavior after a given amount of time in miliseconds has passed
        /// </summary>
        /// <param name="elapsedTimeFunction">function that returns elapsed time</param>
        /// <param name="timeToWait">maximum time to wait before executing behavior</param>
        /// <param name="behavior">behavior to run</param>
        public Timer(Func<int> elapsedTimeFunction, int timeToWait, BehaviorComponent behavior)
        {
            _ElapsedTimeFunction = elapsedTimeFunction;
            _Behavior = behavior;
            _WaitTime = timeToWait;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            try
            {
                _TimeElapsed += _ElapsedTimeFunction.Invoke();

                if (_TimeElapsed >= _WaitTime)
                {
                    _TimeElapsed = 0;
                    ReturnCode = _Behavior.Behave();
                    return ReturnCode;
                }
                else
                {
                    ReturnCode = BehaviorReturnCode.Running;
                    return BehaviorReturnCode.Running;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                ReturnCode = BehaviorReturnCode.Failure;
                return BehaviorReturnCode.Failure;
            }
        }
    }
}
