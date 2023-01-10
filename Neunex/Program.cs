using Neunex.DataGenerator;
using Neunex.DataSetGenerator;
using Neunex.LabelEncoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow;
using Tensorflow.Keras.Datasets;


namespace Neunex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] labels = { "Cat", "Dog", "horse", "cow", "duck" };


            LabelEncoder encoder = new LabelEncoder();
            var encodedResults = encoder.encodeLabels(labels);

            DataSet datasetgen = new DataSet();
            List<Tensor> dataset = new List<Tensor>();

            dataset = datasetgen.DataSetGen(100, encodedResults);
            Console.WriteLine(dataset);
        }
    }
}
