using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neunex.DataSetGenerator;
using Numpy;
using System;

namespace NeuNeXTests.DataSets
{
    [TestClass]
    public class DataSplitting
    {
        [TestMethod]
        [ExpectedException(typeof(Exception),
        "can't split dataset equally")]
        public void testSplitException()
        {
            var m = np.array(new int[2, 2, 3]{
                { { 1, 2, 3}, {4, 5, 6} },
                { { 7, 8, 9}, {10, 11, 12} }
            });

            var z = np.array(new int[2] { 1, 2 });
            var split1 = np.array(new int[1, 2, 3] { { { 1, 2, 3 }, { 4, 5, 6 } } });
            var split2 = np.array(new int[1, 2, 3] { { { 7, 8, 9 }, { 10, 11, 12 } } });

            var result = DataGenerate.Split(3, m, z);
        }

        [TestMethod]
        public void testSplitsAreEqual()
        {
            var m = np.array(new int[2, 2, 3]{
                { { 1, 2, 3}, {4, 5, 6} },
                { { 7, 8, 9}, {10, 11, 12} }
            });

            var z = np.array(new int[2] { 1, 2 });
            var split1 = np.array(new int[1, 2, 3] { { { 1, 2, 3 }, { 4, 5, 6 } } });
            var split2 = np.array(new int[1, 2, 3] { { { 7, 8, 9 }, { 10, 11, 12 } } });

            (var result, var labels) = DataGenerate.Split(2, m, z);

            Assert.AreEqual(result[0], split1);
            Assert.AreEqual(result[1], split2);
        }

        [TestMethod]
        public void testSplitsLabels()
        {
            var m = np.array(new int[2, 2, 3]{
                { { 1, 2, 3}, {4, 5, 6} },
                { { 7, 8, 9}, {10, 11, 12} }
            });

            var z = np.array(new int[2, 1]{ {1 }, { 2} });
            var split1 = np.array(new int[1, 2, 3] { { { 1, 2, 3 }, { 4, 5, 6 } } });
            var split2 = np.array(new int[1, 2, 3] { { { 7, 8, 9 }, { 10, 11, 12 } } });

            (var result, var labels) = DataGenerate.Split(2, m, z);

            Assert.AreEqual(z[0], labels[0]);
            Assert.AreEqual(z[1], labels[1]);
        }
    }
}
