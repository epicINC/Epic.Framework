
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;


namespace DynamicMappingSpike
{
    public delegate void SmartReader<T>(IDataReader reader, out T value);


    public class GenericReader
    {
        private Hashtable _delegateList = new Hashtable();
        private object _lock = new object();


        /// <summary> 
        /// Read the IDataReader and return a list of of the specified type 
        /// </summary> 
        /// <typeparam name="T">The type of the generic list</typeparam> 
        /// <param name="reader">The reader to read</param> 
        /// <returns>A list of class</returns> 
        public IEnumerable<T> GetEnumerator<T>(IDataReader reader) where T : new()
        {
            SmartReader<T> smartReader = InitDelegate<T>(reader);


            // Read 
            while (reader.Read())
            {
                T box;
                smartReader(reader, out box);
                yield return box;
            }
        }


        /// <summary> 
        /// Read the IDataReader and return a list of of the specified type 
        /// </summary> 
        /// <typeparam name="T">The type of the generic list</typeparam> 
        /// <param name="reader">The reader to read</param> 
        /// <returns>A list of class</returns> 
        public List<T> ReadReader<T>(IDataReader reader) where T : new()
        {
            SmartReader<T> smartReader = InitDelegate<T>(reader);
            List<T> containers = new List<T>();
            // Read 
            while (reader.Read())
            {
                try
                {
                    T box;
                    smartReader(reader, out box);
                    containers.Add(box);
                }
                catch (Exception ex)
                {
                }
            }
            return containers;
        }


        private SmartReader<T> InitDelegate<T>(IDataReader reader) where T : new()
        {
            Type type = typeof(T);
            string fullName = type.FullName;
            SmartReader<T> smartReader = null;


            lock (_lock)
            {
                if (_delegateList.ContainsKey(fullName))
                {
                    smartReader = (SmartReader<T>)_delegateList[fullName];
                }
                else
                {
                    SortedList<int, FieldInfo> props = new SortedList<int, FieldInfo>();
                    // Analyse the type 
                    foreach (FieldInfo info in type.GetFields())
                    {
                        foreach (ReaderNameAttribute readerName in info.GetCustomAttributes(typeof(ReaderNameAttribute), true))
                        {
                            string name = readerName.FieldName;
                            int i = reader.GetOrdinal(name);
                            if (i >= 0)
                                props[i] = info;
                        }
                    }


                    smartReader = CreateDynamicMethod<T>(reader, props);
                    // Only for deboging 
                    //CreateMethod<T>(reader, props); 
                    _delegateList[fullName] = smartReader;
                }
            }
            return smartReader;
        }


        /// <summary> 
        /// Create a dynamic method that read the datareader for this class 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="reader"></param> 
        /// <param name="props"></param> 
        /// <returns></returns> 
        private static SmartReader<T> CreateDynamicMethod<T>(IDataReader reader, SortedList<int, FieldInfo> props) where T : new()
        {
            // Create delegate reader 
            Type[] args = { typeof(IDataReader), typeof(T).MakeByRefType() };
            DynamicMethod dynamicMethod = new DynamicMethod("RFR", null, args, typeof(T), true);
            dynamicMethod.DefineParameter(2, ParameterAttributes.Out, "data");
            ILGenerator il = dynamicMethod.GetILGenerator();


            GenerateIL<T>(reader, props, il);


            return (SmartReader<T>)dynamicMethod.CreateDelegate(typeof(SmartReader<T>));
        }


        //private static SmartReader<T> CreateMethod<T>(IDataReader reader, SortedList<int, FieldInfo> props) where T : new() 
        //{ 
        //    AssemblyName assemblyName = new AssemblyName(); 
        //    assemblyName.Name = "MyAssembly"; 
        //    AppDomain domain = AppDomain.CurrentDomain;


        //    AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save); 
        //    ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MyModule", "temp.dll", true); 
        //    TypeBuilder typeBuilder = moduleBuilder.DefineType("MyType", TypeAttributes.Public);


        //    // Create delegate reader 
        //    Type[] args = { typeof(IDataReader), typeof(T).MakeByRefType() }; 
        //    MethodBuilder methodBuilder = typeBuilder.DefineMethod("ReadFrom" + typeof(T).Name, MethodAttributes.Public | MethodAttributes.Static, typeof(void), args); 
        //    methodBuilder.DefineParameter(2, ParameterAttributes.Out, "data"); 
        //    ILGenerator il = methodBuilder.GetILGenerator(); 
        //    GenerateIL<T>(reader, props, il);


