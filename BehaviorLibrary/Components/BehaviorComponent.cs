namespace BehaviorLibrary.Components
{
    public abstract class BehaviorComponent
    {
        protected BehaviorReturnCode ReturnCode;

        public abstract BehaviorReturnCode Behave();

        protected BehaviorReturnCode Failure() { return ReturnCode = BehaviorReturnCode.Failure; }
        protected BehaviorReturnCode Running() { return ReturnCode = BehaviorReturnCode.Running; }
        protected BehaviorReturnCode Success() { return ReturnCode = BehaviorReturnCode.Success; }
    }
}