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
		#region Data
		List<Loot> GetAvailableLoot();
		#endregion

		#region Civilization
		void AddLoot(List<Loot> loot);
		List<Loot> GetLoot();
		void RemoveAllLoot();
		#endregion

		#region Gold
		int GetGold();
		int GetGoldIncomeRate();
		int RequiredGoldToLevelUp();
		bool CanUpgradeGoldLevel();
		void UpgradeGoldLevel();
		float GetGoldActionBarValue();
		#endregion

		#region Shop
		List<Loot> GetShopLoot();
		bool CanPurchaseLoot(Loot loot);
		void PurchaseLoot(Loot loot);
		#endregion

		#region Military
		int GetMilitaryUnitCount(MilitaryType militaryType);
		int GetMilitaryLevel(MilitaryType militaryType);
		int GetMilitaryDamage(MilitaryType militaryType);
		bool CanUpgradeMilitaryLevel(MilitaryType militaryType);
		void UpgradeMilitaryLevel(MilitaryType militaryType);
		bool CanUpgradeMilitaryUnitCount(MilitaryType militaryType);
		void UpgradeMilitaryUnitCount(MilitaryType militaryType);
		float GetMilitaryActionBarValue(MilitaryType militaryType);
		#endregion

		#region Enemy
		EnemyTown GetCurrentEnemyTown();
		EnemyTown GetNewEnemyTown(int level);
		void SetCurrentEnemyTown(EnemyTown enemyTown);
		int CalculateDamageToEnemyTown(int attackDamage, MilitaryType militaryType);
		#endregion

		void TriggerAllActionBars();
		
		
	}
}
