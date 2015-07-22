namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

    public class Inverter : BehaviorComponent
    {
        private readonly BehaviorComponent _Behavior;

        /// <summary>
        /// inverts the given behavior
        /// -Returns Success on Failure or Error
        /// -Returns Failure on Success 
        /// -Returns Running on Running
        /// </summary>
        /// <param name="behavior"></param>
        public Inverter(BehaviorComponent behavior)
        {
            _Behavior = behavior;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (_Behavior.Behave())
                {
                    case BehaviorReturnCode.Failure:
                        return Success();
                    case BehaviorReturnCode.Success:
                        return Failure();
                    case BehaviorReturnCode.Running:
                        return Running();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            return Success();
        }
    }
}