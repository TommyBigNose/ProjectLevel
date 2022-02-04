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
		}

		[TearDown]
		public void TearDown()
		{
			
		}

		[TestCase(0, 0)]
		[TestCase(10, 1)]
		[TestCase(20, 2)]
		public void GetGold(int gameTicks, int expectedGold)
		{
			// Arrange
			// Act
			TriggerGameActionBars(gameTicks);
			var result = _sut.GetGold();

			// Assert
			Assert.IsTrue(result == expectedGold);
		}

		[Test]
		public void PlaceHolderTest()
		{
			// Arrange
			// Act
			// Assert
			Assert.IsTrue(true);
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
