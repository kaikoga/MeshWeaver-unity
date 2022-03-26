using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Shapes
{
    public class RectPathieFactory : IPathieFactory
    {
        public Pathie Build()
        {
            var vertices = new []
            {
                Vector3.zero,
                Vector3.up,
                Vector3.up + Vector3.right,
                Vector3.right
            }.Select(v => new Vertie(v));
            return new Pathie(vertices);
        }
    }
}