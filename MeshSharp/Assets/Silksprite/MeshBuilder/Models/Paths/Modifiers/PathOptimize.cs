using System.Linq;
using Silksprite.MeshBuilder.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Modifiers
{
    public class PathOptimize : IPathieModifier
    {
        public PathOptimize()
        {
        }

        public Pathie Modify(Pathie pathie)
        {
            if (pathie.Active.Vertices.Pairwise((a, b) => (a, b)).All(ab => ab.a.TranslationEquals(ab.b))) return pathie;

            var workVertices = pathie.Vertices.ToArray();
            var verticesCount = workVertices.Length;

            var result = Pathie.Builder();
            for (var i = 0; i < verticesCount; i++)
            {
                var curr = workVertices[i];
                var prevIndex = i;
                for (var j = 0; j < verticesCount; j++)
                {
                    prevIndex = (prevIndex + verticesCount - 1) % verticesCount;
                    if (!workVertices[prevIndex].Culled && !curr.TranslationEquals(workVertices[prevIndex])) break;
                }
                var nextIndex = i;
                for (var j = 0; j < verticesCount; j++)
                {
                    nextIndex = (nextIndex + 1) % verticesCount;
                    if (!workVertices[nextIndex].Culled && !curr.TranslationEquals(workVertices[nextIndex])) break;
                }
                if (Vector3.Angle(curr.Vertex - workVertices[prevIndex].Vertex, curr.Vertex - workVertices[nextIndex].Vertex) < 179.9f)
                {
                    result.Vertices.Add(curr);
                }
            }
            return result.ToPathie();
        }
    }
}