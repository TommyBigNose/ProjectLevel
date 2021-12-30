using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Services.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface ICivilization
	{
		#region Economy
		//int GetGold();
		//int GetGoldIncomeRate();
		void UpgradeGoldLevel();
		#endregion

		#region Military
		int GetUnitCount(MilitaryType militaryType);
		int GetUnitDamage(MilitaryType militaryType);
		//ActionBar GetUnitActionBar(MilitaryType militaryType);
		void UpgradeUnitLevel(MilitaryType militaryType);
		#endregion
	}
}
