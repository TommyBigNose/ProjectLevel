using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface IMilitaryFactory
	{
		IMilitary BuildInitialMilitary();
		IMilitary BuildMilitary(int level);
	}
}
