using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Converter
{

    public delegate bool TryParse<T, K>(T value, out K result);

    public delegate bool TryParseArray<T, K>(T[] value, out K[] result);



}
