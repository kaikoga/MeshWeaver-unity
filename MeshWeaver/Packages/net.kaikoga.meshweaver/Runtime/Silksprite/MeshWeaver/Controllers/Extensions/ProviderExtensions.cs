using System.Linq;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Models.DataObjects;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Extensions
{
    public static class ProviderExtensions
    {
        public static VertexProvider CreateVertex(this CompositePathProvider parent, Vector3 position, bool crease)
        {
            var gameObject = new GameObject("VertexProvider");
            gameObject.transform.SetParent(parent.transform, false);
            gameObject.transform.localPosition = position;
            var vertexProvider = gameObject.AddComponent<VertexProvider>();
            vertexProvider.crease = crease;
            return vertexProvider;
        }
        
        public static VertexProvider CreateVertex(this CompositePathProvider parent, Vector3 position, bool crease, params Vector2[] uvs)
        {
            var vertexProvider = CreateVertex(parent, position, crease);
            vertexProvider.uvs = uvs.Select((uv, i) => new Vector2MuxData { value = uv, channel = i }).ToArray();
            return vertexProvider;
        }
        
        public static CompositePathProvider CreateChildPathProvider(this CompositePathProvider parent, Vector3 position = default)
        {
            var gameObject = new GameObject("CompositePathProvider");
            gameObject.transform.SetParent(parent.transform, false);
            gameObject.transform.localPosition = position;
            return gameObject.AddComponent<CompositePathProvider>();
        }
    }
}