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
		public IMilitary Military { get; private set; }
		public ItemChest ItemChest { get; set; } = new ItemChest();

		public Civilization(IMilitary military)
		{
			Military = military;
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
