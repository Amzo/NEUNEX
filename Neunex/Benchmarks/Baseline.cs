using Keras;
using Keras.Layers;
using Keras.Models;
using Neunex.DataSetGenerator;
using Neunex.LabelEncoding;
using Numpy;
using System;
using System.Collections.Generic;

namespace Neunex.Benchmarks
{
    public class Baseline
    {
        public static void cifar10Benchmark(int epoch, int batch)
        {
            var ((x_train, y_train), (x_test, y_test)) = DataGenerate.Cifar10();
            var test = DataGenerate.Split(10, x_train, y_train);

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

            model.Compile(optimizer: "adam", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });
            model.Fit(x_train, encoded, batch_size: batch, epochs: epoch, verbose: 1);
            var score = model.Evaluate(x_test, encoded2, verbose: 0);
            Console.WriteLine($"Test loss: {score[0]}");
            Console.WriteLine($"Test accuracy: {score[1]}");
        }

        public static void NeunexBenchCifarSplit(int epoch, int batchsize, int splits)
        {
            var ((x_train, y_train), (x_test, y_test)) = DataGenerate.Cifar10();
            var (x_split, y_split) = DataGenerate.Split(10, x_train, y_train);

            var encoded2 = Keras.Utils.Util.ToCategorical(y_test, 10);

            Shape input_shape = (32, 32, 3);
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

            for (int i = 0; i < x_split.Count; i++)
            {
                var weights = model.GetWeights();
                var encoded = Keras.Utils.Util.ToCategorical(y_split[i], 10);
                model.Compile(optimizer: "adam", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });
                model.Fit(x_split[i], encoded, shuffle: true, batch_size: batchsize, epochs: epoch, verbose: 0);

                var score = model.Evaluate(x_test, encoded2, verbose: 0);
                model.SetWeights(weights);
                Console.WriteLine($"MiniLearner {i} scores:");
                Console.WriteLine($"Test loss: {score[0]}");
                Console.WriteLine($"Test accuracy: {score[1]}");
            }
        }

        public void NeunexBenchGeneratedData()
        {
            var labels = new Dictionary<int, string>() {
                {1, "Cat"},
                {2, "Dog"},
                {3, "horse" },
                {4, "cow" },
                {5, "duck" }};


            var encodedResults = LabelEncoder.OneHotEncode(labels);

            (var x, var y) = DataGenerate.Generate(50000, encodedResults);
            (var x_test, var y_test) = DataGenerate.Generate(50000, encodedResults);

            var m = np.array(x).reshape(x.Length / 5, 5);
            var z = np.array(y).reshape(y.Length / 5, 5);
            var m_test = np.array(x_test).reshape(x_test.Length / 5, 5);
            var z_test = np.array(y_test).reshape(y_test.Length / 5, 5);

            var model = new Sequential();
            model.Add(new Dense(10, activation: "relu", input_shape: new Keras.Shape(5)));
            model.Add(new Dense(10, activation: "relu"));
            model.Add(new Dense(5, activation: "softmax"));

            model.Compile(optimizer: "adam", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });
            model.Fit(m, z, batch_size: 512, epochs: 1000, verbose: 1);

            var score = model.Evaluate(m_test, z_test, verbose: 0);
            Console.WriteLine($"Test loss: {score[0]}");
            Console.WriteLine($"Test accuracy: {score[1]}");
        }
    }
}
