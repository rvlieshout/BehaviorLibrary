namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

    public class PartialSelector : BehaviorComponent
    {
        private readonly BehaviorComponent[] behaviors;

        private short selections;

        private readonly short selLength;

        /// <summary>
        /// Selects among the given behavior components (one evaluation per Behave call)
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until
        /// Success is reached or Failure is certain.
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        /// <returns>
        /// Returns Success if a behavior component returns Success; 
        /// Returns Running if a behavior component returns Failure or Running;
        /// Returns Failure if all behavior components returned Failure or an error has occured
        /// </returns>
        public PartialSelector(params BehaviorComponent[] behaviors)
        {
            this.behaviors = behaviors;
            selLength = (short) this.behaviors.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            while (selections < selLength)
            {
                try
                {
                    switch (behaviors[selections].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            selections++;
                            return Running();
                        case BehaviorReturnCode.Success:
                            selections = 0;
                            return Success();
                        case BehaviorReturnCode.Running:
                            return Running();
                        default:
                            selections++;
                            return Failure();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    selections++;
                    return Failure();
                }
            }

            selections = 0;
            return Failure();
        }
    }
}