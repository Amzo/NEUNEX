using Keras.Layers;
using Keras.Models;
using Neunex.DataSetGenerator;
using System.Collections.Generic;
using System;
using Keras;
using Keras.Utils;

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


            // mini learner data gen for layers >= 2 will be internal to minilearner
            // private static
            /*
            var encodedResults = LabelEncoder.OneHotEncode(labels);
            (var x, var y) = DataGenerate.Generate(50000, encodedResults);
            (var x_test, var y_test) = DataGenerate.Generate(50000, encodedResults);
            
            var m = np.array(x).reshape(x.Length / 5, 5);
            var z = np.array(y).reshape(y.Length / 5, 5);
            var m_test = np.array(x_test).reshape(x_test.Length / 5, 5);
            var z_test = np.array(y_test).reshape(y_test.Length / 5, 5);*/

            // cifar10 data splitting code for input layer
            var ((x_train, y_train), (x_test, y_test)) = DataGenerate.Cifar10();
            var test = DataGenerate.Split(10, x_train, y_train);

            (var train, var y) = DataGenerate.Split(10, x_train, y_train);
            var encoded = Keras.Utils.Util.ToCategorical(y_train, 10);
            var encoded2  = Keras.Utils.Util.ToCategorical(y_test, 10);

            Shape input_shape = (32,32,3);
            var model = new Sequential();
            model.Add(new Conv2D(32, kernel_size: (3, 3).ToTuple(),
                                    activation: "relu",
                                    input_shape: input_shape));
            model.Add(new Conv2D(64, (3, 3).ToTuple(), activation: "relu"));
            model.Add(new MaxPooling2D(pool_size: (2, 2).ToTuple()));
            model.Add(new Dropout(0.25));
            model.Add(new Flatten());
            model.Add(new Dense(128, activation: "relu"));
            model.Add(new Dropout(0.5));
            model.Add(new Dense(10, activation: "softmax"));

            var model1 = new Sequential();
            model1.Add(new Dense(64, activation: "relu", input_shape: new Keras.Shape(28,28)));
            model1.Add(new Dense(128, activation: "relu"));
            model1.Add(new Dense(10, activation: "softmax"));

            //np.arr
            //Compile and train
            model.Compile(optimizer: "adam", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });
            //model.Fit(m, z, batch_size: 512, epochs: 200, verbose: 1);
            model.Fit(x_train, encoded, batch_size: 2048, epochs: 50, verbose: 1);

            var score = model.Evaluate(x_test, encoded2, verbose: 0);
            Console.WriteLine($"Test loss: {score[0]}");
            Console.WriteLine($"Test accuracy: {score[1]}");

        }
    }
}
