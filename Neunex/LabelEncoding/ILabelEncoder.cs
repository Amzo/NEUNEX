using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neunex.LabelEncoding
{
    internal interface ILabelEncoder
    {
        Dictionary<string, NDarray> encodeLabels(string[] labels);  
    }
}
