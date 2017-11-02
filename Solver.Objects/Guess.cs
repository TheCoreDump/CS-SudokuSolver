using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public class Guess
	{
		public Guess(BoardState state, int position, List<Values> initialValues)
		{
			State = state;
			Position = position;
			NextGuesses = new Stack<Values>();

			foreach (Values tmpValue in initialValues)
				NextGuesses.Push(tmpValue);
		}

		public BoardState State { get; private set; }

		public int Position { get; private set; }

		public Stack<Values> NextGuesses { get; set; }

	}
}
