using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLevel.Contracts.v1
{
	public class Constants
	{
		public const float ActionBarMax = 1000.0f; 
		public const int GoldPerLevelCost = 10;
		public const int BaseEnemyScaling = 5;

		public enum MilitaryType
		{
			Melee,
			Ranged,
			Siege
		}

	}
}
