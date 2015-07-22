namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

    public class Counter : BehaviorComponent
    {
        private readonly int maxCount;
        private int counter;

        private readonly BehaviorComponent behavior;

        /// <summary>
        /// executes the behavior based on a counter
        /// -each time Counter is called the counter increments by 1
        /// -Counter executes the behavior when it reaches the supplied maxCount
        /// </summary>
        /// <param name="maxCount">max number to count to</param>
        /// <param name="behavior">behavior to run</param>
        public Counter(int maxCount, BehaviorComponent behavior)
        {
            this.maxCount = maxCount;
            this.behavior = behavior;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            try
            {
                if (counter < maxCount)
                {
                    counter++;
                    return Running();
                }

                counter = 0;
                return ReturnCode = behavior.Behave();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Failure();
            }
        }
    }
}