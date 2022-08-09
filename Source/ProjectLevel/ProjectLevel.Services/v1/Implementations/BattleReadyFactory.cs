using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;

namespace ProjectLevel.Services.v1.Implementations
{
	public class BattleReadyFactory : IBattleReadyFactory
	{
		private IMilitaryFactory _militaryFactory;

		public BattleReadyFactory(IMilitaryFactory militaryFactory)
		{
			_militaryFactory = militaryFactory;
		}

		public IBattleReady BuildBattleReady(int level)
		{
			Loot loot = new();
			IBattleReady battleReady = new BattleReadyEnemyTown($"EnemyLv{level}", level, loot, _militaryFactory.BuildMilitary(level));

			return battleReady;
		}
	}
}
