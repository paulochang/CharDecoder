using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base64Library;

namespace Base64LibraryTests
{
    [TestClass]
    public class SixtetHelperTests
    {
        [TestMethod]
        public void AddPaddingZeroesWithValidData()
        {
            byte[] validValue = new byte[] { 1, 4, 55 };
            int zeroesToAdd = 2;
            byte[] expectedValue = new byte[] { 1, 4, 55, 0, 0 };
            byte[] actualValue = SixtetHelper.AddPaddingZeroes(validValue, zeroesToAdd);

            CollectionAssert.Equals(expectedValue, actualValue);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddPaddingZeroesWithInValidData()
        {
            byte[] validValue = new byte[] { 1, 4, 55 };
            int zeroesToAdd = -2;
            SixtetHelper.AddPaddingZeroes(validValue, zeroesToAdd);
        }
    }
}
