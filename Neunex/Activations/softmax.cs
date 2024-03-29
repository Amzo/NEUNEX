﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Neunex.Activations
{
    public class softmax
    {
        public static List<double> calculate(List<double> data)
        {
            // give them a probability of 1. Be nice.
            if (data.Count == 0)
                return new List<double> { 1.0 };

            var z = data.Select(i => i);
            var x = z.Select(Math.Exp);

            var sum_z_exp = x.Sum();

            var softmax = x.Select(i => i / sum_z_exp);

            return softmax.ToList();
        }
    }
}
