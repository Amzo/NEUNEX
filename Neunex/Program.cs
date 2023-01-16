using Keras.Layers;
using Keras.Models;
using Neunex.DataSetGenerator;
using System.Collections.Generic;
using System;
using Keras;
using Keras.Utils;
using Neunex.LabelEncoding;
using Numpy;
using Neunex.Benchmarks;

namespace Neunex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            Baseline.NeunexBenchCifarSplit(50, 2048, 10);
            //Baseline.cifar10Benchmark(50, 2048);
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}
