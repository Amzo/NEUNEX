using Neunex.LabelEncoding;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow;
using Tensorflow.NumPy;

namespace Neunex.DataGenerator
{
    internal interface IData
    {
        List<double> generateDataPoint(int size, int indexKey, LabelStringKey encodedLabels);
    }
}
