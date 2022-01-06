using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Models
{
	public class Civilization //: ICivilization
	{
		public Economy Economy { get; set; } = new Economy();
		public Military Military { get; set; } = new Military();
		public List<Loot> Inventory { get; set; } = new List<Loot>();
		public Civilization()
		{
			// TODO: Kill this later, was just for testing
			Inventory = new List<Loot>
			{
				new Loot()
				{
					Name = "Crap sword",
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
							SpeedRatio = 0.1f,
							RecruitRatio = 0.0f
						}
                    }
                },
				new Loot()
				{
					Name = "Crap sword + 1",
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
							SpeedRatio = 0.1f,
							RecruitRatio = 0.0f
						}
					}
				},
				new Loot()
				{
					Name = "Crap bow",
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
							SpeedRatio = 0.1f,
							RecruitRatio = 0.0f
						}
					}
				}
			};
		}

		public void TriggerAllActionBars()
		{
			Economy.TriggerAllActionBars(Inventory);
			Military.TriggerAllActionBars(Inventory);
		}

		#region Economy

		#endregion

		#region Military

		#endregion

		#region Inventory
		
		#endregion
	}
}
