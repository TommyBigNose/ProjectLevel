using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Services.v1.Implementations;
using ProjectLevel.Tests.TestHelpers;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.Unit.v1.Implementations
{
	public class BattleReadyTests
	{
		private Mock<IMilitaryFactory> _mockMilitaryFactory;
		private IBattleReady _sut;

		[SetUp]
		public void SetUp()
		{
			_mockMilitaryFactory = MockMilitaryFactory.GetMockMilitaryFactory(1);
			_sut = new BattleReadyEnemyTown("Test Enemy Town", 1, new Contracts.v1.Models.Loot(), _mockMilitaryFactory.Object.BuildInitialMilitary());
		}

		[TearDown]
		public void TearDown()
		{
			_mockMilitaryFactory = null;
			_sut = null;
		}

		[TestCase(1, 1, 0, MilitaryType.Melee)]
		[TestCase(1, 5, 4, MilitaryType.Melee)]
		[TestCase(2, 10, 6, MilitaryType.Melee)]
		[TestCase(2, 20, 16, MilitaryType.Melee)]
		public void CalculateDamage(int enemyLevel, int attackDamage, int expected, MilitaryType militaryType)
		{
			// Arrange
			_sut = new BattleReadyEnemyTown("Test Enemy Town", enemyLevel, new Contracts.v1.Models.Loot(), _mockMilitaryFactory.Object.BuildMilitary(enemyLevel));

			// Act
			var result = _sut.CalculateDamage(attackDamage, militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(1, 1, MilitaryType.Melee)]
		[TestCase(1, 5, MilitaryType.Melee)]
		[TestCase(2, 10, MilitaryType.Melee)]
		[TestCase(2, 20, MilitaryType.Melee)]
		public void ApplyDamage(int enemyLevel, int attackDamage, MilitaryType militaryType)
		{
			// Arrange
			_sut = new BattleReadyEnemyTown("Test Enemy Town", enemyLevel, new Contracts.v1.Models.Loot(), _mockMilitaryFactory.Object.BuildMilitary(enemyLevel));
			var damage = _sut.CalculateDamage(attackDamage, militaryType);

			// Act
			_sut.ApplyDamage(damage);
			var result = _sut.HpMax - _sut.HpCurrent;
			var expectedDamage = (damage >= _sut.HpMax) ? _sut.HpMax : damage;

			// Assert
			Assert.AreEqual(expectedDamage, result);
		}

		[TestCase(1, 1, MilitaryType.Melee)]
		[TestCase(1, 5, MilitaryType.Melee)]
		[TestCase(2, 10, MilitaryType.Melee)]
		[TestCase(2, 20, MilitaryType.Melee)]
		public void IsTownDestroyed(int enemyLevel, int attackDamage, MilitaryType militaryType)
		{
			// Arrange
			_sut = new BattleReadyEnemyTown("Test Enemy Town", enemyLevel, new Contracts.v1.Models.Loot(), _mockMilitaryFactory.Object.BuildMilitary(enemyLevel));
			var damage = _sut.CalculateDamage(attackDamage, militaryType);

			// Act
			_sut.ApplyDamage(damage);
			var result = _sut.IsTownDestroyed();
			var expectedTownDestroyed = (damage >= _sut.HpMax);

			// Assert
			Assert.AreEqual(expectedTownDestroyed, result);
		}
	}
}
