using ProjectLevel.Contracts.v1.Models;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface IEconomy
	{
		int Gold { get; }
		ActionBar GoldActionBar { get; }
		int GoldLevel { get;  }

		bool CanUpgradeGoldLevel();
		void RecieveGoldIncome(ItemChest itemChest);
		int RequiredGoldToLevelUp();
		void AddGold(int gold);
		void SpendGold(int gold);
		void UpgradeGoldLevel();
		void TriggerAllActionBars(ItemChest itemChest);
	}
}