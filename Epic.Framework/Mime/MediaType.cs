using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Epic.Mime
{
    public static class MediaType
    {
        [System.Runtime.InteropServices.DllImport("urlmon.dll", EntryPoint = "FindMimeFromData", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.LPWStr)] ref string ppwzMimeOut, int dwReserved);

        public static string MimeFromFile(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException(file + " not found.");

            var fs = new FileStream(file, FileMode.Open);

            var length = fs.Length > 4096 ? 4096 : (int)fs.Length;
            var buff = new byte[length + 1];

            fs.Read(buff, 0, length);
            fs.Close();

            string result = String.Empty;
            FindMimeFromData(IntPtr.Zero, file, buff, length, null, 0, ref result, 0);
            return result;

        }

        public static string MimeFromByte(string file, byte[] buff)
        {
            string result = String.Empty;
            FindMimeFromData(IntPtr.Zero, file, buff, buff.Length, null, 0, ref result, 0);
            return result;
        }


        public static string MimeFromStream(string file, Stream fs)
        {
            var length = fs.Length > 4096 ? 4096 : (int)fs.Length;

            var buff = new byte[length + 1];

            fs.Read(buff, 0, length);
            fs.Position = 0;

            string result = String.Empty;
            FindMimeFromData(IntPtr.Zero, file, buff, length, null, 0, ref result, 0);
            return result;
        }

    }
}
