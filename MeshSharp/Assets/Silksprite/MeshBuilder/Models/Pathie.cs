using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Pathie : IEnumerable<Vertie>
    {
        public readonly List<Vertie> Vertices = new List<Vertie>();

        public void Add(Vertie item) => Vertices.Add(item);

        public void Concat(Pathie other, Matrix4x4 matrix4x4)
        {
            Vertices.AddRange(other.Vertices.Select(v => v.MultiplyPoint(matrix4x4)));
        }

        public IEnumerator<Vertie> GetEnumerator()
        {
            return Vertices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}