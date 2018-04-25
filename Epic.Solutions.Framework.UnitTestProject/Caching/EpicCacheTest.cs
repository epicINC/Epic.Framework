using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Epic.Caching;

namespace Epic.Solutions.Framework.UnitTestProject
{
    public class User
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }



    [TestClass]
    public class EpicCacheTest
    {

        int i = 1;

        public User Init()
        {
            return new User() { ID = i++, Name = "test" };
        }








        [TestMethod]
        public void 静态实例测试()
        {
            var value = this.Init();
            EpicRuntimeCache<User>.Value = value;

            Assert.AreEqual(value.ID, EpicRuntimeCache<User>.Value.ID);

            EpicRuntimeCache<User>.Interval = 1;
            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual(null, EpicRuntimeCache<User>.Value);

        }



        [TestMethod]
        public void 静态委托测试()
        {
            this.i = 1;
            EpicRuntimeCache<User>.Init = this.Init;
            Assert.AreEqual(1, EpicRuntimeCache<User>.Value.ID);

            EpicRuntimeCache<User>.Interval = 1;
            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual(2, EpicRuntimeCache<User>.Value.ID);

        }

    }


}
