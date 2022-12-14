using Expressive.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Functions.Statistical
{
	internal class ModeFunction : FunctionBase
	{
		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public int maxCount;

			internal bool _003CEvaluate_003Eb__2(IGrouping<object, object> g)
			{
				return Enumerable.Count(g) == maxCount;
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			public static Func<object, object> _003C_003E9__2_0;

			public static Func<IGrouping<object, object>, int> _003C_003E9__2_1;

			internal object _003CEvaluate_003Eb__2_0(object v)
			{
				return v;
			}

			internal int _003CEvaluate_003Eb__2_1(IGrouping<object, object> g)
			{
				return Enumerable.Count(g);
			}
		}

		public override string Name => "Mode";

		public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
		{
			_003C_003Ec__DisplayClass2_0 _003C_003Ec__DisplayClass2_ = new _003C_003Ec__DisplayClass2_0();
			ValidateParameterCount(parameters, -1, 1);
			IList<object> list = new List<object>();
			for (int i = 0; i < parameters.Length; i++)
			{
				object obj = parameters[i].Evaluate(base.Variables);
				IEnumerable enumerable = obj as IEnumerable;
				if (enumerable != null)
				{
					foreach (object item in enumerable)
					{
						list.Add(item);
					}
				}
				else
				{
					list.Add(obj);
				}
			}
			IEnumerable<IGrouping<object, object>> source = Enumerable.GroupBy(list, _003C_003Ec._003C_003E9._003CEvaluate_003Eb__2_0);
			_003C_003Ec__DisplayClass2_.maxCount = Enumerable.Max(source, _003C_003Ec._003C_003E9._003CEvaluate_003Eb__2_1);
			return Enumerable.First(source, _003C_003Ec__DisplayClass2_._003CEvaluate_003Eb__2).Key;
		}
	}
}
