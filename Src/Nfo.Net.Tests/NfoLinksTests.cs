using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Nfo.Net.Tests
{
    [TestClass]
    public class NfoLinksTests
    {
        [TestMethod]
        public void NfoReaderLinksFile1()
        {
            string filecontent = File.ReadAllText("TestFiles/Links1.nfo");
            var metadata = NfoReader.Read(filecontent);

            Assert.AreEqual(1, metadata.Identifiers.Count);
        }

        [TestMethod]
        public void NfoReaderLinks1()
        {
            string filecontent = "https://www.imdb.com/title/tt8946378/?ref_=hm_fanfav_tt_1_pd_fp1";
            var metadata = NfoReader.Read(filecontent);

            Assert.AreEqual(1, metadata.Identifiers.Count);
            Assert.AreEqual("imdb", metadata.Identifiers.First().Type);
            Assert.AreEqual("tt8946378", metadata.Identifiers.First().Id);
            Assert.AreEqual(filecontent, metadata.Identifiers.First().Url);
        }

        [TestMethod]
        public void NfoReaderLinks2()
        {
            string filecontent = "https://www.imdb.com/title?ref_=hm_fanfav_tt_1_pd_fp1";
            var metadata = NfoReader.Read(filecontent);

            Assert.AreEqual(1, metadata.Identifiers.Count);
            Assert.AreEqual("imdb", metadata.Identifiers.First().Type);
            Assert.AreEqual(string.Empty, metadata.Identifiers.First().Id);
            Assert.AreEqual(filecontent, metadata.Identifiers.First().Url);
        }

        public void NfoReaderTheMovieDbLinks1()
        {
            string filecontent = "https://www.themoviedb.org/movie/210577-gone-girl?language=en-US";
            var metadata = NfoReader.Read(filecontent);

            Assert.AreEqual(1, metadata.Identifiers.Count);
            Assert.AreEqual("tmdb", metadata.Identifiers.First().Type);
            Assert.AreEqual("210577", metadata.Identifiers.First().Id);
            Assert.AreEqual(filecontent, metadata.Identifiers.First().Url);
        }
    }
}
