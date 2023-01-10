using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow;

namespace Neunex.DataSetGenerator
{
    internal interface IDataSet
    {
        (double[], NDarray) DataSetGen(int quantity, Dictionary<string, NDarray> encodedLabels);
    }
}
