using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLevel.Contracts.v1.Interfaces;

namespace ProjectLevel.Services.v1.Implementations
{
	public class CommandManager
	{
		private readonly Stack<ICommand> commands = new();

		public void Invoke(ICommand command)
		{
			if (command.CanExecute())
			{
				commands.Push(command);
				command.Execute();
			}
		}

		//public void Undo()
		//{
		//	if (commands.Count > 0)
		//	{
		//		ICommand command = commands.Pop();
		//		//command.Undo();
		//	}
		//}
	}
}
