using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public class BoardState
	{

		#region Constructors

		public BoardState(InitializationData data)
		{
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					if (data.HasValue(x, y))
						SetValue(Utility.CalculatePosition(x, y), data.GetValue(x, y));
				}
			}
		}

		public BoardState(BoardState template)
		{
			foreach (int tmpKey in template.Data.Keys)
				Data.Add(tmpKey, template.Data[tmpKey]);
		}

		#endregion

		#region Properties

		private Dictionary<int, Values> Data = new Dictionary<int, Values>();

		#endregion

		#region Functions

		#region SetValue Function

		public void SetValue(int position, Values value)
		{
			if (!Data.ContainsKey(position))
				Data.Add(position, value);
			else
				Data[position] = value;
		}

		#endregion

		#region GetValue Function

		public Values GetValue(int position)
		{
			if (Data.ContainsKey(position))
				return Data[position];

			return 0;
		}

		#endregion

		#region IsSolved Function

		public bool IsSolved(int position)
		{
			return Values.Empty != ((Values) GetValue(position));
		}

		#endregion

		#endregion

	}
}
