using Numpy;
using System.Collections.Generic;

namespace Neunex.LabelEncoding
{
    public class LabelStringKey
    {
        public string Label { get; set; }
        public List<int> Value { get; set; }

        public LabelStringKey(string label, List<int> value)
        {
            List<int> arr = new List<int>(value);
            this.Label = label;
            this.Value = arr;
        }

    }

    internal class LabelEncoder
    {
        public static Dictionary<int, LabelStringKey> OneHotEncode(Dictionary<int, string> labels)
        {
            Dictionary<int, LabelStringKey> encoded = new Dictionary<int, LabelStringKey>();
            List<int> arr = new List<int>(new int[labels.Count]);

            // new loop
            foreach (KeyValuePair<int, string> entry in labels)
            {
                arr[entry.Key - 1] = 1;
                LabelStringKey values = new LabelStringKey(entry.Value, arr);
                encoded.Add(entry.Key,  values);
                arr[entry.Key - 1] = 0;
            }

            return encoded;
        }
    }
}
