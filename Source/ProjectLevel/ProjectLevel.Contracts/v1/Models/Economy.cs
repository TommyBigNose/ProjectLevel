using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;

namespace ProjectLevel.Contracts.v1.Models
{
	public class Economy
	{
		public int Gold { get; set; }
		public int GoldLevel { get; set; }
		public ActionBar GoldActionBar { get; set; }

		public Economy()
		{
			Gold = 0;
			GoldLevel = 1;
			GoldActionBar = new ActionBar();
		}

		public void RecieveGoldIncome(ItemChest itemChest)
		{
			Gold += GoldLevel + itemChest.Inventory.Sum(_ => _.GoldIncome);
			GoldActionBar.ResetActionBar();
		}

		public void TriggerAllActionBars(ItemChest itemChest)
        {
			float ratio = 1.0f + itemChest.Inventory.Sum(_ => _.GoldSpeedRatio);
			GoldActionBar.IncrementActionBar(GoldLevel * ratio);
		}

		public int RequiredGoldToLevelUp()
		{
			return GoldLevel * Constants.GoldPerLevelCost;
		}

		public bool CanUpgradeGoldLevel()
		{
			return (Gold >= RequiredGoldToLevelUp());
		}

		public void UpgradeGoldLevel()
		{
			SpendGold(RequiredGoldToLevelUp());
			GoldLevel++;
		}

		//public int GetGold()
		//{
		//	return _gold;
		//}

		//public int GetGoldIncomeRate()
		//{
		//	return _goldLevel;
		//}



		public void SpendGold(int goldSpent)
		{
			Gold -= goldSpent;
		}
	}
}
