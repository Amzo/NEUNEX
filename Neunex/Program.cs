using Keras.Layers;
using Keras.Models;
using Neunex.DataSetGenerator;
using Neunex.LabelEncoding;
using System.Collections.Generic;
using Numpy;
using static Tensorflow.Binding;
using Neunex.Benchmarks;

namespace Neunex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var labels = new Dictionary<int, string>() {
                {1, "Cat"},
                {2, "Dog"},
                {3, "horse" },
                {4, "cow" },
                {5, "duck" }};


            var encodedResults = LabelEncoder.OneHotEncode(labels);


            //(var x, var y) = DataGenerate.Generate(50000, encodedResults);
            //(var x_test, var y_test) = DataGenerate.Generate(50000, encodedResults);
            var((x_train, y_train), (x_test, y_test)) = DataGenerate.Cifar10();
            var test = DataGenerate.Split(10, x_train);

            /*var m = np.array(x).reshape(x.Length / 5, 5);
            var z = np.array(y).reshape(y.Length / 5, 5);
            var m_test = np.array(x_test).reshape(x_test.Length / 5, 5);
            var z_test = np.array(y_test).reshape(y_test.Length / 5, 5);

            var model = new Sequential();
            model.Add(new Dense(10, activation: "relu", input_shape: new Keras.Shape(48)));
            model.Add(new Dense(10, activation: "relu"));
            model.Add(new Dense(5, activation: "softmax"));

            //np.arr
            //Compile and train
            model.Compile(optimizer: "adam", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });
            model.Fit(m, z, batch_size: 512, epochs: 1000, verbose: 1);

            //var score = model.Evaluate(m_test, z_test, verbose: 0);
            //Console.WriteLine($"Test loss: {score[0]}");
            //Console.WriteLine($"Test accuracy: {score[1]}");*/

        }
    }
}
