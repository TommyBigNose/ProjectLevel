using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using ProjectLevel.Services.v1.Implementations;

namespace ProjectLevel.Tests.TestHelpers
{
	public class MockEconomy
	{
		public static Mock<IEconomy> GetMockEconomy(bool canUpgradeGold)
		{
			var mock = new Mock<IEconomy>();

			mock.Setup(_ => _.Gold)
				.Returns(It.IsAny<int>());

			mock.Setup(_ => _.GoldActionBar)
				.Returns(It.IsAny<ActionBar>());

			mock.Setup(_ => _.GoldLevel)
				.Returns(It.IsAny<int>());

			mock.Setup(_ => _.CanUpgradeGoldLevel())
				.Returns(canUpgradeGold);

			mock.Setup(_ => _.RecieveGoldIncome(It.IsAny<IItemChest>()));

			mock.Setup(_ => _.RequiredGoldToLevelUp())
				.Returns(It.IsAny<int>());

			mock.Setup(_ => _.AddGold(It.IsAny<int>()));

			mock.Setup(_ => _.SpendGold(It.IsAny<int>()));

			mock.Setup(_ => _.UpgradeGoldLevel());

			mock.Setup(_ => _.TriggerAllActionBars(It.IsAny<IItemChest>()));

			return mock;
		}
	}
}
