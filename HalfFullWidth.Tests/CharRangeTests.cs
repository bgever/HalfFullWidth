using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HalfFullWidth.Tests
{
    [TestClass]
    public class CharRangeTests
    {
        [TestMethod]
        public void ToArray_AToBRange()
        {
            char[] array = new CharRange('\u0061', '\u0062').ToArray();
            Console.WriteLine(array);
            CollectionAssert.AreEqual(new char[] { 'a', 'b' }, array);
        }
    }
}
