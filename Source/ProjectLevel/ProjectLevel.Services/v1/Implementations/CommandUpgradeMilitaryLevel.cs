using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;

namespace ProjectLevel.Services.v1.Implementations
{
	public  class CommandUpgradeMilitaryLevel : ICommand
	{
		private readonly IGame _game;
		private readonly Constants.MilitaryType _militaryType;

		public CommandUpgradeMilitaryLevel(IGame game, Constants.MilitaryType militaryType)
		{
			_game = game;
			_militaryType = militaryType;
		}

		public bool CanExecute()
		{
			return _game.CanUpgradeMilitaryLevel(_militaryType);
		}

		public void Execute()
		{
			_game.UpgradeMilitaryLevel(_militaryType);
		}
	}
}
