using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Epic
{
        [Flags]
    public enum LogLevelType
    {
        Trace = 1 << 0,
        Warn = 1 << 1,
        Error = 1 << 2,
        Success = 1 << 3,
        Debug = Trace | Warn | Error | Success,
        Release = Warn | Error,
        All = Trace | Warn | Error | Success
    }



    public static class Loggin
    {

        public static Stream Stream
        {
            get;
            set;
        }



    }
}
