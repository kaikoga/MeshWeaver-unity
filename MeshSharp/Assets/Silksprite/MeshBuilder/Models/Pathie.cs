using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Pathie : IEnumerable<Vertie>
    {
        public readonly List<Vertie> Vertices = new List<Vertie>();

        public Pathie() { }

        public Pathie(IEnumerable<Vertie> vertices)
        {
            Vertices.AddRange(vertices);
        }
        
        public void Add(Vertie item) => Vertices.Add(item);

        public void Concat(Pathie other, Matrix4x4 matrix4x4)
        {
            Vertices.AddRange(other.Vertices.Select(v => v.MultiplyPoint(matrix4x4)));
        }

        public Pathie Modify(Func<Vertie, int, Vertie> modifier)
        {
            var result = new Pathie();
            foreach (var (vertie, i) in this.Select((vertie, i) => (vertie, i)))
            {
                result.Add(modifier(vertie, i));
            }
            return result;
        }

        public IEnumerator<Vertie> GetEnumerator()
        {
            return Vertices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("\n", Vertices.Select(v => v.ToString()));
        }
    }
}