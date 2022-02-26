using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    public class PathProvider : GeometryProvider
    {
        public Pathie ToPathie()
        {
            var pathie = new Pathie();
            pathie.Vertices.AddRange(new []
            {
                Vector3.zero,
                Vector3.left,
                Vector3.left + Vector3.up,
                Vector3.up,
                Vector3.up + Vector3.right,
                Vector3.right
            });
            return pathie;
        }
    }
}