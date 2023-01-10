﻿using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neunex.LabelEncoding
{
    internal class LabelEncoder : ILabelEncoder
    {
        public List<NDarray> encodeLabels(string[] labels)
        {
            int[] arr = new int[labels.Length];
            int currentIndex = 0;
            List<NDarray> encoded = new List<NDarray>();

            foreach (int i in arr)
            {
                arr[i] = 0;
            }

            foreach (string label in labels)
            {
                arr[currentIndex] = 1;
                encoded.Add(arr);
                arr[currentIndex] = 0;
                currentIndex++;
            }

            return encoded;


        }
    }
}
