namespace BehaviorLibrary
{
    using System;
    using System.Diagnostics;

    public class RandomDecorator : BehaviorComponent
    {
        private readonly float probability;

        private readonly Func<float> randomFunction;

        private readonly BehaviorComponent behavior;

        /// <summary>
        /// randomly executes the behavior
        /// </summary>
        /// <param name="probability">probability of execution</param>
        /// <param name="randomFunction">function that determines probability to execute</param>
        /// <param name="behavior">behavior to execute</param>
        public RandomDecorator(float probability, Func<float> randomFunction, BehaviorComponent behavior)
        {
            this.probability = probability;
            this.randomFunction = randomFunction;
            this.behavior = behavior;
        }


        public override BehaviorReturnCode Behave()
        {
            try
            {
                return randomFunction.Invoke() <= probability ? (ReturnCode = behavior.Behave()) : Running();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Failure();
            }
        }
    }
}