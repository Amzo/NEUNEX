using Keras.Layers;
using Keras.Models;
using Neunex.DataSetGenerator;
using Neunex.LabelEncoding;
using System.Collections.Generic;
using Tensorflow;
using Tensorflow.Keras;
using Numpy;
using static Tensorflow.Binding;
using System;

namespace Neunex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var labels = new Dictionary<int, string>() {
                {1,  "Cat"},
                {2, "Dog"},
                {3, "horse" },
                {4, "cow" },
                {5, "duck" }};


            LabelEncoder encoder = new LabelEncoder();
            var encodedResults = encoder.OneHotEncode(labels);

            DataSet datasetgen = new DataSet();

            (var x, var y) = datasetgen.DataSetGen(50000, encodedResults);
            (var x_test, var y_test) = datasetgen.DataSetGen(50000, encodedResults);

            var m = np.array(x).reshape(500000, 5);
            var z = np.array(y).reshape(500000, 5);
            var m_test = np.array(x_test).reshape(500000, 5);
            var z_test = np.array(y_test).reshape(500000, 5);

            var model = new Sequential();
            model.Add(new Dense(10, activation: "sigmoid", input_shape: new Keras.Shape(5)));
            model.Add(new Dense(5, activation: "softmax"));

            //np.arr
            //Compile and train
            model.Compile(optimizer: "adam", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });
            model.Fit(m, z, batch_size: 256, epochs: 100, verbose: 1);

            //var score = model.Evaluate(m_test, z_test, verbose: 0);
            //Console.WriteLine($"Test loss: {score[0]}");
            //Console.WriteLine($"Test accuracy: {score[1]}");
        }
    }
}
