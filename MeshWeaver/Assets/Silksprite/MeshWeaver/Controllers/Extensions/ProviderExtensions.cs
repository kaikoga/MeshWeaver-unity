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
    }
}