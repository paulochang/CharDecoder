using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base64Library;

namespace Base64LibraryTests
{
    [TestClass]
    public class Base64HelperTests
    {
        [TestMethod]
        public void Base64CodingWithValidValue()
        {
            string validValue = "pleasure.";
            string expectedBase64String = "cGxlYXN1cmUu";

            string actualBase64String = Base64Helper.CodeTo64(validValue);

            Assert.AreEqual(expectedBase64String, actualBase64String, "The actual base64String is not encoded correctly");
        }

        [TestMethod]
        public void Base64DeCodingWithValidValue()
        {
            string validValue = "ZWFzdXJlLg==";
            string expectedDecodedString = "easure.";

            string actualDecodedString = Base64Helper.DecodeFrom64(validValue);

            Assert.AreEqual(expectedDecodedString, actualDecodedString, "The actual base64String is not decoded correctly");
        }


    }
}
