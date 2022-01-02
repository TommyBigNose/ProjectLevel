using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface IGame
	{
		//Civilization GetCivilization();
		int GetGold();
		int GetGoldIncomeRate();
		int RequiredGoldToLevelUp();
		bool CanUpgradeGoldLevel();
		void UpgradeGoldLevel();
		float GetGoldActionBarValue();

		int GetMilitaryUnitCount(MilitaryType militaryType);
		int GetMilitaryLevel(MilitaryType militaryType);
		int GetMilitaryDamage(MilitaryType militaryType);
		bool CanUpgradeMilitaryLevel(MilitaryType militaryType);
		void UpgradeMilitaryLevel(MilitaryType militaryType);
		float GetMilitaryActionBarValue(MilitaryType militaryType);

		void TriggerAllActionBars();
		
		
	}
}
