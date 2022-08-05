using Moq;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Services.v1.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLevel.Tests.TestHelpers
{
	public static class MockCivilization
	{
		public static Mock<ICivilization> GetMockCivilization()
		{
			var mock = new Mock<ICivilization>();

			mock.Setup(_ => _.Economy)
				.Returns(It.IsAny<IEconomy>);

			mock.Setup(_ => _.Military)
				.Returns(It.IsAny<IMilitary>);

			mock.Setup(_ => _.ItemChest)
				.Returns(new ItemChest());
				//.Returns(It.IsAny<IItemChest>);

			mock.Setup(_ => _.TriggerAllActionBars());

			return mock;
		}
	}
}
