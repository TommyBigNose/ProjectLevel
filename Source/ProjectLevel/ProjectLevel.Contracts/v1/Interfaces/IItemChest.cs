using ProjectLevel.Contracts.v1.Models;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface IItemChest
	{
		List<Loot> Inventory { get; }

		bool ContainsItemsForMilitaryType(Constants.MilitaryType militaryType);
		List<MilitaryLootStat> GetStatsForMilitaryType(Constants.MilitaryType militaryType);
	}
}