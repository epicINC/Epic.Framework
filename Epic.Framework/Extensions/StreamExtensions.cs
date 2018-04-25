using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Epic.Extensions
{
    public static class StreamExtensions
    {
        public static void Write(this Stream stream, byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
