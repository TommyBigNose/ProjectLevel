using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
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
		public void GetAvailableLoot(int addLootCount, int expected)
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
		public void AddLoot(int addLootCount, int expected)
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
		public void GetLoot(int addLootCount, int expected)
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
		#endregion

		#region Gold
		[TestCase(0, 0)]
		[TestCase(10, 1)]
		[TestCase(20, 2)]
		public void GetGold(int gameTicks, int expected)
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
		public void GetGoldIncomeRate(int gameTicks, int expected, bool upgrade)
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
		public void RequiredGoldToLevelUp(int gameTicks, int expected, bool upgrade)
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
		public void CanUpgradeGoldLevel(int gameTicks, bool expected, bool upgrade)
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
		public void UpgradeGoldLevel(int upgradeCount, int expected)
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
		public void GetGoldActionBarValue(int gameTicks, float expected, bool upgrade)
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

		#region Military
		[TestCase(0, 1, MilitaryType.Melee)]
		[TestCase(10, 1, MilitaryType.Melee)]
		[TestCase(20, 1, MilitaryType.Melee)]
		[TestCase(100, 1, MilitaryType.Melee)]
		public void GetMilitaryUnitCount(int gameTicks, int expected, MilitaryType militaryType)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);

			// Act
			var result = _sut.GetMilitaryUnitCount(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 1, MilitaryType.Melee)]
		[TestCase(10, 1, MilitaryType.Melee)]
		[TestCase(20, 1, MilitaryType.Melee)]
		[TestCase(100, 1, MilitaryType.Melee)]
		public void GetMilitaryLevel(int gameTicks, int expected, MilitaryType militaryType)
		{
			// Arrange
			TriggerGameActionBars(gameTicks);

			// Act
			var result = _sut.GetMilitaryLevel(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, false, MilitaryType.Melee)]
		[TestCase(10, false, MilitaryType.Melee)]
		[TestCase(20, false, MilitaryType.Melee)]
		[TestCase(100, true, MilitaryType.Melee)]
		public void CanUpgradeMilitaryLevel(int gameTicks, bool expected, MilitaryType militaryType)
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
		public void UpgradeMilitaryLevel(int gameTicks, bool expected, bool upgrade, MilitaryType militaryType)
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
		public void CanUpgradeMilitaryUnitCount(int gameTicks, bool expected, MilitaryType militaryType)
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
		public void UpgradeMilitaryUnitCount(int gameTicks, bool expected, bool upgrade, MilitaryType militaryType)
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
		public void GetMilitaryActionBarValue(int gameTicks, float expected, bool upgrade, MilitaryType militaryType)
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

		[TestCase(0, 0)]
		public void PlaceHolderTest(int gameTicks, int expected)
		{
			// Arrange
			// Act
			// Assert
		}

		private void TriggerGameActionBars(int amount)
		{
			for (int _ = 0; _ < amount; _++)
			{
				_sut.TriggerAllActionBars();
			}
		}
	}
}
