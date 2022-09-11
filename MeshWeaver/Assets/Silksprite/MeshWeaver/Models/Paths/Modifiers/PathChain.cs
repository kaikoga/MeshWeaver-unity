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
            var vertices = pathie.Vertices;

            if (vertices.Count <= 1) return pathie;

            var chain = vertices.Pairwise((a, b) => (a, b)).Select((ab, i) =>
                {
                    var (a, b) = ab;
                    var d = b.Vertex - a.Vertex;
                    var mag = d.magnitude;
                    var m = Matrix4x4.TRS(a.Vertex, mag == 0 ? Quaternion.identity : Quaternion.LookRotation(d) * Quaternion.AngleAxis(_rolling * i, Vector3.forward), new Vector3(1, 1, mag));
                    return new Vertie(m, b.Uvs);
                });
            return new Pathie(chain, pathie.isLoop);
        }
    }
}