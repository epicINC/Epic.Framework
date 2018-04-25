using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Epic.Data.V2
{
    public class ObjectEnumerator<T> : IEnumerator<T>
    {

        IDataReader reader;
        IEnumerator<T> e;
        internal ObjectEnumerator(IDataReader reader)
        {
            this.reader = reader;
            this.e = Emit.EmitReader.GetEnumerator<T>(reader).GetEnumerator();

        }

        public T Current
        {
            get { return e.Current; }
        }

        public void Dispose()
        {
            e.Dispose();
            this.reader.Close();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }

        public bool MoveNext()
        {
            return e.MoveNext();
        }

        public void Reset()
        {
            e.Reset();
        }
    }
}
