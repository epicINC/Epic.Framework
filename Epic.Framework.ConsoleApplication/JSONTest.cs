using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Epic.Framework.ConsoleApplication
{
    public static class JSONTest
    {

        public static T Read<T>(string value)
        {
            using (var ms = new MemoryStream())
            {
                using (var sr = new StreamWriter(ms))
                {
                    sr.Write(value);
                    sr.Flush();
                    ms.Position = 0;
                    return (T)(new DataContractJsonSerializer(typeof(T))).ReadObject(ms);

                }
            }
        }

        public static string Write<T>(T value)
        {
            using (var ms = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(ms, value);
                ms.Position = 0;
                using (var sr = new StreamReader(ms, System.Text.Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }

    [DataContract]
    public class UserInDepartment
    {
        [DataMember(Order=0)]
        public int ID
        {
            get;
            set;
        }

        [DataMember(Name = "DID", Order = 1)]
        public int DepartmentID
        {
            get;
            set;
        }

        [DataMember(Name = "UID", Order = 2)]
        public int UserID
        {
            get;
            set;
        }

        [DataMember(Name = "RIDS", Order = 3)]
        public List<int> RoleIDS
        {
            get;
            set;
        }

        [DataMember(Name = "TIDS", Order = 4)]
        public List<int> TabRoleIDS
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public DateTime CreateDate
        {
            get;
            set;
        }
    }
}
