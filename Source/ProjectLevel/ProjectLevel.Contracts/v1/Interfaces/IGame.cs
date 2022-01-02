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
		int GetMilitaryUnitCount(MilitaryType militaryType);
		int GetMilitaryLevel(MilitaryType militaryType);
		int GetMilitaryDamage(MilitaryType militaryType);
		void TriggerAllActionBars();
		float GetGoldActionBarValue();
		float GetMilitaryActionBarValue(MilitaryType militaryType);
	}
}
