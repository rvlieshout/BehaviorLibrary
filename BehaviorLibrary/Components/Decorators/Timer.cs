namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

    public class Timer : BehaviorComponent
    {
        private readonly Func<int> elapsedTimeFunction;

        private readonly BehaviorComponent behavior;

        private int timeElapsed;

        private readonly int waitTime;

        /// <summary>
        /// executes the behavior after a given amount of time in miliseconds has passed
        /// </summary>
        /// <param name="elapsedTimeFunction">function that returns elapsed time</param>
        /// <param name="timeToWait">minimum time to wait before executing behavior</param>
        /// <param name="behavior">behavior to run</param>
        public Timer(Func<int> elapsedTimeFunction, int timeToWait, BehaviorComponent behavior)
        {
            this.elapsedTimeFunction = elapsedTimeFunction;
            this.behavior = behavior;
            waitTime = timeToWait;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            try
            {
                timeElapsed += elapsedTimeFunction.Invoke();

                if (timeElapsed < waitTime) return Running();

                timeElapsed = 0;
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