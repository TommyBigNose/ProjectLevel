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

namespace ProjectLevel.Tests.TestHelpers
{
	public class MockEconomy
	{
		public static Mock<IEconomy> GetMockEconomy(int initialGold = 10000, bool canUpgradeGold = false)
		{
			var mock = new Mock<IEconomy>();

			ActionBar actionBar = new();
			IItemChest itemChest = MockItemChest.GetMockItemChest().Object;

			mock.Setup(_ => _.Gold)
				.Returns(initialGold);

			mock.Setup(_ => _.GoldActionBar)
				.Returns(actionBar);

			mock.SetupSequence(_ => _.GoldLevel)
				.Returns(1)
				.Returns(2);

			mock.Setup(_ => _.CanUpgradeGoldLevel())
				.Returns(canUpgradeGold);

			mock.Setup(_ => _.RecieveGoldIncome(itemChest));

			mock.SetupSequence(_ => _.RequiredGoldToLevelUp())
				.Returns(1 * Constants.GoldPerLevelCost)
				.Returns(2 * Constants.GoldPerLevelCost);

			mock.Setup(_ => _.AddGold(It.IsAny<int>()));

			mock.Setup(_ => _.SpendGold(It.IsAny<int>()));

			mock.Setup(_ => _.UpgradeGoldLevel());

			mock.Setup(_ => _.TriggerAllActionBars(itemChest));

			return mock;
		}
	}
}
