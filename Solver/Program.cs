using System;
using System.IO;
using System.Collections.Generic;
using Solver.Objects;

namespace Solver
{
	class Program
	{
		static void Main(string[] args)
		{

            int[][] Data = new int[][] { new int[] {0, 0, 0,  9, 0, 0,  5, 0, 4},
										 new int[] {0, 0, 0,  0, 3, 5,  8, 0, 0},
										 new int[] {0, 0, 5,  0, 0, 7,  0, 0, 0},
																	 
										 new int[] {1, 0, 3,  0, 8, 0,  0, 0, 0},
										 new int[] {2, 0, 8,  0, 0, 0,  1, 0, 9},
										 new int[] {0, 0, 0,  0, 2, 0,  4, 0, 6},

										 new int[] {0, 0, 0,  5, 0, 0,  6, 0, 0},
										 new int[] {0, 0, 2,  4, 9, 0,  0, 0, 0},
										 new int[] {7, 0, 1,  0, 0, 2,  0, 0, 0} };
			/*
			int[][] Data = new int[][] { new int[] {0, 8, 0,  4, 0, 1,  0, 0, 0},
																	 new int[] {3, 0, 0,  0, 0, 9,  6, 0, 0},
																	 new int[] {0, 0, 0,  0, 0, 0,  0, 0, 7},
																	 
																	 new int[] {0, 0, 5,  0, 0, 0,  2, 0, 0},
																	 new int[] {6, 0, 0,  0, 3, 0,  0, 0, 9},
																	 new int[] {0, 0, 7,  0, 0, 0,  1, 0, 0},

																	 new int[] {9, 0, 0,  0, 0, 0,  0, 0, 0},
																	 new int[] {0, 0, 1,  5, 0, 0,  0, 0, 4},
																	 new int[] {0, 0, 0,  2, 0, 8,  0, 3, 0} };

			 int[][] Data = new int[][] { new int[] {0, 6, 8,  0, 0, 0,  3, 0, 0},
																	 new int[] {0, 5, 0,  6, 0, 8,  0, 4, 0},
																	 new int[] {0, 2, 0,  3, 4, 0,  1, 0, 6},
																	 
																	 new int[] {9, 0, 2,  1, 3, 0,  0, 0, 0},
																	 new int[] {1, 0, 0,  5, 0, 9,  0, 0, 4},
																	 new int[] {0, 0, 0,  0, 7, 6,  9, 0, 8},

																	 new int[] {6, 0, 3,  0, 2, 7,  0, 9, 0},
																	 new int[] {0, 4, 0,  9, 0, 1,  0, 2, 0},
																	 new int[] {0, 0, 5,  0, 0, 0,  8, 7, 0} };
			*/

			InitializationData initData = new InitializationData();
			initData.Load(Data);

			BoardState state = new BoardState(initData);

			StateManager stateManager = new StateManager(state);

			Board tmpBoard = new Board(stateManager);

			using (FileStream FS = new FileStream("C:\\Solver\\Version1.txt", FileMode.Create, FileAccess.Write))
			{
				using (StreamWriter SW = new StreamWriter(FS))
				{
					tmpBoard.Print(SW);
				}
			}

			SimpleSolver solver = new SimpleSolver();
			solver.Solve(tmpBoard);

			Stack<Guess> GuessStack = new Stack<Guess>();

			while (!tmpBoard.IsSolved())
			{
				if (tmpBoard.IsValid())
				{
					// Make a guess.  
					Guess newGuess = MakeGuess(stateManager.CurrentState, tmpBoard);
					GuessStack.Push(newGuess);

					tmpBoard.StateManager.GetCurrentState().SetValue(newGuess.Position, newGuess.NextGuesses.Pop());
				}
				else
				{
					Guess tmpGuess = GuessStack.Peek();

					while (tmpGuess.NextGuesses.Count == 0)
					{
						GuessStack.Pop();
						tmpGuess = GuessStack.Peek();
					}

					stateManager.SetCurrentState(tmpGuess.State);
					tmpBoard.StateManager.GetCurrentState().SetValue(tmpGuess.Position, tmpGuess.NextGuesses.Pop());
				}

				solver.Solve(tmpBoard);
			}


			using (FileStream FS = new FileStream("C:\\Solver\\Version2.txt", FileMode.Create, FileAccess.Write))
			{
				using (StreamWriter SW = new StreamWriter(FS))
				{
					tmpBoard.Print(SW);
				}
			}

		}

		#region MakeGuess Function

		public static Guess MakeGuess(BoardState state, Board board)
		{
			Dictionary<int, List<Values>> PossibleValues = new Dictionary<int, List<Values>>();

			for (int i = 0; i < 81; i++)
			{
				if (!state.IsSolved(i))
					PossibleValues.Add(i, Utility.ExpandPossibleValues(board.GetPossibleValues(i)));
			}

			int MinNumValues = 10;
			int MinNumPosition = -1;

			foreach (int tmpKey in PossibleValues.Keys)
			{
				if (PossibleValues[tmpKey].Count < MinNumValues)
				{
					MinNumValues = PossibleValues[tmpKey].Count;
					MinNumPosition = tmpKey;
				}
			}

			if (MinNumPosition == -1)
				throw new ApplicationException("Unable to find minimum position for guess");

			return new Guess(new BoardState(state), MinNumPosition, PossibleValues[MinNumPosition]);
		}

		#endregion

	}
}
