using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLevel.Contracts.v1
{
	public class Constants
	{
		public const float ActionBarMax = 10.0f; // TODO: Small value for testing only, fix later
		//public const float ActionBarMax = 1000.0f;

		public enum MilitaryType
		{
			Melee,
			Ranged,
			Siege
		}

	}
}
