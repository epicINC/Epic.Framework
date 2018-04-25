using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic
{
    public class FileLogWriter
    {
        public FileLogWriter(string path)
        {


            this.Stack = new Stack<string>();


            Task.Run(async () =>
            {
                var timeout = 1000;
                while (true)
                {
                    if (this.WriteToFile())
                        await Task.Delay(timeout = 1000);
                    else
                    {
                        await Task.Delay(timeout += 1000);
                        if (timeout > 10000)
                            timeout = 1000;
                    }
                }
            });
            
        }


        bool WriteToFile()
        {
            if (this.Stack.Count == 0) return false;
            CheckDir(this.Path);
            using (var writer = new StreamWriter(this.Path, true))
            {
                while (this.Stack.Count > 0)
                {
                    writer.WriteLine(this.Stack.Pop());
                }
            }
            return true;
        }

        Stack<string> Stack
        {
            get;
            set;
        }

        public string Path
        {
            get;
            private set;
        }

        static void CheckDir(string path)
        {
            var dir = System.IO.Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }



    public class LogWriter
    {



        public LogWriter(LogLevelType level, Stream stream)
        {
            this.Level = level;
            this.Writer = new StreamWriter(stream);
        }



        public LogLevelType Level
        {
            get;
            set;
        }

        StreamWriter Writer
        {
            get;
            set;
        }


        public Stream Steam
        {
            get;
            set;
        }

        public bool Assert(bool condition, string message = null, string detailMessage = null)
        {
            if (!condition)
                this.WriteLine("Assert: {0}, {1}", message, detailMessage);

            return condition;
        }

        #region Write

        string Json(object value)
        {
            try
            {
                return Epic.Serialization.JsonFormatter.Serialize(value);
            }
            catch
            {
               
            }

            return String.Empty;
        }

        protected virtual void Write(string value)
        {
            this.Writer.Write(DateTime.Now.ToShortTimeString() +" "+ value);
        }

        void WriteLine(string value)
        {
            this.Write(value + Environment.NewLine);
        }

        void Write(string format, params string[] args)
        {
            this.Write(String.Format(format, args));
        }

        void WriteLine(string format, params string[] args)
        {
            this.WriteLine(String.Format(format, args));
        }

        public bool Write(bool condition, object value)
        {
            return this.Write(this.Write, condition, value);
        }

        public bool WriteLine(bool condition, object value)
        {
            return this.Write(this.WriteLine, condition, value);
        }

        bool Write(Action<string> writer, bool condition, object value)
        {
            return this.Write(writer, condition, this.Json(value));
        }

        public bool Write(bool condition, string value)
        {
            return this.Write(this.Write, condition, value);
        }

        public bool WriteLine(bool condition, string value)
        {
            return this.Write(this.WriteLine, condition, value);
        }

        bool Write(Action<string> writer, bool condition, string value)
        {
            if (condition)
                writer(value);

            return condition;
        }

        #endregion

        #region Trace

        public void Trace(string value)
        {
            if (this.Level.HasFlag(LogLevelType.Trace))
                this.Write("Trace: "+ value);
        }

        public void Trace(string format, params object[] args)
        {
            this.Trace(String.Format(format, args));
        }

        public void TraceLine(string value)
        {
            this.Trace(value + Environment.NewLine);
        }

        public void TraceLine(string format, params object[] args)
        {
            this.TraceLine(String.Format(format, args));
        }

        #endregion

        #region Warn

        public void Warn(string value)
        {
            if (this.Level.HasFlag(LogLevelType.Warn))
                this.Write("Warn: " + value);
        }

        public void Warn(string format, params object[] args)
        {
            this.Warn(String.Format(format, args));
        }

        public void WarnLine(string value)
        {
            this.Warn(value + Environment.NewLine);
        }

        public void WarnLine(string format, params object[] args)
        {
            this.WarnLine(String.Format(format, args));
        }

        #endregion

        #region Error

        public void Error(string value)
        {
            if (this.Level.HasFlag(LogLevelType.Error))
                this.Write("Error: " + value);
        }

        public void Error(string format, params string[] args)
        {
            this.Error(String.Format(format, args));
        }

        public void ErrorLine(string value)
        {
            this.Error(value + Environment.NewLine);
        }

        public void ErrorLine(string format, params string[] args)
        {
            this.ErrorLine(String.Format(format, args));
        }

        public void Error(byte[] value)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("byte[{0}]{{", value.Length);
            sb.Append(String.Join(",", value));
            sb.Append("};");

            this.Error(sb.ToString());
        }



        #endregion

        #region Success

        public void Success(string value)
        {
            if (this.Level.HasFlag(LogLevelType.Success))
                this.Write("Success: " + value);
        }

        public void Success(string format, params string[] args)
        {
            this.Success(String.Format(format, args));
        }

        public void SuccessLine(string value)
        {
            this.Success(value + Environment.NewLine);
        }

        public void SuccessLine(string format, params string[] args)
        {
            this.SuccessLine(String.Format(format, args));
        }

        #endregion

        #region Exception

        public void Write(Exception value)
        {
            this.Write(this.Write, value);  
        }

        public void WriteLine(Exception value)
        {
            this.Write(this.WriteLine, value);
        }

        void Write(Action<string> writer, Exception value)
        {
            if (value == null) return;

            writer(String.Format("Exception: {0}, {1}", value.Message, value.StackTrace));
        }

        #endregion

    }
}
