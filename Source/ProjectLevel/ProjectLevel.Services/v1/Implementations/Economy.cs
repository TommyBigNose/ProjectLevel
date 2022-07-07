using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;

namespace ProjectLevel.Services.v1.Implementations
{
	public class Economy : IEconomy
	{
		public int Gold { get; private set; }
		public int GoldLevel { get; private set; }
		public ActionBar GoldActionBar { get; private set; }

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

		public int RequiredGoldToLevelUp()
		{
			return GoldLevel * Constants.GoldPerLevelCost;
		}

		public bool CanUpgradeGoldLevel()
		{
			return Gold >= RequiredGoldToLevelUp();
		}

		public void UpgradeGoldLevel()
		{
			GoldLevel++;
		}

		public void AddGold(int gold)
		{
			Gold += gold;
		}

		public void SpendGold(int gold)
		{
			Gold -= gold;
		}

		public void TriggerAllActionBars(ItemChest itemChest)
		{
			float ratio = 1.0f + itemChest.Inventory.Sum(_ => _.GoldSpeedRatio);
			GoldActionBar.IncrementActionBar(GoldLevel * ratio);
		}
	}
}
