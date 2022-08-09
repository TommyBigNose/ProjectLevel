using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using static System.Net.Mime.MediaTypeNames;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Services.v1.Implementations
{
	public class Military : IMilitary
	{
		private IEnumerable<MilitaryUnit> _militaryUnitList;

		public Military()
		{
			_militaryUnitList = new List<MilitaryUnit>()
			{
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Melee,
					Count = 1,
					Level = 1
				},
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Ranged,
					Count = 1,
					Level = 1
				},
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Siege,
					Count = 1,
					Level = 1
				},
			};
		}

		public Military(IEnumerable<MilitaryUnit> militaryUnitList)
		{
			_militaryUnitList = militaryUnitList;
		}

		public void SetupMilitaryUnits(IEnumerable<MilitaryUnit> militaryUnits)
		{
			_militaryUnitList = militaryUnits;
		}

		public int GetUnitCount(MilitaryType militaryType)
		{
			return _militaryUnitList.First(_ => _.MilitaryType == militaryType).Count;
		}

		public int GetUnitLevel(MilitaryType militaryType)
		{
			return _militaryUnitList.First(_ => _.MilitaryType == militaryType).Level;
		}

		public int GetBaseUnitDamage(MilitaryType militaryType)
		{
			return GetUnitCount(militaryType) * GetUnitLevel(militaryType);
		}

		public int GetTotalUnitDamage(MilitaryType militaryType, IItemChest itemChest)
		{
			float ratio = 1.0f + itemChest.GetStatsForMilitaryType(militaryType).Sum(_ => _.DamageRatio);
			double result = Math.Round(GetBaseUnitDamage(militaryType) * ratio);
			return (int)result;
		}

		public int RequiredGoldToLevelUp(MilitaryType militaryType)
		{
			return GetUnitLevel(militaryType) * GoldPerLevelCost;
		}

		public void UpgradeUnitLevel(MilitaryType militaryType)
		{
			_militaryUnitList.First(_ => _.MilitaryType == militaryType).Level++;
		}

		public int RequiredGoldForNewUnit(MilitaryType militaryType)
		{
			return GetUnitCount(militaryType) * GoldPerLevelCost;
		}

		public void UpgradeUnitCount(MilitaryType militaryType)
		{
			_militaryUnitList.First(_ => _.MilitaryType == militaryType).Count++;
		}

		public ActionBar GetUnitActionBar(MilitaryType militaryType)
		{
			return _militaryUnitList.First(_ => _.MilitaryType == militaryType).ActionBar;
		}

		public void TriggerAllActionBars(IItemChest itemChest)
		{
			foreach (MilitaryUnit unit in _militaryUnitList)
			{
				float ratio = 1.0f + itemChest.GetStatsForMilitaryType(unit.MilitaryType).Sum(_ => _.SpeedRatio);
				unit.ActionBar.IncrementActionBar(unit.Count * ratio);
			}
		}
	}
}
