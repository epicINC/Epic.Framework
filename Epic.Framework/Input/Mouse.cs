using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Input
{
    public abstract class Mouse
    {

        public abstract void LeftClick();
        public abstract void LeftClick(int x, int y);
        public abstract void RightClick(int x, int y);
        public abstract void RightClick();
        public abstract void MoveTo(int x, int y);

    }
}
