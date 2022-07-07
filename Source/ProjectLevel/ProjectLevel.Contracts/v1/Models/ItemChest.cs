using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Models
{
	public class ItemChest : IItemChest
	{
		public List<Loot> Inventory { get; private set; }

		public ItemChest()
		{
			Inventory = new List<Loot>();
		}

		public ItemChest(List<Loot> currentLoot)
		{
			Inventory = currentLoot;
		}

		public bool ContainsItemsForMilitaryType(MilitaryType militaryType)
		{
			bool result = false;

			foreach (Loot loot in Inventory)
			{
				if (loot.ContainsMilitaryLootStatsForType(militaryType))
				{
					result = true;
					break;
				}
			}

			return result;
		}

		public List<MilitaryLootStat> GetStatsForMilitaryType(MilitaryType militaryType)
		{
			List<MilitaryLootStat> result = new();

			foreach (Loot loot in Inventory)
			{
				if (loot.ContainsMilitaryLootStatsForType(militaryType))
				{
					result.Add(loot.GetMilitaryLootStatsForType(militaryType));
				}
			}

			return result;
		}
	}
}
