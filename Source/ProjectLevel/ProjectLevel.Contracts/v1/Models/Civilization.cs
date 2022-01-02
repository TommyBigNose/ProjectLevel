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
		//private int _gold;
		//private int _goldLevel;
		public Economy Economy { get; set; } = new Economy();
		public Military Military { get; set; } = new Military();

		//private int _meleeUnitCount;
		//private int _rangedUnitCount;
		//private int _siegeUnitCount;

		//private int _meleeLevel;
		//private int _rangedLevel;
		//private int _siegeLevel;

		//public ActionBar MeleeActionBar { get; set; }
		//public ActionBar RangedActionBar { get; set; }
		//public ActionBar SiegeActionBar { get; set; }

		public Civilization()
		{
			//MeleeActionBar = new ActionBar();
			//RangedActionBar = new ActionBar();
			//SiegeActionBar = new ActionBar();
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
			Economy.UpgradeGoldLevel();
		}
		#endregion

		#region Military
		public void UpgradeUnitLevel(MilitaryType militaryType)
		{
			Military.UpgradeUnitLevel(militaryType);
		}

		public void TriggerAllActionBars()
		{
			Economy.GoldActionBar.IncrementActionBar(Economy.GoldLevel);
			Military.MeleeActionBar.IncrementActionBar(Military.GetUnitCount(MilitaryType.Melee));
			Military.RangedActionBar.IncrementActionBar(Military.GetUnitCount(MilitaryType.Ranged));
			Military.SiegeActionBar.IncrementActionBar(Military.GetUnitCount(MilitaryType.Siege));
		}
		#endregion
	}
}
