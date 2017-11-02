using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Objects
{

	public class Square
	{

		#region Constructors

		public Square(int position) 
		{
			Value = Values.Empty;
			Position = position;
		}

		public Square(int position, Values value) 
		{
			Value = value;
			Position = position;
		}

		#endregion

		#region Properties

		public int Position { get; private set; }

		public Values Value { get; private set; }

		#endregion

		public bool IsSolved
		{
			get { return Value != Values.Empty; }
		}

		public void Clear()
		{
			Value = Values.Empty;
		}

		public void SetValue(Values value)
		{
			if (IsSolved)
				throw new ApplicationException(string.Format("Square in position {0} has already been solved", Position));

			Value = value;
		}
	}
}
