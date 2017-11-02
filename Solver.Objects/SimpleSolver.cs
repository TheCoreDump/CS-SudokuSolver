using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public class SimpleSolver : ISolver
	{

		#region ISolver Members

		public int Solve(Board data)
		{
			bool redo = true;

			while (redo)
			{
				redo = false;
				// Check to see if there is only one possible value in any position.
				for (int i = 0; i < 81; i++)
				{
					if (!data.StateManager.GetCurrentState().IsSolved(i))
					{
						Values PossibleValues = data.GetPossibleValues(i);

						if (Utility.ValueToInt(PossibleValues) > 0)
						{
							data.StateManager.GetCurrentState().SetValue(i, PossibleValues);
							redo = true;
						}
					}
				}


				for (int i = 0; i < 9; i++)
				{
					Dictionary<int, List<Values>> RowValues = data.GetPossibleValuesForContainer(data.Rows[i]);
					Dictionary<int, List<Values>> ColumnValues = data.GetPossibleValuesForContainer(data.Columns[i]);
					Dictionary<int, List<Values>> GroupValues = data.GetPossibleValuesForContainer(data.Groups[i]);

					redo = redo | GetSolvedPositionsByContainer(data, RowValues);
					redo = redo | GetSolvedPositionsByContainer(data, ColumnValues);
					redo = redo | GetSolvedPositionsByContainer(data, GroupValues);
				}
			}


			return 0;
		}

		#endregion

		#region GetSolvedPositionsByContainer Function

		public bool GetSolvedPositionsByContainer(Board board, Dictionary<int, List<Values>> data)
		{
			bool Result = false;
			Dictionary<Values, int> Counts = new Dictionary<Values, int>();

			foreach (int tmpPosition in data.Keys)
			{
				foreach (Values tmpValue in data[tmpPosition])
				{
					if (!Counts.ContainsKey(tmpValue))
						Counts.Add(tmpValue, 0);

					Counts[tmpValue]++;
				}
			}

			foreach (Values tmpKey in Counts.Keys)
			{
				if (Counts[tmpKey] == 1)
				{
					foreach (int tmpPosition in data.Keys)
					{
						if (data[tmpPosition].Contains(tmpKey))
						{
							board.StateManager.GetCurrentState().SetValue(tmpPosition, tmpKey);
							Result = true;
						}
					}
				}
			}

			return Result;
		}

		#endregion

	}
}
