namespace BehaviorLibrary.Components.Conditionals
{
    using System;

    public class Conditional : BehaviorComponent
    {
        private readonly Func<Boolean> test;

        /// <summary>
        /// Returns a return code equivalent to the test 
        /// -Returns Success if true
        /// -Returns Failure if false
        /// </summary>
        /// <param name="test">the value to be tested</param>
        public Conditional(Func<Boolean> test)
        {
            this.test = test;
        }

        /// <summary>
        /// performs the given behavior
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
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                return Failure();
            }
        }
    }
}