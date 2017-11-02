using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public class InitializationData
	{
		private Dictionary<int, int> Values = new Dictionary<int, int>();

		private int CalculateIndex(int x, int y)
		{
			return (y * 9) + x;
		}

		public void AddValue(int x, int y, int value)
		{
			Values.Add(CalculateIndex(x, y), value);
		}

		public bool HasValue(int x, int y)
		{
			return Values.ContainsKey(CalculateIndex(x, y));
		}

		public Values GetValue(int x, int y)
		{
			int tmpValue = Values[CalculateIndex(x, y)];

			return (Values) (1 << (tmpValue - 1));
		}

		public void Load(int[][] initData)
		{
			int y = 0;

			foreach (int[] row in initData)
			{
				int x = 0;

				foreach (int colValue in row)
				{
					if (colValue > 0)
						Values[CalculateIndex(x, y)] = colValue;

					x++;
				}

				y++;
			}
		}

	}
}
