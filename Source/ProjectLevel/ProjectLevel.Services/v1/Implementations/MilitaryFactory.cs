using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Services.v1.Implementations
{
	public class MilitaryFactory : IMilitaryFactory
	{
		public IMilitary BuildInitialMilitary()
		{
			var militaryUnits = new List<MilitaryUnit>()
			{
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Melee,
					Count = 1,
					Level = 1
				},
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Ranged,
					Count = 1,
					Level = 1
				},
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Siege,
					Count = 1,
					Level = 1
				},
			};

			return new Military(militaryUnits);
		}

		public IMilitary BuildMilitary(int level)
		{
			IMilitary military = BuildInitialMilitary();

			for (int i = 1; i < level; i++)
			{
				military.UpgradeUnitCount(MilitaryType.Melee);
				military.UpgradeUnitCount(MilitaryType.Ranged);
				military.UpgradeUnitCount(MilitaryType.Siege);

				military.UpgradeUnitLevel(MilitaryType.Melee);
				military.UpgradeUnitLevel(MilitaryType.Ranged);
				military.UpgradeUnitLevel(MilitaryType.Siege);
			}

			return military;
		}
	}
}
