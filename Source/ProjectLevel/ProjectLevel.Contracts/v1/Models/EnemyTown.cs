using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLevel.Contracts.v1.Models
{
	public class EnemyTown
	{
        public string? Name { get; set; } = string.Empty;
        public string? ImageResourceString { get; set; } = string.Empty;
        public int Level { get; set; } = 0;
        public int GoldValue { get; set; } = 0;
        public Military Military { get; set; } = new Military();

        public EnemyTown()
		{

		}
    }
}
