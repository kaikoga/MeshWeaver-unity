using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Controllers.Paths;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class MeshBuilderMenus
    {
        [MenuItem("GameObject/MeshWeaver/Mesh Behaviour", false, 10)]
        public static void CreateMeshBehaviour(MenuCommand menuCommand)
        {
            var gameObject = new GameObject("Mesh Behaviour");
            var meshBehaviour = gameObject.AddComponent<MeshBehaviour>();
            meshBehaviour.materials = new[] { UnityAssetLocator.DefaultMaterial() };
            meshBehaviour.autoUpdate = true;

            DoUnityThings(gameObject, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Mesh Behaviour With Renderer", false, 10)]
        public static void CreateMeshBehaviourWithRenderer(MenuCommand menuCommand)
        {
            var gameObject = new GameObject("Mesh Behaviour");
            var meshBehaviour = gameObject.AddComponent<MeshBehaviour>();
            meshBehaviour.materials = new[] { UnityAssetLocator.DefaultMaterial() };
            meshBehaviour.autoUpdate = true;
            CustomMeshBehaviourEditor.SetupAsMeshRenderer(meshBehaviour);

            DoUnityThings(gameObject, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Empty/Composite Mesh Provider", false, 10)]
        public static void CreateMeshProvider(MenuCommand menuCommand)
        {
            var gameObject = new GameObject("Composite Mesh Provider");
            gameObject.AddComponent<CompositeMeshProvider>();

            DoUnityThings(gameObject, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Empty/Composite Path Provider", false, 10)]
        public static void CreatePathProvider(MenuCommand menuCommand)
        {
            var gameObject = new GameObject("Composite Path Provider");
            gameObject.AddComponent<CompositePathProvider>();

            DoUnityThings(gameObject, menuCommand.context as GameObject);
        }

        static void CreateVertex(Transform parent, Vector3 position, bool crease)
        {
            var gameObject = new GameObject("VertexProvider");
            gameObject.transform.SetParent(parent, false);
            gameObject.transform.localPosition = position;
            gameObject.AddComponent<VertexProvider>().crease = crease;
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Cube", false, 10)]
        public static void CreateCube(MenuCommand menuCommand)
        {
            var gameObject = new GameObject("Cube Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;
            
            var pathXObject = new GameObject("CompositePathProvider_Path X");
            var pathX = pathXObject.transform;
            pathX.SetParent(gameObject.transform, false);
            pillar.pathProviderX = pathXObject.AddComponent<CompositePathProvider>();

            CreateVertex(pathX, new Vector3(0, 0, 0), false);
            CreateVertex(pathX, new Vector3(1, 0, 0), true);
            CreateVertex(pathX, new Vector3(1, 0, 1), true);
            CreateVertex(pathX, new Vector3(0, 0, 1), true);
            CreateVertex(pathX, new Vector3(0, 0, 0), false);

            var pathYObject = new GameObject("CompositePathProvider_Path Y");
            var pathY = pathYObject.transform;
            pathY.SetParent(gameObject.transform, false);
            pillar.pathProviderY = pathYObject.AddComponent<CompositePathProvider>();

            CreateVertex(pathY, new Vector3(0, 0, 0), false);
            CreateVertex(pathY, new Vector3(0, 1, 0), false);

            DoUnityThings(gameObject, menuCommand.context as GameObject);
        }

        static void DoUnityThings(GameObject gameObject, GameObject parent)
        {
            GameObjectUtility.SetParentAndAlign(gameObject, parent);
            Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
            Selection.activeObject = gameObject;
        }
    }
}