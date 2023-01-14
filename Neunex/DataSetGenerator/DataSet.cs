using Keras;
using Keras.Datasets;
using Neunex.DataGenerator;
using Neunex.LabelEncoding;
using Numpy;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Neunex.DataSetGenerator
{
    internal class DataSet: IDataSet
    {
        public (double[], int[]) DataSetGen(int quantity, Dictionary<int, LabelStringKey> encodedLabels)
        {
            List <double> dataset = new List<double>(quantity * encodedLabels.Count());
            List <int> datalabels = new List<int>(quantity * encodedLabels.Count());
            var tasks = new List<Task>();

            if (quantity % encodedLabels.Count() != 0)
            {
                throw new Exception("Can't balance classes based on quantitiy provided");
            }
            else
            {
                //Parallel.ForEach(encodedLabels, entry =>
                foreach (KeyValuePair<int, LabelStringKey> entry in encodedLabels)
                {
                   // tasks.Add(Task.Run(() =>
                   // {
                        for(int i = 0; i < quantity / encodedLabels.Count(); i++)
                        {
                            datalabels.AddRange(entry.Value.Value);
                            dataset.AddRange(DataPoint.Get(encodedLabels.Count, entry.Key, entry.Value));
                        }
                   // }));
                };//);

                Task t = Task.WhenAll(tasks);
                try
                {
                    t.Wait();
                }
                catch { }

                if (t.Status == TaskStatus.RanToCompletion)
                    return (dataset.ToArray(), datalabels.ToArray());
                else
                    return (null, null);
            }
        }

        public ((NDarray, NDarray), (NDarray, NDarray)) loadCifar10()
        {
            var ((x_train, y_train), (x_test, y_test)) = Cifar10.LoadData();

            return ((x_train, y_train), (x_test, y_test));
        }

        public (List<NDarray>, List<NDarray>) SplitDataSet(int quantity, NDarray dataset, NDarray labels)
        {
            if (dataset.len % quantity != 0)
                throw new Exception("can't split dataset equally");

            int stepSize = dataset.len / quantity;
            List<NDarray> splitDataset = new List<NDarray>(quantity);
            List<NDarray> splitLabels = new List<NDarray>(quantity);

            Balance(stepSize, dataset, labels, 10);
            for (int i = 0; i < quantity; i++)
            {
                splitDataset.Insert(i, dataset[$"{i * stepSize}:{i * stepSize + stepSize}"]);
                splitLabels.Insert(i, labels[$"{i * stepSize}:{i * stepSize + stepSize}"]);
            }

            return (splitDataset, splitLabels);
        }

        private static void Balance(int splits, NDarray dataset, NDarray labels, int numLabels)
        {
            int stepsize = splits / numLabels;
            NDarray sorted = np.empty_like(dataset);
            List<int> counter = new List<int>( new int[numLabels]);
            List<int> totalAdded = new List<int>(new int [numLabels]);
            int labelVal;

            for ( int x = 0; x < dataset.len; x++)
            {

                var value = dataset[x];
                var label = labels[x];

                bool success = int.TryParse(label[0].str, out labelVal);

                // calculate starting index
                var startingPos = labelVal - 1;

                // increment index based on split
                var index = (startingPos * stepsize + totalAdded[labelVal]) 
                    + (stepsize * numLabels) * counter[labelVal];

                // keep track of counter
                if (totalAdded[labelVal] == stepsize)
                {
                    counter[labelVal] += 1;
                    totalAdded[labelVal] = 0;
                }
                else
                    totalAdded[labelVal] += 1;

                // add to correct position
                sorted[index] = dataset[index];
            }
        }
    }
}
