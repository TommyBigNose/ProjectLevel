﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Moq;
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
		private Mock<IDataSource> _mockDataSource;
		private Mock<IMilitaryFactory> _mockMilitaryFactory;
		private Mock<ICivilization> _mockCivilization;

		private IGame _sut;

		[SetUp]
		public void SetUp()
		{
			// TestDataSource has some special items to speed up loot timer
			_mockDataSource = MockDataSource.GetMockDataSource();
			_mockMilitaryFactory = MockMilitaryFactory.GetMockMilitaryFactory(1);
			_mockCivilization = MockCivilization.GetMockCivilization();
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			//_sut.AddLoot(_mockDataSource.Object.GetAvailableLoot());
		}

		[TearDown]
		public void TearDown()
		{
			_mockDataSource = null;
			_mockMilitaryFactory = null;
			_mockCivilization = null;
			_sut = null;
		}

		#region Data
		[Test]
		public void Data_GetAvailableLoot()
		{
			// Arrange
			int expected = _mockDataSource.Object.GetAvailableLoot().Count;

			// Act
			var result = _sut.GetAvailableLoot().Count;

			// Assert
			Assert.AreEqual(expected, result);
		}
		#endregion

		#region Civilization
		[Test]
		public void Civilization_AddLoot()
		{
			// Arrange
			var initial = _sut.GetLoot().Count;

			// Act
			_sut.AddLoot(_sut.GetAvailableLoot());
			var result = _sut.GetLoot().Count;

			// Assert
			Assert.That(result, Is.GreaterThan(initial));
		}

		[Test]
		public void Civilization_GetLoot()
		{
			// Arrange
			var expected = _mockDataSource.Object.GetAvailableLoot();

			// Act
			_sut.AddLoot(_mockDataSource.Object.GetAvailableLoot());
			var result = _sut.GetLoot();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void Civilization_RemoveAllLoot()
		{
			// Arrange
			int expected = 0;
			_sut.AddLoot(_mockDataSource.Object.GetAvailableLoot());

			// Act
			_sut.RemoveAllLoot();
			var result = _sut.GetLoot().Count;

			// Assert
			Assert.AreEqual(expected, result);
		}
		#endregion

		#region Gold
		[Test]
		public void Gold_GetGold()
		{
			// Arrange
			int expected = 100;

			_mockCivilization = MockCivilization.GetMockCivilization(expected, 1);

			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);
			
			// Act
			var result = _sut.GetGold();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void Gold_GetGoldIncomeRate()
		{
			// Arrange
			int expected = 1;

			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);

			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			// Act
			var result = _sut.GetGoldIncomeRate();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void Gold_RequiredGoldToLevelUp()
		{
			// Arrange
			int expected = 10;

			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);

			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			// Act
			var result = _sut.RequiredGoldToLevelUp();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void Gold_CanUpgradeGoldLevel()
		{
			// Arrange
			bool expected = true;

			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1, expected);

			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			// Act
			var result = _sut.CanUpgradeGoldLevel();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void Gold_UpgradeGoldLevel()
		{
			// Arrange
			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);
			int initial = _sut.GetGoldIncomeRate();

			// Act
			_sut.UpgradeGoldLevel();
			var result = _sut.GetGoldIncomeRate();

			// Assert
			Assert.That(result, Is.GreaterThan(initial));
		}

		[Test]
		public void Gold_GetGoldActionBarValue()
		{
			// Arrange
			float expected = 0.0f;

			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);

			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			// Act
			var result = _sut.GetGoldActionBarValue();

			// Assert
			Assert.AreEqual(expected, result);
		}
		#endregion

		#region Shop
		[Test]
		public void Shop_GetShopLoot()
		{
			// Arrange
			var expected = _mockDataSource.Object.GetAvailableLoot().FindAll(_=>_.IsShopItem);

			// Act
			var result = _sut.GetShopLoot();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void Shop_CanPurchaseLoot()
		{
			// Arrange
			bool expected = true;

			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			List<Loot> loot = _sut.GetShopLoot();

			// Act
			bool result = _sut.CanPurchaseLoot(loot.First());

			// Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void Shop_PurchaseLoot()
		{
			// Arrange
			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			List<Loot> loot = _sut.GetShopLoot();

			int initialLootCount = _sut.GetLoot().Count;

			// Act
			_sut.PurchaseLoot(loot.First());
			int result = _sut.GetLoot().Count;

			// Assert
			Assert.That(result, Is.GreaterThan(initialLootCount));
		}
		#endregion

		#region Military
		[TestCase(1, MilitaryType.Melee)]
		[TestCase(1, MilitaryType.Ranged)]
		[TestCase(1, MilitaryType.Siege)]
		public void Military_GetMilitaryUnitCount(int expected, MilitaryType militaryType)
		{
			// Arrange
			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			// Act
			var result = _sut.GetMilitaryUnitCount(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(1, MilitaryType.Melee)]
		[TestCase(1, MilitaryType.Ranged)]
		[TestCase(1, MilitaryType.Siege)]
		public void Military_GetMilitaryLevel(int expected, MilitaryType militaryType)
		{
			// Arrange
			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			// Act
			var result = _sut.GetMilitaryLevel(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(1, 1, MilitaryType.Melee)]
		[TestCase(2, 4, MilitaryType.Melee)]
		[TestCase(1, 1, MilitaryType.Ranged)]
		[TestCase(2, 4, MilitaryType.Ranged)]
		[TestCase(1, 1, MilitaryType.Siege)]
		[TestCase(2, 4, MilitaryType.Siege)]
		public void Military_GetMilitaryDamage(int level, int expected, MilitaryType militaryType)
		{
			// Arrange
			_mockCivilization = MockCivilization.GetMockCivilization(10000, level);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			// Act
			var result = _sut.GetMilitaryDamage(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(true, MilitaryType.Melee)]
		[TestCase(true, MilitaryType.Ranged)]
		[TestCase(true, MilitaryType.Siege)]
		public void Military_CanUpgradeMilitaryLevel(bool expected, MilitaryType militaryType)
		{
			// Arrange
			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);


			// Act
			var result = _sut.CanUpgradeMilitaryLevel(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(MilitaryType.Melee)]
		[TestCase(MilitaryType.Ranged)]
		[TestCase(MilitaryType.Siege)]
		public void Military_UpgradeMilitaryLevel(MilitaryType militaryType)
		{
			// Arrange
			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);
			var initial = _sut.GetMilitaryLevel(militaryType);

			// Act
			_sut.UpgradeMilitaryLevel(militaryType);
			var result = _sut.GetMilitaryLevel(militaryType);

			// Assert
			Assert.That(result, Is.GreaterThan(initial));
		}

		[TestCase(true, MilitaryType.Melee)]
		[TestCase(true, MilitaryType.Ranged)]
		[TestCase(true, MilitaryType.Siege)]
		public void Military_CanUpgradeMilitaryUnitCount(bool expected, MilitaryType militaryType)
		{
			// Arrange
			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);

			// Act
			var result = _sut.CanUpgradeMilitaryUnitCount(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(false, false, MilitaryType.Melee)]
		[TestCase(false, false, MilitaryType.Ranged)]
		[TestCase(false, false, MilitaryType.Siege)]
		public void Military_UpgradeMilitaryUnitCount(bool expected, bool upgrade, MilitaryType militaryType)
		{
			// Arrange
			_mockCivilization = MockCivilization.GetMockCivilization(10000, 1);
			_sut = new Game(_mockDataSource.Object, _mockCivilization.Object, _mockMilitaryFactory.Object);
			//int initial = _sut.GetMilitaryUnitCount();

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
			IBattleReady enemy = _sut.GetNewEnemyTown(enemyLevel);

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
			IBattleReady enemy = _sut.GetNewEnemyTown(enemyLevel);
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
			IBattleReady enemy = _sut.GetNewEnemyTown(enemyLevel);
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
			IBattleReady enemy = _sut.GetNewEnemyTown(enemyLevel);
			_sut.SetCurrentEnemyTown(enemy);
			var damage = _sut.CalculateDamageToEnemyTown(attackDamage, militaryType);

			// Act
			_sut.ApplyDamageToEnemyTown(damage);
			var result = _sut.IsEnemyTownDestroyed();
			var expectedTownDestroyed = (damage >= _sut.GetCurrentEnemyTown().HpMax);
			
			// Assert
			Assert.AreEqual(expectedTownDestroyed, result);
		}
		#endregion

		#region Battle
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(10)]
		public void Battle_RewardPlayerItemFromEnemyTown(int enemyLevel)
		{
			// Arrange
			IBattleReady enemy = _sut.GetNewEnemyTown(enemyLevel);
			_sut.SetCurrentEnemyTown(enemy);
			int currentLoot = _sut.GetLoot().Count;

			int currentGold = _sut.GetGold();

			// Act
			_sut.RewardPlayerItemFromEnemyTown();

			// Assert
			Assert.AreEqual(currentLoot + 1, _sut.GetLoot().Count);
			Assert.AreEqual(currentGold + (enemyLevel * Constants.BaseEnemyScaling), _sut.GetGold());
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
