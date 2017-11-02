using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public class StateManager : IStateManager
	{

		public StateManager(BoardState state)
		{
			CurrentState = state;
		}

		public BoardState CurrentState { get; set; }

		public BoardState GetCurrentState()
		{
			return CurrentState;
		}

		public void SetCurrentState(BoardState state)
		{
			CurrentState = state;
		}

	}
}
