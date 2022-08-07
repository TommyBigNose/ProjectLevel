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
	public class CivilizationTests
	{
		private Mock<IEconomy> _mockEconomy;
		private Mock<IMilitary> _mockMilitary;
		private Mock<IItemChest> _mockItemChest;
		private ICivilization _sut;

		[SetUp]
		public void SetUp()
		{
			//_militaryFactory = new MilitaryFactory();
			_mockEconomy = MockEconomy.GetMockEconomy();
			_mockMilitary = MockMilitary.GetMockMilitary(1, 1);
			_mockItemChest = MockItemChest.GetMockItemChest();
			_sut = new Civilization(_mockEconomy.Object, _mockMilitary.Object, _mockItemChest.Object);
		}

		[TearDown]
		public void TearDown()
		{
			_mockEconomy = null;
			_mockMilitary = null;
			_mockItemChest = null;
			_sut = null;
		}

		[Test]
		public void Economy()
		{
			// Arrange
			// Act
			// Assert
			Assert.That(_sut.Economy, Is.Not.Null);
		}

		[Test]
		public void Military()
		{
			// Arrange
			// Act
			// Assert
			Assert.That(_sut.Military, Is.Not.Null);
		}

		[Test]
		public void ItemChest()
		{
			// Arrange
			// Act
			// Assert
			Assert.That(_sut.ItemChest, Is.Not.Null);
		}

		[Test]
		public void TriggerAllActionBars()
		{
			// Arrange
			// Act
			_sut.TriggerAllActionBars();

			// Assert
			_mockEconomy.Verify(_ => _.TriggerAllActionBars(It.IsAny<IItemChest>()), Times.Once);
		}
	}
}
