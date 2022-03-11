using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Shapes
{
    public class RectPathieFactory
    {
        public Pathie Build()
        {
            var pathie = new Pathie();
            pathie.Vertices.AddRange(new []
            {
                Vector3.zero,
                Vector3.up,
                Vector3.up + Vector3.right,
                Vector3.right
            }.Select(v => new Vertie(v, Vector2.zero)));
            return pathie;
        }
    }
}