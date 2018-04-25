using System;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security;

namespace Epic.Emit
{
    public class ILGeneratorWrapper : IILGeneratorWrapper
    {
        ILGenerator il;
        internal ILGeneratorWrapper(ILGenerator il)
        {
            this.il = il;
        }

        /// <summary>
        /// 获取由 System.Reflection.Emit.ILGenerator 发出的 Microsoft 中间语言 (MSIL) 流中的当前偏移量（以字节为单位）。
        /// </summary>
        /// <returns>MSIL 流中的偏移量，将在此处发出下一个指令。</returns>
        public int ILOffset
        {
            get { return this.il.ILOffset; }
        }

        /// <summary>
        /// 开始 Catch 块。
        /// </summary>
        /// <param name="exceptionType">表示异常的 System.Type 对象。</param>
        /// <exception cref="System.ArgumentException">Catch 块在已筛选的异常中。</exception>
        /// <exception cref="System.ArgumentNullException">exceptionType 为 null，并且异常筛选器块没有返回一个值，该值指示在找到此 Catch 块之前一直运行 Finally 块。</exception>
        /// <exception cref="System.NotSupportedException">要生成的 Microsoft 中间语言 (MSIL) 当前不在异常块中。</exception>
        public void BeginCatchBlock(Type exceptionType)
        {
            this.il.BeginCatchBlock(exceptionType);
        }

        /// <summary>
        /// 开始已筛选异常的异常块。
        /// </summary>
        /// <exception cref="System.NotSupportedException">要生成的 Microsoft 中间语言 (MSIL) 当前不在异常块中。- 或 -此 System.Reflection.Emit.ILGenerator 属于某个 System.Reflection.Emit.DynamicMethod。</exception>
        public void BeginExceptFilterBlock()
        {
            this.il.BeginExceptFilterBlock();
        }

        /// <summary>
        /// 开始非筛选异常的异常块。
        /// </summary>
        /// <returns>块结尾的标签。这将使您停在正确的位置执行 Finally 块或完成 Try 块。</returns>
        public Label BeginExceptionBlock()
        {
            return this.il.BeginExceptionBlock();
        }

        /// <summary>
        /// 在 Microsoft 中间语言 (MSIL) 流中开始一个异常错误块。
        /// </summary>
        /// <exception cref="System.NotSupportedException">生成的 MSIL 当前不在异常块中。- 或 -此 System.Reflection.Emit.ILGenerator 属于某个 System.Reflection.Emit.DynamicMethod。</exception>
        public void BeginFaultBlock()
        {
            this.il.BeginFaultBlock();
        }

        /// <summary>
        /// 在 Microsoft 中间语言 (MSIL) 指令流中开始一个 Finally 块。
        /// </summary>
        /// <exception cref="System.NotSupportedException">生成的 MSIL 当前不在异常块中。</exception>
        public void BeginFinallyBlock()
        {
            this.il.BeginFinallyBlock();
        }

        /// <summary>
        /// 开始词法范围。
        /// </summary>
        /// <exception cref="System.NotSupportedException">此 System.Reflection.Emit.ILGenerator 属于某个 System.Reflection.Emit.DynamicMethod。</exception>
        public void BeginScope()
        {
            this.il.BeginScope();
        }

        /// <summary>
        /// 声明指定类型的局部变量。
        /// </summary>
        /// <param name="localType">一个 System.Type 对象，表示局部变量的类型。</param>
        /// <returns>已声明的局部变量。</returns>
        /// <exception cref="System.ArgumentNullException">localType 为 null。</exception>
        /// <exception cref="System.InvalidOperationException">包含类型已由 System.Reflection.Emit.TypeBuilder.CreateType() 方法创建。</exception>
        public LocalBuilder DeclareLocal(Type localType)
        {
            return this.il.DeclareLocal(localType);
        }

