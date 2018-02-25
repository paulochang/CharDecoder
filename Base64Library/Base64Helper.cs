using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base64Library
{
    /// <summary>
    /// Base64 coding and decoding main helper class.
    /// </summary>
    public class Base64Helper
    {
        /// <summary>
        /// Codes the passed value to a Base64 representation.
        /// </summary>
        /// <param name="value">The passed parameter.</param>
        /// <returns>The converted parameter.</returns>
        public static string CodeTo64(string value)
        {
            byte[] byteRepresentation = ASCIIEncoding.ASCII.GetBytes(value);
            int paddingBytesNr = 0;
            byte[] sixtetRepresentation = SixtetHelper.ConvertToSixtetRepresentation(byteRepresentation, out paddingBytesNr);
            string result = Base64Encoding.GetString(sixtetRepresentation, paddingBytesNr);
            return result;
        }

        /// <summary>
        /// Decodes the passed value to a Base64 representation.
        /// </summary>
        /// <param name="value">The passed parameter.</param>
        /// <returns>The converted parameter.</returns>
        public static string DecodeFrom64(string value)
        {
            int paddingBytesNr = 0;
            byte[] sixtetRepresentation = Base64Encoding.GetSixtetRepresentation(value, out paddingBytesNr);
            byte[] byteRepresentation = SixtetHelper.ConvertToByteRepresentation(sixtetRepresentation, paddingBytesNr);
            string result = ASCIIEncoding.ASCII.GetString(byteRepresentation);
            return result;
        }
    }
}
