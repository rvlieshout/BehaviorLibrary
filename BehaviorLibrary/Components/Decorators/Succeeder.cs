namespace BehaviorLibrary.Components.Decorators
{
    public class Succeeder : BehaviorComponent
    {
        private readonly BehaviorComponent behavior;

        /// <summary>
        /// returns a success even when the decorated component failed
        /// </summary>
        /// <param name="behavior">behavior to run</param>
        public Succeeder(BehaviorComponent behavior)
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
            if (ReturnCode == BehaviorReturnCode.Failure)
            {
                ReturnCode = BehaviorReturnCode.Success;
            }
            return ReturnCode;
        }
    }
}