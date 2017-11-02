using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public abstract class ContainerBase : IContainer
	{

		#region Constructors

		public ContainerBase(IStateManager stateManager, Board board)
		{
			Positions = new List<int>();
			StateManager = stateManager;
			Board = board;
		}

		#endregion

		#region Properties

		public List<int> Positions { get; private set; }

		public IStateManager StateManager { get; private set; }

		public Board Board { get; private set; }

		#endregion

		#region IContainer Implementation

		#region Contains Function

		public bool Contains(int value)
		{
			foreach (int tmpPosition in Positions)
			{
				if (StateManager.GetCurrentState().IsSolved(tmpPosition))
				{
					if (((int)StateManager.GetCurrentState().GetValue(tmpPosition)) == value)
						return true;
				}
			}

			return false;
		}

		#endregion

		#region IsValid Function

		public bool IsValid()
		{
			int CumulativeFlag = 0;

			foreach (int tmpPosition in Positions)
			{
				if (StateManager.GetCurrentState().IsSolved(tmpPosition))
				{
					if ((((int)StateManager.GetCurrentState().GetValue(tmpPosition)) & CumulativeFlag) > 0)
						return false;
					else
						CumulativeFlag = ((int)StateManager.GetCurrentState().GetValue(tmpPosition)) | CumulativeFlag;
				}
			}

			return true;
		}

		#endregion

		#region IsSolved Function

		public bool IsSolved()
		{
			int CumulativeFlag = 0;

			foreach (int tmpPosition in Positions)
			{
				if (StateManager.GetCurrentState().IsSolved(tmpPosition))
				{
					if ((((int)StateManager.GetCurrentState().GetValue(tmpPosition)) & CumulativeFlag) > 0)
						return false;
					else
						CumulativeFlag = ((int)StateManager.GetCurrentState().GetValue(tmpPosition)) | CumulativeFlag;
				}
			}

			return CumulativeFlag == 0x1ff;
		}

		#endregion

		#region GetSolvedValues Function

		public Values GetSolvedValues()
		{
			Values SolvedValues = 0;

			foreach (int tmpPosition in Positions)
			{
				if (StateManager.GetCurrentState().IsSolved(tmpPosition))
					SolvedValues = SolvedValues | StateManager.GetCurrentState().GetValue(tmpPosition);
			}

			return SolvedValues;
		}

		#endregion

		#endregion

	}
}
