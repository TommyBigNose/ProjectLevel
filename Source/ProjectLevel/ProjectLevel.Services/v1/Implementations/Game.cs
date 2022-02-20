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
		private readonly Civilization _civilization;

		public Game(IDataSource dataSource)
		{
			_dataSource = dataSource;

			_civilization = new Civilization();
		}

		#region Data
		public List<Loot> GetAvailableLoot()
		{
			return _dataSource.GetAvailableLoot();
		}

		public List<Loot> GetLoot()
		{
			return _civilization.ItemChest.Inventory;
		}
		#endregion

		#region Civilization
		public void AddLoot(List<Loot> loot)
		{
			_civilization.ItemChest.Inventory.AddRange(loot);
		}
		#endregion

		#region Gold
		public int GetGold()
		{
			return _civilization.Economy.Gold;
		}

		public int GetGoldIncomeRate()
		{
			return _civilization.Economy.GoldLevel;
		}

		public int RequiredGoldToLevelUp()
		{
			return _civilization.Economy.RequiredGoldToLevelUp();
		}

		public bool CanUpgradeGoldLevel()
		{
			return _civilization.Economy.CanUpgradeGoldLevel();
		}

		public void UpgradeGoldLevel()
		{
			_civilization.Economy.UpgradeGoldLevel();
		}

		public float GetGoldActionBarValue()
		{
			return _civilization.Economy.GoldActionBar.Value;
		}
		#endregion

		#region Military
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

		public float GetMilitaryActionBarValue(MilitaryType militaryType)
		{
			return _civilization.Military.GetUnitActionBar(militaryType).Value;
		}

		public bool CanUpgradeMilitaryLevel(MilitaryType militaryType)
		{
			return _civilization.Economy.Gold >= _civilization.Military.RequiredGoldToLevelUp(militaryType);
		}

		public void UpgradeMilitaryLevel(MilitaryType militaryType)
		{
			_civilization.Economy.SpendGold(_civilization.Military.RequiredGoldToLevelUp(militaryType));
			_civilization.Military.UpgradeUnitLevel(militaryType);
		}

		public bool CanUpgradeMilitaryUnitCount(MilitaryType militaryType)
		{
			return _civilization.Economy.Gold >= _civilization.Military.RequiredGoldForNewUnit(militaryType);
		}

		public void UpgradeMilitaryUnitCount(MilitaryType militaryType)
		{
			_civilization.Economy.SpendGold(_civilization.Military.RequiredGoldForNewUnit(militaryType));
			_civilization.Military.UpgradeUnitCount(militaryType);
		}
		#endregion

		public void TriggerAllActionBars()
		{
			_civilization.TriggerAllActionBars();
			if(_civilization.Economy.GoldActionBar.IsReady())
			{
				_civilization.Economy.RecieveGoldIncome(_civilization.ItemChest);
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
		
	}
}
