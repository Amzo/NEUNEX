using Numpy;
using System.Collections.Generic;

namespace Neunex.LabelEncoding
{
    internal interface ILabelEncoder
    {
        Dictionary<int, LabelStringKey> OneHotEncode(Dictionary<int, string> labels);  
    }
}
