using Expressive.Expressions;

namespace Expressive.Operators.Relational
{
	internal class LessThanOperator : OperatorBase
	{
		public override string[] Tags => new string[1]
		{
			"<"
		};

		public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
		{
			return new BinaryExpression(BinaryExpressionType.LessThan, expressions[0], expressions[1], options);
		}

		public override OperatorPrecedence GetPrecedence(Token previousToken)
		{
			return OperatorPrecedence.LessThan;
		}
	}
}
