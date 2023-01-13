using BenchmarkDotNet.Attributes;
using Neunex.Activations;
using Neunex.LabelEncoding;
using System;
using System.Collections.Generic;

namespace Neunex.DataGenerator
{
    internal class Data : IData
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        [Benchmark]
        private static double GetRandomNumber(double minimum, double maximum)
        {
            lock (syncLock)
            { // synchronize
                return random.NextDouble() * (maximum - minimum) + minimum; ;
            }
        }

        [Benchmark]
        public List<double> generateDataPoint(int size, int indexKey, LabelStringKey encodedLabels)
        {
            List<double> generated = new List<double> (new double[size]);

            generated[indexKey - 1] = GetRandomNumber((double)0, (double)10);

            for (int x = 0; x < generated.Count; x++)
            {
                if (x != indexKey)
                    generated[x] = GetRandomNumber((double)0.0, generated[indexKey - 1]);
            }
            return softmax.calculate(generated); // generated);
        }
    }
}
