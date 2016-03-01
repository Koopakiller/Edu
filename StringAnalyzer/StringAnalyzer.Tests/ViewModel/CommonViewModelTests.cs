using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringAnalyzer.ViewModel;

namespace StringAnalyzer.Tests.ViewModel
{
    [TestClass()]
    public class CommonViewModelTests
    {
        [TestMethod()]
        public void GetText_IgnoreLST()
        {
            var x = new CommonViewModel
            {
                Text = "A\tB C\rD\nE",
                IgnoreLinebreaks = true,
                IgnoreSpaces = true,
                IgnoreTabs = true
            };

            Assert.AreEqual("ABCDE", x.GetText());
        }
        [TestMethod()]
        public void GetText_IgnoreST()
        {
            var x = new CommonViewModel
            {
                Text = "A\tB C\rD\nE",
                IgnoreLinebreaks = false,
                IgnoreSpaces = true,
                IgnoreTabs = true
            };

            Assert.AreEqual("ABC\rD\nE", x.GetText());
        }
        [TestMethod()]
        public void GetText_IgnoreLT()
        {
            var x = new CommonViewModel
            {
                Text = "A\tB C\rD\nE",
                IgnoreLinebreaks = true,
                IgnoreSpaces = false,
                IgnoreTabs = true
            };

            Assert.AreEqual("AB CDE", x.GetText());
        }
        [TestMethod()]
        public void GetText_IgnoreT()
        {
            var x = new CommonViewModel
            {
                Text = "A\tB C\rD\nE",
                IgnoreLinebreaks = false,
                IgnoreSpaces = false,
                IgnoreTabs = true
            };

            Assert.AreEqual("AB C\rD\nE", x.GetText());
        }
        [TestMethod()]
        public void GetText_IgnoreLS()
        {
            var x = new CommonViewModel
            {
                Text = "A\tB C\rD\nE",
                IgnoreLinebreaks = true,
                IgnoreSpaces = true,
                IgnoreTabs = false
            };

            Assert.AreEqual("A\tBCDE", x.GetText());
        }
        [TestMethod()]
        public void GetText_IgnoreS()
        {
            var x = new CommonViewModel
            {
                Text = "A\tB C\rD\nE",
                IgnoreLinebreaks = false,
                IgnoreSpaces = true,
                IgnoreTabs = false
            };

            Assert.AreEqual("A\tBC\rD\nE", x.GetText());
        }
        [TestMethod()]
        public void GetText_IgnoreL()
        {
            var x = new CommonViewModel
            {
                Text = "A\tB C\rD\nE",
                IgnoreLinebreaks = true,
                IgnoreSpaces = false,
                IgnoreTabs = false
            };

            Assert.AreEqual("A\tB CDE", x.GetText());
        }
        [TestMethod()]
        public void GetText_IgnoreNone()
        {
            var x = new CommonViewModel
            {
                Text = "A\tB C\rD\nE",
                IgnoreLinebreaks = false,
                IgnoreSpaces = false,
                IgnoreTabs = false
            };

            Assert.AreEqual("A\tB C\rD\nE", x.GetText());
        }
    }
}