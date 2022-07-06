using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;

namespace ProjectLevel.Contracts.v1.Models
{
	public class EnemyTown
	{
        public string? Name { get; private set; } = string.Empty;
        public string? ImageResourceString { get; private set; } = string.Empty;
        public int Level { get; private set; } = 0;
        public int HpCurrent { get; private set; } = 0;
        public int HpMax { get; private set; } = 0;
        public int GoldValue { get; private set; } = 0;
        public Loot Loot { get; private set; }
        public IMilitary Military { get; private set; }

        public EnemyTown(string name, int level, Loot loot, IMilitary military)
		{
			Name = name;
			Level = level;
			HpCurrent = level * Constants.BaseEnemyScaling;
			HpMax = level * Constants.BaseEnemyScaling;
			GoldValue = level * Constants.BaseEnemyScaling;
			Loot = loot;
			Military = military;

			for (int i = 1; i < level; i++)
			{
				Military.UpgradeUnitCount(Constants.MilitaryType.Melee);
				Military.UpgradeUnitCount(Constants.MilitaryType.Ranged);
				Military.UpgradeUnitCount(Constants.MilitaryType.Siege);

				Military.UpgradeUnitLevel(Constants.MilitaryType.Melee);
				Military.UpgradeUnitLevel(Constants.MilitaryType.Ranged);
				Military.UpgradeUnitLevel(Constants.MilitaryType.Siege);
			}
		}

		public void ApplyDamage(int attackDamage)
		{
            if(HpCurrent - attackDamage <= 0)
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
