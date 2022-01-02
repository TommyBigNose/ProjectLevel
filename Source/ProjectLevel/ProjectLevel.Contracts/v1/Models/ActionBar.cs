using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1;
using ProjectLevel.Contracts.v1.Interfaces;

namespace ProjectLevel.Contracts.v1.Models
{
	public class ActionBar
	{
		public float Value { get; set; }

		public ActionBar()
		{

		}

		public bool IsReady()
		{
			return Value >= Constants.ActionBarMax;
		}

		public void IncrementActionBar(float increment)
		{
			Value += increment;
			if (Value >= Constants.ActionBarMax) Value = Constants.ActionBarMax;
		}

		public void ResetActionBar()
		{
			Value = 0.0f;
		}
	}
}
