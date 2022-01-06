using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Models
{
    public class MilitaryLootStat
    {
        public MilitaryType MilitaryType { get; set; }
        public float DamageRatio { get; set; } = 0.0f;
        public float RecruitRatio { get; set; } = 0.0f;
        public float SpeedRatio { get; set; } = 0.0f;
    }
}
