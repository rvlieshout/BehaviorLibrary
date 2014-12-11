namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;
    using Components;
    using Components.Composites;

    public enum BehaviorReturnCode
    {
        Failure,
        Success,
        Running
    }

    public delegate BehaviorReturnCode BehaviorReturn();

    /// <summary>
    /// 
    /// </summary>
    public class Behavior
    {
        private readonly BehaviorComponent root;

        public BehaviorReturnCode ReturnCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public Behavior(IndexSelector root)
        {
            this.root = root;
        }

        public Behavior(BehaviorComponent root)
        {
            this.root = root;
        }

        /// <summary>
        /// perform the behavior
        /// </summary>
        public BehaviorReturnCode Behave()
        {
            try
            {
                ReturnCode = root.Behave();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                ReturnCode = BehaviorReturnCode.Failure;
            }

            return ReturnCode;
        }
    }
}