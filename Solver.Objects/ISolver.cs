using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public interface ISolver
	{
		int Solve(Board data);
	}
}
