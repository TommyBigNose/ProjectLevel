using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Models
{
    public class MilitaryUnit
    {
        public MilitaryType MilitaryType { get; set; }
        public int Count { get; set; }
        public int Level { get; set; }
        public ActionBar ActionBar { get; set; } = new ActionBar();
    }
}
