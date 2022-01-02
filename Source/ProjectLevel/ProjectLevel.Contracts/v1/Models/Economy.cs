using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;

namespace ProjectLevel.Contracts.v1.Models
{
	public class Economy
	{
		public int Gold { get; set; }
		public int GoldLevel { get; set; }

		public Economy()
		{
			Gold = 0;
			GoldLevel = 1;
		}

		public void RecieveGoldIncome()
		{
			Gold += GoldLevel;
		}
		//public int GetGold()
		//{
		//	return _gold;
		//}

		//public int GetGoldIncomeRate()
		//{
		//	return _goldLevel;
		//}

		public void UpgradeGoldLevel()
		{
			GoldLevel++;
		}
	}
}
