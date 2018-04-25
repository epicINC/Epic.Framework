using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections;
using Epic.Data.Mapper;

namespace Epic.Data.Builder
{
    internal class ObjectReader<T> : IEnumerable<T> where  T : class, new()
    {
        Enumerator enumerator;

        internal ObjectReader(DbDataReader reader)
        {
            this.enumerator = new Enumerator(reader);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var e = this.enumerator;
            if (e == null)
                throw new InvalidOperationException("Cannot enumerate more than once");
            this.enumerator = null;
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        class Enumerator : IEnumerator<T>, IDisposable
        {
            DbDataReader reader;
            T current;

            internal Enumerator(DbDataReader reader)
            {
                this.reader = reader;
            }


            public T Current
            {
                get { return this.current; }
            }


            object IEnumerator.Current
            {
                get { return this.current; }
            }


            public bool MoveNext()
            {
                if (this.reader.Read())
                {
                    this.current = EmitSqlDataMapperWrapper<T>.Convert(reader);
                    return true;
                }
                return false;
            }


            public void Reset()
            {
            }


            public void Dispose()
            {
                this.reader.Dispose();
            }
        }
    }
}
