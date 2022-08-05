using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface ICivilization
	{
		IEconomy Economy { get; }
		IMilitary Military { get; }
		IItemChest ItemChest { get; }
		void TriggerAllActionBars();
	}
}
