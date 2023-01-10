using Neunex.LabelEncoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow.Keras.Datasets;


namespace Neunex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] labels = { "Cat", "Dog", "horse", "cow" };


            LabelEncoder encoder = new LabelEncoder();
            var encodedResults = encoder.encodeLabels(labels);
            Console.WriteLine(encodedResults);
        }
    }
}
