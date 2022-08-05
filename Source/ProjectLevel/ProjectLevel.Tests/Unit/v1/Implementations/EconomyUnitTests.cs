using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Services.v1.Implementations;
using ProjectLevel.Tests.TestHelpers;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.Unit.v1.Implementations
{
	public class EconomyUnitTests
	{
		private IDataSource _dataSource;
		private ItemChest _itemChest;
		private IEconomy _sut;

		[SetUp]
		public void SetUp()
		{
			_dataSource = new TestDataSource();
			_itemChest = new ItemChest(_dataSource.GetAvailableLoot());
			_sut = new Economy();
		}

		[TearDown]
		public void TearDown()
		{

		}

		[TestCase(1, 0, false)]
		[TestCase(1, 10, true)]
		[TestCase(2, 10, false)]
		[TestCase(2, 20, true)]
		public void CanUpgradeGoldLevel(int level, int gold, bool expected)
		{
			// Arrange
			for (int i = 1; i < level; i++)
			{
				_sut.UpgradeGoldLevel();
			}
			_sut.AddGold(gold);
			
			// Act
			var result = _sut.CanUpgradeGoldLevel() ;

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(1, 0, false, 1)]
		[TestCase(1, 10, false, 11)]
		[TestCase(2, 10, false, 12)]
		[TestCase(2, 20, false, 22)]
		[TestCase(1, 0, true, 1)]
		[TestCase(1, 10, true, 11)]
		[TestCase(2, 10, true, 12)]
		[TestCase(2, 20, true, 22)]
		public void RecieveGoldIncome(int level, int gold, bool useLoot, int expected)
		{
			// Arrange
			ItemChest itemChest = (useLoot) ? _itemChest : new ItemChest();
			for (int i = 1; i < level; i++)
			{
				_sut.UpgradeGoldLevel();
			}
			_sut.AddGold(gold);

			// Act
			_sut.RecieveGoldIncome(itemChest);
			var result = _sut.Gold;

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(1)]
		[TestCase(2)]
		[TestCase(10)]
		public void RequiredGoldToLevelUp(int level)
		{
			// Arrange
			for (int i = 1; i < level; i++)
			{
				_sut.UpgradeGoldLevel();
			}

			// Act
			var result = _sut.RequiredGoldToLevelUp();

			// Assert
			Assert.AreEqual(level * Constants.GoldPerLevelCost, result);
		}

		[TestCase(0, 0, 0)]
		[TestCase(10, 5, 5)]
		[TestCase(100, 10, 90)]
		public void SpendGold(int gold, int goldSpent, int expected)
		{
			// Arrange
			_sut.AddGold(gold);

			// Act
			_sut.SpendGold(goldSpent);
			var result = _sut.Gold;

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(1)]
		[TestCase(2)]
		[TestCase(10)]
		public void UpgradeGoldLevel(int level)
		{
			// Arrange
			// Act
			for (int i = 1; i < level; i++)
			{
				_sut.UpgradeGoldLevel();
			}

			var result = _sut.GoldLevel;

			// Assert
			Assert.AreEqual(level, result);
		}

		[TestCase(false, 1.0f)]
		[TestCase(true, 101.0f)]
		public void TriggerAllActionBars(bool useLoot, float expectedIncrease)
		{
			// Arrange
			float currentActionBarValue = _sut.GoldActionBar.Value;

			// Act
			TriggerGameActionBars(1, useLoot);
			var result = _sut.GoldActionBar.Value;

			// Assert
			Assert.AreEqual(currentActionBarValue + expectedIncrease, result);
		}

		private void TriggerGameActionBars(int amount, bool useLoot)
		{
			for (int _ = 0; _ < amount; _++)
			{
				if (useLoot) _sut.TriggerAllActionBars(_itemChest);
				else _sut.TriggerAllActionBars(new ItemChest());
			}
		}
	}
}
