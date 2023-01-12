using Neunex.LabelEncoding;
using System.Collections.Generic;

namespace Neunex.DataSetGenerator
{
    internal interface IDataSet
    {
        (double[], int[]) DataSetGen(int quantity, Dictionary<int, LabelStringKey> encodedLabels);
    }
}
