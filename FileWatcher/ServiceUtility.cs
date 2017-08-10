using System;

namespace FileWatcher
{
	/// <summary>
	/// Summary description for ServiceUtility.
	/// </summary>
	public class ServiceUtility
	{
		public ServiceUtility()
		{
			m_strOp = null;
		}

		/// <summary>
		/// operation to be performed
		/// </summary>
		public string Op
		{
			get
			{
				return m_strOp;
			}
			set
			{
				m_strOp = value;
			}
		}

		//Data Members
		private string m_strOp;

		/// <summary>
		/// calculates operation on two operands
		/// </summary>
		public int Calculate(int a, int b)
		{
			switch (m_strOp)
			{
				case "+":
					return a + b;
				case "*":
					return a * b;
				case "-":
					return a - b;
				case "/":
					return a / b;
				default:
					break;
			}
			return 0;
		}
	}
}
