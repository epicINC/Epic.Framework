using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.AOP
{
    internal class ObjectInterceptorItem
    {
        internal ObjectInterceptorItem(string name, params IInterceptor[] values)
        {
            this.Name = name;
            this.Value = new List<IInterceptor>();
            this.Value.AddRange(values);
        }


        public string Name
        {
            get;
            set;
        }

        public List<IInterceptor> Value
        {
            get;
            set;
        }
    }

    internal class ObjectInterceptor
    {
        List<ObjectInterceptorItem> items = new List<ObjectInterceptorItem>();
        internal ObjectInterceptor()
        {
            this.Property = new List<IInterceptor>();
            this.Method = new List<IInterceptor>();
        }

        public IInterceptor GetProperty(string name)
        {
            var result = this.Find(name);
            if (this.Property != null || this.Property.Count > 0)
            {
                return Combine(result.Value, this.Property);
            }
            return Combine(result.Value.ToArray());
        }

        public ObjectInterceptorItem Find(string name)
        {
            return this.Find(e => e.Name == name);
        }

        public ObjectInterceptorItem Find(Func<ObjectInterceptorItem, bool> selector)
        {
            if (this.items.Count == 0) return null;
            return items.SingleOrDefault(selector);
        }


        public void Set(string name, Func<string, object[], object> beforeCall, Action<string, object, object> afterCall)
        {
            this.Set(name, new FuncInterceptor(beforeCall, afterCall));
        }

        public void Set(string name, IInterceptor value)
        {
            var result = this.Find(name);
            if (result == null)
            {
                this.items.Add(new ObjectInterceptorItem(name, value));
                return;
            }
            if (result.Value.Exists(e => e == value)) return;

            result.Value.Add(value);

        }


        public List<IInterceptor> Cotr
        {
            get;
            set;
        }


        public List<IInterceptor> Property
        {
            get;
            set;
        }

        public List<IInterceptor> Method
        {
            get;
            set;
        }

        static IInterceptor Combine(List<IInterceptor> source, List<IInterceptor> dest)
        {
            var collection = new List<IInterceptor>();
            collection.AddRange(source);
            collection.AddRange(dest);
            return Combine(collection.ToArray());
        }

        static IInterceptor Combine(params IInterceptor[] collection)
        {
            if (collection == null || collection.Count() == 0) return null;
            var item = collection[0];
            for (int i = 1; i < collection.Length; i++)
            {
                item = Combine(item, collection[i]);
            }
            return item;
        }

        static IInterceptor Combine(IInterceptor source, IInterceptor dest)
        {
            if (source == null) return dest;
            if (dest == null) return source;
            return new FuncInterceptor(
                (name, values) =>
                {
                    return new Tuple<object, object>(source.BeforeCall(name, values), dest.BeforeCall(name, values));
                },
                (name, result, state) =>
                {
                    var tuple = state as Tuple<object, object>;
                    source.AfterCall(name, result, tuple.Item1);
                    dest.AfterCall(name, result, tuple.Item2);
                }
                );
        }


    }
}
