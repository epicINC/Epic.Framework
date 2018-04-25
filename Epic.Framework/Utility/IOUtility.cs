using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;

namespace Epic.Utility
{
    public static class IOUtility
    {

        public static string BaseDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        public static string PathCombine(params string[] paths)
        {
            if (paths == null || paths.Length == 0) throw Error.ArgumentNull("paths");


            var result = new List<string>();


            var isVirtualPath = paths[0][0] == '~';
            if (isVirtualPath)
                result.Add(BaseDirectory);

            foreach (var item in paths)
            {
                if (String.IsNullOrWhiteSpace(item)) continue;
                result.Add(item.Trim('/', '\\', '~'));
            }
            return String.Join<string>(Path.DirectorySeparatorChar.ToString(), result);

        }



        #region CreateDirectory
        // The "_mkdir" function is used by the "CreateDirectory" method.
        [DllImport("msvcrt.dll", SetLastError = true)]
        private static extern int _mkdir(string path);

        public static DirectoryInfo CreateDirectory(string path)
        {
            DirectoryInfo oDir = new DirectoryInfo(Path.GetFullPath(path));

            try
            {
                if (!oDir.Exists)
                    oDir.Create();
                return oDir;
            }
            catch
            {
                CreateDirectoryUsingDll(oDir);
                return new DirectoryInfo(path);
            }
        }

        private static void CreateDirectoryUsingDll(DirectoryInfo dir)
        {
            ArrayList oDirsToCreate = new ArrayList();

            while (dir != null && !dir.Exists)
            {
                oDirsToCreate.Add(dir.FullName);
                dir = dir.Parent;
            }

            if (dir == null)
                throw (new System.IO.DirectoryNotFoundException("Directory \"" + oDirsToCreate[oDirsToCreate.Count - 1] + "\" not found."));

            for (int i = oDirsToCreate.Count - 1; i >= 0; i--)
            {
                string sPath = (string)oDirsToCreate[i];
                int iReturn = _mkdir(sPath);

                if (iReturn != 0)
                    throw new ApplicationException("Error calling [msvcrt.dll]:_wmkdir(" + sPath + "), error code: " + iReturn);
            }
        }

        #endregion
    }
}
