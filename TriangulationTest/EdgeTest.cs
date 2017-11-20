using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triangulation.Geometry;

namespace TriangulationTest
{
    [TestClass]
    public class EdgeTest
    {
        [TestMethod]
        public void ShouldBeEqual()
        {
            var e1 = new Edge(new Vertex(1, 1), new Vertex(2, 2));
            var e2 = new Edge(new Vertex(1, 1), new Vertex(2, 2));
            var e3 = new Edge(new Vertex(2, 2), new Vertex(1, 1));

            Assert.AreEqual(e1.GetHashCode(), e2.GetHashCode());
            Assert.AreEqual(true, e1.Equals(e2));
            Assert.AreEqual(e1.GetHashCode(), e3.GetHashCode());
            Assert.AreEqual(true, e1.Equals(e3));
        }
    }
}