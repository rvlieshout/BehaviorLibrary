namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

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
                Debug.WriteLine(e.ToString());
                return Failure();
            }
        }
    }
}