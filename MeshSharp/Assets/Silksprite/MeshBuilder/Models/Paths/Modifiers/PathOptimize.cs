using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
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

            var verticesCount = pathie.Vertices.Count;
            var result = Pathie.Empty();
            for (var i = 0; i < verticesCount; i++)
            {
                var curr = pathie.Vertices[i];
                var prevIndex = i;
                for (var j = 0; j < verticesCount; j++)
                {
                    prevIndex = (prevIndex + verticesCount - 1) % verticesCount;
                    if (!pathie.Vertices[prevIndex].Culled && !curr.TranslationEquals(pathie.Vertices[prevIndex])) break;
                }
                var nextIndex = i;
                for (var j = 0; j < verticesCount; j++)
                {
                    nextIndex = (nextIndex + 1) % verticesCount;
                    if (!pathie.Vertices[nextIndex].Culled && !curr.TranslationEquals(pathie.Vertices[nextIndex])) break;
                }
                if (Vector3.Angle(curr.Vertex - pathie.Vertices[prevIndex].Vertex, curr.Vertex - pathie.Vertices[nextIndex].Vertex) < 179.9f)
                {
                    result.Vertices.Add(curr);
                }
            }
            return result;
        }
    }
}