using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Epic.IO
{
    /// <summary>
    /// File IO 扩展
    /// </summary>
    /// <remarks>
    /// 2011-04-26 Create(S)
    /// </remarks>
    public static class FileExtensions
    {

        /// <summary>
        /// 如果文件存在 则删除文件
        /// </summary>
        /// <remarks>
        /// 2011-04-26 Create(S)
        /// </remarks>
        /// <param name="path">文件路径</param>
        /// <returns>返回 文件是否存在</returns>
        public static bool DelIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;

        }
    }
}
