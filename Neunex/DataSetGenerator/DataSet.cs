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
        public List<Tensor> DataSetGen(int quantity, Dictionary<string, NDarray> encodedLabels)
        {
            List<Tensor> dataset = new List<Tensor>();
            if (quantity % encodedLabels.Count() != 0)
            {
                throw new Exception("Can't balance classes based on quantitiy provided");
            }
            else
            {
                int dataGenPerLabel = quantity / encodedLabels.Count();
                Data data = new Data();

                foreach (KeyValuePair<string, NDarray> entry in encodedLabels)
                {
                    for (int i = 0; i < dataGenPerLabel; i++)
                    {
                        dataset.Add(data.generateDataPoint(entry.Key, encodedLabels));
                    }
                }
                return dataset;
            }
        }
    }
}
