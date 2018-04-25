using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Epic.Extensions
{
    public static class SizeExtensions
    {
        public static bool TryParse(this string input, out Size output)
        {
            int width; int height = 0;
            int offset = input.IndexOf("x");
            if (offset > -1)
            {
                Int32.TryParse(input.Substring(0, offset), out width);
                Int32.TryParse(input.Substring(offset + 1), out height);
            }
            else
                Int32.TryParse(input, out width);

            if (width == 0)
                width = height;
            if (height == 0)
                height = width;

            output = new Size(width, height);
            return true;
        }

    }
}
