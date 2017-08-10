using System;
using System.EnterpriseServices;
using System.Reflection;


[assembly: ApplicationName("My Component")]
[assembly: AssemblyKeyFileAttribute("MyComponent.snk")]
[assembly: ApplicationActivation(ActivationOption.Server)]

namespace ComPlusTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	/// 

	[Transaction(TransactionOption.Required)]
	public class MyComponent : ServicedComponent
	{
		public MyComponent()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[AutoComplete]
		public void Call( string message )
		{
			Console.WriteLine("Callee called: " + message);
		}

	}
}
