using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic
{
    public delegate bool ParseAction<TInput, TOutput>(TInput input, out TOutput output);
}
