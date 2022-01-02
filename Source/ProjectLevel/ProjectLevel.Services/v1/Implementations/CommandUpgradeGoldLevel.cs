using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;

namespace ProjectLevel.Services.v1.Implementations
{
	public class CommandUpgradeGoldLevel : ICommand
	{
		private readonly IGame _game;

		public CommandUpgradeGoldLevel(IGame game)
		{
			_game = game;
		}

		public bool CanExecute()
		{
			return _game.CanUpgradeGoldLevel();
		}

		public void Execute()
		{
			_game.UpgradeGoldLevel();
		}
	}
}
