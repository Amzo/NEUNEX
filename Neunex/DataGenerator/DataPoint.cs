using Neunex.LabelEncoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neunex.DataGenerator
{
    internal class DataPoint
    {
        public static List<double> Get(int size, int indexKey, LabelStringKey encodedLabels)
        {
            Data data = new Data();

            return data.generateDataPoint(size, indexKey, encodedLabels);

        }
    }
}
