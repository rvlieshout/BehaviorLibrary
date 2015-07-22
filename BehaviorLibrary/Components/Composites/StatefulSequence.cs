namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

    public class StatefulSequence : BehaviorComponent
    {
        private readonly BehaviorComponent[] behaviors;

        private int lastBehavior;

        /// <summary>
        /// attempts to run the behaviors all in one cycle (stateful on running)
        /// -Returns Success when all are successful
        /// -Returns Failure if one behavior fails or an error occurs
        /// -Does not Return Running
        /// </summary>
        /// <param name="behaviors"></param>
        public StatefulSequence(params BehaviorComponent[] behaviors)
        {
            this.behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            //start from last remembered position
            for (; lastBehavior < behaviors.Length; lastBehavior++)
            {
                try
                {
                    switch (behaviors[lastBehavior].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            lastBehavior = 0;
                            return Failure();
                        case BehaviorReturnCode.Success:
                            continue;
                        case BehaviorReturnCode.Running:
                            return Running();
                        default:
                            lastBehavior = 0;
                            return Success();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    lastBehavior = 0;
                    return Failure();
                }
            }

            lastBehavior = 0;
            return Success();
        }
    }
}