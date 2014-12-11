namespace BehaviorLibrary.Components.Decorators
{
    public class RepeatUntilFail : BehaviorComponent
    {
        private readonly BehaviorComponent behavior;

        /// <summary>
        /// executes the behavior every time again until it fails.
        /// </summary>
        /// <param name="behavior">behavior to run</param>
        public RepeatUntilFail(BehaviorComponent behavior)
        {
            this.behavior = behavior;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            ReturnCode = behavior.Behave();
            return ReturnCode == BehaviorReturnCode.Failure ? Failure() : Running();
        }
    }
}