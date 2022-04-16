using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths.Modifiers
{
    public class PathChain : IPathieModifier
    {
        public Pathie Modify(Pathie pathie)
        {
            if (pathie.Vertices.Count <= 1) return pathie;

            var activeVertices = pathie.Active.Vertices.ToArray();
            var lastVertex = activeVertices[activeVertices.Length - 1];
            var last = new Vertie(Matrix4x4.TRS(lastVertex.Translation.GetPosition(), lastVertex.Translation.rotation, Vector3.zero), true, lastVertex.Uvs);
            var vertices = activeVertices.Pairwise((a, b) =>
                {
                    var d = b.Vertex - a.Vertex;
                    var m = Matrix4x4.TRS(a.Vertex, Quaternion.LookRotation(d), new Vector3(1, 1, d.magnitude));
                    return new Vertie(m, b.Culled, b.Uvs);
                }).Concat(Enumerable.Repeat(last, 1));
            return new Pathie(vertices);
        }
    }
}