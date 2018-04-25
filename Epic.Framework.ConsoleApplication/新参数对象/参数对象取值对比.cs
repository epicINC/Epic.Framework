using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Framework.ConsoleApplication.新参数对象
{
    class 参数对象取值对比
    {
        public static void Test()
        {

            Func<RssItem, int> value = e => e.CategoryID;
            var il = value.Method.GetMethodBody().GetILAsByteArray();

            var result = FindProperty(Epic.Emit.ILDisassembler.Disassemble(value.Method));


        }

        static string FindProperty(string value)
        {
            var offset = value.IndexOf("get_") + 4;
            var end = value.IndexOf(Environment.NewLine, offset);

            return value.Substring(offset, end - offset);

        }

        public static void Test1()
        {
            Expression<Func<RssItem, int>> value = e => e.CategoryID;
            var result = Epic.FluentAPI.FluentAPIExtensions.GetComplexPropertyAccess(value).Single().Name;

        }

    }
}
