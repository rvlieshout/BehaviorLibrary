using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Utility
{
    public class UtilityVector
    {
        public UtilityVector(params float[] values){
			this.values = values;
		}

		public float[] values{ get; set; }

		//return the magnitude of this vector
		public float magnitude{ 
			get{ 
				float mag = 0;
				for (int i = 0; i < this.values.Length; i++)
					mag += this.values [i] * this.values [i];

				return (float) Math.Sqrt (mag);
			}
		}

		/// <summary>
		/// Return a new vector based on the normalization of this instance.
		/// </summary>
		public UtilityVector normalize(){
			if (this.values.Length <= 0)
				return null;

			UtilityVector vec = new UtilityVector();
			vec.values = new float[this.values.Length];
			this.values.CopyTo (vec.values, 0);

			float mag = vec.magnitude;

			for (int i = 0; i < vec.values.Length; i++)
				vec.values [i] = vec.values [i] / mag;

			return vec;
		}

		/// <summary>
		/// Dot between this and another specified vector. (based on normalized vectors)
		/// </summary>
		/// <param name="vector">Vector.</param>
		public float dot(UtilityVector vector){
			if (this.magnitude == 0 || vector.magnitude == 0)
				return -2;

			UtilityVector a = this.normalize ();
			UtilityVector b = vector.normalize ();

			float val = 0;

			for (int i = 0; i < this.values.Length; i++)
				val += a.values [i] * b.values [i];

			return val;
		}
	}
}