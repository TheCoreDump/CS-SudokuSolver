using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public class Group : ContainerBase
	{

		public Group(int groupNumber, IStateManager stateManager, Board board)
			: base(stateManager, board)
		{
			GroupNumber = groupNumber;

			int x = groupNumber % 3;
			int y = groupNumber / 3;

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
					Positions.Add(Utility.CalculatePosition((x * 3) + i, (y * 3) + j));
			}
		}

		public int GroupNumber { get; private set; }

	}
}
