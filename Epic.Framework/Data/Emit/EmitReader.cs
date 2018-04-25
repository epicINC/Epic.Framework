using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Reflection.Emit;
using Epic.Data.Schema;

namespace Epic.Data.Emit
{
    public static class EmitReader
    {


        static readonly Type Reader;

        static MethodInfo FieldCount;
        static MethodInfo GetBoolean;
        static MethodInfo GetByte;
        static MethodInfo GetChar;
        static MethodInfo GetDataTypeName;
        static MethodInfo GetDateTime;
        static MethodInfo GetDecimal;
        static MethodInfo GetDouble;
        static MethodInfo GetFieldType;
        static MethodInfo GetFloat;
        static MethodInfo GetGuid;
        static MethodInfo GetInt16;
        static MethodInfo GetInt32;
        static MethodInfo GetInt64;
        static MethodInfo GetName;
        static MethodInfo GetOrdinal;
        static MethodInfo GetString;
        static MethodInfo GetValue;
        static MethodInfo IsDBNull;

        static EmitReader()
        {
            Reader = typeof(IDataRecord);
            var readerMethodFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            //FieldCount = Reader.GetMethod("FieldCount", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetBoolean = Reader.GetMethod("GetBoolean", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetByte = Reader.GetMethod("GetByte", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetChar = Reader.GetMethod("GetChar", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetDataTypeName = Reader.GetMethod("GetDataTypeName", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetDateTime = Reader.GetMethod("GetDateTime", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetDecimal = Reader.GetMethod("GetDecimal", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetDouble = Reader.GetMethod("GetDouble", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetFieldType = Reader.GetMethod("GetFieldType", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetFloat = Reader.GetMethod("GetFloat", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetGuid = Reader.GetMethod("GetGuid", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetInt16 = Reader.GetMethod("GetInt16", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetInt32 = Reader.GetMethod("GetInt32", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetInt64 = Reader.GetMethod("GetInt64", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetName = Reader.GetMethod("GetName", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetOrdinal = Reader.GetMethod("GetOrdinal", readerMethodFlags, null, new Type[] { typeof(string) }, null);
            GetString = Reader.GetMethod("GetString", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            GetValue = Reader.GetMethod("GetValue", readerMethodFlags, null, new Type[] { typeof(int) }, null);
            IsDBNull = Reader.GetMethod("IsDBNull", readerMethodFlags, null, new Type[] { typeof(int) }, null);
        }


        public static IEnumerable<T> GetEnumerator<T>(IDataReader reader)
        {
            var emitReader = EmitDataReader<T>.Init(reader);
            do
            {
                while (reader.Read())
                {
                    yield return emitReader(reader);
                }
            } while (reader.NextResult());
        }

        public static List<T> GetList<T>(IDataReader reader)
        {
            var emitReader = EmitDataReader<T>.Init(reader);
            var result = new List<T>();
            do
            {
                while (reader.Read())
                {
                    result.Add(emitReader(reader));
                }
            } while (reader.NextResult());
            return result;
        }


        #region EmitDataReader

        static class EmitDataReader<T>
        {
            static Func<IDataReader, T> emitReader;

            internal static Func<IDataReader, T> Init(IDataReader reader)
            {
                if (emitReader == null)
                {
                    emitReader = CreateDynamicMethod(reader);
                    //CreateMethod(reader);
                }

                return emitReader;
            }



            static Func<IDataReader, T> CreateDynamicMethod(IDataReader reader)
            {
                var type = typeof(T);
                var dynamicMethod = new DynamicMethod("Epic.Data.Common.Parse" + type.Name, type, new Type[] { typeof(IDataReader) }, type, true);
                var il = dynamicMethod.GetILGenerator();

                GenerateIL(il, reader);
                return (Func<IDataReader, T>)dynamicMethod.CreateDelegate(typeof(Func<IDataReader, T>));
            }




            static Func<IDataReader, T> CreateMethod(IDataReader reader)
            {
                var type = typeof(T);

                AssemblyName assemblyName = new AssemblyName();
                assemblyName.Name = "Epic.Framework.Dynamic";
                AppDomain domain = AppDomain.CurrentDomain;

                AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Epic.Framework.Dynamic", "Epic.Framework.Dynamic.dll");
                TypeBuilder typeBuilder = moduleBuilder.DefineType("DynamicReader", TypeAttributes.Public);

                Type[] args = { typeof(IDataReader) };
                var methodBuilder = typeBuilder.DefineMethod("Parse" + type.Name, MethodAttributes.Public | MethodAttributes.Static, type, new Type[] { typeof(IDataReader) });
                methodBuilder.DefineParameter(1, ParameterAttributes.None, "dr");

                ILGenerator il = methodBuilder.GetILGenerator();
                GenerateIL(il, reader);

                var newType = typeBuilder.CreateType();
                var mInfo = newType.GetMethod("Parse" + type.Name);


                assemblyBuilder.Save("Epic.Framework.Dynamic.dll");

                return (Func<IDataReader, T>)Delegate.CreateDelegate(typeof(Func<IDataReader, T>), mInfo);


            }


            static SortedList<int, ColumnSchema> GetOrdina(IDataRecord dr)
            {
                var result = new SortedList<int, ColumnSchema>();
                foreach (var item in TableSchema<T>.Columns)
                {
                    result.Add(dr.GetOrdinal(item.DbName), item);
                }

                return result;


            }

            static void GenerateIL(ILGenerator il, IDataReader dr)
            {
                var type = typeof(T);
                var ordinas = GetOrdina(dr);

                il.DeclareLocal(type);
                il.DeclareLocal(typeof(bool));



                il.Emit(OpCodes.Nop);

                if (type.IsClass)
                {
                    il.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { }));
                    il.Emit(OpCodes.Stloc_0);
                }
                else
                {
                    il.Emit(OpCodes.Ldloca_S, 0);
                    il.Emit(OpCodes.Initobj, type);
                    il.Emit(OpCodes.Ldloca_S, 0);


                }


                foreach (KeyValuePair<int, ColumnSchema> item in ordinas)
                {
                    GenerateField(il, dr, item);

                }
                //il.Emit(OpCodes.Stloc_0);
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ret);
            }


