using System;
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
	public class MilitaryUnitTests
	{
		private Mock<IDataSource> _mockDataSource;
		private Mock<IMilitaryFactory> _mockMilitaryFactory;
		private Mock<IItemChest> _mockItemChest;
		private IMilitary _sut;

		[SetUp]
		public void SetUp()
		{
			_mockDataSource = MockDataSource.GetMockDataSource();
			_mockMilitaryFactory = MockMilitaryFactory.GetMockMilitaryFactory(1);
			_mockItemChest = MockItemChest.GetMockItemChest();

			_sut = new Military(_mockMilitaryFactory.Object.BuildMilitaryUnits(1));
		}

		[TearDown]
		public void TearDown()
		{
			_mockDataSource = null;
			_mockMilitaryFactory = null;
			_mockItemChest = null;

			_sut = null;
		}

		[TestCase(1, MilitaryType.Melee)]
		[TestCase(5, MilitaryType.Melee)]
		[TestCase(1, MilitaryType.Ranged)]
		[TestCase(5, MilitaryType.Ranged)]
		[TestCase(1, MilitaryType.Siege)]
		[TestCase(5, MilitaryType.Siege)]
		public void SetupMilitaryUnits(int expected, MilitaryType militaryType)
		{
			// Arrange
			IEnumerable<MilitaryUnit> militaryUnitList = MockMilitaryFactory.GetMilitaryUnits(expected);

			// Act
			_sut.SetupMilitaryUnits(militaryUnitList);

			// Assert
			Assert.AreEqual(expected, _sut.GetUnitLevel(militaryType));
		}

		[TestCase(1, MilitaryType.Melee)]
		[TestCase(5, MilitaryType.Melee)]
		[TestCase(10, MilitaryType.Melee)]
		[TestCase(1, MilitaryType.Ranged)]
		[TestCase(5, MilitaryType.Ranged)]
		[TestCase(10, MilitaryType.Ranged)]
		[TestCase(1, MilitaryType.Siege)]
		[TestCase(5, MilitaryType.Siege)]
		[TestCase(10, MilitaryType.Siege)]
		public void GetUnitCount(int level, MilitaryType militaryType)
		{
			// Arrange
			_sut = new Military(MockMilitaryFactory.GetMilitaryUnits(level));

			// Act
			var result = _sut.GetUnitCount(militaryType);

			// Assert
			Assert.AreEqual(level, result);
		}

		[TestCase(1, MilitaryType.Melee)]
		[TestCase(5, MilitaryType.Melee)]
		[TestCase(10, MilitaryType.Melee)]
		[TestCase(1, MilitaryType.Ranged)]
		[TestCase(5, MilitaryType.Ranged)]
		[TestCase(10, MilitaryType.Ranged)]
		[TestCase(1, MilitaryType.Siege)]
		[TestCase(5, MilitaryType.Siege)]
		[TestCase(10, MilitaryType.Siege)]
		public void GetBaseUnitDamage(int level, MilitaryType militaryType)
		{
			// Arrange
			_sut = new Military(MockMilitaryFactory.GetMilitaryUnits(level));

			// Act
			var result = _sut.GetBaseUnitDamage(militaryType);

			// Assert
			Assert.AreEqual(_sut.GetUnitCount(militaryType)
				* _sut.GetUnitLevel(militaryType)
				, result);
			Assert.AreEqual(_sut.GetUnitCount(militaryType)
				* _sut.GetUnitLevel(militaryType)
				, result);
			Assert.AreEqual(_sut.GetUnitCount(militaryType)
				* _sut.GetUnitLevel(militaryType)
				, result);
		}

		[TestCase(1, MilitaryType.Melee)]
		[TestCase(5, MilitaryType.Melee)]
		[TestCase(10, MilitaryType.Melee)]
		[TestCase(1, MilitaryType.Ranged)]
		[TestCase(5, MilitaryType.Ranged)]
		[TestCase(10, MilitaryType.Ranged)]
		[TestCase(1, MilitaryType.Siege)]
		[TestCase(5, MilitaryType.Siege)]
		[TestCase(10, MilitaryType.Siege)]
		public void GetTotalUnitDamage(int level, MilitaryType militaryType)
		{
			// Arrange
			_sut = new Military(MockMilitaryFactory.GetMilitaryUnits(level));
			float ratio = 1.0f + _mockItemChest.Object.GetStatsForMilitaryType(militaryType).Sum(_ => _.DamageRatio);
			double expected = Math.Round(_sut.GetBaseUnitDamage(militaryType) * ratio);

			// Act
			var result = _sut.GetTotalUnitDamage(militaryType, _mockItemChest.Object);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(1, MilitaryType.Melee)]
		[TestCase(5, MilitaryType.Melee)]
		[TestCase(10, MilitaryType.Melee)]
		[TestCase(1, MilitaryType.Ranged)]
		[TestCase(5, MilitaryType.Ranged)]
		[TestCase(10, MilitaryType.Ranged)]
		[TestCase(1, MilitaryType.Siege)]
		[TestCase(5, MilitaryType.Siege)]
		[TestCase(10, MilitaryType.Siege)]
		public void GetUnitLevel(int level, MilitaryType militaryType)
		{
			// Arrange
			_sut = new Military(MockMilitaryFactory.GetMilitaryUnits(level));

			// Act
			var result = _sut.GetUnitLevel(militaryType);

			// Assert
			Assert.AreEqual(level, result);
		}

		[TestCase(1, MilitaryType.Melee)]
		[TestCase(5, MilitaryType.Melee)]
		[TestCase(10, MilitaryType.Melee)]
		[TestCase(1, MilitaryType.Ranged)]
		[TestCase(5, MilitaryType.Ranged)]
		[TestCase(10, MilitaryType.Ranged)]
		[TestCase(1, MilitaryType.Siege)]
		[TestCase(5, MilitaryType.Siege)]
		[TestCase(10, MilitaryType.Siege)]
		public void RequiredGoldForNewUnit(int level, MilitaryType militaryType)
		{
			// Arrange
			_sut = new Military(MockMilitaryFactory.GetMilitaryUnits(level));

			// Act
			var result = _sut.RequiredGoldForNewUnit(militaryType);

			// Assert
			Assert.AreEqual(level * Constants.GoldPerLevelCost, result);
		}

		[TestCase(1, MilitaryType.Melee)]
		[TestCase(5, MilitaryType.Melee)]
		[TestCase(10, MilitaryType.Melee)]
		[TestCase(1, MilitaryType.Ranged)]
		[TestCase(5, MilitaryType.Ranged)]
		[TestCase(10, MilitaryType.Ranged)]
		[TestCase(1, MilitaryType.Siege)]
		[TestCase(5, MilitaryType.Siege)]
		[TestCase(10, MilitaryType.Siege)]
		public void RequiredGoldToLevelUp(int level, MilitaryType militaryType)
		{
			// Arrange
			_sut = new Military(MockMilitaryFactory.GetMilitaryUnits(level));

			// Act
			var result = _sut.RequiredGoldToLevelUp(militaryType);

			// Assert
			Assert.AreEqual(level * Constants.GoldPerLevelCost, result);
		}

		[TestCase(MilitaryType.Melee)]
		[TestCase(MilitaryType.Ranged)]
		[TestCase(MilitaryType.Siege)]
		public void UpgradeUnitCount(MilitaryType militaryType)
		{
			// Arrange
			int currentLevel = _sut.GetUnitCount(militaryType);

			// Act
			_sut.UpgradeUnitCount(militaryType);

			// Assert
			Assert.AreEqual(currentLevel + 1, _sut.GetUnitCount(militaryType));
		}

		[TestCase(MilitaryType.Melee)]
		[TestCase(MilitaryType.Ranged)]
		[TestCase(MilitaryType.Siege)]
		public void UpgradeUnitLevel(MilitaryType militaryType)
		{
			// Arrange
			int currentLevel = _sut.GetUnitCount(militaryType);

			// Act
			_sut.UpgradeUnitLevel(militaryType);

			// Assert
			Assert.AreEqual(currentLevel + 1, _sut.GetUnitLevel(militaryType));
		}

		[TestCase(MilitaryType.Melee)]
		[TestCase(MilitaryType.Ranged)]
		[TestCase(MilitaryType.Siege)]
		public void GetUnitActionBar(MilitaryType militaryType)
		{
			// Arrange
			float currentActionBarValue = _sut.GetUnitActionBar(militaryType).Value;

			// Act
			var result = _sut.GetUnitActionBar(militaryType).Value;

			// Assert
			Assert.AreEqual(currentActionBarValue, result);
		}

		[TestCase(false, 1.0f, MilitaryType.Melee)]
		[TestCase(true, 101.0f, MilitaryType.Melee)]
		[TestCase(false, 1.0f, MilitaryType.Ranged)]
		[TestCase(true, 101.0f, MilitaryType.Ranged)]
		[TestCase(false, 1.0f, MilitaryType.Siege)]
		[TestCase(true, 101.0f, MilitaryType.Siege)]
		public void TriggerAllActionBars(bool useLoot, float expectedIncrease, MilitaryType militaryType)
		{
			// Arrange
			float currentActionBarValue = _sut.GetUnitActionBar(militaryType).Value;

			// Act
			TriggerGameActionBars(1, useLoot);

			// Assert
			Assert.AreEqual(currentActionBarValue + expectedIncrease, _sut.GetUnitActionBar(militaryType).Value);
		}

		private void TriggerGameActionBars(int amount, bool useLoot)
		{
			for (int _ = 0; _ < amount; _++)
			{
				if(useLoot)	_sut.TriggerAllActionBars(_mockItemChest.Object);
				else _sut.TriggerAllActionBars(new ItemChest());
			}
		}
	}
}
