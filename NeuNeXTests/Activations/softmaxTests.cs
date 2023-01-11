using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neunex.Activations;
using System;
using System.Linq;

namespace NeuNeXTests.Activations
{
    [TestClass]
    public class softmaxTests
    {
        [TestMethod]
        public void TestSoftResults()
        {
            var testArray = new[] { 1.0, 2.0, 3.0, 4.0, 1.0, 2.0, 3.0 };
            var expectedResults = new[] {
                0.023640543021591385, 
                0.064261658510496159, 
                0.17468129859572226, 
                0.47483299974438031,
                0.023640543021591385, 
                0.064261658510496159, 
                0.17468129859572226 
            };

            var results = softmax.calculate(testArray);
            bool isEqual = Enumerable.SequenceEqual(expectedResults, results);

            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void TestSoftmaxPrecision()
        {
            var testArray = new double[] { 5, 8, 7, 3 };

            // max digits support byu
            var expectedResults = new decimal[] {
                0.034952901291011960949897084527685464103421372685911m,
                0.70204778945315464171797414671768606170886362822167m,
                0.25826894845967292817661790770366938402863065047131m,
                0.0047303607961604691555108610509590901590843486211167m
            };

            var results = softmax.calculate(testArray);
            double[] convertedExpected = Array.ConvertAll(expectedResults, x => (double)x);
            bool isEqual = Enumerable.SequenceEqual(convertedExpected, results);

            Assert.IsTrue(isEqual);
        }
    }
}
