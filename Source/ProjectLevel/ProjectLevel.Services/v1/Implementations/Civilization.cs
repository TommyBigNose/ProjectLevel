using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Services.v1.Implementations
{
	public class Civilization : ICivilization
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
	}
}
