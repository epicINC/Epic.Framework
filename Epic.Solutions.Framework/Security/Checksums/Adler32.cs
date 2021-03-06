﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Security.Checksums
{
    /// <summary>
    /// 代码来自 ICSharpCode.SharpZipLib.Checksums
    /// </summary>
    internal sealed class Adler32 : IChecksum
    {
        /// <summary>
        /// largest prime smaller than 65536
        /// </summary>
        readonly static uint BASE = 65521;

        uint checksum;

        /// <summary>
        /// Returns the Adler32 data checksum computed so far.
        /// </summary>
        public long Value
        {
            get
            {
                return checksum;
            }
        }

        /// <summary>
        /// Creates a new instance of the <code>Adler32</code> class.
        /// The checksum starts off with a value of 1.
        /// </summary>
        public Adler32()
        {
            Reset();
        }

        /// <summary>
        /// Resets the Adler32 checksum to the initial value.
        /// </summary>
        public void Reset()
        {
            checksum = 1; //Initialize to 1
        }

        /// <summary>
        /// Updates the checksum with the byte b.
        /// </summary>
        /// <param name="bval">
        /// the data value to add. The high byte of the int is ignored.
        /// </param>
        public void Update(int bval)
        {
            //We could make a length 1 byte array and call update again, but I
            //would rather not have that overhead
            uint s1 = checksum & 0xFFFF;
            uint s2 = checksum >> 16;

            s1 = (s1 + ((uint)bval & 0xFF)) % BASE;
            s2 = (s1 + s2) % BASE;

            checksum = (s2 << 16) + s1;
        }

        /// <summary>
        /// Updates the checksum with the bytes taken from the array.
        /// </summary>
        /// <param name="buffer">
        /// buffer an array of bytes
        /// </param>
        public void Update(byte[] buffer)
        {
            Update(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Updates the checksum with the bytes taken from the array.
        /// </summary>
        /// <param name="buf">
        /// an array of bytes
        /// </param>
        /// <param name="off">
        /// the start of the data used for this update
        /// </param>
        /// <param name="len">
        /// the number of bytes to use for this update
        /// </param>
        public void Update(byte[] buf, int off, int len)
        {
            if (buf == null)
            {
                throw new ArgumentNullException("buf");
            }

            if (off < 0 || len < 0 || off + len > buf.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            //(By Per Bothner)
            uint s1 = checksum & 0xFFFF;
            uint s2 = checksum >> 16;

            while (len > 0)
            {
                // We can defer the modulo operation:
                // s1 maximally grows from 65521 to 65521 + 255 * 3800
                // s2 maximally grows by 3800 * median(s1) = 2090079800 < 2^31
                int n = 3800;
                if (n > len)
                {
                    n = len;
                }
                len -= n;
                while (--n >= 0)
                {
                    s1 = s1 + (uint)(buf[off++] & 0xFF);
                    s2 = s2 + s1;
                }
                s1 %= BASE;
                s2 %= BASE;
            }

            checksum = (s2 << 16) | s1;
        }
    }
}
