using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
	public class ItemChestUnitTests
	{
		private IItemChest _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ItemChest();
		}

		[TearDown]
		public void TearDown()
		{

		}

		[TestCase(false, false, MilitaryType.Melee)]
		[TestCase(false, false, MilitaryType.Ranged)]
		[TestCase(false, false, MilitaryType.Siege)]
		[TestCase(true, true, MilitaryType.Melee)]
		[TestCase(true, true, MilitaryType.Ranged)]
		[TestCase(true, true, MilitaryType.Siege)]
		public void ContainsItemsForMilitaryType(bool useLoot, bool expected, MilitaryType militaryType)
		{
			// Arrange
			if(useLoot) _sut = new ItemChest(new TestDataSource().GetAvailableLoot());

			// Act
			var result = _sut.ContainsItemsForMilitaryType(militaryType);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestCase(false, MilitaryType.Melee)]
		[TestCase(false, MilitaryType.Ranged)]
		[TestCase(false, MilitaryType.Siege)]
		[TestCase(true, MilitaryType.Melee)]
		[TestCase(true, MilitaryType.Ranged)]
		[TestCase(true, MilitaryType.Siege)]
		public void GetStatsForMilitaryType(bool useLoot, MilitaryType militaryType)
		{
			// Arrange
			if (useLoot) _sut = new ItemChest(new TestDataSource().GetAvailableLoot());

			// Act
			var result = _sut.GetStatsForMilitaryType(militaryType);
			
			// Assert
			Assert.AreEqual(useLoot, result.FindAll(_ => _.MilitaryType == militaryType).Any());
			Assert.That(!useLoot || result.FindAll(_ => _.MilitaryType == militaryType).Sum(_ => _.DamageRatio) > 0.0);
			Assert.That(!useLoot || result.FindAll(_ => _.MilitaryType == militaryType).Sum(_ => _.SpeedRatio) > 0.0);
		}
	}
}
