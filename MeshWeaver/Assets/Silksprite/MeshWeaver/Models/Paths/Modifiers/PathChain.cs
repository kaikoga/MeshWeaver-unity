using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths.Modifiers
{
    public class PathChain : IPathieModifier
    {
        readonly float _rolling;

        public PathChain(float rolling)
        {
            _rolling = rolling;
        }

        public Pathie Modify(Pathie pathie)
        {
            if (pathie.Vertices.Count <= 1) return pathie;

            var vertices = pathie.Vertices.ToArray();
            // FIXME: last is only an "end bone", how can we get rid of this without culling?
            var lastVertex = vertices[vertices.Length - 1];
            var last = new Vertie(Matrix4x4.TRS(lastVertex.Translation.GetPosition(), lastVertex.Translation.rotation, Vector3.zero), lastVertex.Uvs);
            var chain = vertices.Pairwise((a, b) => (a, b)).Select((ab, i) =>
                {
                    var (a, b) = ab;
                    var d = b.Vertex - a.Vertex;
                    var mag = d.magnitude;
                    var m = Matrix4x4.TRS(a.Vertex, mag == 0 ? Quaternion.identity : Quaternion.LookRotation(d) * Quaternion.AngleAxis(_rolling * i, Vector3.forward), new Vector3(1, 1, mag));
                    return new Vertie(m, b.Uvs);
                }).Concat(Enumerable.Repeat(last, 1));
            return new Pathie(chain, pathie.isLoop);
        }
    }
}