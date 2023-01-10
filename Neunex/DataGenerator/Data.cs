using Google.Protobuf.Reflection;
using Keras.Layers;
using Numpy;
using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tensorflow;
using static Tensorflow.Binding;

namespace Neunex.DataGenerator
{
    internal class Data : IData
    {
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public Tensor generateDataPoint(string targetLabel, Dictionary<string, NDarray> encodedLabels)
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

                generated[indexer] = GetRandomNumber((double)0.5, (double)1.0);

                for (int x = 0; x < generated.Length; x++)
                {
                    if (x != indexer)
                    {
                        generated[x] = GetRandomNumber((double)0.0, generated[indexer]);
                    }
                }
                Tensor tensor = tf.convert_to_tensor(generated);
                return tf.nn.softmax(tensor);

            }
        }
    }
}
