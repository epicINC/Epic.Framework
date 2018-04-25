using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Epic.Extensions;

namespace Epic.Emit
{
    internal class FastPropertyAccessor
    {

        #region static



        static FastPropertyAccessor()
        {
            ValueTpyeOpCodes = new Dictionary<Type, OpCode>();
            ValueTpyeOpCodes[typeof(sbyte)] = OpCodes.Ldind_I1;
            ValueTpyeOpCodes[typeof(byte)] = OpCodes.Ldind_U1;
            ValueTpyeOpCodes[typeof(char)] = OpCodes.Ldind_U2;
            ValueTpyeOpCodes[typeof(short)] = OpCodes.Ldind_I2;
            ValueTpyeOpCodes[typeof(ushort)] = OpCodes.Ldind_U2;
            ValueTpyeOpCodes[typeof(int)] = OpCodes.Ldind_I4;
            ValueTpyeOpCodes[typeof(uint)] = OpCodes.Ldind_U4;
            ValueTpyeOpCodes[typeof(long)] = OpCodes.Ldind_I8;
            ValueTpyeOpCodes[typeof(ulong)] = OpCodes.Ldind_I8;
            ValueTpyeOpCodes[typeof(bool)] = OpCodes.Ldind_I1;
            ValueTpyeOpCodes[typeof(double)] = OpCodes.Ldind_R8;
            ValueTpyeOpCodes[typeof(float)] = OpCodes.Ldind_R4;
        }


        static Dictionary<Type, OpCode> ValueTpyeOpCodes
        {
            get;
            set;
        }

        #endregion

        #region Ctro


        internal FastPropertyAccessor(Type value)
        {
            this.TargetType = value;
            this.Dictionary = new Dictionary<string, Tuple<Func<object, object>, Action<object, object>>>();
        }


        void Init()
        {

        }

        #endregion



        Type TargetType
        {
            get;
            set;
        }

        Dictionary<string, Tuple<Func<object, object>, Action<object, object>>> Dictionary
        {
            get;
            set;
        }

  


        internal object Get(object instance, string propertyName)
        {
            if (!this.Dictionary.ContainsKey(propertyName))
                this.CreateFunction(propertyName);
            return this.Dictionary[propertyName].Item1(instance);
        }

        internal void Set(object instance, string propertyName, object value)
        {
            if (!this.Dictionary.ContainsKey(propertyName))
                this.CreateFunction(propertyName);
            this.Dictionary[propertyName].Item2(instance, value);
        }

        void CreateFunction(string propertyName)
        {
            var property = this.TargetType.GetProperty(propertyName);

            Errors.CheckArgumentNull(property, "property", () => "{0} 类型 未找到名为 {1} 的属性".Formatting(this.TargetType.Name, propertyName)).Throw();

            this.Dictionary.Add(propertyName, CreateFunction(property));
        }


        Tuple<Func<object, object>, Action<object, object>> CreateFunction(PropertyInfo value)
        {
            if (value == null) return null;
                
            return new Tuple<Func<object, object>, Action<object, object>>(
                this.CreateGetFunction(value.GetGetMethod()),
                this.CreateSetFunction(value.GetSetMethod())
                );
        }



        Func<object, object> CreateGetFunction(MethodInfo method)
        {
            var result = new DynamicMethod("GetValue", typeof(object), new Type[] { typeof(object) });
            var ilGenerator = result.GetILGenerator();
            ilGenerator.DeclareLocal(typeof(object));
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Castclass, this.TargetType);
            ilGenerator.EmitCall(OpCodes.Call, method, null);
            if (method.ReturnType.IsValueType)
                ilGenerator.Emit(OpCodes.Box, method.ReturnType);

            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ret);

            result.DefineParameter(1, ParameterAttributes.In, "value");
            return (Func<object, object>)result.CreateDelegate(typeof(Func<object, object>));
        }

        Action<object, object> CreateSetFunction(MethodInfo method)
        {
            var result = new DynamicMethod("SetValue", null, new Type[] { typeof(object), typeof(object) });
            ILGenerator ilGenerator = result.GetILGenerator();
            Type paramType = method.GetParameters()[0].ParameterType;
            ilGenerator.DeclareLocal(paramType);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Castclass, this.TargetType);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            if (paramType.IsValueType)
            {
                ilGenerator.Emit(OpCodes.Unbox, paramType);
                if (ValueTpyeOpCodes.ContainsKey(paramType))
                    ilGenerator.Emit(ValueTpyeOpCodes[paramType]);
                else
                    ilGenerator.Emit(OpCodes.Ldobj, paramType);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Castclass, paramType);
            }

            ilGenerator.EmitCall(OpCodes.Callvirt, method, null);
            ilGenerator.Emit(OpCodes.Ret);

            result.DefineParameter(1, ParameterAttributes.In, "obj");
            result.DefineParameter(2, ParameterAttributes.In, "value");
            return (Action<object, object>)result.CreateDelegate(typeof(Action<object, object>));
        }
    }
}
