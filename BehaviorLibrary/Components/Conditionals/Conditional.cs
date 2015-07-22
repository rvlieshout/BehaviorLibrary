namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

    public class Conditional : BehaviorComponent
    {
        private readonly Func<bool> test;

        /// <summary>
        ///     Returns a return code equivalent to the test
        ///     -Returns Success if true
        ///     -Returns Failure if false
        /// </summary>
        /// <param name="test">the value to be tested</param>
        public Conditional(Func<bool> test)
        {
            this.test = test;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            try
            {
                return (test.Invoke()) ? Success() : Failure();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Failure();
            }
        }
    }
}