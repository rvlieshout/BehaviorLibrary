using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Utility
{
    public class UtilitySelector : BehaviorComponent
    {
        private UtilityPair[] _utility_pairs;
		private Func<UtilityVector> _utility_function;
        
		public UtilitySelector(Func<UtilityVector> utility_function, params UtilityPair[] pairs)
        {
            this._utility_pairs = pairs;
			this._utility_function = utility_function;
        }
    
        public override BehaviorReturnCode Behave()
        {
			try{
				UtilityVector func_vector = this._utility_function.Invoke ();

				float min = -2.0f;
				UtilityPair best_match = null;

				//find max pair match
				foreach(UtilityPair pair in this._utility_pairs){ 
					float val = func_vector.dot(pair.vector);
					if(val > min){
						min = val;
						best_match = pair;
					}
				}

				//make sure we found a match
				if(best_match == null){
					#if DEBUG
					Console.WriteLine("best_match not defined...");
					#endif
					this.ReturnCode = BehaviorReturnCode.Failure;
					return this.ReturnCode;
				}

				//execute best pair match and return result
				this.ReturnCode = best_match.behavior.Behave();
				return this.ReturnCode;
			}catch(Exception e){
				#if DEBUG
				Console.WriteLine(e.ToString());
				#endif
				this.ReturnCode = BehaviorReturnCode.Failure;
				return BehaviorReturnCode.Failure;
			}
        }
    }
}
