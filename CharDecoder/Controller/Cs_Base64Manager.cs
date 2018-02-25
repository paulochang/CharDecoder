using Base64Converter.Model;
using Base64Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base64Converter.Controller
{
    /// <summary>
    /// Handles the communication and codification between GUI and database.
    /// </summary>
    class Cs_Base64Manager
    {
        /// <summary>
        /// Encodes a string to base64 format and attempts to save it to the database.
        /// </summary>
        /// <param name="theOriginalString">The parameter string</param>
        /// <returns>Returns the id of the newly encoded string.</returns>
        public static int? EncodeAndSaveString(string theOriginalString)
        {
            string theEncodedKey = Base64Helper.CodeTo64(theOriginalString);
            key tempKey = new key()
            {
                secret_key = theEncodedKey
            };

            return AddKeyIfNotExists(tempKey);
        }

        /// <summary>
        /// Attemps to save the key if it doesn't already exists.
        /// </summary>
        /// <param name="tempKey">The key to store.</param>
        /// <returns>The id of the new or matching key.</returns>
        private static int? AddKeyIfNotExists(key tempKey)
        {
            using (cs_base64Entities ctx = new cs_base64Entities())
            {
                var matching_results = ctx.keys
                                            .Where(r => r.secret_key == tempKey.secret_key);

                int matching_results_count = matching_results.Count();

                try
                {
                    if (matching_results_count == 0)
                    {
                        ctx.keys.Add(tempKey);
                        ctx.SaveChanges();
                    }

                    int id_result = matching_results.Select(r => r.id_key).FirstOrDefault();
                    return id_result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets an encoded string by Id.
        /// </summary>
        /// <param name="idKey">The key id.</param>
        /// <returns>The encoded string</returns>
        public static string GetEncodedString(int idKey)
        {
            using (cs_base64Entities ctx = new cs_base64Entities())
            {
                string theEncodedKey = ctx.keys
                                                .Where(r => r.id_key == idKey)
                                                .Select(r => r.secret_key)
                                                .FirstOrDefault();
                return theEncodedKey;
            }
        }

        /// <summary>
        /// Gets a list of all keys stored in the current database.
        /// </summary>
        /// <returns>A list with all the keys currently stored in the database</returns>
        public static List<key> GetAllKeys()
        {
            using (cs_base64Entities ctx = new cs_base64Entities())
            {
                List<key> theCodedStrings = ctx.keys
                                            .Select(r => r)
                                            .ToList();
                return theCodedStrings;
            }
        }

    }
}
