using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Epic.Utility;

namespace Epic.Solutions.Framework.UnitTestProject.Utility
{
    [Flags]
    public enum Token
    {
        Insert = 1 << 0,
        Update = 1 << 1,
        Delete = 1 << 2,
        All = Insert | Update | Delete
    }


    //[TestClass]
    //public class EnumTest
    //{
    //    [TestMethod]
    //    public void 枚举运算()
    //    {
    //        Assert.AreEqual(EnumHelper.And(Token.Insert, Token.Update), Token.Insert & Token.Update);
    //        Assert.AreEqual(EnumHelper.Or(Token.Insert, Token.Update), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Xor(Token.Insert, Token.Update), Token.Insert ^ Token.Update);
    //        Assert.AreEqual(EnumHelper.Add(Token.Insert, Token.Update), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Remove(Token.All, Token.Delete), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Set(Token.All, Token.Delete), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Set(Token.Insert, Token.Update), Token.Insert | Token.Update);

    //        Assert.IsTrue(EnumHelper.HasValue(Token.All, Token.Update));
    //        Assert.IsFalse(EnumHelper.HasValue(Token.Insert, Token.Update));
    //    }

    //    [TestMethod]
    //    public void 枚举运算Byte()
    //    {
    //        Assert.AreEqual(EnumHelper.And(Token.Insert, (byte)2), Token.Insert & Token.Update);
    //        Assert.AreEqual(EnumHelper.Or(Token.Insert, (byte)2), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Xor(Token.Insert, (byte)2), Token.Insert ^ Token.Update);
    //        Assert.AreEqual(EnumHelper.Add(Token.Insert, (byte)2), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Remove(Token.All, (byte)4), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Set(Token.All, (byte)4), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Set(Token.Insert, (byte)2), Token.Insert | Token.Update);

    //        Assert.IsTrue(EnumHelper.HasValue(Token.All, (byte)2));
    //        Assert.IsFalse(EnumHelper.HasValue(Token.Insert, (byte)2));
    //    }

    //    [TestMethod]
    //    public void 枚举运算Int()
    //    {
    //        Assert.AreEqual(EnumHelper.And(Token.Insert, 2), Token.Insert & Token.Update);
    //        Assert.AreEqual(EnumHelper.Or(Token.Insert, 2), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Xor(Token.Insert, 2), Token.Insert ^ Token.Update);
    //        Assert.AreEqual(EnumHelper.Add(Token.Insert, 2), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Remove(Token.All, 4), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Set(Token.All, 4), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Set(Token.Insert, 2), Token.Insert | Token.Update);

    //        Assert.IsTrue(EnumHelper.HasValue(Token.All, 2));
    //        Assert.IsFalse(EnumHelper.HasValue(Token.Insert, 2));
    //    }


    //    [TestMethod]
    //    public void 枚举运算Long()
    //    {
    //        Assert.AreEqual(EnumHelper.And(Token.Insert, 2L), Token.Insert & Token.Update);
    //        Assert.AreEqual(EnumHelper.Or(Token.Insert, 2L), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Xor(Token.Insert, 2L), Token.Insert ^ Token.Update);
    //        Assert.AreEqual(EnumHelper.Add(Token.Insert, 2L), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Remove(Token.All, 4L), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Set(Token.All, 4L), Token.Insert | Token.Update);
    //        Assert.AreEqual(EnumHelper.Set(Token.Insert, 2L), Token.Insert | Token.Update);

    //        Assert.IsTrue(EnumHelper.HasValue(Token.All, 2L));
    //        Assert.IsFalse(EnumHelper.HasValue(Token.Insert, 2L));
    //    }
    //}
}
