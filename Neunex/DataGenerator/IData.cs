using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow;

namespace Neunex.DataGenerator
{
    internal interface IData
    {
        Tensor generateDataPoint();
    }
}
