﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLevel.Contracts.v1.Interfaces
{
	public interface ICommand
	{
		void Execute();
		bool CanExecute();
		void Undo();
	}
}