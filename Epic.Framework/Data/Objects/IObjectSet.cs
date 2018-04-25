using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Epic.Data.Objects
{
    public interface IObjectSet<T> : IQueryable<T>, IEnumerable<T>, IQueryable, IEnumerable where T: class
    {
    }


    public static class IObjectSetEX
    {


        public static int Save<T>(this T value) where T : class, IObjectSet<T>
        {
            return 0;
        }

        public static int Delete<T>(this T value) where T : class, IObjectSet<T>
        {
            return 0;
        }

    }

}
