using static Tensorflow.Binding;
using static Tensorflow.KerasApi;
using Tensorflow;
using Tensorflow.NumPy;
using Tensorflow.Keras.Layers;
using Tensorflow.Keras.Utils;
using Neunex.DataSetGenerator;
using Neunex.LabelEncoding;
using System.Collections.Generic;

namespace Neunex.Benchmarks
{
    public class Baseline
    {
        public void cifar10Benchmark()
        {
            var layers = new LayersApi();
            // input layer
            var inputs = keras.Input(shape: (32, 32, 3), name: "img");
            // convolutional layer
            var x = layers.Conv2D(32, (3, 3), activation: "relu").Apply(inputs);
            x = layers.Conv2D(64, (3, 3), activation: "relu").Apply(x);
            var block_1_output = layers.MaxPooling2D((2, 2)).Apply(x);
            var outputs = layers.Flatten().Apply(x);
            //x = layers.Dense(256, activation: "relu").Apply(x);

            // output layer
            //var outputs = layers.Dense(10, activation: "softmax").Apply(x);
            // build keras model
            var model = keras.Model(inputs, outputs, name: "toy_resnet");
            model.summary();
            // compile keras model in tensorflow static graph
            model.compile(optimizer: keras.optimizers.RMSprop(1e-3f),
                loss: keras.losses.CategoricalCrossentropy(from_logits: true),
                metrics: new[] { "acc" });
            // prepare dataset
            var ((x_train, y_train), (x_test, y_test)) = keras.datasets.cifar10.load_data();
            x_train = x_train / 255.0f;
            y_train = np_utils.to_categorical(y_train, 10);
            // training
            var result = model.predict(x_train);
            //model.fit(x_train[new Slice(0, 2000)], y_train[new Slice(0, 2000)],
            //          batch_size: 64,
            //          epochs: 10,
            //          validation_split: 0.2f);

            //x_test = x_test / 255.0f;
            //y_test = np_utils.to_categorical(y_test, 10);
            //model.evaluate(x_test[new Slice(0, 2000)], y_test[new Slice(0, 2000)]);
        }

        public void NeunexBench()
        {
            /*var labels = new Dictionary<int, string>() {
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
