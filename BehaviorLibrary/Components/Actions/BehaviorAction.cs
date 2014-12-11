namespace BehaviorLibrary.Components.Actions
{
    using System;

    public class BehaviorAction : BehaviorComponent
    {
        private readonly Func<BehaviorReturnCode> action;

        public BehaviorAction(Func<BehaviorReturnCode> action)
        {
            this.action = action;
        }

        public override BehaviorReturnCode Behave()
        {
            try
            {
                return action.Invoke();
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                return Failure();
            }
        }
    }
}