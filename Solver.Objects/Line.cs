using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public class Line : ContainerBase
	{

		#region Constructors

		public Line(int index, bool horizontal, IStateManager stateManager, Board board) 
			: base(stateManager, board)
		{
			Index = index;
			Horizontal = horizontal;

			for (int i = 0; i < 9; i++)
			{
				if (Horizontal)
					Positions.Add((Index * 9) + i);
				else
					Positions.Add((i * 9) + Index);
			}
		}

		#endregion

		#region Properties

		public int Index { get; private set; }

		public bool Horizontal { get; private set; }

		#endregion

	}
}
