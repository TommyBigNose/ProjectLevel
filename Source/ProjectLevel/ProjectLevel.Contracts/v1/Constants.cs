using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLevel.Contracts.v1
{
	public class Constants
	{
		// TODO: ActionBarMax =  Small value for testing only, fix later to 1000.0f
		public const float ActionBarMax = 10.0f; 
		public const int GoldPerLevelCost = 10;

		public enum MilitaryType
		{
			Melee,
			Ranged,
			Siege
		}

	}
}
