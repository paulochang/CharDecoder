using System;
using Base64Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Base64LibraryTests
{
    [TestClass]
    public class Base64EncodingTests
    {
        [TestMethod]
        public void GetStringWithValidSixtets()
        {
            byte[] validByteStream = new byte[] { 28, 55, 21, 50, 25, 18, 56, 0 };
            int paddingBytesNr = 1;
            string expectedBase64String = "c3VyZS4=";

            string actualBase64String = Base64Encoding.GetString(validByteStream, paddingBytesNr);

            Assert.AreEqual(expectedBase64String, actualBase64String, "The Byte Stream is not being converted correctly");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetStringWithNotValidPaddingBytesNr()
        {
            byte[] validByteStream = new byte[] { 28, 55, 21, 50, 25, 18, 56, 0 };
            int paddingBytesNr = -5;

            Base64Encoding.GetString(validByteStream, paddingBytesNr);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetStringWithMorePaddingThanAvailable()
        {
            byte[] validByteStream = new byte[] { 28, 55, 21, 50, 25, 18, 56, 0 };
            int paddingBytesNr = 9;

            Base64Encoding.GetString(validByteStream, paddingBytesNr);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetStringWithInvalidSingleValue()
        {
            byte[] validByteStream = new byte[] { 55, 255, 21, 0 };
            int paddingBytesNr = 1;

            Base64Encoding.GetString(validByteStream, paddingBytesNr);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetStringWithLogicalInvalidSingleValue()
        {
            byte[] validByteStream = new byte[] { 55, 65, 21, 0 };
            int paddingBytesNr = 1;

            Base64Encoding.GetString(validByteStream, paddingBytesNr);
        }

        [TestMethod]
        public void GetSystetWithValidString()
        {

            string validBase64String = "c3VyZS4=";
            byte[] expectedByteStream = new byte[] { 28, 55, 21, 50, 25, 18, 56, 64 };
            int expectedPaddingBytesNr = 1;

            int actualPaddingBytesNr;
            byte[] actualByteStream = Base64Encoding.GetSixtetRepresentation(validBase64String, out actualPaddingBytesNr);

            CollectionAssert.AreEqual(expectedByteStream, actualByteStream, "The Byte Stream is not being converted correctly");
            Assert.AreEqual(expectedPaddingBytesNr, actualPaddingBytesNr, "The actual Padding Bytes nr is not being counted correctly");
        }

        [TestMethod]
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException))]
        public void GetSystetWithNonValidCharacter()
        {

            string validBase64String = "@c3VyZS4=";

            int actualPaddingBytesNr;
            Base64Encoding.GetSixtetRepresentation(validBase64String, out actualPaddingBytesNr);
        }
    }
}
