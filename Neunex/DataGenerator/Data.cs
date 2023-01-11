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
                double [] generated = new double[encodedLabels.Count()];
                int indexer = 0;

                // generate the target label first so all other targets generated after
                // can be less than the main target
                foreach(KeyValuePair<string, NDarray> entry in encodedLabels)
                {
                    if (entry.Key == targetLabel)
                    {
                        generated[indexer] = GetRandomNumber((double)0, (double)100);
                        break;
                    }
                    indexer++;
                }

                for (int x = 0; x < generated.Length; x++)
                {
                    if (x != indexer)
                    {
                        generated[x] = GetRandomNumber((double)0.0, generated[indexer]);
                    }
                }

                return softmax.calculate(generated);
            }
        }
    }
}
