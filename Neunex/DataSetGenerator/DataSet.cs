using BenchmarkDotNet.Attributes;
using Neunex.DataGenerator;
using Neunex.LabelEncoding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neunex.DataSetGenerator
{
    internal class DataSet: IDataSet
    {
        [Benchmark]
        public (double[], int[]) DataSetGen(int quantity, Dictionary<int, LabelStringKey> encodedLabels)
        {
            List <double> dataset = new List<double>(quantity * encodedLabels.Count());
            List <int> datalabels = new List<int>(quantity * encodedLabels.Count());

            if (quantity % encodedLabels.Count() != 0)
            {
                throw new Exception("Can't balance classes based on quantitiy provided");
            }
            else
            {
                foreach (KeyValuePair<int, LabelStringKey> entry in encodedLabels)
                {
                    for (int i = 0; i < quantity / encodedLabels.Count(); i++)
                    {
                        datalabels.AddRange(entry.Value.Value);
                        dataset.AddRange(DataPoint.Get(encodedLabels.Count, entry.Key, entry.Value));
                    }
                }
                return (dataset.ToArray(), datalabels.ToArray());
            }
        }
    }
}
