namespace BehaviorLibrary.Components.Composites
{
    using System;
    using System.Diagnostics;

    public class Sequence : BehaviorComponent
    {
        private readonly BehaviorComponent[] behaviors;

        private int sequence;

        /// <summary>
        /// Performs the given behavior components sequentially (one evaluation per Behave call)
        /// Performs an AND-Like behavior and will perform each successive component
        /// -Returns Success if all behavior components return Success
        /// -Returns Running if an individual behavior component returns Success or Running
        /// -Returns Failure if a behavior components returns Failure or an error is encountered
        /// </summary>
        /// <param name="behaviors">one or more behavior components</param>
        public Sequence(params BehaviorComponent[] behaviors)
        {
            this.behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            while (sequence < behaviors.Length)
            {
                try
                {
                    switch (behaviors[sequence].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            sequence = 0;
                            return Failure();
                        case BehaviorReturnCode.Success:
                            sequence++;
                            return Running();
                        case BehaviorReturnCode.Running:
                            return Running();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    sequence = 0;
                    return Failure();
                }
            }

            sequence = 0;
            return Success();
        }
    }
}