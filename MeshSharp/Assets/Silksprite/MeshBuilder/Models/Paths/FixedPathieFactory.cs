using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public class FixedPathieFactory
    {
        public Pathie Build(Pathie pathie)
        {
            pathie.Vertices.AddRange(new []
            {
                Vector3.zero,
                Vector3.left,
                Vector3.left + Vector3.up,
                Vector3.up,
                Vector3.up + Vector3.right,
                Vector3.right
            }.Select(v => new Vertie(v, Vector2.zero, Matrix4x4.identity)));
            return pathie;
        }
    }
}