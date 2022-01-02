using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Services.v1.Implementations
{
	public class Game : IGame
	{
		private readonly IDataSource _dataSource;
		private Civilization _civilization;
		//private CommandManager _commandManager;

		public Game(IDataSource dataSource)
		{
			_dataSource = dataSource;

			_civilization = new Civilization();
			//_commandManager = new CommandManager();
		}

		//public ICivilization GetCivilization()
		//{
		//	return _civilization;
		//}
		//public Civilization GetCivilization()
		//{
		//	return _civilization;
		//}

		public int GetGold()
		{
			return _civilization.Economy.Gold;
		}

		public int GetGoldIncomeRate()
		{
			return _civilization.Economy.GoldLevel;
		}

		public int GetMilitaryUnitCount(MilitaryType militaryType)
		{
			return _civilization.Military.GetUnitCount(militaryType);
		}

		public int GetMilitaryLevel(MilitaryType militaryType)
		{
			return _civilization.Military.GetUnitLevel(militaryType);
		}

		public int GetMilitaryDamage(MilitaryType militaryType)
		{
			return _civilization.Military.GetUnitDamage(militaryType);
		}

		public void TriggerAllActionBars()
		{
			_civilization.TriggerAllActionBars();
			if(_civilization.Economy.GoldActionBar.IsReady())
			{
				_civilization.Economy.RecieveGoldIncome();
			}

			foreach(MilitaryType militaryType in Enum.GetValues(typeof(MilitaryType)))
			{
				var actionBar = _civilization.Military.GetUnitActionBar(militaryType);
				if (actionBar.IsReady())
				{
					actionBar.ResetActionBar();
				}
			}
		}

		public float GetGoldActionBarValue()
		{
			return _civilization.Economy.GoldActionBar.Value;
		}

		public float GetMilitaryActionBarValue(MilitaryType militaryType)
		{
			return _civilization.Military.GetUnitActionBar(militaryType).Value;
		}
	}
}
