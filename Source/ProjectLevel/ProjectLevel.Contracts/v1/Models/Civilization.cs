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
		public IEconomy Economy { get; set; }
		public IMilitary Military { get; private set; }
		public IItemChest ItemChest { get; set; }

		public Civilization(IEconomy economy, IMilitary military, IItemChest itemChest)
		{
			Economy = economy;
			Military = military;
			ItemChest = itemChest;
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
