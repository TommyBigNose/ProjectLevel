using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Models
{
	public class Civilization //: ICivilization
	{
		public Economy Economy { get; set; } = new Economy();
		public Military Military { get; set; } = new Military();
		public ItemChest ItemChest { get; set; }
		public Civilization(IDataSource dataSource)
		{
			ItemChest = new ItemChest(dataSource.GetAvailableLoot());
		}

		public void TriggerAllActionBars()
		{
			Economy.TriggerAllActionBars(ItemChest);
			Military.TriggerAllActionBars(ItemChest);
		}

		#region Economy

		#endregion

		#region Military

		#endregion

		#region Inventory
		
		#endregion
	}
}