        //    Type newType = typeBuilder.CreateType(); 
        //    MethodInfo mInfo = newType.GetMethod("ReadFrom" + typeof(T).Name); 
        //    assemblyBuilder.Save("temp.dll"); 
        //    return (SmartReader<T>)Delegate.CreateDelegate(typeof(SmartReader<T>), mInfo);


        //}


        private static void GenerateIL<T>(IDataReader reader, SortedList<int, FieldInfo> props, ILGenerator il) where T : new()
        {
            // Create the method info 
            MethodInfo mGetValue = reader.GetType().GetMethod("GetValue", new Type[] { typeof(int) });
            MethodInfo mGetString = reader.GetType().GetMethod("GetString", new Type[] { typeof(int) });
            MethodInfo mGetTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle", new Type[1] { typeof(RuntimeTypeHandle) });
            MethodInfo mParseEnum = typeof(Enum).GetMethod("Parse", new Type[] { typeof(Type), typeof(string) });
            MethodInfo mInfoConvert = typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) });
            FieldInfo dbNull = typeof(DBNull).GetField("Value");


            Type type = typeof(T);


            il.DeclareLocal(typeof(object));
            il.DeclareLocal(typeof(bool));


            il.Emit(OpCodes.Nop);


            if (type.IsClass)
            {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { }));
                il.Emit(OpCodes.Stind_Ref);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Initobj, type);
            }


            foreach (KeyValuePair<int, FieldInfo> pair in props)
            {
                int index = pair.Key;
                FieldInfo fi = pair.Value;
                bool isNullableValueType = fi.FieldType.IsGenericType &&
                    (fi.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>));
                Type genericType = fi.FieldType;
                if (isNullableValueType)
                    genericType = fi.FieldType.GetGenericArguments()[0];


                Type readerType = reader.GetFieldType(index);



                if (isNullableValueType == false)// || readerType == genericType) 
                    il.Emit(OpCodes.Ldarg_1);
                if (type.IsClass && (isNullableValueType == false))
                    il.Emit(OpCodes.Ldind_Ref);
                il.Emit(OpCodes.Ldarg_0);
                // Load the index of the DataReader field 
                if (index == 0)
                    il.Emit(OpCodes.Ldc_I4_0);
                else if (index == 1)
                    il.Emit(OpCodes.Ldc_I4_1);
                else if (index == 2)
                    il.Emit(OpCodes.Ldc_I4_2);
                else if (index == 3)
                    il.Emit(OpCodes.Ldc_I4_3);
                else if (index == 4)
                    il.Emit(OpCodes.Ldc_I4_4);
                else if (index == 5)
                    il.Emit(OpCodes.Ldc_I4_5);
                else if (index == 6)
                    il.Emit(OpCodes.Ldc_I4_6);
                else if (index == 7)
                    il.Emit(OpCodes.Ldc_I4_7);
                else if (index == 8)
                    il.Emit(OpCodes.Ldc_I4_8);
                else
                    il.Emit(OpCodes.Ldc_I4, index);


                // Call the data reader. 
                il.Emit(OpCodes.Callvirt, mGetValue);


                Label myLabel = il.DefineLabel(); // Label; 
                if (isNullableValueType == true)
                {
                    il.Emit(OpCodes.Stloc_0);
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldsfld, dbNull);
                    il.Emit(OpCodes.Ceq);
                    il.Emit(OpCodes.Stloc_1);
                    il.Emit(OpCodes.Ldloc_1);


                    il.Emit(OpCodes.Brtrue_S, myLabel); // jump to the lable


                    il.Emit(OpCodes.Ldarg_1);
                    if (type.IsClass && (isNullableValueType == false))
                        il.Emit(OpCodes.Ldind_Ref);
                    il.Emit(OpCodes.Ldloc_0);
                }


                if (readerType != genericType)
                {
                    il.Emit(OpCodes.Ldtoken, genericType);
                    il.Emit(OpCodes.Call, mGetTypeFromHandle);
                    il.Emit(OpCodes.Call, mInfoConvert);
                }


                if (fi.FieldType.IsValueType)
                    il.Emit(OpCodes.Unbox_Any, fi.FieldType);
                else
                    il.Emit(OpCodes.Castclass, fi.FieldType);
                il.Emit(OpCodes.Stfld, fi);


                il.MarkLabel(myLabel);


            }
            il.Emit(OpCodes.Ret);
        }
    }

    public class ReaderNameAttribute : Attribute
    {
        private string _fieldName;


        public ReaderNameAttribute(string fieldName)
        {
            _fieldName = fieldName;
        }


        public string FieldName
        {
            get { return _fieldName; }
        }
    }
}