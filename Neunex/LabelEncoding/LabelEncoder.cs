using Numpy;
using System.Collections.Generic;

namespace Neunex.LabelEncoding
{
    internal class LabelEncoder : ILabelEncoder
    {
        public Dictionary<string, NDarray> OneHotEncode(Dictionary<int, string> labels)
        {
            int[] arr = new int[labels.Count];
            Dictionary<string, NDarray> encoded = new Dictionary<string, NDarray>();

            // new loop
            foreach (KeyValuePair<int, string> entry in labels)
            {
                arr[entry.Key - 1] = 1;
                encoded.Add(entry.Value, arr);
                arr[entry.Key - 1] = 0;
            }

            return encoded;
        }
    }
}
