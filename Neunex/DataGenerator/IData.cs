using Neunex.LabelEncoding;
using System.Collections.Generic;

namespace Neunex.DataGenerator
{
    internal interface IData
    {
        List<double> generateDataPoint(int size, int indexKey, LabelStringKey encodedLabels);
    }
}
