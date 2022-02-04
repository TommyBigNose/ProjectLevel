using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Models
{
	public class ItemChest
	{
		public List<Loot> Inventory { get; set; }

		public ItemChest(List<Loot> allAvailableLoot)
		{
			Inventory = allAvailableLoot;
		}

		public bool ContainsItemsForMilitaryType(MilitaryType militaryType)
		{
			bool result = false;

			foreach(Loot loot in Inventory)
			{
				if(loot.ContainsMilitaryLootStatsForType(militaryType))
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
