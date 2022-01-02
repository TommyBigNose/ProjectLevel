using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.Unit.v1.Implementations
{
	[TestFixture]
	internal class CivilizationUnitTests
	{
		[SetUp]
		public void SetUp()
		{

		}

		[TearDown]
		public void TearDown()
		{

		}

		#region Economy
		[Test]
		void GetGold()
		{
			// Arrange
			// Act
			// Assert
		}

		void GetGoldIncomeRate()
		{
			// Arrange
			// Act
			// Assert
		}

		void UpgradeGoldLevel()
		{
			// Arrange
			// Act
			// Assert
		}
		#endregion

		#region Military
		void GetUnitCount(MilitaryType militaryType)
		{
			// Arrange
			// Act
			// Assert
		}
		void GetUnitDamage(MilitaryType militaryType)
		{
			// Arrange
			// Act
			// Assert
		}

		void GetUnitActionBar(MilitaryType militaryType)
		{
			// Arrange
			// Act
			// Assert
		}

		void UpgradeUnitLevel(MilitaryType militaryType)
		{
			// Arrange
			// Act
			// Assert
		}
		#endregion
	}
}
