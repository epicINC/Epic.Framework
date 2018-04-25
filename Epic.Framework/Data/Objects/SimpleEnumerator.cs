using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Epic.Data.Objects
{
    internal class SimpleEnumerator<T> : IEnumerator<T>
    {

        internal SimpleEnumerator(IDataReader reader)
        {
            this.reader = reader;
        }

        IDataReader reader;
        T current;

        public T Current
        {
            get { return this.current; }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.reader.Close();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.current; }
        }

        public bool MoveNext()
        {
            if (this.reader.Read())
            {
                this.current = (T)reader.GetValue(0);
                return true;
            }
            this.Dispose();
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
