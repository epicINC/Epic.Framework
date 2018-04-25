using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions.FluentExtensions
{
    internal class FluentContainer
    {
        internal static IFluentContainer<T> Create<T>(T value)
        {
            return new FluentContainer<T>(value);
        }

        internal static IFluentRemoveContainer<T> CreateRemove<T>(IFluentContainer<T> value)
        {
            return new FluentRemoveContainer<T>(value.Value);
        }
    }

    public interface IFluentContainer<T>
    {
        T Value { get; set; }
    }

    public interface IFluentRemoveContainer<T> : IFluentContainer<T>
    {
    }


    /// <summary>
    /// Flunt API 容器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FluentContainer<T> : IFluentContainer<T>
    {

        internal FluentContainer(T value)
        {
            this.Value = value;
        }


        public T Value
        {
            get;
            set;
        }
    }



    public class FluentRemoveContainer<T> : FluentContainer<T>, IFluentRemoveContainer<T>
    {
        internal FluentRemoveContainer(T value) : base(value)
        {
        }

    }


}
