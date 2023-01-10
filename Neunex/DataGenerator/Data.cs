using Google.Protobuf.Reflection;
using Keras.Layers;
using Neunex.Activations;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tensorflow;
using Tensorflow.NumPy;
using static Tensorflow.Binding;

namespace Neunex.DataGenerator
{
    internal class Data : IData
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        public static double GetRandomNumber(double minimum, double maximum)
        {
            lock (syncLock)
            { // synchronize
                return random.NextDouble() * (maximum - minimum) + minimum; ;
            }
        }

        public double[] generateDataPoint(string targetLabel, Dictionary<string, NDarray> encodedLabels)
        {
            if (!encodedLabels.ContainsKey(targetLabel))
                throw new Exception("Target label not found in encoded list");
            else
            {
                Random nd = new Random();
                double [] generated = new double[encodedLabels.Count()];
                int indexer = 0;

                foreach(KeyValuePair<string, NDarray> entry in encodedLabels)
                {
                    if (entry.Key == targetLabel)
                        break;

                    indexer++;
                }

                generated[indexer] = GetRandomNumber((double)20, (double)100);

                for (int x = 0; x < generated.Length; x++)
                {
                    if (x != indexer)
                    {
                        generated[x] = GetRandomNumber((double)0.0, generated[indexer]);
                    }
                }
                //Tensor tensor = tf.convert_to_tensor(generated);
                //var results = tf.nn.softmax(tensor);
                softmax test = new softmax();

                return test.calculate(generated);

            }
        }
    }
}
