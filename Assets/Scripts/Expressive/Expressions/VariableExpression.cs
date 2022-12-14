using System;
using System.Collections.Generic;

namespace Expressive.Expressions
{
	internal class VariableExpression : IExpression
	{
		private readonly string _variableName;

		internal VariableExpression(string variableName)
		{
			_variableName = variableName;
		}

		public object Evaluate(IDictionary<string, object> variables)
		{
			if (variables == null || !variables.ContainsKey(_variableName))
			{
				throw new ArgumentException("The variable '" + _variableName + "' has not been supplied.");
			}
			Expression expression = variables[_variableName] as Expression;
			if (expression != null)
			{
				return expression.Evaluate(variables);
			}
			return variables[_variableName];
		}
	}
}
