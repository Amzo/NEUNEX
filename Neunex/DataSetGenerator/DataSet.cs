using Keras;
using Keras.Datasets;
using Neunex.DataGenerator;
using Neunex.LabelEncoding;
using Numpy;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

        public (List<NDarray>, List<NDarray>) SplitDataSet(int quantity, NDarray dataset, NDarray labels, int numlabels)
        {
            if (dataset.len % quantity != 0)
                throw new Exception("can't split dataset equally");

            int stepSize = dataset.len / quantity;
            List<NDarray> splitDataset = new List<NDarray>(quantity);
            List<NDarray> splitLabels = new List<NDarray>(quantity);

            (dataset, labels) = Balance(stepSize, dataset, labels, numlabels);

            for (int i = 0; i < quantity; i++)
            {
                splitDataset.Insert(i, dataset[$"{i * stepSize}:{i * stepSize + stepSize}"]);
                splitLabels.Insert(i, labels[$"{i * stepSize}:{i * stepSize + stepSize}"]);
            }

            return (splitDataset, splitLabels);
        }

        public (List<NDarray>, List<NDarray>) SplitDataSet(int quantity, NDarray dataset, NDarray labels)
        {
            if (dataset.len % quantity != 0)
                throw new Exception("can't split dataset equally");

            int stepSize = dataset.len / quantity;
            List<NDarray> splitDataset = new List<NDarray>(quantity);
            List<NDarray> splitLabels = new List<NDarray>(quantity);

            for (int i = 0; i < quantity; i++)
            {
                splitDataset.Insert(i, dataset[$"{i * stepSize}:{i * stepSize + stepSize}"]);
                splitLabels.Insert(i, labels[$"{i * stepSize}:{i * stepSize + stepSize}"]);
            }

            return (splitDataset, splitLabels);
        }
        public void sortClasses(NDarray dataset, NDarray labels)
        {
            int[,,,] sortedArr = new int[50000, 32, 32, 3];
            int[] labelArr = new int[50000];
            List<int> counter = new List<int>(new int[10]);

            int labelVal;
            int val;
            for (int x = 0; x < dataset.len; x++)
            {
                var value = dataset[x];
                var label = labels[x];
                bool success = int.TryParse(label[0].str, out labelVal);

                var index = (labelVal * (dataset.len / 10)) + counter[labelVal];

                counter[labelVal] += 1;

                int.TryParse(value.str, out val);
                for (int h = 0; h < 32; h++)
                    for (int w = 0; w < 32; w++)
                        for (int c = 0; c < 3; c++)
                        {
                            int.TryParse(value[h, w, c].str, out val);
                            sortedArr[index, h, w, c] = val;
                        }

                labelArr[index] = labelVal;
            }

            WriteToBinaryFile("C:\\Users\\Amzo\\Documents\\GitHub\\NEUNEX\\sortedDataset", sortedArr);
            WriteToBinaryFile("C:\\Users\\Amzo\\Documents\\GitHub\\NEUNEX\\sortedLabels", labelArr);
        }

        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static object ReadFromBinaryFile(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return binaryFormatter.Deserialize(stream);
            }
        }

        private static (NDarray, NDarray) Balance(int splits, NDarray dataset, NDarray labels, int numLabels)
        {
            int stepsize = splits / numLabels;
            NDarray sorted = np.empty_like(dataset);
            NDarray sortedLabels = np.empty_like(labels);
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
                sortedLabels[index] = labels[index];
            }
            return (sorted, sortedLabels);
        }
    }
}
