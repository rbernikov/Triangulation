using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triangulation.Grd;

namespace TriangulationTest
{
    [TestClass]
    public class GrdFileTest
    {
        [TestMethod]
        public void ShouldReadFile()
        {
            var file = GrdFile.Read(@"D:\input\zones.grd");

            Assert.AreEqual("DSBB", file.Info);
            Assert.AreEqual(944, file.Row);
            Assert.AreEqual(944, file.Column);
            Assert.AreEqual(8457525, file.MinX);
            Assert.AreEqual(8504675, file.MaxX);
            Assert.AreEqual(5364775, file.MinY);
            Assert.AreEqual(5411925, file.MaxY);
            Assert.AreEqual(58, file.MaxZ);
        }

        [TestMethod]
        public void ShouldWriteFile()
        {
            var file = new GrdFile();
            file.Write(@"D:\input\zones_test.grd");

            file = GrdFile.Read(@"D:\input\zones_test.grd");

            Assert.AreEqual("DSBB", file.Info);
            Assert.AreEqual(944, file.Row);
            Assert.AreEqual(944, file.Column);
            Assert.AreEqual(8457525, file.MinX);
            Assert.AreEqual(8504675, file.MaxX);
            Assert.AreEqual(5364775, file.MinY);
            Assert.AreEqual(5411925, file.MaxY);
            Assert.AreEqual(0, file.MaxZ);
        }
    }
}
