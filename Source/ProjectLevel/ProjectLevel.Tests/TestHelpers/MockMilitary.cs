using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using ProjectLevel.Services.v1.Implementations;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.TestHelpers
{
	public static class MockMilitary
	{
		public static Mock<IMilitary> GetMockMilitary(int initialCount, int initialLevel)
		{
			var mock = new Mock<IMilitary>();
			
			ActionBar actionBar = new();

			mock.Setup(_ => _.SetupMilitaryUnits(It.IsAny<IEnumerable<MilitaryUnit>>()));

			mock.Setup(_ => _.GetUnitCount(It.IsAny<MilitaryType>()))
				.Returns(initialCount);

			mock.Setup(_ => _.GetBaseUnitDamage(It.IsAny<MilitaryType>()))
				.Returns(initialCount * initialLevel);

			mock.Setup(_ => _.GetTotalUnitDamage(It.IsAny<MilitaryType>(), MockItemChest.GetMockItemChest().Object))
				.Returns(It.IsAny<int>());

			mock.Setup(_ => _.GetUnitLevel(It.IsAny<MilitaryType>()))
				.Returns(initialLevel);

			mock.Setup(_ => _.RequiredGoldForNewUnit(It.IsAny<MilitaryType>()))
				.Returns(initialCount * Constants.GoldPerLevelCost);

			mock.Setup(_ => _.RequiredGoldToLevelUp(It.IsAny<MilitaryType>()))
				.Returns(initialLevel * Constants.GoldPerLevelCost);

			mock.Setup(_ => _.UpgradeUnitCount(It.IsAny<MilitaryType>()));

			mock.Setup(_ => _.UpgradeUnitLevel(It.IsAny<MilitaryType>()));

			mock.Setup(_ => _.GetUnitActionBar(It.IsAny<MilitaryType>()))
				.Returns(actionBar);

			mock.Setup(_ => _.TriggerAllActionBars(It.IsAny<IItemChest>()));

			return mock;
		}
	}
}
