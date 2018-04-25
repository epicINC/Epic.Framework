using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Epic.MVC.Comet
{
    public class CometController : AsyncController
    {
        public void TestAsync()
        {
            System.Timers.Timer timer = new System.Timers.Timer(5000);
            AsyncManager.OutstandingOperations.Increment();

            timer.Elapsed += (sender, e) =>
                {

                    AsyncManager.Parameters["now"] = e.SignalTime;

                    AsyncManager.OutstandingOperations.Decrement();
                };

            timer.Start();
        }

        public ActionResult TestCompleted(DateTime now)
        {
            return Json(new
            {
                d = now.ToString("MM-dd HH:mm:ss ") +
                    "-- Welcome to cnblogs.com!"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