        /// <summary>
        /// 声明指定类型的局部变量，还可以选择固定该变量所引用的对象。
        /// </summary>
        /// <param name="localType">一个 System.Type 对象，表示局部变量的类型。</param>
        /// <param name="pinned">如果要将对象固定在内存中，则为 true；否则为 false。</param>
        /// <returns>一个 System.Reflection.Emit.LocalBuilder 对象，表示局部变量。</returns>
        /// <exception cref="System.ArgumentNullException">localType 为 null</exception>
        /// <exception cref="System.InvalidOperationException">包含类型已由 System.Reflection.Emit.TypeBuilder.CreateType() 方法创建。- 或 -封闭方法的方法体已由 System.Reflection.Emit.MethodBuilder.CreateMethodBody(System.Byte[],System.Int32) 方法创建。</exception>
        /// <exception cref="System.NotSupportedException">与此 System.Reflection.Emit.ILGenerator 关联的方法不由 System.Reflection.Emit.MethodBuilder 来表示。</exception>
        public LocalBuilder DeclareLocal(Type localType, bool pinned)
        {
            return this.il.DeclareLocal(localType, pinned);
        }

        /// <summary>
        /// 声明新标签。
        /// </summary>
        /// <returns>返回可用作分支标记的新标签。</returns>
        public Label DefineLabel()
        {
            return this.il.DefineLabel();
        }

        /// <summary>
        /// 将指定的指令放到指令流上。
        /// </summary>
        /// <param name="opcode">要放到流上的 Microsoft 中间语言 (MSIL) 指令。</param>
        public void Emit(OpCode opcode)
        {
            this.il.Emit(opcode);
        }

        /// <summary>
        /// 将指定的指令和字符参数放在 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要放到流上的 MSIL 指令。</param>
        /// <param name="arg">紧接着该指令推到流中的字符参数。</param>
        public void Emit(OpCode opcode, byte arg)
        {
            this.il.Emit(opcode, arg);
        }

        /// <summary>
        /// 将指定构造函数的指定指令和元数据标记放到 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。</param>
        /// <param name="con">表示构造函数的 ConstructorInfo。</param>\
        /// <exception cref="System.ArgumentNullException">con 为 null。此异常是 中新出现的。</exception>
        [SecuritySafeCritical]
        [ComVisible(true)]
        public void Emit(OpCode opcode, ConstructorInfo con)
        {
            this.il.Emit(opcode, con);
        }

        /// <summary>
        /// 将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要放到流上的 MSIL 指令。在 OpCodes 枚举中定义。</param>
        /// <param name="arg">紧接着该指令推到流中的数字参数。</param>
        [SecuritySafeCritical]
        public void Emit(OpCode opcode, double arg)
        {
            this.il.Emit(opcode, arg);
        }

        /// <summary>
        /// 将指定字段的指定指令和元数据标记放到 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。</param>
        /// <param name="field">表示字段的 FieldInfo。</param>
        public void Emit(OpCode opcode, FieldInfo field)
        {
            this.il.Emit(opcode, field);
        }

        /// <summary>
        /// 将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要放到流上的 MSIL 指令。</param>
        /// <param name="arg">紧接着该指令推到流上的 Float 参数。</param>
        [SecuritySafeCritical]
        public void Emit(OpCode opcode, float arg)
        {
            this.il.Emit(opcode, arg);
        }

        /// <summary>
        /// 将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要放到流上的 MSIL 指令。</param>
        /// <param name="arg">紧接着该指令推到流中的数字参数。</param>
        public void Emit(OpCode opcode, int arg)
        {
            this.il.Emit(opcode, arg);
        }

        /// <summary>
        /// 将指定的指令放在 Microsoft 中间语言 (MSIL) 流上，并留出在完成修正时加上标签所需的空白。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。</param>
        /// <param name="label">从此位置分支到的标签。</param>
        public void Emit(OpCode opcode, Label label)
        {
            this.il.Emit(opcode, label);
        }

