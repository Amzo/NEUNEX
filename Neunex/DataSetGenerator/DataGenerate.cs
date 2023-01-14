using Neunex.DataGenerator;
using Neunex.LabelEncoding;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neunex.DataSetGenerator
{
    public class DataGenerate
    {
        public static (double[], int[]) Generate(int quantity, Dictionary<int, LabelStringKey> encodedLabels)
        {
            DataSet gen = new DataSet();
            return gen.DataSetGen(quantity, encodedLabels);
        }

        public static ((NDarray, NDarray), (NDarray, NDarray)) Cifar10()
        {
            DataSet gen = new DataSet();
            return gen.loadCifar10();
        }
        public static (List<NDarray>, List<NDarray>) Split(int amount, NDarray dataset, NDarray labels) 
        { 
            DataSet gen = new DataSet();
            return gen.SplitDataSet(amount, dataset, labels);
        }
    }
}
