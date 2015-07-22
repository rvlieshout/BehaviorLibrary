namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

    public class IndexSelector : BehaviorComponent
    {
        private readonly BehaviorComponent[] behaviors;

        private readonly Func<int> index;

        /// <summary>
        /// The selector for the root node of the behavior tree
        /// </summary>
        /// <param name="index">an index representing which of the behavior branches to perform</param>
        /// <param name="behaviors">the behavior branches to be selected from</param>
        public IndexSelector(Func<int> index, params BehaviorComponent[] behaviors)
        {
            this.index = index;
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
                return behaviors[index.Invoke()].Behave();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Failure();
            }
        }
    }
}