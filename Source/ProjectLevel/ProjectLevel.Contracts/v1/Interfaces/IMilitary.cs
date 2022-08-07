using ProjectLevel.Contracts.v1.Models;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface IMilitary
	{
		void SetupMilitaryUnits(IEnumerable<MilitaryUnit> militaryUnits);
		int GetUnitCount(Constants.MilitaryType militaryType);
		int GetBaseUnitDamage(Constants.MilitaryType militaryType);
		int GetTotalUnitDamage(Constants.MilitaryType militaryType, IItemChest itemChest);
		int GetUnitLevel(Constants.MilitaryType militaryType);
		int RequiredGoldForNewUnit(Constants.MilitaryType militaryType);
		int RequiredGoldToLevelUp(Constants.MilitaryType militaryType);
		void UpgradeUnitCount(Constants.MilitaryType militaryType);
		void UpgradeUnitLevel(Constants.MilitaryType militaryType);
		ActionBar GetUnitActionBar(Constants.MilitaryType militaryType);
		void TriggerAllActionBars(IItemChest itemChest);
	}
}