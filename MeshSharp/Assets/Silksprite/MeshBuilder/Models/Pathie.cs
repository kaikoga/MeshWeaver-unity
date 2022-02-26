using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Pathie
    {
        public readonly List<Vector3> Vertices = new List<Vector3>();

        public void Concat(Pathie other, Matrix4x4 matrix4x4)
        {
            Vertices.AddRange(other.Vertices.Select(matrix4x4.MultiplyPoint));
        }
    }
}