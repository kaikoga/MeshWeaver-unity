using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Shapes
{
    public class LinePathieFactory
    {
        public Pathie Build(Pathie pathie)
        {
            pathie.Vertices.AddRange(new []
            {
                Vector3.zero,
                Vector3.right / 2,
                Vector3.right
            }.Select(v => new Vertie(v, Vector2.zero)));
            return pathie;
        }
    }
}