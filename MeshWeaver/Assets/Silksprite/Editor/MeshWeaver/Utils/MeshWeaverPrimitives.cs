using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Controllers.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class MeshWeaverPrimitives
    {
        public static MeshBehaviour CreateMeshBehaviour(bool renderer)
        {
            var gameObject = new GameObject("Mesh Behaviour");
            var meshBehaviour = gameObject.AddComponent<MeshBehaviour>();
            meshBehaviour.materials = new[] { UnityAssetLocator.DefaultMaterial() };
            meshBehaviour.autoUpdate = true;
            if (renderer) CustomMeshBehaviourEditor.SetupAsMeshRenderer(meshBehaviour);
            return meshBehaviour;
        }

        public static GameObject WrapPrimitiveIfNeeded(MeshProvider meshProvider, GameObject parent)
        {
            if (parent && parent.GetComponentInParent<CustomMeshBehaviour>()) return meshProvider.gameObject;

            var meshBehaviour = CreateMeshBehaviour(true);
            meshProvider.transform.SetParent(meshBehaviour.transform, false);
            return meshBehaviour.gameObject;
        }

        public static MeshProvider CreateCubePrimitive()
        {
            var gameObject = new GameObject("Cube Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;

            var pathX = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path X");
            pillar.pathProviderX = pathX;

            pathX.CreateVertex(new Vector3(0, 0, 0), false);
            pathX.CreateVertex(new Vector3(1, 0, 0), true);
            pathX.CreateVertex(new Vector3(1, 0, 1), true);
            pathX.CreateVertex(new Vector3(0, 0, 1), true);
            pathX.CreateVertex(new Vector3(0, 0, 0), false);

            var pathY = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            pillar.pathProviderY = pathY;

            pathY.CreateVertex(new Vector3(0, 0, 0), false);
            pathY.CreateVertex(new Vector3(0, 1, 0), false);
            return pillar;
        }
    }
}