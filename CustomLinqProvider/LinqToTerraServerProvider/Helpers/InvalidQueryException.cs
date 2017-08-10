using System;

namespace LinqToTerraServerProvider.Helpers
{
	class InvalidQueryException : Exception
	{
		private readonly string _message;

		public InvalidQueryException(string message)
		{
			_message = message + " ";
		}

		public override string Message
		{
			get
			{
				return "The client query is invalid: " + _message;
			}
		}
	}
}
