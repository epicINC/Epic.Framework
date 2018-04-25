using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Emit.RunSharp
{
    [Flags]
    public enum AccessAttributes
    {
        // 摘要:
        //     指示该成员不能被引用。
        PrivateScope = 0,
        //
        // 摘要:
        //     指示此方法将重复使用 vtable 中的现有槽。这是默认行为。
        ReuseSlot = 0,
        //
        // 摘要:
        //     指示此方法只能由当前类访问。
        Private = 1,
        //
        // 摘要:
        //     指示此方法只能由该类型和它在此程序集中的派生类型的成员访问。
        FamANDAssem = 2,
        //
        // 摘要:
        //     指示此方法可由该程序集的任何类访问。
        Assembly = 3,
        //
        // 摘要:
        //     指示此方法只可由该类及其派生类的成员访问。
        Family = 4,
        //
        // 摘要:
        //     指示此方法可由任意位置的派生类访问，也可由程序集中的任何类访问。
        FamORAssem = 5,
        //
        // 摘要:
        //     指示此方法可由任何包括该对象的对象访问。
        Public = 6,
        //
        // 摘要:
        //     检索可访问性信息。
        MemberAccessMask = 7,
        //
        // 摘要:
        //     指示此托管方法由 thunk 导出为非托管代码。
        UnmanagedExport = 8,
        //
        // 摘要:
        //     指示在类型上定义此方法，否则基于每个实例定义此方法。
        Static = 16,
        //
        // 摘要:
        //     指示无法重写此方法。
        Final = 32,
        //
        // 摘要:
        //     指示此方法为虚方法。
        Virtual = 64,
        //
        // 摘要:
        //     指示此方法按名称和签名隐藏，否则只按名称隐藏。
        HideBySig = 128,
        //
        // 摘要:
        //     检索 vtable 属性。
        VtableLayoutMask = 256,
        //
        // 摘要:
        //     指示此方法总是获取 vtable 中的新槽。
        NewSlot = 256,
        //
        // 摘要:
        //     指示仅当此方法可访问时，才可以对其进行重写。
        CheckAccessOnOverride = 512,
        //
        // 摘要:
        //     指示此类不提供此方法的实现。
        Abstract = 1024,
        //
        // 摘要:
        //     指示此方法是特殊的。名称描述此方法的特殊性。
        SpecialName = 2048,
        //
        // 摘要:
        //     指示公共语言运行时检查名称编码。
        RTSpecialName = 4096,
        //
        // 摘要:
        //     指示此方法的实现通过 PInvoke（平台调用服务）转发。
        PinvokeImpl = 8192,
        //
        // 摘要:
        //     指示此方法具有关联的安全性。保留此标志仅供运行时使用。
        HasSecurity = 16384,
        //
        // 摘要:
        //     指示此方法调用另一个包含安全性代码的方法。保留此标志仅供运行时使用。
        RequireSecObject = 32768,
        //
        // 摘要:
        //     指示仅供运行时使用的保留标志。
        ReservedMask = 53248,


        //
        // 摘要:
        //     指定给定字段的访问级别。
        FieldAccessMask = 7,

        //
        // 摘要:
        //     指定该字段只能初始化，初始化后不能写入。
        InitOnly = 32,
        //
        // 摘要:
        //     指定该字段的值是一个编译时（静态或早期绑定）常数。只能从构造函数设置该字段；对其进行设置的其他任何尝试都会引发 System.FieldAccessException。
        Literal = 64,
        //
        // 摘要:
        //     指定扩展类型时不必序列化该字段。
        NotSerialized = 128,
        //
        // 摘要:
        //     指定该字段具有相对虚拟地址 (RVA)。RVA 是方法体在当前图像中的位置，它是相对于它所在的图像文件的开始的地址。
        HasFieldRVA = 256,

        //
        // 摘要:
        //     指定该字段包含封送处理信息。
        HasFieldMarshal = 4096,

        //
        // 摘要:
        //     指定该字段具有默认值。
        HasDefault = 32768,


    }
}
