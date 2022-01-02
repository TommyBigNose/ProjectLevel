﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Models
{
	public class Military
	{
		private int _meleeUnitCount;
		private int _rangedUnitCount;
		private int _siegeUnitCount;

		private int _meleeLevel;
		private int _rangedLevel;
		private int _siegeLevel;

		public ActionBar MeleeActionBar { get; set; }
		public ActionBar RangedActionBar { get; set; }
		public ActionBar SiegeActionBar { get; set; }

		public Military()
		{
			_meleeUnitCount = 1;
			_rangedUnitCount = 0;
			_siegeUnitCount = 0;

			_meleeLevel = 0;
			_rangedLevel = 0;
			_siegeLevel = 0;

			MeleeActionBar = new ActionBar();
			RangedActionBar = new ActionBar();
			SiegeActionBar = new ActionBar();
		}

		public int GetUnitCount(MilitaryType militaryType)
		{
			return militaryType switch
			{
				MilitaryType.Melee => _meleeUnitCount,
				MilitaryType.Ranged => _rangedUnitCount,
				MilitaryType.Siege => _siegeUnitCount,
				_ => 0,
			};
		}

		public int GetUnitLevel(MilitaryType militaryType)
		{
			return militaryType switch
			{
				MilitaryType.Melee => _meleeUnitCount,
				MilitaryType.Ranged => _rangedUnitCount,
				MilitaryType.Siege => _siegeUnitCount,
				_ => 0,
			};
		}

		public int GetUnitDamage(MilitaryType militaryType)
		{
			return militaryType switch
			{
				MilitaryType.Melee => _meleeUnitCount * _meleeLevel,
				MilitaryType.Ranged => _rangedUnitCount * _rangedLevel,
				MilitaryType.Siege => _siegeUnitCount * _siegeLevel,
				_ => 0,
			};
		}

		public ActionBar GetUnitActionBar(MilitaryType militaryType)
		{
			return militaryType switch
			{
				MilitaryType.Melee => MeleeActionBar,
				MilitaryType.Ranged => RangedActionBar,
				MilitaryType.Siege => SiegeActionBar,
				_ => new ActionBar(),
			};
		}

		public void UpgradeUnitLevel(MilitaryType militaryType)
		{
			switch (militaryType)
			{
				case MilitaryType.Melee:
					_meleeLevel++;
					break;
				case MilitaryType.Ranged:
					_rangedLevel++;
					break;
				case MilitaryType.Siege:
					_siegeLevel++;
					break;
			}
		}
	}
}