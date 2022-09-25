using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models
{
    public class PathieBuilderTest
    {
        [Test]
        public void TestNoSmoothJoin()
        {
            var v1 = ReallyRandomVertie();
            var v2 = ReallyRandomVertie();
            var v3 = ReallyRandomVertie();
            var pathA = new Pathie(new [] {v1, v1, v2}, false);
            var pathB = new Pathie(new [] {v2, v2, v3}, false);
            var path = Pathie.Builder(true).Concat(pathA, Matrix4x4.identity).Concat(pathB, Matrix4x4.identity).ToPathie();
            Assert.AreEqual(6, path.Vertices.Count);

        }

        [Test]
        public void TestSmoothJoin()
        {
            var v1 = ReallyRandomVertie();
            var v2 = ReallyRandomVertie();
            var v3 = ReallyRandomVertie();
            var pathA = new Pathie(new [] {v1, v1, v2}, false);
            var pathB = new Pathie(new [] {v2, v2, v3}, false);
            var path = Pathie.Builder(true, true).Concat(pathA, Matrix4x4.identity).Concat(pathB, Matrix4x4.identity).ToPathie();
            Assert.AreEqual(5, path.Vertices.Count);
        }

        static Vertie ReallyRandomVertie()
        {
            return new Vertie(Matrix4x4.TRS(new Vector3(Random.value, Random.value, Random.value),
                    Quaternion.Euler(new Vector3(Random.value, Random.value, Random.value)),
                    new Vector3(Random.value, Random.value, Random.value)),
                Mux.Single(new Vector2(Random.value, Random.value)));
        }
    }
}