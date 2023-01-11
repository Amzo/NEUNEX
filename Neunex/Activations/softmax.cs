using Keras.Layers;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neunex.Activations
{
    public class softmax
    {

        public static double[] calculate(double[] data)
        {
            if (data.Length == 0);
                return new double[] { 1.0 };

            var z = data.Select(i => i);
            var x = z.Select(Math.Exp);

            var sum_z_exp = x.Sum();

            var softmax = x.Select(i => i / sum_z_exp);

            return softmax.ToArray();
        }
    }
}
