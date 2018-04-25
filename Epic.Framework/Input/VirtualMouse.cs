using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Input
{
    public class VirtualMouse : Mouse
    {

        IntPtr handle;


        public void Handle(IntPtr handle)
        {
            this.handle = handle;
        }


        static int MakeLParam(int LoWord, int HiWord)
        {
            return ((HiWord << 16) | (LoWord & 0xffff));
        } 


        public override void LeftClick()
        {
            throw new NotImplementedException();
        }

        public override void LeftClick(int x, int y)
        {
            SendMessage(x, y, MouseMessage.WM_LBUTTONDOWN, MouseMessage.WM_LBUTTONUP);
        }

        public override void RightClick()
        {
            throw new NotImplementedException();
        }

        public override void RightClick(int x, int y)
        {
            SendMessage(x, y, MouseMessage.WM_RBUTTONDOWN, MouseMessage.WM_RBUTTONUP);
        }



        public override void MoveTo(int x, int y)
        {
            SendMessage(MouseMessage.WM_MOUSEMOVE, x, y);
        }



        void SendMessage(int x, int y, params MouseMessage[] msgs)
        {
            var l = MakeLParam(x, y);

            foreach (var item in msgs)
                SendMessage((int)item, l);
        }


        void SendMessage(MouseMessage wMsg, int l)
        {
            Windows.User32.SendMessage(this.handle, (int)wMsg, 0, l);
        }

        void SendMessage(MouseMessage wMsg, int x, int y)
        {
            Windows.User32.SendMessage(this.handle, (int)wMsg, 0, MakeLParam(x, y));
        }

    }
}
