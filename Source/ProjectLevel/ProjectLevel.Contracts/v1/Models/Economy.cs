﻿using System;
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

		public void RecieveGoldIncome()
		{
			Gold += GoldLevel;
			GoldActionBar.ResetActionBar();
		}

		public void TriggerAllActionBars(List<Loot> inventory)
        {
			float ratio = 1.0f + inventory.Sum(_ => _.GoldSpeedRatio);
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
			Gold -= RequiredGoldToLevelUp();
			GoldLevel++;
		}
	}
}
