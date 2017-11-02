using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{
	public class Utility
	{
		public static int CalculatePosition(int x, int y)
		{
			return (y * 9) + x;
		}

		public static int CalculateGroupIndex(int x, int y)
		{
			int GroupX = x / 3;
			int GroupY = y / 3;

			return (GroupY * 3) + GroupX;
		}

		public static int CalculateGroupSubIndex(int x, int y)
		{
			int GroupX = x % 3;
			int GroupY = y % 3;

			return (GroupY * 3) + GroupX;
		}

		public static int ValueToInt(Values value)
		{
			if (((int)value) == ((int)Values.One))
				return 1;

			if (((int)value) == ((int)Values.Two))
				return 2;

			if (((int)value) == ((int)Values.Three))
				return 3;

			if (((int)value) == ((int)Values.Four))
				return 4;

			if (((int)value) == ((int)Values.Five))
				return 5;

			if (((int)value) == ((int)Values.Six))
				return 6;

			if (((int)value) == ((int)Values.Seven))
				return 7;

			if (((int)value) == ((int)Values.Eight))
				return 8;

			if (((int)value) == ((int)Values.Nine))
				return 9;

			return 0;
		}

		public static List<Values> ExpandPossibleValues(Values possibleValues)
		{
			List<Values> Result = new List<Values>();

			if ((possibleValues & Values.One) > 0)
				Result.Add(Values.One);

			if ((possibleValues & Values.Two) > 0)
				Result.Add(Values.Two);

			if ((possibleValues & Values.Three) > 0)
				Result.Add(Values.Three);

			if ((possibleValues & Values.Four) > 0)
				Result.Add(Values.Four);

			if ((possibleValues & Values.Five) > 0)
				Result.Add(Values.Five);

			if ((possibleValues & Values.Six) > 0)
				Result.Add(Values.Six);

			if ((possibleValues & Values.Seven) > 0)
				Result.Add(Values.Seven);

			if ((possibleValues & Values.Eight) > 0)
				Result.Add(Values.Eight);

			if ((possibleValues & Values.Nine) > 0)
				Result.Add(Values.Nine);

			return Result;
		}
	}
}
