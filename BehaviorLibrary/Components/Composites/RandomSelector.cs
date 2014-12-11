namespace BehaviorLibrary.Components.Composites
{
    using System;
    using System.Diagnostics;

    public class RandomSelector : BehaviorComponent
    {
        private readonly BehaviorComponent[] behaviors;

        //use current milliseconds to set random seed
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Randomly selects and performs one of the passed behaviors
        /// -Returns Success if selected behavior returns Success
        /// -Returns Failure if selected behavior returns Failure
        /// -Returns Running if selected behavior returns Running
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public RandomSelector(params BehaviorComponent[] behaviors)
        {
            this.behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            try
            {
                return behaviors[Random.Next(0, behaviors.Length - 1)].Behave();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Failure();
            }
        }
    }
}