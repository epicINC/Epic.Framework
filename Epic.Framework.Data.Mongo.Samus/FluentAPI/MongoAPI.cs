using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.Mongo.Samus.FluentAPI
{

    /// <summary>
    /// xcopy "$(TargetDir)*.*" "..\..\..\Publish" /S /I /F <NUL:
    /// MongoAPI<User>.ID(e => e.ID).Set((e, id) => e.ID = id);
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoAPI<T>
    {

        static object locker = new object();
        static MongoAPIConfig<T> instant;

        static MongoAPI()
        {
            lock (locker)
            {
                instant = new MongoAPIConfig<T>();
            }
        }


        public static string Name
        {
            get { return instant.Name; }
        }


        public static string FullName
        {
            get { return instant.FullName; }
        }


        public static MongoAPIConfig<T> Config
        {
            get { return instant; }
        }


        public static bool HasID()
        {
            return instant.ID != null;
        }

        public static bool IsValidID(T value)
        {
            return HasID() && GetID(value) > 0;
        }


        public static void SetID(T value, int id)
        {
            instant.ID.Set(value, id);
        }

        public static int GetID(T value)
        {
            return instant.ID.Get(value);
        }


        public static MongoAPIConfig<T> Instant
        {
            get { return instant; }
        }

    
    }

    public static class MongoAPIHelper
    {
        public static MongoAPIConfig<T> GetID<T>(this MongoAPIConfig<T> value, Func<T, int> action)
        {
            value.ID.Get = action;
            return value;
        }

        public static MongoAPIConfig<T> TableAlias<T>(this MongoAPIConfig<T> value, string name)
        {
            value.TableAlias = name;
            return value;
        }


        public static MongoAPIConfig<T> SetID<T>(this MongoAPIConfig<T> value, Action<T, int> action)
        {
            value.ID.Set = action;
            return value;
        }



    }

}
