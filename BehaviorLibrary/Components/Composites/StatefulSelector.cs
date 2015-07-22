namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

    public class StatefulSelector : BehaviorComponent
    {
        private readonly BehaviorComponent[] behaviors;

        private int lastBehavior;

        /// <summary>
        /// Selects among the given behavior components (stateful on running) 
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Running
        /// -Returns Failure if all behavior components returned Failure
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public StatefulSelector(params BehaviorComponent[] behaviors)
        {
            this.behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            for (; lastBehavior < behaviors.Length; lastBehavior++)
            {
                try
                {
                    switch (behaviors[lastBehavior].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            continue;
                        case BehaviorReturnCode.Success:
                            lastBehavior = 0;
                            return Success();
                        case BehaviorReturnCode.Running:
                            return Running();
                        default:
                            continue;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }

            lastBehavior = 0;
            return Failure();
        }
    }
}