using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public interface IContainer
	{
		bool Contains(int value);
		bool IsValid();
		bool IsSolved();

		Values GetSolvedValues();

		List<int> Positions { get; }

		IStateManager StateManager { get; }
	}
}
