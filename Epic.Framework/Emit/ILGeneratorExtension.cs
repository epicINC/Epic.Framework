using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Epic.Emit
{
	public static class ILGeneratorExtension
	{
		#region Load
		
		public static ILGenerator Ldtoken<T>(this ILGenerator il)
		{
			return Ldtoken(il, typeof(T));
		}

		public static ILGenerator Ldtoken(this ILGenerator il, Type value)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Ldtoken, value);
			return il;
		}


		/// <summary>
		/// 将索引为 0 的参数加载到计算堆栈上。
		/// </summary>
		/// <returns></returns>
		public static ILGenerator Ldarg(this ILGenerator il)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Ldarg_0);
			return il;

		}

		/// <summary>
		/// 将参数（由指定索引值引用）加载到堆栈上。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public static ILGenerator Ldarg(this ILGenerator il, int index)
		{
			if (il == null) return null;

			switch (index)
			{
				case 0:
					il.Emit(OpCodes.Ldarg_0);
					break;
				case 1:
					il.Emit(OpCodes.Ldarg_1);
					break;
				case 2:
					il.Emit(OpCodes.Ldarg_2);
					break;
				case 3:
					il.Emit(OpCodes.Ldarg_3);
					break;
				default:
					if (index < 128)
						il.Emit(OpCodes.Ldarg_S, index);
					else
						il.Emit(OpCodes.Ldarg, index);
					break;
			}
			return il;
		}



		public static ILGenerator Ldfld(this ILGenerator il, FieldBuilder value)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Ldfld, value);
			return il;
		}

		/// <summary>
		/// 将整数值 0 作为 int32 推送到计算堆栈上。
		/// </summary>
		/// <returns></returns>
		public static ILGenerator Ldc(this ILGenerator il)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Ldc_I4_0);
			return il;
		}



		/// <summary>
		/// 将 参数 作为 int32 推送到计算堆栈上。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public static ILGenerator Ldc(this ILGenerator il, int index)
		{
			if (il == null) return null;

			switch (index)
			{
				case 0:
					il.Emit(OpCodes.Ldc_I4_0);
					break;
				case 1:
					il.Emit(OpCodes.Ldc_I4_1);
					break;
				case 2:
					il.Emit(OpCodes.Ldc_I4_2);
					break;
				case 3:
					il.Emit(OpCodes.Ldc_I4_3);
					break;
				case 4:
					il.Emit(OpCodes.Ldc_I4_4);
					break;
				case 5:
					il.Emit(OpCodes.Ldc_I4_5);
					break;
				case 6:
					il.Emit(OpCodes.Ldc_I4_6);
					break;
				case 7:
					il.Emit(OpCodes.Ldc_I4_7);
					break;
				case 8:
					il.Emit(OpCodes.Ldc_I4_8);
					break;
				default:
					if (index < 128)
						il.Emit(OpCodes.Ldc_I4_S, index);
					else
						il.Emit(OpCodes.Ldc_I4, index);
					break;
			}
			return il;
		}


		/// <summary>
		/// 将指定索引处的局部变量加载到计算堆栈上。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public static ILGenerator Ldloc(this ILGenerator il, int index)
		{
			if (il == null) return null;

			switch (index)
			{
				case 0:
					il.Emit(OpCodes.Ldloc_0);
					break;
				case 1:
					il.Emit(OpCodes.Ldloc_1);
					break;
				case 2:
					il.Emit(OpCodes.Ldloc_2);
					break;
				case 3:
					il.Emit(OpCodes.Ldloc_3);
					break;
				default:
					if (index < 128)
						il.Emit(OpCodes.Ldloc_S, index);
					else
						il.Emit(OpCodes.Ldloc, index);
					break;
			}
			return il;
		}


		/// <summary>
		/// 推送对元数据中存储的字符串的新对象引用
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ILGenerator Ldstr(this ILGenerator il, string value)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Ldstr, value);
			return il;
		}

		public static ILGenerator Ldnull(this ILGenerator il)
		{
			il.Emit(OpCodes.Ldnull);
			return il;
		}

		#endregion

		/// <summary>
		/// 用新值替换在对象引用或指针的字段中存储的值
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		public static ILGenerator Stfld(this ILGenerator il, FieldInfo field)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Stfld, field);
			return il;
		}


		/// <summary>
		/// 从计算堆栈的顶部弹出当前值并将其存储到指定索引处的局部变量列表中。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public static ILGenerator Stloc(this ILGenerator il, int index)
		{
			if (il == null) return null;

			switch (index)
			{
				case 0:
					il.Emit(OpCodes.Stloc_0);
					break;
				case 1:
					il.Emit(OpCodes.Stloc_1);
					break;
				case 2:
					il.Emit(OpCodes.Ldarg_2);
					break;
				case 3:
					il.Emit(OpCodes.Stloc_3);
					break;
				default:
					if (index < 128)
						il.Emit(OpCodes.Stloc_S, index);
					else
						il.Emit(OpCodes.Stloc, index);
					break;
			}
			return il;
		}

		#region Call

		/// <summary>
		/// 调用由传递的方法说明符指示的方法。
		/// </summary>
		/// <param name="mehotd"></param>
		/// <returns></returns>
		public static ILGenerator Call(this ILGenerator il, MethodInfo mehotd)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Call, mehotd);
			return il;
		}

		public static ILGenerator Call(this ILGenerator il, ConstructorInfo value)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Call, value);
			return il;
		}

		/// <summary>
		/// 对对象调用后期绑定方法，并且将返回值推送到计算堆栈上。
		/// </summary>
		/// <param name="mehotd"></param>
		/// <returns></returns>
		public static ILGenerator Callvirt(this ILGenerator il, MethodInfo mehotd)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Callvirt, mehotd);
			return il;
		}

		#endregion


		/// <summary>
		/// 如果修补操作码，则填充空间。尽管可能消耗处理周期，但未执行任何有意义的操作。
		/// </summary>
		/// <returns></returns>
		public static ILGenerator Nop(this ILGenerator il)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Nop);
			return il;
		}

		/// <summary>
		/// 从当前方法返回，并将返回值（如果存在）从调用方的计算堆栈推送到被调用方的计算堆栈上。
		/// </summary>
		/// <returns></returns>
		public static ILGenerator Ret(this ILGenerator il)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Ret);
			return il;
		}

		public static ILGenerator Stelem_Ref(this ILGenerator il)
		{
			if (il == null) return null;
			il.Emit(OpCodes.Stelem_Ref);
			return il;
		}

		public static ILGenerator Newarr<T>(this ILGenerator il)
		{
			return Newarr(il, typeof(T));
		}

		public static ILGenerator Newarr(this ILGenerator il, Type value)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Newarr, value);
			return il;
		}

		#region Box

		/// <summary>
		/// 将值类转换为对象引用（O 类型）。
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <returns></returns>
		public static ILGenerator Box<T>(this ILGenerator il)
		{
			return Box(il, typeof(T));
		}

		/// <summary>
		/// 将值类转换为对象引用（O 类型）。
		/// </summary>
		/// <param name="type">Type</param>
		/// <returns></returns>
		public static ILGenerator Box(this ILGenerator il, Type type)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Box, type);
			return il;
		}

		/// <summary>
		/// 将指令中指定类型的已装箱的表示形式转换成未装箱形式。
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <returns></returns>
		public static ILGenerator UnboxAny<T>(this ILGenerator il)
		{
			return UnboxAny(il, typeof(T));
		}

		/// <summary>
		/// 将指令中指定类型的已装箱的表示形式转换成未装箱形式。
		/// </summary>
		/// <param name="type">Type</param>
		/// <returns></returns>
		public static ILGenerator UnboxAny(this ILGenerator il, Type type)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Unbox_Any, type);
			return il;
		}

		#endregion

		#region Newobj


		public static ILGenerator Noewobj<T>(this ILGenerator il, params Type[] types)
		{
			return Newobj(il, typeof(T), types);
		}

		public static ILGenerator Newobj(this ILGenerator il, Type type, params Type[] types)
		{
			if (il == null) return null;

			il.Emit(OpCodes.Newobj, type.GetConstructor(types));
			return il;
		}

		#endregion


		public static ILGenerator Brtrue_S(this ILGenerator il, TupleWrapper<Label> value = null)
		{
			if (il == null) return null;

			if (value == null)
				value.Item1 = il.DefineLabel();
			il.Emit(OpCodes.Brtrue_S, value.Item1);
			return il;
		}


		#region Label

		public static ILGenerator DefineLabel(this ILGenerator il, ref Label result)
		{
			if (il == null) return null;

			result = il.DefineLabel();
			return il;
		}



		public static ILGenerator MarkLabel(this ILGenerator il, Label label)
		{
			if (il == null) return null;

			if (label != null) il.MarkLabel(label);
			return il;
		}

		#endregion

		#region Declare

		/// <summary>
		/// 声明指定类型的局部变量。
		/// </summary>
		/// <typeparam name="T">表示局部变量的类型</typeparam>
		/// <param name="name">局部变量的名称</param>
		/// <returns></returns>
		public static ILGenerator DeclareLocal<T>(this ILGenerator il, string name = null)
		{
			if (il == null) return null;
			return il.DeclareLocal(typeof(T), name);
		}

		/// <summary>
		/// 声明指定类型的局部变量。
		/// </summary>
		/// <param name="type">一个 System.Type 对象，表示局部变量的类型。</param>
		/// <param name="name">局部变量的名称</param>
		/// <returns></returns>
		public static ILGenerator DeclareLocal(this ILGenerator il, Type type, string name = null)
		{
			if (il == null) return null;
			if (String.IsNullOrWhiteSpace(name))
				il.DeclareLocal(type);
			else
				il.DeclareLocal(type).SetLocalSymInfo(name);
			return il;
		}

		/// <summary>
		/// 声明指定类型的局部变量。
		/// </summary>
		/// <typeparam name="T">表示局部变量的类型</typeparam>
		/// <param name="name">局部变量的名称</param>
		/// <returns>已声明的局部变量</returns>
		public static ILGenerator DeclareNamedLocal<T>(this ILGenerator il, ref LocalBuilder result, string name = null)
		{
			return DeclareNamedLocal(il, ref result, typeof(T), name);
		}

		/// <summary>
		/// 声明指定类型的局部变量。
		/// </summary>
		/// <param name="type">一个 System.Type 对象，表示局部变量的类型。</param>
		/// <param name="name">局部变量的名称</param>
		/// <returns>已声明的局部变量。</returns>
		public static ILGenerator DeclareNamedLocal(this ILGenerator il, ref LocalBuilder result, Type type, string name = null)
		{
			if (il == null) return null;

			result = il.DeclareLocal(type);
			if (!String.IsNullOrWhiteSpace(name))
				result.SetLocalSymInfo(name);
			return il;
		}

		#endregion

		#region Control

		public static ILGenerator IIF(this ILGenerator il, bool condition, Action<ILGenerator> trueAction, Action<ILGenerator> falseAction)
		{
			if (il == null) return null;

			if (condition)
				trueAction(il);
			else
				falseAction(il);

			return il;
		}

		public static T IF<T>(this ILGenerator il, bool condition, Func<ILGenerator, T> func)
		{
			if (il != null && condition)
				return func(il);
			return default(T);
		}


		public static ILGenerator IF(this ILGenerator il, bool condition, Action action)
		{
			if (il != null && condition) action();
			return il;
		}

		public static ILGenerator IF(this ILGenerator il, bool condition, Action<ILGenerator> action)
		{
			if (il != null && condition) action(il);
			return il;
		}

		public static ILGenerator ElseIF(this ILGenerator il, bool condition1, Action action1, bool condition2, Action action2)
		{
			if (il == null) return null;

			if (condition1)
				action1();
			else if (condition2)
				action2();
			return il;
		}

		public static ILGenerator ElseIF(this ILGenerator il, bool condition1, Action<ILGenerator> action1, bool condition2, Action<ILGenerator> action2)
		{
			if (il == null) return null;

			if (condition1)
				action1(il);
			else if (condition2)
				action2(il);
			return il;
		}

		public static ILGenerator For<T>(this ILGenerator il, IList<T> collection, Action<int> action)
		{
			if (il == null) return null;

			for (int i = 0; i < collection.Count; i++)
			{
				action(i);
			}
			return il;
		}

		public static ILGenerator For<T>(this ILGenerator il, IList<T> collection, Action<int, T> action)
		{
			if (il == null) return null;

			for (int i = 0; i < collection.Count; i++)
			{
				action(i, collection[i]);
			}
			return il;
		}

		public static ILGenerator For<T>(this ILGenerator il, IList<T> collection, Action<ILGenerator, int> action)
		{
			if (il == null) return null;

			for (int i = 0; i < collection.Count; i++)
			{
				action(il, i);
			}
			return il;
		}

		public static ILGenerator For<T>(this ILGenerator il, IList<T> collection, Action<ILGenerator, int, T> action)
		{
			if (il == null) return null;

			for (int i = 0; i < collection.Count; i++)
			{
				action(il, i, collection[i]);
			}
			return il;
		}

		public static ILGenerator Break(this ILGenerator il)
		{
			return null;
		}


		#endregion

	}
}
