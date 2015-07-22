namespace BehaviorLibrary
{
    public class Repeater : BehaviorComponent
    {
        private readonly BehaviorComponent behavior;

        /// <summary>
        /// executes the behavior every time again
        /// </summary>
        /// <param name="behavior">behavior to run</param>
        public Repeater(BehaviorComponent behavior)
        {
            this.behavior = behavior;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            behavior.Behave();
            return Running();
        }
    }
}