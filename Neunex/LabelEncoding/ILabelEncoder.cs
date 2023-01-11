using Numpy;
using System.Collections.Generic;

namespace Neunex.LabelEncoding
{
    internal interface ILabelEncoder
    {
        Dictionary<string, NDarray> OneHotEncode(string[] labels);  
    }
}
