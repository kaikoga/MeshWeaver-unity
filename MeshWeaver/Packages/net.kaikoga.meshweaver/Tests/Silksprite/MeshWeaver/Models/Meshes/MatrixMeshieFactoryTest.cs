using Array = System.Array;
using NUnit.Framework;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class MatrixMeshieFactoryTest
    {
        [Test]
        public void TestEmpty()
        {
            var pathieX = PathieFactory.Empty;
            var pathieY = PathieFactory.Empty;
            var factory = SimpleFactory(pathieX, pathieY);
            var meshie = factory.Build(LodMaskLayer.LOD0);
            Assert.AreEqual(0, meshie.Vertices.Count);
            Assert.AreEqual(0, meshie.Gons.Count);
        }

        [Test]
        public void TestNonLoop()
        {
            var pathieX = CompositePathieFactory.Builder(false).ConcatVertex(Vector3.right * 1).ConcatVertex(Vector3.right * 2).ConcatVertex(Vector3.right * 3).ToFactory();
            var pathieY = CompositePathieFactory.Builder(false).ConcatVertex(Vector3.up * 1).ConcatVertex(Vector3.up * 2).ConcatVertex(Vector3.up * 3).ToFactory();
            var factory = SimpleFactory(pathieX, pathieY);
            var meshie = factory.Build(LodMaskLayer.LOD0);
            Assert.AreEqual(9, meshie.Vertices.Count);
            Assert.AreEqual(8, meshie.Gons.Count);
        }

        [Test]
        public void TestLoop()
        {
            var pathieX = CompositePathieFactory.Builder(true).ConcatVertex(Vector3.right * 1).ConcatVertex(Vector3.right * 2).ConcatVertex(Vector3.right * 3).ToFactory();
            var pathieY = CompositePathieFactory.Builder(true).ConcatVertex(Vector3.up * 1).ConcatVertex(Vector3.up * 2).ConcatVertex(Vector3.up * 3).ToFactory();
            var factory = SimpleFactory(pathieX, pathieY);
            var meshie = factory.Build(LodMaskLayer.LOD0);
            Assert.AreEqual(9, meshie.Vertices.Count);
            Assert.AreEqual(18, meshie.Gons.Count);
        }

        static MatrixMeshieFactory SimpleFactory(IPathieFactory pathieX, IPathieFactory pathieY)
        {
            return new MatrixMeshieFactory(pathieX, pathieY,
                MatrixMeshieFactory.OperatorKind.TranslateOnly, MatrixMeshieFactory.CellPatternKind.Default, Array.Empty<MatrixMeshieFactory.CellOverride>(),
                0);
        }
    }
}