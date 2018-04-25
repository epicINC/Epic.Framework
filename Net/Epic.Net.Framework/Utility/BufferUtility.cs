using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Net.Utility
{
    public static class BufferUtility
    {

        public static unsafe byte[] ToBytes(char value)
        {
            return FromStruct((byte*)&value, 2);
        }

        public static unsafe byte[] ToBytes(short value)
        {
            return FromStruct((byte*)&value, 2);
        }

        public static unsafe byte[] ToBytes(int value)
        {
            return FromStruct((byte*)&value, 4);
        }

        public static unsafe byte[] ToBytes(long value)
        {
            return FromStruct((byte*)&value, 8);
        }

        public static unsafe byte[] ToBytes(ushort value)
        {
            return FromStruct((byte*)&value, 2);
        }

        public static unsafe byte[] ToBytes(uint value)
        {
            return FromStruct((byte*)&value, 4);
        }

        public static unsafe byte[] ToBytes(ulong value)
        {
            return FromStruct((byte*)&value, 8);
        }

        public unsafe static DateTime ToDateTime(byte[] source, int offset)
        {
            fixed (byte* pb = &source[offset])
            {
                return *(DateTime*)pb;
            }
        }

        unsafe static short ToInt16(byte[] source, int offset)
        {
            fixed (byte* pt = &source[offset])
            {
                return *(short*)pt;
            }
        }

        unsafe static uint ToUInt32(byte[] source, int offset)
        {
            fixed (byte* pt = &source[offset])
            {
                return *(uint*)pt;
            }
        }

        unsafe static int ToInt32(byte[] source, int offset)
        {
            fixed (byte* pt = &source[offset])
            {
                return *(int*)pt;
            }
        }

        unsafe static double ToDouble(byte[] source, int offset)
        {
            fixed (byte* pt = &source[offset])
            {
                return *(double*)pt;
            }
        }


        public unsafe static void ToStruct(byte[] source, int offset, int size, byte* pDest)
        {
            fixed (byte* pt = &source[offset])
            {
                byte* ps = pt;
                for (int i = 0; i < size; i++)
                {
                    *pDest = *ps;
                    ps++;
                    pDest++;
                }
            }
        }

        public unsafe static byte[] FromStruct(byte* value, int size)
        {

            var result = new byte[size];
            fixed (byte* pt = result)
            {
                byte* ps = pt;
                byte* px = value;

                for (int i = 0; i < size; i++)
                {
                    *ps = *px;
                    ps++;
                    px++;
                }
            }
            return result;
        }

        public unsafe static byte[] FromStruct(DateTime value)
        {
            var result = new byte[sizeof(DateTime)];
            fixed (byte* pt = result)
            {
                *((DateTime*)pt) = value;
            }
            return result;
        }

        public unsafe static byte[] FromStruct(byte[] source, int offset, DateTime value)
        {
            if (source.Length < sizeof(DateTime))
                throw new ArgumentOutOfRangeException("source");

            fixed (byte* pt = &source[offset])
            {
                *((DateTime*)pt) = value;
            }
            return source;
        }



        static unsafe void Copy(byte[] source, int sourceOffset, byte[] dest, int targetOffset, int count)
        {
            Buffer.BlockCopy(source, sourceOffset, dest, targetOffset, count);
            return;

            if ((source == null) || (dest == null))
                throw new System.ArgumentException();

            if ((sourceOffset < 0) || (targetOffset < 0) || (count < 0))
                throw new System.ArgumentException();


            if ((source.Length - sourceOffset < count) ||
                (dest.Length - targetOffset < count))
                throw new System.ArgumentException();


            fixed (byte* pSource = source, pTarget = dest)
            {
                byte* ps = pSource + sourceOffset;
                byte* pt = pTarget + targetOffset;

                for (int i = 0; i < count; i++)
                {
                    *pt = *ps;
                    pt++;
                    ps++;
                }
            }
        }

        static unsafe void Memcpy(byte* src, byte* dest, int len)
        {
            if (len >= 0x10)
            {
                do
                {
                    *((int*)dest) = *((int*)src);
                    *((int*)(dest + 4)) = *((int*)(src + 4));
                    *((int*)(dest + 8)) = *((int*)(src + 8));
                    *((int*)(dest + 12)) = *((int*)(src + 12));
                    dest += 0x10;
                    src += 0x10;
                }
                while ((len -= 0x10) >= 0x10);
            }
            if (len > 0)
            {
                if ((len & 8) != 0)
                {
                    *((int*)dest) = *((int*)src);
                    *((int*)(dest + 4)) = *((int*)(src + 4));
                    dest += 8;
                    src += 8;
                }
                if ((len & 4) != 0)
                {
                    *((int*)dest) = *((int*)src);
                    dest += 4;
                    src += 4;
                }
                if ((len & 2) != 0)
                {
                    *((short*)dest) = *((short*)src);
                    dest += 2;
                    src += 2;
                }
                if ((len & 1) != 0)
                {
                    dest++;
                    src++;
                    dest[0] = src[0];
                }
            }
        }

        static unsafe void Memcpy(byte* src, int srcIndex, byte[] dest, int destIndex, int len)
        {
            if (len != 0)
            {
                fixed (byte* pDest = dest)
                {
                    Memcpy(src + srcIndex, pDest + destIndex, len);
                }
            }
        }
        static unsafe void Memcpy(byte[] src, int srcIndex, byte* pDest, int destIndex, int len)
        {
            if (len != 0)
            {
                fixed (byte* pSrc = src)
                {
                    Memcpy(pSrc + srcIndex, pDest + destIndex, len);
                }
            }
        }

        static unsafe void Memcpy(char* pSrc, int srcIndex, char* pDest, int destIndex, int len)
        {
            if (len != 0)
            {
                Memcpy((byte*)(pSrc + srcIndex), (byte*)(pDest + destIndex), len * 2);
            }
        }

        static unsafe void Memcpy(byte[] src, int srcIndex, byte[] dest, int destIndex, int len)
        {
            if (len != 0)
            {
                fixed (byte* pDest = dest, pSrc = src)
                {
                    Memcpy(pSrc + srcIndex, pDest + destIndex, len);
                }
            }
        }


    }
}
