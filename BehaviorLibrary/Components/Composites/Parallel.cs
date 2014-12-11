namespace BehaviorLibrary.Components.Composites
{
    using System;
    using System.Diagnostics;

    public class Parallel : BehaviorComponent
    {
        private readonly BehaviorComponent[] behaviors;

        /// <summary>
        /// attempts to run the behaviors all in one cycle
        /// -Returns Success when all are successful
        /// -Returns Failure if one behavior fails or an error occurs
        /// -Returns Running if any are running
        /// </summary>
        /// <param name="behaviors"></param>
        public Parallel(params BehaviorComponent[] behaviors)
        {
            this.behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            //add watch for any running behaviors
            var anyRunning = false;

            foreach (var bc in behaviors)
            {
                try
                {
                    switch (bc.Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            return Failure();
                        case BehaviorReturnCode.Success:
                            continue;
                        case BehaviorReturnCode.Running:
                            anyRunning = true;
                            continue;
                        default:
                            return Success();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    return Failure();
                }
            }

            return anyRunning ? Running() : Success();
        }
    }
}