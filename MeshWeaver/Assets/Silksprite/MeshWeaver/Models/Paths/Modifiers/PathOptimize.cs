using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths.Modifiers
{
    public class PathOptimize : IPathieModifier
    {
        public PathOptimize()
        {
        }

        public Pathie Modify(Pathie pathie)
        {
            var vertices = pathie.Vertices;
            if (vertices.Pairwise((a, b) => (a, b)).All(ab => ab.a.TranslationEquals(ab.b))) return pathie;

            var verticesCount = vertices.Count;

            var result = Pathie.Builder(pathie.isLoop);
            for (var i = 0; i < verticesCount; i++)
            {
                var curr = vertices[i];
                var prevIndex = i;
                for (var j = 0; j < verticesCount; j++)
                {
                    prevIndex = (prevIndex + verticesCount - 1) % verticesCount;
                    if (!curr.TranslationEquals(vertices[prevIndex])) break;
                }
                var nextIndex = i;
                for (var j = 0; j < verticesCount; j++)
                {
                    nextIndex = (nextIndex + 1) % verticesCount;
                    if (!curr.TranslationEquals(vertices[nextIndex])) break;
                }
                if (Vector3.Angle(curr.Vertex - vertices[prevIndex].Vertex, curr.Vertex - vertices[nextIndex].Vertex) < 179.9f)
                {
                    result.Vertices.Add(curr);
                }
            }
            return result.ToPathie();
        }
    }
}