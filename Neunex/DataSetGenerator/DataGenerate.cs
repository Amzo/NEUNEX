using Neunex.DataGenerator;
using Neunex.LabelEncoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Neunex.DataSetGenerator
{
    public class DataGenerate
    {
        public static (double[], int[]) Generate(int quantity, Dictionary<int, LabelStringKey> encodedLabels)
        {
            DataSet gen = new DataSet();
            return gen.DataSetGen(quantity, encodedLabels);
        }
    }
}
