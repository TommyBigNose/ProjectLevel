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
	public class BattleReadyEnemyTown : IBattleReady
	{
		public string? Name { get; private set; } = string.Empty;
		public string? ImageResourceString { get; private set; } = string.Empty;
		public int Level { get; private set; } = 0;
		public int HpCurrent { get; private set; } = 0;
		public int HpMax { get; private set; } = 0;
		public int GoldValue { get; private set; } = 0;
		public Loot Loot { get; private set; }
		public IMilitary Military { get; private set; }

		public BattleReadyEnemyTown(string name, int level, Loot loot, IMilitary military)
		{
			Name = name;
			Level = level;
			HpCurrent = level * BaseEnemyScaling;
			HpMax = level * BaseEnemyScaling;
			GoldValue = level * BaseEnemyScaling;
			Loot = loot;
			Military = military;
		}

		public int CalculateDamage(int attackDamage, MilitaryType militaryType)
		{
			int defense = Military.GetBaseUnitDamage(militaryType);
			int output = (attackDamage - defense > 0) ? (attackDamage - defense) : 0;
			return output;
		}

		public void ApplyDamage(int attackDamage)
		{
			if (HpCurrent - attackDamage <= 0)
			{
				HpCurrent = 0;
			}
			else
			{
				HpCurrent -= attackDamage;
			}
		}

		public bool IsTownDestroyed()
		{
			return HpCurrent <= 0;
		}
	}
}
