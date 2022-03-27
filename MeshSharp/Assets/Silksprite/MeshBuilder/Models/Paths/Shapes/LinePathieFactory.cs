using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Shapes
{
    public class LinePathieFactory : IPathieFactory
    {
        public Pathie Build(LodMaskLayer lod)
        {
            var vertices = new []
            {
                Vector3.zero,
                Vector3.right / 2,
                Vector3.right
            }.Select(v => new Vertie(v));
            return new Pathie(vertices);
        }
    }
}