            static void GenerateField(ILGenerator il, IDataRecord dr, KeyValuePair<int, ColumnSchema> pair)
            {
                var type = typeof(T);
                var index = pair.Key;
                var column = pair.Value;

                var label = il.DefineLabel();

                if (column.IsNull)
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.EmitLdc(index);
                    il.Emit(OpCodes.Callvirt, IsDBNull);
                    il.Emit(OpCodes.Stloc_1);
                    il.Emit(OpCodes.Ldloc_1);
                    il.Emit(OpCodes.Brtrue_S, label);
                }

                if (type.IsClass)
                    il.Emit(OpCodes.Ldloc_0);
                else
                    il.Emit(OpCodes.Ldloca_S, 0);
                il.Emit(OpCodes.Ldarg_0);

                il.EmitLdc(index);
                GenerateMethod(il, column);

                if (column.SetMethod != null)
                    il.Emit(OpCodes.Callvirt, column.SetMethod);
                else
                    il.Emit(OpCodes.Stfld, column.Field);


                il.Emit(OpCodes.Nop);


                if (column.IsNull)
                    il.MarkLabel(label);
            }


            static void GenerateMethod(ILGenerator il, ColumnSchema column)
            {

                if (column.Type.IsEnum)
                {
                    il.Emit(OpCodes.Callvirt, GetValue);
                    il.Emit(OpCodes.Unbox_Any, column.Type);
                }
                else
                {
                    switch (Type.GetTypeCode(column.Type))
                    {
                        case TypeCode.Boolean:
                            il.Emit(OpCodes.Callvirt, GetBoolean);
                            break;
                        case TypeCode.Byte:
                            il.Emit(OpCodes.Callvirt, GetByte);
                            break;
                        case TypeCode.Char:
                            il.Emit(OpCodes.Callvirt, GetChar);
                            break;
                        case TypeCode.DateTime:
                            il.Emit(OpCodes.Callvirt, GetDateTime);
                            break;
                        case TypeCode.Decimal:
                            il.Emit(OpCodes.Callvirt, GetDecimal);
                            break;
                        case TypeCode.Double:
                            il.Emit(OpCodes.Callvirt, GetDouble);
                            break;
                        case TypeCode.Int16:
                            il.Emit(OpCodes.Callvirt, GetInt16);
                            break;
                        case TypeCode.Int32:
                            il.Emit(OpCodes.Callvirt, GetInt32);
                            break;
                        case TypeCode.Int64:
                            il.Emit(OpCodes.Callvirt, GetInt64);
                            break;
                        case TypeCode.SByte:
                            il.Emit(OpCodes.Callvirt, GetValue);
                            il.Emit(OpCodes.Unbox_Any, typeof(sbyte));
                            break;
                        case TypeCode.Single:
                            il.Emit(OpCodes.Callvirt, typeof(Single));
                            break;
                        case TypeCode.String:
                            il.Emit(OpCodes.Callvirt, GetString);
                            break;
                        case TypeCode.UInt16:
                            il.Emit(OpCodes.Callvirt, GetValue);
                            il.Emit(OpCodes.Unbox_Any, typeof(UInt16));
                            break;
                        case TypeCode.UInt32:
                            il.Emit(OpCodes.Callvirt, GetValue);
                            il.Emit(OpCodes.Unbox_Any, typeof(UInt32));
                            break;
                        case TypeCode.UInt64:
                            il.Emit(OpCodes.Callvirt, GetValue);
                            il.Emit(OpCodes.Unbox_Any, typeof(UInt64));
                            break;
                        default:
                            throw new ApplicationException(String.Format("无效 TypeCode: {0}, Column:{1}", Type.GetTypeCode(column.Type), column.Name));
                            break;
                    }
                }
            }

        }


        #endregion


        static void EmitLdc(this ILGenerator il, int index)
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
        }


    }
}
