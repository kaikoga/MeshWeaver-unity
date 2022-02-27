using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Pathie : IEnumerable<Vector3>
    {
        public readonly List<Vector3> Vertices = new List<Vector3>();

        public void Add(Vector3 item) => Vertices.Add(item);

        public void Concat(Pathie other, Matrix4x4 matrix4x4)
        {
            Vertices.AddRange(other.Vertices.Select(matrix4x4.MultiplyPoint));
        }

        public IEnumerator<Vector3> GetEnumerator()
        {
            return Vertices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}