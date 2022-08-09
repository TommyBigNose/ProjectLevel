using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using ProjectLevel.Services.v1.Implementations;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.TestHelpers
{
	public static class MockBattleReadyFactory
	{
		public static Mock<IBattleReadyFactory> GetMockBattleReadyFactory(int level)
		{
			var mock = new Mock<IBattleReadyFactory>();

			mock.Setup(_ => _.BuildBattleReady(level))
				.Returns(GetBattleReady(level));

			return mock;
		}

		public static IBattleReady GetBattleReady(int level)
		{
			Loot loot = MockBattleReady.GetLoot(level);
			IBattleReady battleReady = new BattleReadyEnemyTown($"EnemyLv{level}", level, loot, MockMilitaryFactory.GetMilitary(level));

			return battleReady;
		}
	}
}
