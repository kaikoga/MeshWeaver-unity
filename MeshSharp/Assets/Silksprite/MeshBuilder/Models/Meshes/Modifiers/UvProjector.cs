using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class UvProjector : IMeshieModifier, IPathieModifier
    {
        readonly Matrix4x4 _translation;
        readonly int _minIndex;

        public UvProjector(Matrix4x4 translation, int minIndex)
        {
            _translation = translation;
            _minIndex = minIndex;
        }

        public Meshie Modify(Meshie meshie)
        {
            var result = new Meshie();
            result.Vertices.AddRange(meshie.Vertices.Select(v =>
            {
                var translation = _translation;
                return v.AddUv(new Channel<Vector2>(translation.MultiplyPoint(v.Vertex), _minIndex));
            }));
            result.Indices.AddRange(meshie.Indices);
            return result;
        }

        public Pathie Modify(Pathie meshie)
        {
            var result = new Pathie();
            result.Vertices.AddRange(meshie.Vertices.Select(v =>
            {
                var translation = _translation;
                return v.AddUv(new Channel<Vector2>(translation.MultiplyPoint(v.Vertex), _minIndex));
            }));
            return result;
        }
    }
}