using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.TestHelpers
{
	internal class TestDataSource : IDataSource
	{
		public List<Loot> GetAvailableLoot()
		{
			List<Loot> result = new()
			{
				new Loot()
				{
					Name = "Speed Up Gold Income",
					ImageResourceString = "Default.png",
					Level = 1,
					GoldValue = 5,

					GoldIncome = 0,
					GoldSpeedRatio = 100.0f,
					MilitaryLootStats = new List<MilitaryLootStat>()
				},
				new Loot()
				{
					Name = "Melee - Speed up",
					ImageResourceString = "Default.png",
					Level = 1,
					GoldValue = 5,

					GoldIncome = 0,
					GoldSpeedRatio = 0.0f,
					MilitaryLootStats = new List<MilitaryLootStat>
					{
						new MilitaryLootStat()
						{
							MilitaryType = MilitaryType.Melee,
							DamageRatio = 0.1f,
							SpeedRatio = 100.0f,
							RecruitRatio = 0.0f
						}
					}
				},
				new Loot()
				{
					Name = "Ranged - Speed up",
					ImageResourceString = "Default.png",
					Level = 1,
					GoldValue = 5,

					GoldIncome = 0,
					GoldSpeedRatio = 0.0f,
					MilitaryLootStats = new List<MilitaryLootStat>
					{
						new MilitaryLootStat()
						{
							MilitaryType = MilitaryType.Ranged,
							DamageRatio = 0.1f,
							SpeedRatio = 100.0f,
							RecruitRatio = 0.0f
						}
					}
				},
				new Loot()
				{
					Name = "Siege - Speed up",
					ImageResourceString = "Default.png",
					Level = 1,
					GoldValue = 5,

					GoldIncome = 0,
					GoldSpeedRatio = 0.0f,
					MilitaryLootStats = new List<MilitaryLootStat>
					{
						new MilitaryLootStat()
						{
							MilitaryType = MilitaryType.Siege,
							DamageRatio = 0.1f,
							SpeedRatio = 100.0f,
							RecruitRatio = 0.0f
						}
					}
				}
			};

			return result;
		}
	}
}
