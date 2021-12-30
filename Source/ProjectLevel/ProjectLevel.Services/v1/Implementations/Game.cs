using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;

namespace ProjectLevel.Services.v1.Implementations
{
	public class Game : IGame
	{
		private readonly IDataSource _dataSource;
		private ICivilization _civilization;

		public Game(IDataSource dataSource)
		{
			_dataSource = dataSource;

			_civilization = new Civilization();
		}
	}
}
