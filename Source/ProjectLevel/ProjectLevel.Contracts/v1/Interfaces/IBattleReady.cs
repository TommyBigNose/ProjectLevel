using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Models;
using static ProjectLevel.Contracts.v1.Constants;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface IBattleReady
	{
		string? Name { get; }
		string? ImageResourceString { get; }
		int Level { get; }
		int HpCurrent { get; }
		int HpMax { get; }
		int GoldValue { get; }
		Loot Loot { get; }
		int CalculateDamage(int attackDamage, MilitaryType militaryType);
		void ApplyDamage(int attackDamage);
		bool IsTownDestroyed();
	}
}
