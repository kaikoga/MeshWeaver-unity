using Silksprite.MeshWeaver.Controllers.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Extensions
{
    public static class ProviderExtensions
    {
        public static void CreateVertex(this CompositePathProvider parent, Vector3 position, bool crease)
        {
            var gameObject = new GameObject("VertexProvider");
            gameObject.transform.SetParent(parent.transform, false);
            gameObject.transform.localPosition = position;
            gameObject.AddComponent<VertexProvider>().crease = crease;
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