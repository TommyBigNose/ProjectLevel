using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using ProjectLevel.Services.v1.Implementations;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Tests.TestHelpers
{
	public static class MockMilitaryFactory
	{
		public static Mock<IMilitaryFactory> GetMockMilitaryFactory(int buildMilitaryLevel)
		{
			var mock = new Mock<IMilitaryFactory>();

			mock.Setup(_ => _.BuildInitialMilitary())
				.Returns(GetMilitary(1));

			mock.Setup(_ => _.BuildMilitary(It.Is<int>(level => level == buildMilitaryLevel)))
				.Returns(GetMilitary(buildMilitaryLevel));

			mock.Setup(_ => _.BuildMilitaryUnits(It.Is<int>(level => level == buildMilitaryLevel)))
				.Returns(GetMilitaryUnits(buildMilitaryLevel));

			return mock;
		}

		public static IMilitary GetMilitary(int level)
		{
			return new Military(GetMilitaryUnits(level));
		}

		public static IEnumerable<MilitaryUnit> GetMilitaryUnits(int level)
		{
			var militaryUnits = new List<MilitaryUnit>()
			{
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Melee,
					Count = level,
					Level = level
				},
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Ranged,
					Count = level,
					Level = level
				},
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Siege,
					Count = level,
					Level = level
				},
			};

			return militaryUnits;
		}
	}
}
