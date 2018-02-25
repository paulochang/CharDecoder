using Base64Converter.Controller;
using Base64Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base64Converter.Model
{
    [DataObject(true)]
    class HumanFriendlyKey
    {
        /// <summary>
        /// The key's id.
        /// </summary>
        public int id_key { get; set; }

        /// <summary>
        /// The key's encoded string.
        /// </summary>
        public string secret_key { get; set; }

        /// <summary>
        /// The key's original string.
        /// </summary>
        public string decoded_key { get; set; }


        /// <summary>
        /// Retrieves all the keys in a human friendly format.
        /// </summary>
        /// <returns>The keys in human friendly format</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static IEnumerable<HumanFriendlyKey> HumanFriendlyKeys_GetData()
        {
            List<HumanFriendlyKey> resultsList = new List<HumanFriendlyKey>();

            List<key> allKeys = Cs_Base64Manager.GetAllKeys();
            foreach (key singleKey in allKeys)
            {
                string decodedKey = Base64Helper.DecodeFrom64(singleKey.secret_key);
                HumanFriendlyKey tempKey = new HumanFriendlyKey()
                {
                    id_key = singleKey.id_key,
                    secret_key = singleKey.secret_key,
                    decoded_key = decodedKey
                };
                resultsList.Add(tempKey);
            }

            return resultsList;
        }
    }
}
