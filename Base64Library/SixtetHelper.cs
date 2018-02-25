using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base64Library
{
    /// <summary>
    /// Helper Class to handle Sixtet bit manipulation.
    /// </summary>
    internal class SixtetHelper
    {
        /// <summary>
        /// Converts the byte representation to a sixtet-based representation.
        /// </summary>
        /// <param name="byteRepresentation">The original byte representation.</param>
        /// <param name="paddingBytesNr">When the method returns, contains the number of padding bytes added.</param>
        /// <returns>An array with the sixtet representation of the byteRepresentation parameter.</returns>
        internal static byte[] ConvertToSixtetRepresentation(byte[] byteRepresentation, out int paddingBytesNr)
        {
            int arrayLength = byteRepresentation.Length;
            if (arrayLength % 3 != 0)
                paddingBytesNr = 3 - (arrayLength % 3);
            else
                paddingBytesNr = 0;
            byte[] processedArray = AddPaddingZeroes(byteRepresentation, paddingBytesNr);
            byte[] result = SplitToSixtets(processedArray);
            return result;
        }

        /// <summary>
        /// Adds n zeroed bytes to bit sequence, used for later "=" character inclusion.
        /// </summary>
        /// <param name="valueToProcess">The value array to process.</param>
        /// <param name="zeroedBytesToAdd">The number of zeroed bytes to add.</param>
        /// <returns>The array with padded zeroes.</returns>
        internal static byte[] AddPaddingZeroes(byte[] valueToProcess, int zeroedBytesToAdd)
        {
            if (zeroedBytesToAdd < 0)
                throw new ArgumentOutOfRangeException("zeroedBytesToAdd");
            byte[] result = new byte[valueToProcess.Length + zeroedBytesToAdd];
            valueToProcess.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// Splits the byte representations into a sixtet-based array.
        /// </summary>
        /// <param name="processedArray">The byte representation with padding zeroes added.</param>
        /// <returns>An array with the splitted representation of the padded array.</returns>
        internal static byte[] SplitToSixtets(byte[] processedArray)
        {
            int segmentsNr = processedArray.Length * 4 / 3;
            byte[] result = new byte[segmentsNr];

            for (int segment = 0; segment < segmentsNr; segment++)
            {
                int startIndex = segment * 6;
                result[segment] = ExtractSingleSixtet(startIndex, processedArray);
            }
            return result;
        }

        /// <summary>
        /// Extracts a 6-bit binary representation from the bits starting at the specified position.
        /// </summary>
        /// <param name="startPosition">The starting position of the sixtet.</param>
        /// <param name="byteArray">The byteArray to browse.</param>
        /// <returns>A byte representation of the 6-bit binary group.</returns>
        private static byte ExtractSingleSixtet(int startPosition, byte[] byteArray)
        {
            int endIndex = startPosition + 5;

            int startArrayIndex = startPosition / 8;
            int startBitIndex = startPosition % 8;

            int endArrayIndex = endIndex / 8;
            int endBitIndex = endIndex % 8;

            int firstSegment = 0;

            if (startBitIndex > 2)
            {
                firstSegment = byteArray[startArrayIndex];
                firstSegment <<= (startBitIndex - 2);
            };
            int secondSegment = byteArray[endArrayIndex];
            secondSegment >>= (7 - endBitIndex);

            int tempByte = firstSegment + secondSegment;
            tempByte &= 63;

            return (byte)tempByte;
        }

        internal static byte[] ConvertToByteRepresentation(byte[] sixtetRepresentation, int paddingBytesNr)
        {
            byte[] byteRepresentation = SplitToOctets(sixtetRepresentation);
            byte[] processedArray = RemovePaddingZeroes(byteRepresentation, paddingBytesNr);
            byte[] result = processedArray;
            return result;
        }

        private static byte[] RemovePaddingZeroes(byte[] valueToProcess, int zeroedBytesToRemove)
        {
            int newLength = valueToProcess.Length - zeroedBytesToRemove;
            byte[] result = new byte[newLength];
            Array.Copy(valueToProcess, result, newLength);
            return result;

        }

        private static byte[] SplitToOctets(byte[] sixtetRepresentation)
        {
            int segmentsNr = sixtetRepresentation.Length * 3 / 4;
            byte[] result = new byte[segmentsNr];

            for (int segment = 0; segment < segmentsNr; segment++)
            {
                int startIndex = segment * 8;
                result[segment] = ExtractSingleOctet(startIndex, sixtetRepresentation);
            }
            return result;
        }

        private static byte ExtractSingleOctet(int startPosition, byte[] sixtetArray)
        {
            int endIndex = startPosition + 7;

            int startArrayIndex = startPosition / 6;
            int startBitIndex = startPosition % 6;

            int endArrayIndex = endIndex / 6;
            int endBitIndex = endIndex % 6;

            int firstSegment = 0;

            firstSegment = sixtetArray[startArrayIndex];
            firstSegment <<= (startBitIndex + 2);

            int secondSegment = sixtetArray[endArrayIndex];
            secondSegment >>= (5 - endBitIndex);

            int tempByte = firstSegment + secondSegment;

            return (byte)tempByte;
        }
    }
}
