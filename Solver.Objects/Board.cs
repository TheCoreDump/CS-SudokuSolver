using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public class Board
	{

		#region Constructions

		public Board(StateManager data)
		{
			Rows = new Dictionary<int, Line>();
			Columns = new Dictionary<int, Line>();
			Groups = new Dictionary<int, Group>();

			StateManager = data;

			BuildBoard(data);
		}

		#endregion

		#region Properties

		public Dictionary<int, Line> Rows { get; private set; }

		public Dictionary<int, Line> Columns { get; private set; }

		public Dictionary<int, Group> Groups { get; private set; }

		public IStateManager StateManager { get; private set; }

		#endregion

		#region Methods

		#region BuildBoard Function

		private void BuildBoard(StateManager data)
		{
			for (int i = 0; i < 9; i++)
			{
				Rows.Add(i, new Line(i, true, data, this));
				Columns.Add(i, new Line(i, false, data, this));
				Groups.Add(i, new Group(i, data, this));
			}
		}

		#endregion

		#region IsSolved Function

		public bool IsSolved()
		{
			for (int i = 0; i < 81; i++)
			{
				if (!StateManager.GetCurrentState().IsSolved(i))
					return false;
			}

			return true;
		}

		#endregion

		#region Print Function

		public void Print(TextWriter writer)
		{
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					for (int k = 0; k < 9; k++)
					{
						int position = Utility.CalculatePosition(k, i);

						if (StateManager.GetCurrentState().IsSolved(position))
						{
							writer.Write("{0}{0}{0}", Utility.ValueToInt(StateManager.GetCurrentState().GetValue(position)));
						}
						else
						{
							Values possibleValues = GetPossibleValues(position);

							switch (j)
							{
								case 0:
									writer.Write("{0}{1}{2}",
										(possibleValues & Values.One) == Values.One ? "1" : "*",
										(possibleValues & Values.Two) == Values.Two ? "2" : "*",
										(possibleValues & Values.Three) == Values.Three ? "3" : "*");
									break;
								case 1:
									writer.Write("{0}{1}{2}",
										(possibleValues & Values.Four) == Values.Four ? "4" : "*",
										(possibleValues & Values.Five) == Values.Five ? "5" : "*",
										(possibleValues & Values.Six) == Values.Six ? "6" : "*");
									break;
								case 2:
									writer.Write("{0}{1}{2}",
										(possibleValues & Values.Seven) == Values.Seven ? "7" : "*",
										(possibleValues & Values.Eight) == Values.Eight ? "8" : "*",
										(possibleValues & Values.Nine) == Values.Nine ? "9" : "*");
									break;
							}
						}

						writer.Write(" ");

						if ((k % 3) == 2)
							writer.Write(" ");
					}

					writer.WriteLine();
				}

				writer.WriteLine();

				if ((i % 3) == 2)
					writer.WriteLine();
			}
		}

		#endregion

		#region GetPossibleValues Function

		public Values GetPossibleValues(int position)
		{
			Line Row = GetRow(position);
			Line Column = GetColumn(position);
			Group Group = GetGroup(position);

			Values SolvedValues = Row.GetSolvedValues();
			SolvedValues = SolvedValues | Column.GetSolvedValues();
			SolvedValues = SolvedValues | Group.GetSolvedValues();

			return (Values) (((int) SolvedValues) ^ 0x1FF);
		}

		#endregion

		#region GetPossibleValuesForContainer Function

		public Dictionary<int, List<Values>> GetPossibleValuesForContainer(IContainer container)
		{
			Dictionary<int, List<Values>> Result = new Dictionary<int, List<Values>>();

			foreach (int tmpPosition in container.Positions)
			{
				if (!StateManager.GetCurrentState().IsSolved(tmpPosition))
				{
					Result.Add(tmpPosition, Utility.ExpandPossibleValues(GetPossibleValues(tmpPosition)));
				}
			}

			return Result;
		}

		#endregion

		#region IsValid Function

		public bool IsValid()
		{
			for (int i = 0; i < 81; i++)
			{
				if (!StateManager.GetCurrentState().IsSolved(i))
				{
					if (GetPossibleValues(i) == Values.Empty)
						return false;
				}
			}

			return true;
		}

		#endregion

		#region GetInvalidValues Function

		public Values GetInvalidValues(int position)
		{
			return Values.Empty;
		}

		#endregion

		#region GetRow Function

		public Line GetRow(int position)
		{
			return Rows[position / 9];
		}

		#endregion

		#region GetColumn Function

		public Line GetColumn(int position)
		{
			return Columns[position % 9];
		}

		#endregion

		#region GetGroup Function

		public Group GetGroup(int position)
		{
			return Groups[(((position / 9) / 3) * 3) + (position % 9) / 3];
		}

		#endregion

		#endregion

	}
}
