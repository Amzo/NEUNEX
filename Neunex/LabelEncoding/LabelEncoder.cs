﻿using Numpy;
using System.Collections.Generic;

namespace Neunex.LabelEncoding
{
    internal class LabelEncoder : ILabelEncoder
    {
        public Dictionary<string, NDarray> OneHotEncode(string[] labels)
        {
            int[] arr = new int[labels.Length];
            int currentIndex = 0;
            Dictionary<string, NDarray> encoded = new Dictionary<string, NDarray>();

            foreach (string label in labels)
            {
                arr[currentIndex] = 1;
                encoded.Add(label, arr);
                arr[currentIndex] = 0;
                currentIndex++;
            }

            return encoded;
        }
    }
}
