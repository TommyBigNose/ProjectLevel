using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using ProjectLevel.Services.v1.Implementations;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.TestHelpers
{
	public class MockItemChest
	{
		public static Mock<IItemChest> GetMockItemChest()
		{
			var mock = new Mock<IItemChest>();

			mock.Setup(_ => _.Inventory)
				.Returns(MockDataSource.GetTestingLoot());

			mock.Setup(_ => _.GetStatsForMilitaryType(MilitaryType.Melee))
				.Returns(GetMilitaryLootStat(MilitaryType.Melee).ToList());
			mock.Setup(_ => _.GetStatsForMilitaryType(MilitaryType.Ranged))
				.Returns(GetMilitaryLootStat(MilitaryType.Ranged).ToList());
			mock.Setup(_ => _.GetStatsForMilitaryType(MilitaryType.Siege))
				.Returns(GetMilitaryLootStat(MilitaryType.Siege).ToList());

			return mock;
		}

		public static IEnumerable<MilitaryLootStat> GetMilitaryLootStat(MilitaryType militaryType)
		{
			var militaryLootStats = new List<MilitaryLootStat>()
			{
				new MilitaryLootStat()
				{
					MilitaryType = militaryType,
					DamageRatio = 0.2f,
					SpeedRatio = 100.0f,
					RecruitRatio = 0.0f
				}
			};

			return militaryLootStats;
		}
	}
}
