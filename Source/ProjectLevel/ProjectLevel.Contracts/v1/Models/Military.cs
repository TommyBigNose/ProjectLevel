using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Models
{
	public class Military
	{
		private readonly List<MilitaryUnit> _militaryUnitList;

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
					Count = 0,
					Level = 1
				},
				new MilitaryUnit()
				{
					MilitaryType = MilitaryType.Siege,
					Count = 0,
					Level = 1
				},
			};
		}

		public int GetUnitCount(MilitaryType militaryType)
		{
			return _militaryUnitList.First(_ => _.MilitaryType == militaryType).Count;
		}

		public int GetUnitLevel(MilitaryType militaryType)
		{
			return _militaryUnitList.First(_ => _.MilitaryType == militaryType).Level;
		}

		public int GetUnitDamage(MilitaryType militaryType)
		{
			return GetUnitCount(militaryType) * GetUnitLevel(militaryType);
		}

		public ActionBar GetUnitActionBar(MilitaryType militaryType)
		{
			return _militaryUnitList.First(_ => _.MilitaryType == militaryType).ActionBar;
		}

		public void TriggerAllActionBars(ItemChest itemChest)
        {
			foreach(MilitaryUnit unit in _militaryUnitList)
            {
				float ratio = 1.0f + itemChest.GetStatsForMilitaryType(unit.MilitaryType).Sum(_ => _.SpeedRatio);
				unit.ActionBar.IncrementActionBar(unit.Count * ratio);
            }
        }

		public int RequiredGoldToLevelUp(MilitaryType militaryType)
		{
			return GetUnitLevel(militaryType) * Constants.GoldPerLevelCost;
		}

		public void UpgradeUnitLevel(MilitaryType militaryType)
		{
			_militaryUnitList.First(_ => _.MilitaryType == militaryType).Level++;
		}

		public int RequiredGoldForNewUnit(MilitaryType militaryType)
		{
			return GetUnitCount(militaryType) * Constants.GoldPerLevelCost;
		}

		public void UpgradeUnitCount(MilitaryType militaryType)
		{
			_militaryUnitList.First(_ => _.MilitaryType == militaryType).Count++;
		}
	}
}
