using System.Collections.Generic;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Pathie
    {
        public readonly List<Vector3> Vertices = new List<Vector3>();

        public void Concat(Pathie other)
        {
            Vertices.AddRange(other.Vertices);
        }
    }
}