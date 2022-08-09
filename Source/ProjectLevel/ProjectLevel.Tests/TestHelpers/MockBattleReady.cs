using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.TestHelpers
{
	public static class MockBattleReady
	{
		public static Mock<IBattleReady> GetMockBattleReady(int enemyLevel = 1)
		{
			var mock = new Mock<IBattleReady>();

			Loot loot = GetLoot(enemyLevel);

			mock.Setup(_ => _.Name)
				.Returns(It.IsAny<string>());

			mock.Setup(_ => _.ImageResourceString)
				.Returns(It.IsAny<string>());

			mock.Setup(_ => _.Level)
				.Returns(enemyLevel);

			mock.Setup(_ => _.HpCurrent)
				.Returns(enemyLevel);
			
			mock.Setup(_ => _.HpMax)
				.Returns(enemyLevel);

			mock.Setup(_ => _.GoldValue)
				.Returns(enemyLevel * Constants.GoldPerLevelCost);

			mock.Setup(_ => _.Loot)
				.Returns(loot);

			mock.Setup(_ => _.CalculateDamage(It.IsAny<int>(), It.IsAny<MilitaryType>()))
				.Returns(It.IsAny<int>());

			mock.Setup(_ => _.ApplyDamage(It.IsAny<int>()));

			mock.Setup(_ => _.IsTownDestroyed())
				.Returns(It.IsAny<bool>());

			return mock;
		}

		public static Loot GetLoot(int level = 1)
		{
			Loot loot = new()
			{
				Name = "Enemy Reward",
				ImageResourceString = "Default.png",
				Level = level,
				GoldValue = level * Constants.GoldPerLevelCost,
				GoldIncome = 0,
				GoldSpeedRatio = 0.0f,
				IsShopItem = true,
				MilitaryLootStats = new List<MilitaryLootStat>
				{
					new MilitaryLootStat()
					{
						MilitaryType = MilitaryType.Melee,
						DamageRatio = 0.2f,
						SpeedRatio = 100.0f,
						RecruitRatio = 0.0f
					}
				}
			};

			return loot;
		}
	}
}
