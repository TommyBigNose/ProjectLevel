using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Services.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Services.v1.Implementations
{
	public class Civilization : ICivilization
	{
		private int _gold;
		private int _goldLevel;
		public Economy Economy { get; set; }

		private int _meleeUnitCount;
		private int _rangedUnitCount;
		private int _siegeUnitCount;

		private int _meleeLevel;
		private int _rangedLevel;
		private int _siegeLevel;

		public ActionBar MeleeActionBar { get; set; }
		public ActionBar RangedActionBar { get; set; }
		public ActionBar SiegeActionBar { get; set; }

		public Civilization()
		{
			Economy = new Economy();
			MeleeActionBar = new ActionBar();
			RangedActionBar = new ActionBar();
			SiegeActionBar = new ActionBar();
		}

		#region Economy
		//public int GetGold()
		//{
		//	return _gold;
		//}

		//public int GetGoldIncomeRate()
		//{
		//	return _goldLevel;
		//}

		public void UpgradeGoldLevel()
		{
			Economy.GoldLevel++;
		}
		#endregion

		#region Military
		public int GetUnitCount(MilitaryType militaryType)
		{
			return militaryType switch
			{
				MilitaryType.Melee => _meleeUnitCount,
				MilitaryType.Ranged => _rangedUnitCount,
				MilitaryType.Siege => _siegeUnitCount,
				_ => 0,
			};
		}
		public int GetUnitDamage(MilitaryType militaryType)
		{
			return militaryType switch
			{
				MilitaryType.Melee => _meleeUnitCount * _meleeLevel,
				MilitaryType.Ranged => _rangedUnitCount * _rangedLevel,
				MilitaryType.Siege => _siegeUnitCount * _siegeLevel,
				_ => 0,
			};
		}

		//public ActionBar GetUnitActionBar(MilitaryType militaryType)
		//{
		//	return militaryType switch
		//	{
		//		MilitaryType.Melee => _meleeActionBar,
		//		MilitaryType.Ranged => _rangedActionBar,
		//		MilitaryType.Siege => _siegeActionBar,
		//		_ => new ActionBar(),
		//	};
		//}

		public void UpgradeUnitLevel(MilitaryType militaryType)
		{
			switch (militaryType)
			{
				case MilitaryType.Melee:
					_meleeLevel++;
					break;
				case MilitaryType.Ranged:
					_rangedLevel++;
					break;
				case MilitaryType.Siege:
					_siegeLevel++;
					break;
			}
		}
		#endregion
	}
}
