using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Pathie
    {
        public readonly List<Vertie> Vertices = new List<Vertie>();
        public IEnumerable<Vertie> ActiveVertices => Vertices.Where(v => !v.Culled);

        public Vertie First => Vertices.Count > 0 ? Vertices[0] : default;
        public Vertie Last => Vertices.Count > 0 ? Vertices[Vertices.Count - 1] : default;
        
        public Vertie Diff => Last / First;

        public Pathie() { }

        public Pathie(IEnumerable<Vertie> vertices)
        {
            Vertices.AddRange(vertices);
        }

        public void Concat(Pathie other, Matrix4x4 matrix4x4)
        {
            Vertices.AddRange(other.Vertices.Select(v => v.MultiplyPoint(matrix4x4)));
        }

        public Pathie Modify(Func<Vertie, int, Vertie> modifier)
        {
            var result = new Pathie();
            foreach (var (vertie, i) in Vertices.Select((vertie, i) => (vertie, i)))
            {
                result.Vertices.Add(modifier(vertie, i));
            }
            return result;
        }

        public override string ToString()
        {
            return $"V[{Vertices.Count}]";
        }

        public string Dump()
        {
            var vertices = string.Join("\n", Vertices.Select(v => v.ToString()));
            return $"V[{Vertices.Count}]\n{vertices}";
        }
    }
}