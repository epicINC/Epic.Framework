using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Epic.Emit
{



	public abstract class EmitGenerator
	{
		protected IILGeneratorWrapper il;
		protected IConstructor emitConstructor;



		public IILGeneratorWrapper GetILGenerator()
		{
			return this.il;
		}

		public IConstructor GetEmitConstructor()
		{
			return this.emitConstructor;
		}

		#region Load

		public EmitGenerator Ldtoken<T>()
		{
			return this.Ldtoken(typeof(T));
		}

		public EmitGenerator Ldtoken(Type value)
		{
			il.Emit(OpCodes.Ldtoken, value);
			return this;
		}

		/// <summary>
		/// 将索引为 0 的参数加载到计算堆栈上。
		/// </summary>
		/// <returns></returns>
		public EmitGenerator Ldarg()
		{
			il.Emit(OpCodes.Ldarg_0);
			return this;
		}

		/// <summary>
		/// 将参数（由指定索引值引用）加载到堆栈上。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public EmitGenerator Ldarg(int index)
		{
			switch (index)
			{
				case 0 :
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
			return this;
		}

		public EmitGenerator Ldfld(FieldBuilder value)
		{
			il.Emit(OpCodes.Ldfld, value);
			return this;
		}

		/// <summary>
		/// 将整数值 0 作为 int32 推送到计算堆栈上。
		/// </summary>
		/// <returns></returns>
		public EmitGenerator Ldc()
		{
			il.Emit(OpCodes.Ldc_I4_0);
			return this;
		}

		/// <summary>
		/// 将 参数 作为 int32 推送到计算堆栈上。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public EmitGenerator Ldc(int index)
		{
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
					il.Emit(OpCodes.Ldc_I4, index);
					break;
			}
			return this;
		}


		/// <summary>
		/// 将指定索引处的局部变量加载到计算堆栈上。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public EmitGenerator Ldloc(int index)
		{
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
			return this;
		}


		/// <summary>
		/// 推送对元数据中存储的字符串的新对象引用
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public EmitGenerator Ldstr(string value)
		{
			il.Emit(OpCodes.Ldstr, value);
			return this;
		}

		public EmitGenerator Ldnull()
		{
			il.Emit(OpCodes.Ldnull);
			return this;
		}

		#endregion

		/// <summary>
		/// 用新值替换在对象引用或指针的字段中存储的值
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		public EmitGenerator Stfld(FieldInfo field)
		{
			il.Emit(OpCodes.Stfld, field);
			return this;
		}


		/// <summary>
		/// 从计算堆栈的顶部弹出当前值并将其存储到指定索引处的局部变量列表中。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public EmitGenerator Stloc(int index)
		{
			switch (index)
			{
				case 0:
					il.Emit(OpCodes.Stloc_0);
					break;
				case 1:
					il.Emit(OpCodes.Stloc_1);
					break;
				case 2:
                    il.Emit(OpCodes.Stloc_2);
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
			return this;
		}

		#region Call

		/// <summary>
		/// 调用由传递的方法说明符指示的方法。
		/// </summary>
		/// <param name="mehotd"></param>
		/// <returns></returns>
		public EmitGenerator Call(MethodInfo mehotd)
		{
			il.Emit(OpCodes.Call, mehotd);
			return this;
		}

        public EmitGenerator Call(Type value)
        {
            Call(value.GetConstructor(Type.EmptyTypes));
            return this;
        }

        public EmitGenerator Call<T>()
        {
            return Call(TypesPool.Typeof<T>());
        }

		public EmitGenerator Call(ConstructorInfo value)
		{
			il.Emit(OpCodes.Call, value);
			return this;
		}

		/// <summary>
		/// 对对象调用后期绑定方法，并且将返回值推送到计算堆栈上。
		/// </summary>
		/// <param name="mehotd"></param>
		/// <returns></returns>
		public EmitGenerator Callvirt(MethodInfo mehotd)
		{
			il.Emit(OpCodes.Callvirt, mehotd);
			return this;
		}

		#endregion


		/// <summary>
		/// 如果修补操作码，则填充空间。尽管可能消耗处理周期，但未执行任何有意义的操作。
		/// </summary>
		/// <returns></returns>
		public EmitGenerator Nop()
		{
			il.Emit(OpCodes.Nop);
			return this;
		}

		/// <summary>
		/// 从当前方法返回，并将返回值（如果存在）从调用方的计算堆栈推送到被调用方的计算堆栈上。
		/// </summary>
		/// <returns></returns>
		public EmitGenerator Ret()
		{
			il.Emit(OpCodes.Ret);
			return this;
		}

		public EmitGenerator Stelem_Ref()
		{
			il.Emit(OpCodes.Stelem_Ref);
			return this;
		}

		public EmitGenerator Newarr<T>()
		{
			return this.Newarr(typeof(T));
		}

		public EmitGenerator Newarr(Type value)
		{
			il.Emit(OpCodes.Newarr, value);
			return this;
		}

		#region Box

		/// <summary>
		/// 将值类转换为对象引用（O 类型）。
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <returns></returns>
		public EmitGenerator Box<T>()
		{
			return this.Box(typeof(T));
		}

		/// <summary>
		/// 将值类转换为对象引用（O 类型）。
		/// </summary>
		/// <param name="type">Type</param>
		/// <returns></returns>
		public EmitGenerator Box(Type type)
		{
			il.Emit(OpCodes.Box, type);
			return this;
		}

		/// <summary>
		/// 将指令中指定类型的已装箱的表示形式转换成未装箱形式。
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <returns></returns>
		public EmitGenerator UnboxAny<T>()
		{
			return this.UnboxAny(typeof(T));
		}

		/// <summary>
		/// 将指令中指定类型的已装箱的表示形式转换成未装箱形式。
		/// </summary>
		/// <param name="type">Type</param>
		/// <returns></returns>
		public EmitGenerator UnboxAny(Type type)
		{
			il.Emit(OpCodes.Unbox_Any, type);
			return this;
		}

		#endregion

		#region Newobj

		public EmitGenerator Newobj(Type type, params Type[] types)
		{
			il.Emit(OpCodes.Newobj, type.GetConstructor(types));
			return this;
		}

		public EmitGenerator Noewobj<T>(params Type[] types)
		{
			return this.Newobj(typeof(T), types);
		}

		#endregion

		Label label;
		public EmitGenerator Brtrue_S()
		{
			this.label = il.DefineLabel();
			il.Emit(OpCodes.Brtrue_S, this.label);
			return this;
		}


		#region Label

		public Label DefineLabel()
		{
			return il.DefineLabel();
		}

		public EmitGenerator MarkLabel()
		{;
			return this.MarkLabel(this.label);
		}

		public EmitGenerator MarkLabel(Label label)
		{
			if (label != null)
				il.MarkLabel(label);
			return this;
		}

		#endregion

		#region Declare

		/// <summary>
		/// 声明指定类型的局部变量。
		/// </summary>
		/// <typeparam name="T">表示局部变量的类型</typeparam>
		/// <param name="name">局部变量的名称</param>
		/// <returns></returns>
		public EmitGenerator DeclareLocal<T>(string name = null)
		{
			return this.DeclareLocal(typeof(T), name);
		}

		/// <summary>
		/// 声明指定类型的局部变量。
		/// </summary>
		/// <param name="type">一个 System.Type 对象，表示局部变量的类型。</param>
		/// <param name="name">局部变量的名称</param>
		/// <returns></returns>
		public EmitGenerator DeclareLocal(Type type, string name = null)
		{
			if (String.IsNullOrWhiteSpace(name))
				il.DeclareLocal(type);
			else
				il.DeclareLocal(type).SetLocalSymInfo(name);
			return this;
		}

		/// <summary>
		/// 声明指定类型的局部变量。
		/// </summary>
		/// <typeparam name="T">表示局部变量的类型</typeparam>
		/// <param name="name">局部变量的名称</param>
		/// <returns>已声明的局部变量</returns>
		public LocalBuilder DeclareNamedLocal<T>(string name = null)
		{
			return this.DeclareNamedLocal(typeof(T), name);
		}

		/// <summary>
		/// 声明指定类型的局部变量。
		/// </summary>
		/// <param name="type">一个 System.Type 对象，表示局部变量的类型。</param>
		/// <param name="name">局部变量的名称</param>
		/// <returns>已声明的局部变量。</returns>
		public LocalBuilder DeclareNamedLocal(Type type, string name = null)
		{
			var result = il.DeclareLocal(type);
			if (!String.IsNullOrWhiteSpace(name))
				result.SetLocalSymInfo(name);
			return result;
		}

		#endregion

		#region Control

		public EmitGenerator IIF(bool condition, Action<EmitGenerator> trueAction, Action<EmitGenerator> falseAction)
		{
			if (condition)
				trueAction(this);
			else
				falseAction(this);

			return this;
		}

        //public T IF<T>(bool condition, Func<EmitGenerator, T> func)
        //{
        //    if (condition)
        //        return func(this);
        //    return default(T);
        //}


		public EmitGenerator IF(bool condition, Action action)
		{
			if (condition) action();
			return this;
		}

		public EmitGenerator IF(bool condition, Action<EmitGenerator> action)
		{
			if (condition) action(this);
			return this;
		}

        public EmitGenerator IF(bool condition, Action action1, Action action2)
        {
            if (condition)
                action1();
            else
                action2();
            return this;
        }

        public EmitGenerator IF(bool condition, Action<EmitGenerator> action1, Action<EmitGenerator> action2)
        {
            if (condition)
                action1(this);
            else
                action2(this);
            return this;
        }

        public EmitGenerator IFElse(bool condition1, Action action1, bool condition2, Action action2)
		{
			if (condition1)
				action1();
			else if (condition2)
				action2();
			return this;
		}

        public EmitGenerator IFElse(bool condition1, Action<EmitGenerator> action1, bool condition2, Action<EmitGenerator> action2)
		{
			if (condition1)
				action1(this);
			else if (condition2)
				action2(this);
			return this;
		}

        #region For

        public EmitGenerator For<T>(IEnumerable<T> collection, Action<int> action)
        {
            var i = 0;
            foreach (var item in collection)
            {
                action(i);
                i++;
            }
            return this;
        }

        public EmitGenerator For<T>(IEnumerable<T> collection, Action<int, T> action)
        {
            var i = 0;
            foreach (var item in collection)
            {
                action(i, item);
                i++;
            }
            return this;
        }

        public EmitGenerator For<T>(IEnumerable<T> collection, Action<EmitGenerator, int> action)
        {
            var i = 0;
            foreach (var item in collection)
            {
                action(this, i);
                i++;
            }
            return this;
        }

        public EmitGenerator For<T>(IEnumerable<T> collection, Action<EmitGenerator, int, T> action)
        {
            var i = 0;
            foreach (var item in collection)
            {
                action(this, i, item);
                i++;
            }
            return this;
        }

        public EmitGenerator For<T>(IList<T> collection, Action<int> action)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				action(i);
			}
			return this;
		}

		public EmitGenerator For<T>(IList<T> collection, Action<int, T> action)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				action(i, collection[i]);
			}
			return this;
		}

		public EmitGenerator For<T>(IList<T> collection, Action<EmitGenerator, int> action)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				action(this, i);
			}
			return this;
		}

		public EmitGenerator For<T>(IList<T> collection, Action<EmitGenerator, int, T> action)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				action(this, i, collection[i]);
			}
			return this;
		}

        #endregion

        public EmitGenerator Break()
		{
			this.il = new BlankILGeneratorWrapper();
			return this;
		}
	

		#endregion


		public T CreateDelegate<T>(MethodInfo method) where T : class
		{
			return Delegate.CreateDelegate(typeof(T), method) as T;
		}
	}
}
