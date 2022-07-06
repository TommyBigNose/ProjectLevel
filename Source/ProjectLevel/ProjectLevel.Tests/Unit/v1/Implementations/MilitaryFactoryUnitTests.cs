using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Services.v1.Implementations;
using ProjectLevel.Tests.TestHelpers;

namespace ProjectLevel.Tests.Unit.v1.Implementations
{
	public class MilitaryFactoryUnitTests
	{
		private IMilitaryFactory _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new MilitaryFactory();
		}

		[TearDown]
		public void TearDown()
		{

		}

		[Test]
		public void BuildInitialMilitary()
		{
			// Arrange


			// Act
			var result = _sut.BuildInitialMilitary();

			// Assert
			Assert.AreEqual(1, result.GetUnitCount(Contracts.v1.Constants.MilitaryType.Melee));
			Assert.AreEqual(1, result.GetUnitCount(Contracts.v1.Constants.MilitaryType.Ranged));
			Assert.AreEqual(1, result.GetUnitCount(Contracts.v1.Constants.MilitaryType.Siege));

			Assert.AreEqual(1, result.GetUnitDamage(Contracts.v1.Constants.MilitaryType.Melee));
			Assert.AreEqual(1, result.GetUnitDamage(Contracts.v1.Constants.MilitaryType.Ranged));
			Assert.AreEqual(1, result.GetUnitDamage(Contracts.v1.Constants.MilitaryType.Siege));

			Assert.AreEqual(1, result.GetUnitLevel(Contracts.v1.Constants.MilitaryType.Melee));
			Assert.AreEqual(1, result.GetUnitLevel(Contracts.v1.Constants.MilitaryType.Ranged));
			Assert.AreEqual(1, result.GetUnitLevel(Contracts.v1.Constants.MilitaryType.Siege));
		}
	}
}
