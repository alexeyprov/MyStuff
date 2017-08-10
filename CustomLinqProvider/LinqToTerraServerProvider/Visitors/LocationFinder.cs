using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToTerraServerProvider.BusinessEntities;
using LinqToTerraServerProvider.Helpers;

namespace LinqToTerraServerProvider.Visitors
{
	internal class LocationFinder : ExpressionVisitor
	{
		private readonly Expression _expression;
		private List<string> _locations;

		public LocationFinder(Expression exp)
		{
			_expression = exp;
		}

		public List<string> Locations
		{
			get
			{
				if (null == _locations)
				{
					_locations = new List<string>();
					Visit(_expression);
				}
				return _locations;
			}
		}

		protected override Expression VisitBinary(BinaryExpression be)
		{
			if (be.NodeType == ExpressionType.Equal)
			{
				if (ExpressionTreeHelper.IsMemberEqualsValueExpression(be, typeof(Place), "Name"))
				{
					_locations.Add(ExpressionTreeHelper.GetValueFromEqualsExpression(be, typeof(Place), "Name"));
					return be;
				}
				
				if (ExpressionTreeHelper.IsMemberEqualsValueExpression(be, typeof(Place), "State"))
				{
					_locations.Add(ExpressionTreeHelper.GetValueFromEqualsExpression(be, typeof(Place), "State"));
					return be;
				}
			}
			
			return base.VisitBinary(be);
		}
	}
}
