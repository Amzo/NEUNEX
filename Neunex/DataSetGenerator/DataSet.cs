using Neunex.DataGenerator;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow;

namespace Neunex.DataSetGenerator
{
    public class DataSet : IDataSet
    {
        public (double[], NDarray) DataSetGen(int quantity, Dictionary<string, NDarray> encodedLabels)
        {
            double[] dataset = new double[quantity * encodedLabels.Count()];
            NDarray datalabels = np.arange(quantity * encodedLabels.Count());
            int dataGenPerLabel = quantity / encodedLabels.Count();
            Data data = new Data();
            int index = 0;

            if (quantity % encodedLabels.Count() != 0)
            {
                throw new Exception("Can't balance classes based on quantitiy provided");
            }
            else
            {
                foreach (KeyValuePair<string, NDarray> entry in encodedLabels)
                {
                    for (int i = 0; i < dataGenPerLabel; i++)
                    {
                        var results = data.generateDataPoint(entry.Key, encodedLabels);

                        for (int x = 0; x < results.Length; x++)
                        {
                            datalabels[index] = entry.Value[x];
                            dataset[index] = results[x];
                            index++;
                        }
                    }
                }
                return (dataset, datalabels);
            }
        }
    }
}
