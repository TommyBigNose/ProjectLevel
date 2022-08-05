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
		private readonly IMilitaryFactory _militaryFactory;
		public IEconomy Economy { get; set; }
		public IMilitary Military { get; private set; }
		public IItemChest ItemChest { get; set; }

		public Civilization(IEconomy economy, IMilitaryFactory militaryFactory, IItemChest itemChest)
		{
			Economy = economy;
			_militaryFactory = militaryFactory;
			ItemChest = itemChest;

			Military = _militaryFactory.BuildInitialMilitary();
		}

		public void TriggerAllActionBars()
		{
			Economy.TriggerAllActionBars(ItemChest);
			Military.TriggerAllActionBars(ItemChest);
		}
	}
}