        /// <summary>
        /// 将指定的指令放在 Microsoft 中间语言 (MSIL) 流上，并留出在完成修正时加上标签所需的空白。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。</param>
        /// <param name="labels">从此位置分支到的标签对象的数组。将使用所有标签。</param>
        /// <exception cref="System.ArgumentNullException">con 为 null。此异常是 中新出现的。</exception>
        public void Emit(OpCode opcode, Label[] labels)
        {
            this.il.Emit(opcode, labels);
        }

        /// <summary>
        /// 将指定的指令放到 Microsoft 中间语言 (MSIL) 流上，后跟给定局部变量的索引。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。</param>
        /// <param name="local">局部变量。</param>
        /// <exception cref="System.ArgumentException">local 参数的父方法与此 System.Reflection.Emit.ILGenerator 关联的方法不匹配。</exception>
        /// <exception cref="System.ArgumentNullException">local 为 null。</exception>
        /// <exception cref="System.InvalidOperationException">opcode 是单字节指令，并且 local 表示索引大于 Byte.MaxValue 的局部变量。</exception>
        public void Emit(OpCode opcode, LocalBuilder local)
        {
            this.il.Emit(opcode, local);
        }

        /// <summary>
        /// 将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要放到流上的 MSIL 指令。</param>
        /// <param name="arg">紧接着该指令推到流中的数字参数。</param>
        public void Emit(OpCode opcode, long arg)
        {
            this.il.Emit(opcode, arg);
        }

        /// <summary>
        /// 将指定的指令放到 Microsoft 中间语言 (MSIL) 流上，后跟给定方法的元数据标记。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。</param>
        /// <param name="meth">表示方法的 MethodInfo。</param>
        /// <exception cref="System.ArgumentNullException">meth 为 null。</exception>
        /// <exception cref="System.NotSupportedException">meth 为泛型方法，其 System.Reflection.MethodInfo.IsGenericMethodDefinition 属性为 false。</exception>
        [SecuritySafeCritical]
        public void Emit(OpCode opcode, MethodInfo meth)
        {
            this.il.Emit(opcode, meth);
        }

        /// <summary>
        /// 将指定的指令和字符参数放在 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要放到流上的 MSIL 指令。</param>
        /// <param name="arg">紧接着该指令推到流中的字符参数。</param>
        public void Emit(OpCode opcode, sbyte arg)
        {
            this.il.Emit(opcode, arg);
        }

        /// <summary>
        /// 将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。</param>
        /// <param name="arg">紧接着该指令推到流中的 Short 参数。</param>
        public void Emit(OpCode opcode, short arg)
        {
            this.il.Emit(opcode, arg);
        }

        /// <summary>
        /// 将指定的指令和签名标记放在 Microsoft 中间语言 (MSIL) 指令流上。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。</param>
        /// <param name="signature">用于构造签名标记的帮助器。</param>
        /// <exception cref="System.ArgumentNullException">signature 为 null。</exception>
        public void Emit(OpCode opcode, SignatureHelper signature)
        {
            this.il.Emit(opcode, signature);
        }

        /// <summary>
        /// 将指定的指令放到 Microsoft 中间语言 (MSIL) 流上，后跟给定字符串的元数据标记。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。</param>
        /// <param name="str">要发出的 String。</param>
        public void Emit(OpCode opcode, string str)
        {
            this.il.Emit(opcode, str);
        }

        /// <summary>
        /// 将指定的指令放到 Microsoft 中间语言 (MSIL) 流上，后跟给定类型的元数据标记。
        /// </summary>
        /// <param name="opcode">要放到流上的 MSIL 指令。</param>
        /// <param name="cls">Type。</param>
        /// <exception cref="System.ArgumentNullException">cls 为 null。</exception>
        [SecuritySafeCritical]
        public void Emit(OpCode opcode, Type cls)
        {
            this.il.Emit(opcode, cls);
        }

