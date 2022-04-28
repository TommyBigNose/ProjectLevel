using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Models
{
    public class Loot
    {
        public string? Name { get; set; } = string.Empty;
        public string? ImageResourceString { get; set; } = string.Empty;
        public int Level { get; set; } = 0;
        public int GoldValue { get; set; } = 0;
        public int GoldIncome { get; set; } = 0;
        public float GoldSpeedRatio { get; set; } = 0.0f;
        public List<MilitaryLootStat> MilitaryLootStats { get; set; } = new List<MilitaryLootStat>();

        public bool ContainsMilitaryLootStatsForType(MilitaryType militaryType)
        {
            return MilitaryLootStats.Exists(_ => _.MilitaryType == militaryType);
        }

        public MilitaryLootStat GetMilitaryLootStatsForType(MilitaryType militaryType)
        {
            return MilitaryLootStats.First(_ => _.MilitaryType == militaryType);
        }
    }
}
