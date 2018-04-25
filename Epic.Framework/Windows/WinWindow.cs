using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Epic.Windows
{

    
    public static class ClassNames
    {
        public const string IE = "Internet Explorer_Server";
        public const string Flash = "MacromediaFlashPlayerActiveX";
    }


    public class WinWindow
    {
        public IntPtr root;
        public IntPtr child;

        public IntPtr FindWindow(string windowsName)
        {
            return FindWindow(null, windowsName);
            var t = System.Xml.Linq.XElement.Parse("");

        }

        public IntPtr Descendants(string findClass)
        {
            StringBuilder className = new StringBuilder(100);
            var handle = this.root;
            while (className.ToString() != findClass) // The class control for the browser
            {
                handle = User32.GetWindow(handle, 5); // Get a handle to the child window
                User32.GetClassName(handle, className, className.Capacity);
            }
            return handle;
        }

        public IntPtr FindWindow(string className, string windowsName)
        {
            return this.root = User32.FindWindow(className, windowsName);
        }

        public IntPtr FindWindowEx(string className, string windowsName)
        {
            return this.child = User32.FindWindowEx(root, IntPtr.Zero, className, windowsName);
        }


        public IntPtr FindWindowEx(string className)
        {
            if (className.IndexOf('>') > 0)
            {
                return FindWindowEx(className.Split('>'));
            }
            else
            {
                return FindWindowEx(className, null);
            }
            
        }

        public IntPtr FindWindowEx(params string[] paths)
        {

            IntPtr result = this.child == IntPtr.Zero ? this.root : this.child;
            foreach (var item in paths)
            {
                var temp = User32.FindWindowEx(result, IntPtr.Zero, item, null);
                if (temp == IntPtr.Zero) break;
                result = temp;
            }

            return result = this.child;
        }

        public void Activate()
        {
            if (this.root == IntPtr.Zero) return;

            User32.SetForegroundWindow(this.root);
        }






    }
}