        /// <summary>
        /// 将 call 或 callvirt 指令放到 Microsoft 中间语言 (MSIL) 流上，以便调用 varargs 方法。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。必须为 System.Reflection.Emit.OpCodes.Call、System.Reflection.Emit.OpCodes.Callvirt 或 System.Reflection.Emit.OpCodes.Newobj。</param>
        /// <param name="methodInfo">要调用的 varargs 方法。</param>
        /// <param name="optionalParameterTypes">如果该方法是 varargs 方法，则为可选参数的类型；否则为 null。</param>
        /// <exception cref="System.ArgumentException">opcode 未指定方法调用。</exception>
        /// <exception cref="System.ArgumentNullException">methodInfo 为 null。</exception>
        /// <exception cref="System.InvalidOperationException">此方法的调用约定不是 varargs，但是提供了可选的参数类型。在 .NET Framework 1.0 版和 1.1 版中会引发此异常。在后续版本中，则不会引发任何异常。</exception>
        [SecuritySafeCritical]
        public void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
        {
            this.il.EmitCall(opcode, methodInfo, optionalParameterTypes);
        }

        /// <summary>
        /// 将 System.Reflection.Emit.OpCodes.Calli 指令放到 Microsoft 中间语言 (MSIL) 流，并指定间接调用的非托管调用约定。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。必须为 System.Reflection.Emit.OpCodes.Calli。</param>
        /// <param name="unmanagedCallConv">要使用的非托管调用约定。</param>
        /// <param name="returnType">结果的 System.Type。</param>
        /// <param name="parameterTypes">指令的必选参数的类型。</param>
        public void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
        {
            this.il.EmitCalli(opcode, unmanagedCallConv, returnType, parameterTypes);
        }

        /// <summary>
        /// 将 System.Reflection.Emit.OpCodes.Calli 指令放到 Microsoft 中间语言 (MSIL) 流，并指定间接调用的托管调用约定。
        /// </summary>
        /// <param name="opcode">要发到流中的 MSIL 指令。必须为 System.Reflection.Emit.OpCodes.Calli。</param>
        /// <param name="callingConvention">要使用的托管调用约定。</param>
        /// <param name="returnType">结果的 System.Type。</param>
        /// <param name="parameterTypes">指令的必选参数的类型。</param>
        /// <param name="optionalParameterTypes">varargs 调用的可选参数的类型。</param>
        /// <exception cref="System.InvalidOperationException">optionalParameterTypes 不为 null，但 callingConvention 不包括 System.Reflection.CallingConventions.VarArgs 标志。</exception>
        [SecuritySafeCritical]
        public void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
        {
            this.il.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
        }

        /// <summary>
        /// 发出用给定字段调用 Overload:System.Console.WriteLine 所需的 Microsoft 中间语言 (MSIL)。
        /// </summary>
        /// <param name="fld">其值要被写到控制台的字段。</param>
        /// <exception cref="System.ArgumentException">不存在接受指定字段类型的 Overload:System.Console.WriteLine 方法重载。</exception>
        /// <exception cref="System.ArgumentNullException">fld 为 null。</exception>
        /// <exception cref="System.NotSupportedException">字段类型为 System.Reflection.Emit.TypeBuilder 或 System.Reflection.Emit.EnumBuilder，这两种类型都不受支持。</exception>
        [SecuritySafeCritical]
        public void EmitWriteLine(FieldInfo fld)
        {
            this.il.EmitWriteLine(fld);
        }

        /// <summary>
        /// 发出用给定局部变量调用 Overload:System.Console.WriteLine 所需的 Microsoft 中间语言 (MSIL)。
        /// </summary>
        /// <param name="localBuilder">其值要被写到控制台的局部变量。</param>
        /// <exception cref="System.ArgumentException">localBuilder 的类型为 System.Reflection.Emit.TypeBuilder 或 System.Reflection.Emit.EnumBuilder，这两种类型都不受支持。- 或 -不存在接受 localBuilder 的类型的 Overload:System.Console.WriteLine 重载。</exception>
        /// <exception cref="System.ArgumentNullException">localBuilder 为 null。</exception>
        [SecuritySafeCritical]
        public void EmitWriteLine(LocalBuilder localBuilder)
        {
            this.il.EmitWriteLine(localBuilder);
        }

