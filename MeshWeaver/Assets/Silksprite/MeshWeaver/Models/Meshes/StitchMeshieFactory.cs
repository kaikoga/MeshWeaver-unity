using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class StitchMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathieA;
        readonly IPathieFactory _pathieB;

        readonly int _materialIndex;

        public StitchMeshieFactory(IPathieFactory pathieA, IPathieFactory pathieB,
            int materialIndex)
        {
            _pathieA = pathieA;
            _pathieB = pathieB;
            _materialIndex = materialIndex;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var pathieA = _pathieA.Build(lod);
            var pathieB = _pathieB.Build(lod);

            var countA = pathieA.Vertices.Count;
            if (countA < 2) return Meshie.Empty();
            var countB = pathieB.Vertices.Count;
            if (countB < 2) return Meshie.Empty();

            var proportionsA = pathieA.ToNetProportions().ToArray();
            var proportionsB = pathieB.ToNetProportions().ToArray();

            var a = 0;
            var b = 0;
            var gons = new List<Gon>();

            void NextA()
            {
                gons.Add(new Gon(new[] { a, countA + b, a + 1 }, _materialIndex));
                a++;
            }

            void NextB()
            {
                gons.Add(new Gon(new[] { a, countA + b, countA + b + 1 }, _materialIndex));
                b++;
            }
            
            while (a < proportionsA.Length - 1 && b < proportionsB.Length - 1)
            {
                if (proportionsA[a + 1] > proportionsB[b + 1])
                {
                    NextB();
                }
                else
                {
                    NextA();
                }
            }

            while (a < proportionsA.Length - 1) NextA();

            while (b < proportionsB.Length - 1) NextB();


            return Meshie.Builder(pathieA.Vertices.Concat(pathieB.Vertices), gons).ToMeshie();
        }

        public Pathie Extract(string pathName, LodMaskLayer lod)
        {
            switch (pathName)
            {
                case PathNames.A:
                    return _pathieA.Build(lod);
                case PathNames.B:
                    return _pathieB.Build(lod);
                default:
                    return Pathie.Empty();
            }
        }

        public static class PathNames
        {
            public const string A = "A";
            public const string B = "B";
        }
    }
}