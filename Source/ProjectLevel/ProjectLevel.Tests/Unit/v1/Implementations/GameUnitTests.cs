using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using ProjectLevel.Services.v1.Implementations;
using ProjectLevel.Tests.TestHelpers;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.Unit.v1.Implementations
{
	[TestFixture]
	public class GameUnitTests
	{
		private IGame _sut;

		[SetUp]
		public void SetUp()
		{
			// TestDataSource has some special items to speed up loot timer
			IDataSource dataSource = new TestDataSource();
			_sut = new Game(dataSource);

			_sut.AddLoot(dataSource.GetAvailableLoot());
		}

		[TearDown]
		public void TearDown()
		{
			
		}

		#region Data
		[TestCase(0, 4)]
		[TestCase(1, 4)]
		[TestCase(2, 4)]
		public void Data_GetAvailableLoot(int addLootCount, int expected)
		{
			// Arrange
			IDataSource dataSource = new TestDataSource();
			_sut = new Game(dataSource);

			// Act
			for (int _ = 0; _ < addLootCount; _++)
			{
				_sut.AddLoot(_sut.GetAvailableLoot());
			}
			var result = _sut.GetAvailableLoot().Count;

			// Assert
			Assert.AreEqual(expected, result);
		}
		#endregion

		#region Civilization
		[TestCase(0, 0)]
		[TestCase(1, 4)]
		[TestCase(2, 8)]
		public void Civilization_AddLoot(int addLootCount, int expected)
		{
			// Arrange
			IDataSource dataSource = new TestDataSource();
			_sut = new Game(dataSource);

			// Act
			for (int _ = 0; _ < addLootCount; _++)
			{
				_sut.AddLoot(_sut.GetAvailableLoot());
			}
			var result = _sut.GetLoot().Count;

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 0)]
		[TestCase(1, 4)]
		[TestCase(2, 8)]
		public void Civilization_GetLoot(int addLootCount, int expected)
		{
			// Arrange
			IDataSource dataSource = new TestDataSource();
			_sut = new Game(dataSource);

			for (int _ = 0; _ < addLootCount; _++)
			{
				_sut.AddLoot(dataSource.GetAvailableLoot());
			}

			// Act

			var result = _sut.GetLoot().Count;

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0)]
		public void Civilization_RemoveAllLoot(int expected)
		{
			// Arrange
			// Act
			_sut.RemoveAllLoot();
			var result = _sut.GetLoot().Count;

			// Assert
			Assert.AreEqual(expected, result);
		}
		#endregion

		#region Gold
		[TestCase(0, 0)]
		[TestCase(10, 1)]
		[TestCase(20, 2)]
		public void Gold_GetGold(int gameTicks, int expected)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);

			// Act
			var result = _sut.GetGold();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 1, false)]
		[TestCase(10, 1, false)]
		[TestCase(20, 1, false)]
		[TestCase(100, 2, true)]
		public void Gold_GetGoldIncomeRate(int gameTicks, int expected, bool upgrade)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);
			if(upgrade) _sut.UpgradeGoldLevel();

			// Act
			var result = _sut.GetGoldIncomeRate();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 10, false)]
		[TestCase(10, 10, false)]
		[TestCase(20, 10, false)]
		[TestCase(100, 20, true)]
		public void Gold_RequiredGoldToLevelUp(int gameTicks, int expected, bool upgrade)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);
			if (upgrade) _sut.UpgradeGoldLevel();

			// Act
			var result = _sut.RequiredGoldToLevelUp();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, false, false)]
		[TestCase(10, false, false)]
		[TestCase(20, false, false)]
		[TestCase(100, true, false)]
		[TestCase(100, false, true)]
		public void Gold_CanUpgradeGoldLevel(int gameTicks, bool expected, bool upgrade)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);
			if (upgrade) _sut.UpgradeGoldLevel();

			// Act
			var result = _sut.CanUpgradeGoldLevel();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 1)]
		[TestCase(1, 2)]
		[TestCase(2, 3)]
		[TestCase(100, 101)]
		public void Gold_UpgradeGoldLevel(int upgradeCount, int expected)
		{
			// Arrange
			// Act
			for (int _ = 0; _ < upgradeCount; _++)
			{
				_sut.UpgradeGoldLevel();
			}
			var result = _sut.GetGoldIncomeRate();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 0.0f, false)]
		[TestCase(5, 505.0f, false)]
		[TestCase(10, 0.0f, false)]
		[TestCase(11, 101.0f, false)]
		[TestCase(20, 0.0f, false)]
		[TestCase(100, 0.0f, false)]
		public void Gold_GetGoldActionBarValue(int gameTicks, float expected, bool upgrade)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);
			if (upgrade) _sut.UpgradeGoldLevel();

			// Act
			var result = _sut.GetGoldActionBarValue();

			// Assert
			Assert.AreEqual(expected, result);
		}
		#endregion

		#region Shop
		[TestCase(1, 3)]
		[TestCase(2, 6)]
		public void Shop_GetShopLoot(int getLootCount, int expected)
		{
			// Arrange
			List<Loot> loot = new();

			// Act
			for (int i = 0; i < getLootCount; i++)
			{
				loot.AddRange(_sut.GetShopLoot());
			}
			
			int result = loot.Count;

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, false, false)]
		[TestCase(5, false, false)]
		[TestCase(10, false, false)]
		[TestCase(50, true, false)]
		[TestCase(55, false, true)]
		[TestCase(100, true, true)]
		public void Shop_CanPurchaseLoot(int gameTicks, bool expected, bool purchaseLoot)
		{
			// Arrange
			List<Loot> loot = _sut.GetShopLoot();
			TriggerGameActionBars(gameTicks);
			if(purchaseLoot)
			{
				_sut.PurchaseLoot(loot.First());
			}
			
			// Act
			bool result = _sut.CanPurchaseLoot(loot.First());

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 0, false)]
		[TestCase(5, 0, false)]
		[TestCase(10, 0, false)]
		[TestCase(50, 1, true)]
		[TestCase(55, 0, false)]
		[TestCase(100, 2, true)]
		public void Shop_PurchaseLoot(int gameTicks, int expected, bool purchaseAllLootPossible)
		{
			// Arrange
			List<Loot> loot = _sut.GetShopLoot();
			TriggerGameActionBars(gameTicks);
			int initialLootCount = _sut.GetLoot().Count;

			// Act
			while (purchaseAllLootPossible && _sut.CanPurchaseLoot(loot.First()))
			{
				_sut.PurchaseLoot(loot.First());
			}
			int result = _sut.GetLoot().Count;

			// Assert
			Assert.AreEqual(expected + initialLootCount, result);
		}
		#endregion

		#region Military
		[TestCase(0, 1, MilitaryType.Melee)]
		[TestCase(10, 1, MilitaryType.Melee)]
		[TestCase(20, 1, MilitaryType.Melee)]
		[TestCase(100, 1, MilitaryType.Melee)]
		[TestCase(0, 1, MilitaryType.Ranged)]
		[TestCase(10, 1, MilitaryType.Ranged)]
		[TestCase(20, 1, MilitaryType.Ranged)]
		[TestCase(100, 1, MilitaryType.Ranged)]
		[TestCase(0, 1, MilitaryType.Siege)]
		[TestCase(10, 1, MilitaryType.Siege)]
		[TestCase(20, 1, MilitaryType.Siege)]
		[TestCase(100, 1, MilitaryType.Siege)]
		public void Military_GetMilitaryUnitCount(int gameTicks, int expected, MilitaryType militaryType)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);

			// Act
			var result = _sut.GetMilitaryUnitCount(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 1, false, MilitaryType.Melee)]
		[TestCase(10, 1, false, MilitaryType.Melee)]
		[TestCase(20, 1, false, MilitaryType.Melee)]
		[TestCase(100, 2, true, MilitaryType.Melee)]
		[TestCase(0, 1, false, MilitaryType.Ranged)]
		[TestCase(10, 1, false, MilitaryType.Ranged)]
		[TestCase(20, 1, false, MilitaryType.Ranged)]
		[TestCase(100, 2, true, MilitaryType.Ranged)]
		[TestCase(0, 1, false, MilitaryType.Siege)]
		[TestCase(10, 1, false, MilitaryType.Siege)]
		[TestCase(20, 1, false, MilitaryType.Siege)]
		[TestCase(100, 2, true, MilitaryType.Siege)]
		public void Military_GetMilitaryLevel(int gameTicks, int expected, bool upgrade, MilitaryType militaryType)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);
			if (upgrade) _sut.UpgradeMilitaryLevel(militaryType);

			// Act
			var result = _sut.GetMilitaryLevel(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 1, false, false, false, MilitaryType.Melee)]
		[TestCase(10, 1, false, false, false, MilitaryType.Melee)]
		[TestCase(100, 2, true, false, false, MilitaryType.Melee)]
		[TestCase(100, 4, true, true, false, MilitaryType.Melee)]
		[TestCase(100, 5, true, true, true, MilitaryType.Melee)]
		[TestCase(0, 1, false, false, false, MilitaryType.Ranged)]
		[TestCase(10, 1, false, false, false, MilitaryType.Ranged)]
		[TestCase(20, 1, false, false, false, MilitaryType.Ranged)]
		[TestCase(100, 2, true, false, false, MilitaryType.Ranged)]
		[TestCase(100, 4, true, true, false, MilitaryType.Ranged)]
		[TestCase(100, 5, true, true, true, MilitaryType.Ranged)]
		[TestCase(0, 1, false, false, false, MilitaryType.Siege)]
		[TestCase(10, 1, false, false, false, MilitaryType.Siege)]
		[TestCase(20, 1, false, false, false, MilitaryType.Siege)]
		[TestCase(100, 2, true, false, false, MilitaryType.Siege)]
		[TestCase(100, 4, true, true, false, MilitaryType.Siege)]
		[TestCase(100, 5, true, true, true, MilitaryType.Siege)]
		public void Military_GetMilitaryDamage(int gameTicks, int expected, bool upgradeLevel, bool addUnit, bool hasLoot, MilitaryType militaryType)
		{
			// Arrange
			if (!hasLoot) _sut.RemoveAllLoot();
			
			TriggerGameActionBars(gameTicks);
			if (upgradeLevel) _sut.UpgradeMilitaryLevel(militaryType);
			if (addUnit) _sut.UpgradeMilitaryUnitCount(militaryType);

			// Act
			var result = _sut.GetMilitaryDamage(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, false, MilitaryType.Melee)]
		[TestCase(10, false, MilitaryType.Melee)]
		[TestCase(20, false, MilitaryType.Melee)]
		[TestCase(100, true, MilitaryType.Melee)]
		[TestCase(0, false, MilitaryType.Ranged)]
		[TestCase(10, false, MilitaryType.Ranged)]
		[TestCase(20, false, MilitaryType.Ranged)]
		[TestCase(100, true, MilitaryType.Ranged)]
		[TestCase(0, false, MilitaryType.Siege)]
		[TestCase(10, false, MilitaryType.Siege)]
		[TestCase(20, false, MilitaryType.Siege)]
		[TestCase(100, true, MilitaryType.Siege)]
		public void Military_CanUpgradeMilitaryLevel(int gameTicks, bool expected, MilitaryType militaryType)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);

			// Act
			var result = _sut.CanUpgradeMilitaryLevel(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, false, false, MilitaryType.Melee)]
		[TestCase(10, false, false, MilitaryType.Melee)]
		[TestCase(20, false, false, MilitaryType.Melee)]
		[TestCase(100, true, false, MilitaryType.Melee)]
		[TestCase(100, false, true, MilitaryType.Melee)]
		[TestCase(0, false, false, MilitaryType.Ranged)]
		[TestCase(10, false, false, MilitaryType.Ranged)]
		[TestCase(20, false, false, MilitaryType.Ranged)]
		[TestCase(100, true, false, MilitaryType.Ranged)]
		[TestCase(100, false, true, MilitaryType.Ranged)]
		[TestCase(0, false, false, MilitaryType.Siege)]
		[TestCase(10, false, false, MilitaryType.Siege)]
		[TestCase(20, false, false, MilitaryType.Siege)]
		[TestCase(100, true, false, MilitaryType.Siege)]
		[TestCase(100, false, true, MilitaryType.Siege)]
		public void Military_UpgradeMilitaryLevel(int gameTicks, bool expected, bool upgrade, MilitaryType militaryType)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);
			if (upgrade) _sut.UpgradeMilitaryLevel(militaryType);

			// Act
			var result = _sut.CanUpgradeMilitaryLevel(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, false, MilitaryType.Melee)]
		[TestCase(10, false, MilitaryType.Melee)]
		[TestCase(20, false, MilitaryType.Melee)]
		[TestCase(100, true, MilitaryType.Melee)]
		[TestCase(0, false, MilitaryType.Ranged)]
		[TestCase(10, false, MilitaryType.Ranged)]
		[TestCase(20, false, MilitaryType.Ranged)]
		[TestCase(100, true, MilitaryType.Ranged)]
		[TestCase(0, false, MilitaryType.Siege)]
		[TestCase(10, false, MilitaryType.Siege)]
		[TestCase(20, false, MilitaryType.Siege)]
		[TestCase(100, true, MilitaryType.Siege)]
		public void Military_CanUpgradeMilitaryUnitCount(int gameTicks, bool expected, MilitaryType militaryType)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);

			// Act
			var result = _sut.CanUpgradeMilitaryUnitCount(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, false, false, MilitaryType.Melee)]
		[TestCase(10, false, false, MilitaryType.Melee)]
		[TestCase(20, false, false, MilitaryType.Melee)]
		[TestCase(100, true, false, MilitaryType.Melee)]
		[TestCase(100, false, true, MilitaryType.Melee)]
		[TestCase(0, false, false, MilitaryType.Ranged)]
		[TestCase(10, false, false, MilitaryType.Ranged)]
		[TestCase(20, false, false, MilitaryType.Ranged)]
		[TestCase(100, true, false, MilitaryType.Ranged)]
		[TestCase(100, false, true, MilitaryType.Ranged)]
		[TestCase(0, false, false, MilitaryType.Siege)]
		[TestCase(10, false, false, MilitaryType.Siege)]
		[TestCase(20, false, false, MilitaryType.Siege)]
		[TestCase(100, true, false, MilitaryType.Siege)]
		[TestCase(100, false, true, MilitaryType.Siege)]
		public void Military_UpgradeMilitaryUnitCount(int gameTicks, bool expected, bool upgrade, MilitaryType militaryType)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);
			if (upgrade) _sut.UpgradeMilitaryUnitCount(militaryType);

			// Act
			var result = _sut.CanUpgradeMilitaryUnitCount(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 0.0f, false, MilitaryType.Melee)]
		[TestCase(5, 505.0f, false, MilitaryType.Melee)]
		[TestCase(10, 0.0f, false, MilitaryType.Melee)]
		[TestCase(11, 101.0f, false, MilitaryType.Melee)]
		[TestCase(20, 0.0f, false, MilitaryType.Melee)]
		[TestCase(100, 0.0f, false, MilitaryType.Melee)]
		[TestCase(0, 0.0f, false, MilitaryType.Ranged)]
		[TestCase(5, 505.0f, false, MilitaryType.Ranged)]
		[TestCase(10, 0.0f, false, MilitaryType.Ranged)]
		[TestCase(11, 101.0f, false, MilitaryType.Ranged)]
		[TestCase(20, 0.0f, false, MilitaryType.Ranged)]
		[TestCase(100, 0.0f, false, MilitaryType.Ranged)]
		[TestCase(0, 0.0f, false, MilitaryType.Siege)]
		[TestCase(5, 505.0f, false, MilitaryType.Siege)]
		[TestCase(10, 0.0f, false, MilitaryType.Siege)]
		[TestCase(11, 101.0f, false, MilitaryType.Siege)]
		[TestCase(20, 0.0f, false, MilitaryType.Siege)]
		[TestCase(100, 0.0f, false, MilitaryType.Siege)]
		public void Military_GetMilitaryActionBarValue(int gameTicks, float expected, bool upgrade, MilitaryType militaryType)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);
			if (upgrade) _sut.UpgradeMilitaryLevel(militaryType);

			// Act
			var result = _sut.GetMilitaryActionBarValue(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region Enemy
		[Test]
		public void Enemy_GetCurrentEnemyTown()
		{
			// Arrange
			// Act
			var enemy = _sut.GetCurrentEnemyTown();
			int enemyLevel = enemy.Level;

			// Assert
			Assert.IsFalse(string.IsNullOrWhiteSpace(enemy.Name));
			Assert.AreEqual(enemyLevel * Constants.BaseEnemyScaling, enemy.GoldValue);
			Assert.AreEqual(enemyLevel * Constants.BaseEnemyScaling, enemy.HpCurrent);
			Assert.AreEqual(enemyLevel * Constants.BaseEnemyScaling, enemy.HpMax);
		}

		[TestCase(1)]
		[TestCase(10)]
		public void Enemy_GetNewEnemyTown(int enemyLevel)
		{
			// Arrange
			// Act
			var enemy = _sut.GetNewEnemyTown(enemyLevel);

			// Assert
			Assert.AreEqual(enemyLevel, enemy.Level);
			Assert.IsFalse(string.IsNullOrWhiteSpace(enemy.Name));
			Assert.AreEqual(enemyLevel * Constants.BaseEnemyScaling, enemy.GoldValue);
			Assert.AreEqual(enemyLevel * Constants.BaseEnemyScaling, enemy.HpCurrent);
			Assert.AreEqual(enemyLevel * Constants.BaseEnemyScaling, enemy.HpMax);
		}

		[TestCase(1)]
		[TestCase(10)]
		public void Enemy_SetCurrentEnemyTown(int enemyLevel)
		{
			// Arrange
			EnemyTown enemy = _sut.GetNewEnemyTown(enemyLevel);

			// Act
			_sut.SetCurrentEnemyTown(enemy);
			enemy = _sut.GetCurrentEnemyTown();

			// Assert
			Assert.AreEqual(enemyLevel, enemy.Level);
			Assert.IsFalse(string.IsNullOrWhiteSpace(enemy.Name));
			Assert.AreEqual(enemyLevel * Constants.BaseEnemyScaling, enemy.GoldValue);
			Assert.AreEqual(enemyLevel * Constants.BaseEnemyScaling, enemy.HpCurrent);
			Assert.AreEqual(enemyLevel * Constants.BaseEnemyScaling, enemy.HpMax);
		}

		[TestCase(1, 1, 0, MilitaryType.Melee)]
		[TestCase(1, 5, 4, MilitaryType.Melee)]
		[TestCase(2, 10, 6, MilitaryType.Melee)]
		[TestCase(2, 20, 16, MilitaryType.Melee)]
		public void Enenmy_CalculateDamageToEnemyTown(int enemyLevel, int attackDamage, int expected, MilitaryType militaryType)
		{
			// Arrange
			EnemyTown enemy = _sut.GetNewEnemyTown(enemyLevel);
			_sut.SetCurrentEnemyTown(enemy);

			// Act
			var result = _sut.CalculateDamageToEnemyTown(attackDamage, militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(1, 1, MilitaryType.Melee)]
		[TestCase(1, 5, MilitaryType.Melee)]
		[TestCase(2, 10, MilitaryType.Melee)]
		[TestCase(2, 20, MilitaryType.Melee)]
		public void Enemy_ApplyDamageToEnemyTown(int enemyLevel, int attackDamage, MilitaryType militaryType)
		{
			// Arrange
			EnemyTown enemy = _sut.GetNewEnemyTown(enemyLevel);
			_sut.SetCurrentEnemyTown(enemy);
			var damage = _sut.CalculateDamageToEnemyTown(attackDamage, militaryType);

			// Act
			_sut.ApplyDamageToEnemyTown(damage);
			var result = _sut.GetCurrentEnemyTown().HpMax - _sut.GetCurrentEnemyTown().HpCurrent;
			var expectedDamage = (damage >= _sut.GetCurrentEnemyTown().HpMax) ? _sut.GetCurrentEnemyTown().HpMax : damage;
			
			
			// Assert
			Assert.AreEqual(expectedDamage, result);
		}

		[TestCase(1, 1, MilitaryType.Melee)]
		[TestCase(1, 5, MilitaryType.Melee)]
		[TestCase(2, 10, MilitaryType.Melee)]
		[TestCase(2, 20, MilitaryType.Melee)]
		public void Enemy_IsEnemyTownDestroyed(int enemyLevel, int attackDamage, MilitaryType militaryType)
		{
			// Arrange
			EnemyTown enemy = _sut.GetNewEnemyTown(enemyLevel);
			_sut.SetCurrentEnemyTown(enemy);
			var damage = _sut.CalculateDamageToEnemyTown(attackDamage, militaryType);

			// Act
			_sut.ApplyDamageToEnemyTown(damage);
			var result = _sut.IsEnemyTownDestroyed();
			var expectedTownDestroyed = (damage >= _sut.GetCurrentEnemyTown().HpMax) ? true : false;
			
			// Assert
			Assert.AreEqual(expectedTownDestroyed, result);
		}
		#endregion

		private void TriggerGameActionBars(int amount)
		{
			for (int _ = 0; _ < amount; _++)
			{
				_sut.TriggerAllActionBars();
			}
		}
	}
}