        /// <summary>
        /// 发出 Microsoft 中间语言 (MSIL) 以用字符串调用 Overload:System.Console.WriteLine。
        /// </summary>
        /// <param name="value">要打印的字符串。</param>
        [SecuritySafeCritical]
        public void EmitWriteLine(string value)
        {
            this.il.EmitWriteLine(value);
        }

        /// <summary>
        /// 结束异常块。
        /// </summary>
        /// <exception cref="System.InvalidOperationException">结束异常块在代码流中的意外位置出现。</exception>
        /// <exception cref="System.NotSupportedException">要生成的 Microsoft 中间语言 (MSIL) 当前不在异常块中。</exception>
        public void EndExceptionBlock()
        {
            this.il.EndExceptionBlock();
        }

        /// <summary>
        /// 结束词法范围。
        /// </summary>
        /// <exception cref="System.NotSupportedException">此 System.Reflection.Emit.ILGenerator 属于某个 System.Reflection.Emit.DynamicMethod。</exception>
        public void EndScope()
        {
            this.il.EndScope();
        }

        /// <summary>
        /// 用给定标签标记 Microsoft 中间语言 (MSIL) 流的当前位置。
        /// </summary>
        /// <param name="loc">为其设置索引的标签。</param>
        /// <exception cref="System.ArgumentException">loc 表示标签数组中的无效索引。- 或 -已定义了 loc 的索引。</exception>
        public void MarkLabel(Label loc)
        {
            this.il.MarkLabel(loc);
        }

        /// <summary>
        /// 在 Microsoft 中间语言 (MSIL) 流中标记序列点。
        /// </summary>
        /// <param name="document">为其定义序列点的文档。</param>
        /// <param name="startLine">序列点开始的行。</param>
        /// <param name="startColumn">序列点开始的行中的列。</param>
        /// <param name="endLine">序列点结束的行。</param>
        /// <param name="endColumn">序列点结束的行中的列。</param>
        /// <exception cref="System.ArgumentOutOfRangeException">startLine 或 endLine 为 <= 0。</exception>
        /// <exception cref="System.NotSupportedException">此 System.Reflection.Emit.ILGenerator 属于某个 System.Reflection.Emit.DynamicMethod。</exception>
        public void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
        {
            this.il.MarkSequencePoint(document, startLine, startColumn, endLine, endColumn);
        }

        /// <summary>
        /// 发出指令以引发异常。
        /// </summary>
        /// <param name="excType">要引发的异常类型的类。</param>
        /// <exception cref="System.ArgumentException">excType 不是 System.Exception 类或 System.Exception 的派生类。- 或 -此类型没有默认的构造函数。</exception>
        /// <exception cref="System.ArgumentNullException">excType 为 null。</exception>
        [SecuritySafeCritical]
        public void ThrowException(Type excType)
        {
            this.il.ThrowException(excType);
        }

        /// <summary>
        /// 指定用于计算当前活动词法范围的局部变量和监视值的命名空间。
        /// </summary>
        /// <param name="usingNamespace">用于计算当前活动词法范围的局部变量和监视值的命名空间。</param>
        /// <exception cref="System.ArgumentException">usingNamespace 的长度为零。</exception>
        /// <exception cref="System.ArgumentNullException">usingNamespace 为 null。</exception>
        /// <exception cref="System.NotSupportedException">此 System.Reflection.Emit.ILGenerator 属于某个 System.Reflection.Emit.DynamicMethod。</exception>
        public void UsingNamespace(string usingNamespace)
        {
            this.il.UsingNamespace(usingNamespace);
        }
    }
}
