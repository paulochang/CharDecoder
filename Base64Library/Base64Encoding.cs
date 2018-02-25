using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Base64LibraryTests")]
namespace Base64Library
{
    /// <summary>
    /// The Base64 encoding class. 
    /// </summary>
    internal class Base64Encoding
    {
        /// <summary>
        /// String used for fast byte[] to Base64 conversion.
        /// Note that each caracter positions corresponds to its own value.
        /// We can therefore do a simple index-based lookup to get the character associated with each value.
        /// 
        /// '=' is added for later convenience.
        /// </summary>
        const string BASE_64_DICTIONARY = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

        /// <summary>
        /// Gets the base64 string representation of a certain sixtet sequence.
        /// </summary>
        /// <param name="sixtetRepresentation">A sixtet representation of a string.</param>
        /// <param name="paddingBytesNr">The bytes quantity to skip while converting.</param>
        /// <returns>A base64 string representation of the sixtet sequence.</returns>
        internal static string GetString(byte[] sixtetRepresentation, int paddingBytesNr)
        {

            if (paddingBytesNr < 0)
                throw new ArgumentOutOfRangeException("paddingBytesNr");

            if (paddingBytesNr > sixtetRepresentation.Length)
                throw new ArgumentOutOfRangeException("paddingBytesNr");

            string result = "";

            for (int currentPos = 0; currentPos < (sixtetRepresentation.Length - paddingBytesNr); currentPos++)
            {
                byte currentValue = sixtetRepresentation[currentPos];

                if (currentValue > BASE_64_DICTIONARY.Length - 1)
                    throw new IndexOutOfRangeException();

                result += BASE_64_DICTIONARY[currentValue];
            }

            for (int paddedIndex = 0; paddedIndex < paddingBytesNr; paddedIndex++)
            {
                result += "=";
            }

            return result;
        }

        /// <summary>
        /// Gets the sixtet sequence corresponding to a certain base64 string.
        /// </summary>
        /// <param name="base64Representation">A base64 string.</param>
        /// <param name="paddingBytesNr">The padded bytes quantity.</param>
        /// <returns>A sixtet sequence corresponding to the passed base64 string</returns>
        internal static byte[] GetSixtetRepresentation(string base64Representation, out int paddingBytesNr)
        {
            paddingBytesNr = 0;
            byte[] result = new byte[base64Representation.Length];
            for (int currentPos = 0; currentPos < base64Representation.Length; currentPos++)
            {
                char currentChar = base64Representation[currentPos];
                int base64Pos = BASE_64_DICTIONARY.IndexOf(currentChar);
                if (base64Pos == -1)
                    throw new KeyNotFoundException();
                else
                    result[currentPos] = (byte)base64Pos;



                if (currentChar == '=')
                {
                    paddingBytesNr += 1;
                }
            }
            return result;
        }
    }
}
