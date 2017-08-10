using System;
using System.Reflection;

using Ciloci.Flee;

// This program uses Flee to dynamically evaluate a property of an unknown type
// It uses the on-demand variable feature to supply the values of the properties as they are required
namespace DynamicFlee
{
	class Program
	{
		private static object _entity;
		private static Type _entityType;

		static Program()
		{
			_entity = new BusinessEntity()
			{
				Name = "SampleEntity",
				Flag = 3
			};

			_entityType = _entity.GetType();
		}

		private static void Main(string[] args)
		{
			ExpressionContext context = new ExpressionContext();
			// Use string.format
			context.Imports.AddType(typeof(string));

			// Use on demand variables to provide the values for the columns
			context.Variables.ResolveVariableType += new EventHandler<ResolveVariableTypeEventArgs>(Variables_ResolveVariableType);
			context.Variables.ResolveVariableValue += new EventHandler<ResolveVariableValueEventArgs>(Variables_ResolveVariableValue);

			// Create the expression; Flee will now query for the types of ItemName, Price, and Tax
			IDynamicExpression ex1 = context.CompileDynamic("(Flag and 2) <> 0");
			IDynamicExpression ex2 = context.CompileDynamic("(Flag and 4) <> 0");

			Console.WriteLine("\"{0}\" is evaluated as {1}", ex1.Text, ex1.Evaluate());
			Console.WriteLine("\"{0}\" is evaluated as {1}", ex2.Text, ex2.Evaluate());
		}

		private static void Variables_ResolveVariableType(object sender, ResolveVariableTypeEventArgs e)
		{
			PropertyInfo pi = _entityType.GetProperty(e.VariableName, BindingFlags.ExactBinding | BindingFlags.Public | BindingFlags.Instance);

			if (pi != null)
			{
				e.VariableType = pi.PropertyType;
			}
		}

		private static void Variables_ResolveVariableValue(object sender, ResolveVariableValueEventArgs e)
		{
			PropertyInfo pi = _entityType.GetProperty(e.VariableName, BindingFlags.ExactBinding | BindingFlags.Public | BindingFlags.Instance);

			if (pi != null)
			{
				e.VariableValue = pi.GetValue(_entity, null);
			}
		}
	}
}
