using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{

    public static class ConvertExtensions
    {

        public static bool TryParse<TInput, TOutput>(this TInput[] input, out TOutput[] output, ParseAction<TInput, TOutput> action, bool force)
        {
            List<TOutput> result = new List<TOutput>();
            bool convertResult = true;
            TOutput local;
            foreach (var item in input)
            {
                
                if (action(item, out local))
                    result.Add(local);
                else
                {
                    if (!force)
                    {
                        convertResult = false;
                        break;
                    }
                }
                
            }
            output = result.ToArray();
            return convertResult || output.Length > 0;
        }




    }
}
