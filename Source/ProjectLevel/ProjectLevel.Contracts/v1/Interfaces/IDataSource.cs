using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Models;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface IDataSource
	{
		List<Loot> GetAvailableLoot();
	}
}
