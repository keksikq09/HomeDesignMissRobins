using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace TMPro.Examples
{
	public class SkewTextExample : MonoBehaviour
	{
		private sealed class _003CWarpText_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public SkewTextExample _003C_003E4__this;

			private float _003Cold_CurveScale_003E5__2;

			private float _003Cold_ShearValue_003E5__3;

			private AnimationCurve _003Cold_curve_003E5__4;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CWarpText_003Ed__7(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				int num = _003C_003E1__state;
				SkewTextExample skewTextExample = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					skewTextExample.VertexCurve.preWrapMode = WrapMode.Once;
					skewTextExample.VertexCurve.postWrapMode = WrapMode.Once;
					skewTextExample.m_TextComponent.havePropertiesChanged = true;
					skewTextExample.CurveScale *= 10f;
					_003Cold_CurveScale_003E5__2 = skewTextExample.CurveScale;
					_003Cold_ShearValue_003E5__3 = skewTextExample.ShearAmount;
					_003Cold_curve_003E5__4 = skewTextExample.CopyAnimationCurve(skewTextExample.VertexCurve);
					break;
				case 1:
					_003C_003E1__state = -1;
					break;
				case 2:
					_003C_003E1__state = -1;
					break;
				}
				TMP_TextInfo textInfo;
				int characterCount;
				do
				{
					if (!skewTextExample.m_TextComponent.havePropertiesChanged && _003Cold_CurveScale_003E5__2 == skewTextExample.CurveScale && _003Cold_curve_003E5__4.keys[1].value == skewTextExample.VertexCurve.keys[1].value && _003Cold_ShearValue_003E5__3 == skewTextExample.ShearAmount)
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
					_003Cold_CurveScale_003E5__2 = skewTextExample.CurveScale;
					_003Cold_curve_003E5__4 = skewTextExample.CopyAnimationCurve(skewTextExample.VertexCurve);
					_003Cold_ShearValue_003E5__3 = skewTextExample.ShearAmount;
					skewTextExample.m_TextComponent.ForceMeshUpdate();
					textInfo = skewTextExample.m_TextComponent.textInfo;
					characterCount = textInfo.characterCount;
				}
				while (characterCount == 0);
				float x = skewTextExample.m_TextComponent.bounds.min.x;
				float x2 = skewTextExample.m_TextComponent.bounds.max.x;
				for (int i = 0; i < characterCount; i++)
				{
					if (textInfo.characterInfo[i].isVisible)
					{
						int vertexIndex = textInfo.characterInfo[i].vertexIndex;
						int materialReferenceIndex = textInfo.characterInfo[i].materialReferenceIndex;
						Vector3[] vertices = textInfo.meshInfo[materialReferenceIndex].vertices;
						Vector3 vector = new Vector2((vertices[vertexIndex].x + vertices[vertexIndex + 2].x) / 2f, textInfo.characterInfo[i].baseLine);
						vertices[vertexIndex] += -vector;
						vertices[vertexIndex + 1] += -vector;
						vertices[vertexIndex + 2] += -vector;
						vertices[vertexIndex + 3] += -vector;
						float num2 = skewTextExample.ShearAmount * 0.01f;
						Vector3 vector2 = new Vector3(num2 * (textInfo.characterInfo[i].topRight.y - textInfo.characterInfo[i].baseLine), 0f, 0f);
						Vector3 a = new Vector3(num2 * (textInfo.characterInfo[i].baseLine - textInfo.characterInfo[i].bottomRight.y), 0f, 0f);
						vertices[vertexIndex] += -a;
						vertices[vertexIndex + 1] += vector2;
						vertices[vertexIndex + 2] += vector2;
						vertices[vertexIndex + 3] += -a;
						float num3 = (vector.x - x) / (x2 - x);
						float num4 = num3 + 0.0001f;
						float y = skewTextExample.VertexCurve.Evaluate(num3) * skewTextExample.CurveScale;
						float y2 = skewTextExample.VertexCurve.Evaluate(num4) * skewTextExample.CurveScale;
						Vector3 lhs = new Vector3(1f, 0f, 0f);
						Vector3 rhs = new Vector3(num4 * (x2 - x) + x, y2) - new Vector3(vector.x, y);
						float num5 = Mathf.Acos(Vector3.Dot(lhs, rhs.normalized)) * 57.29578f;
						float z = (Vector3.Cross(lhs, rhs).z > 0f) ? num5 : (360f - num5);
						Matrix4x4 matrix4x = Matrix4x4.TRS(new Vector3(0f, y, 0f), Quaternion.Euler(0f, 0f, z), Vector3.one);
						vertices[vertexIndex] = matrix4x.MultiplyPoint3x4(vertices[vertexIndex]);
						vertices[vertexIndex + 1] = matrix4x.MultiplyPoint3x4(vertices[vertexIndex + 1]);
						vertices[vertexIndex + 2] = matrix4x.MultiplyPoint3x4(vertices[vertexIndex + 2]);
						vertices[vertexIndex + 3] = matrix4x.MultiplyPoint3x4(vertices[vertexIndex + 3]);
						vertices[vertexIndex] += vector;
						vertices[vertexIndex + 1] += vector;
						vertices[vertexIndex + 2] += vector;
						vertices[vertexIndex + 3] += vector;
					}
				}
				skewTextExample.m_TextComponent.UpdateVertexData();
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private TMP_Text m_TextComponent;

		public AnimationCurve VertexCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.25f, 2f), new Keyframe(0.5f, 0f), new Keyframe(0.75f, 2f), new Keyframe(1f, 0f));

		public float CurveScale = 1f;

		public float ShearAmount = 1f;

		private void Awake()
		{
			m_TextComponent = base.gameObject.GetComponent<TMP_Text>();
		}

		private void Start()
		{
			StartCoroutine(WarpText());
		}

		private AnimationCurve CopyAnimationCurve(AnimationCurve curve)
		{
			return new AnimationCurve
			{
				keys = curve.keys
			};
		}

		private IEnumerator WarpText()
		{
			return new _003CWarpText_003Ed__7(0)
			{
				_003C_003E4__this = this
			};
		}
	}
}
