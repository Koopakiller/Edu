using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringAnalyzer.Helper;

namespace StringAnalyzer.Tests.Analyzer
{
    [TestClass()]
    public class OccurenceFinderTests
    {
        #region FindRepetitions

        //[TestMethod]
        //public void FindRepetitions()
        //{
        //    const string input = "ABCABCABC";
        //    var repetitions = OccurenceFinder.FindRepetitions(input).ToArray();
        //    var filtered = OccurenceFinder.FilterRepetitions(repetitions).ToArray();
        //    Assert.IsTrue(repetitions.SequenceEqual(new[]
        //    {
        //        new Occurence("ABC") {Indices = {0, 3, 6}},
        //        new Occurence("BCA") {Indices = {1, 4}},
        //        new Occurence("CAB") {Indices = {2, 5}},
        //        new Occurence("ABCA") {Indices = {0, 3}},
        //        new Occurence("BCAB") {Indices = {1, 4}},
        //        new Occurence("CABC") {Indices = {2, 5}},
        //        new Occurence("ABCAB") {Indices = {0, 3}},
        //        new Occurence("BCABC") {Indices = {1, 4}},
        //        new Occurence("CABCA") {Indices = {2}},
        //        new Occurence("ABCABC") {Indices = {0, 3}},
        //        new Occurence("BCABCA") {Indices = {1}},
        //        new Occurence("CABCAB") {Indices = {2}},
        //        new Occurence("ABCABCA") {Indices = {0}},
        //        new Occurence("BCABCAB") {Indices = {1}},
        //        new Occurence("CABCABC") {Indices = {2}},
        //        new Occurence("ABCABCAB") {Indices = {0}},
        //        new Occurence("BCABCABC") {Indices = {1}},
        //        new Occurence("ABCABCABC") {Indices = {0}},
        //    }));
        //    Assert.IsTrue(filtered.SequenceEqual(new[]
        //    {
        //        new Occurence("ABC") {Indices = {0, 3, 6}},
        //        new Occurence("ABCABC") {Indices = {0, 3}},
        //        new Occurence("ABCABCABC") {Indices = {0}},
        //    }));
        //}

        #endregion
    }
}