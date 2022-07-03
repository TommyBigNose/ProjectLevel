using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Services.v1.Implementations
{
	public class Game : IGame
	{
		private readonly IDataSource _dataSource;
		private readonly Civilization _civilization;
		private EnemyTown _enemyTown;

		public Game(IDataSource dataSource)
		{
			_dataSource = dataSource;

			_civilization = new Civilization();
			_enemyTown = GetNewEnemyTown(1);
		}

		#region Data
		public List<Loot> GetAvailableLoot()
		{
			return _dataSource.GetAvailableLoot();
		}

		public List<Loot> GetLoot()
		{
			return _civilization.ItemChest.Inventory;
		}
		#endregion

		#region Civilization
		public void AddLoot(List<Loot> loot)
		{
			_civilization.ItemChest.Inventory.AddRange(loot);
		}

		public void RemoveAllLoot()
		{
			_civilization.ItemChest.Inventory.Clear();
		}
		#endregion

		#region Gold
		public int GetGold()
		{
			return _civilization.Economy.Gold;
		}

		public int GetGoldIncomeRate()
		{
			return _civilization.Economy.GoldLevel;
		}

		public int RequiredGoldToLevelUp()
		{
			return _civilization.Economy.RequiredGoldToLevelUp();
		}

		public bool CanUpgradeGoldLevel()
		{
			return _civilization.Economy.CanUpgradeGoldLevel();
		}

		public void UpgradeGoldLevel()
		{
			_civilization.Economy.UpgradeGoldLevel();
		}

		public float GetGoldActionBarValue()
		{
			return _civilization.Economy.GoldActionBar.Value;
		}
		#endregion

		#region Shop
		public List<Loot> GetShopLoot()
		{
			return _dataSource.GetAvailableLoot().FindAll(_=>_.IsShopItem);
		}

		public bool CanPurchaseLoot(Loot loot)
		{
			return _civilization.Economy.Gold >= loot.GoldValue;
		}

		public void PurchaseLoot(Loot loot)
		{
			_civilization.Economy.Gold -= loot.GoldValue;
			_civilization.ItemChest.Inventory.Add(loot);
		}
		#endregion

		#region Military
		public int GetMilitaryUnitCount(MilitaryType militaryType)
		{
			return _civilization.Military.GetUnitCount(militaryType);
		}

		public int GetMilitaryLevel(MilitaryType militaryType)
		{
			return _civilization.Military.GetUnitLevel(militaryType);
		}

		public int GetMilitaryDamage(MilitaryType militaryType)
		{
			float ratio = 1.0f + _civilization.ItemChest.GetStatsForMilitaryType(militaryType).Sum(_ => _.DamageRatio);
			double result = Math.Round(_civilization.Military.GetUnitDamage(militaryType) * ratio);
			return (int)result;
		}

		public float GetMilitaryActionBarValue(MilitaryType militaryType)
		{
			return _civilization.Military.GetUnitActionBar(militaryType).Value;
		}

		public bool CanUpgradeMilitaryLevel(MilitaryType militaryType)
		{
			return _civilization.Economy.Gold >= _civilization.Military.RequiredGoldToLevelUp(militaryType);
		}

		public void UpgradeMilitaryLevel(MilitaryType militaryType)
		{
			_civilization.Economy.SpendGold(_civilization.Military.RequiredGoldToLevelUp(militaryType));
			_civilization.Military.UpgradeUnitLevel(militaryType);
		}

		public bool CanUpgradeMilitaryUnitCount(MilitaryType militaryType)
		{
			return _civilization.Economy.Gold >= _civilization.Military.RequiredGoldForNewUnit(militaryType);
		}

		public void UpgradeMilitaryUnitCount(MilitaryType militaryType)
		{
			_civilization.Economy.SpendGold(_civilization.Military.RequiredGoldForNewUnit(militaryType));
			_civilization.Military.UpgradeUnitCount(militaryType);
		}
		#endregion

		#region Enemy
		public EnemyTown GetCurrentEnemyTown()
		{
			return _enemyTown;
		}

		public EnemyTown GetNewEnemyTown(int level)
		{
			// TODO: Some kind of factory or something?
			EnemyTown enemy = new("Test Town", level, GetAvailableLoot().First());
			return enemy;
		}

		public void SetCurrentEnemyTown(EnemyTown enemyTown)
		{
			_enemyTown = enemyTown;
		}

		public int CalculateDamageToEnemyTown(int attackDamage, MilitaryType militaryType)
		{
			int defense = _enemyTown.Military.GetUnitDamage(militaryType);
			int output = (attackDamage - defense > 0)?(attackDamage - defense):0;
			return output;
		}

		public void ApplyDamageToEnemyTown(int attackDamage)
		{
			_enemyTown.ApplyDamage(attackDamage);
		}
		
		public bool IsEnemyTownDestroyed()
		{
			return _enemyTown.IsTownDestroyed();
		}
		#endregion

		#region Battle
		public void RewardPlayerItemFromEnemyTown()
		{
			_civilization.Economy.Gold += _enemyTown.GoldValue;
			AddLoot(new List<Loot> { _enemyTown.Loot });
		}
		#endregion

		public void TriggerAllActionBars()
		{
			_civilization.TriggerAllActionBars();
			if(_civilization.Economy.GoldActionBar.IsReady())
			{
				_civilization.Economy.RecieveGoldIncome(_civilization.ItemChest);
			}

			foreach(MilitaryType militaryType in Enum.GetValues(typeof(MilitaryType)))
			{
				var actionBar = _civilization.Military.GetUnitActionBar(militaryType);
				if (actionBar.IsReady())
				{
					actionBar.ResetActionBar();
				}
			}
		}
		
	}
}
