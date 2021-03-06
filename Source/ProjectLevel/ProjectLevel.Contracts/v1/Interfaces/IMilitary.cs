using ProjectLevel.Contracts.v1.Models;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface IMilitary
	{
		int GetUnitCount(Constants.MilitaryType militaryType);
		int GetUnitDamage(Constants.MilitaryType militaryType);
		int GetUnitLevel(Constants.MilitaryType militaryType);
		int RequiredGoldForNewUnit(Constants.MilitaryType militaryType);
		int RequiredGoldToLevelUp(Constants.MilitaryType militaryType);
		void UpgradeUnitCount(Constants.MilitaryType militaryType);
		void UpgradeUnitLevel(Constants.MilitaryType militaryType);
		ActionBar GetUnitActionBar(Constants.MilitaryType militaryType);
		void TriggerAllActionBars(IItemChest itemChest);
	}
}