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
            meshBehaviour.autoUpdate = true;

            DoUnityThings(gameObject, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Mesh Behaviour With Renderer", false, 10)]
        public static void CreateMeshBehaviourWithRenderer(MenuCommand menuCommand)
        {
            var gameObject = new GameObject("Mesh Behaviour");
            var meshBehaviour = gameObject.AddComponent<MeshBehaviour>();
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

        [MenuItem("GameObject/MeshWeaver/Primitives/Cube", false, 10)]
        public static void CreateCube(MenuCommand menuCommand)
        {
            var gameObject = new GameObject("Cube Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;
            var pathX = new GameObject("CompositePathProvider_Path X", typeof(CompositePathProvider));
            pathX.transform.SetParent(gameObject.transform, false);
            pillar.pathProviderX = pathX.GetComponent<CompositePathProvider>();

            var x0 = new GameObject("VertexProvider", typeof(VertexProvider));
            x0.transform.SetParent(pathX.transform, false);
            x0.transform.localPosition = new Vector3(0, 0, 0);

            var x1 = new GameObject("VertexProvider", typeof(VertexProvider));
            x1.transform.SetParent(pathX.transform, false);
            x1.transform.localPosition = new Vector3(1, 0, 0);
            x1.GetComponent<VertexProvider>().crease = true;

            var x2 = new GameObject("VertexProvider", typeof(VertexProvider));
            x2.transform.SetParent(pathX.transform, false);
            x2.transform.localPosition = new Vector3(1, 0, 1);
            x2.GetComponent<VertexProvider>().crease = true;

            var x3 = new GameObject("VertexProvider", typeof(VertexProvider));
            x3.transform.SetParent(pathX.transform, false);
            x3.transform.localPosition = new Vector3(0, 0, 1);
            x3.GetComponent<VertexProvider>().crease = true;

            var x4 = new GameObject("VertexProvider", typeof(VertexProvider));
            x4.transform.SetParent(pathX.transform, false);
            x4.transform.localPosition = new Vector3(0, 0, 0);

            var pathY = new GameObject("CompositePathProvider_Path Y", typeof(CompositePathProvider));
            pathY.transform.SetParent(gameObject.transform, false);
            pillar.pathProviderY = pathY.GetComponent<CompositePathProvider>();

            var y0 = new GameObject("VertexProvider", typeof(VertexProvider));
            y0.transform.SetParent(pathY.transform, false);
            y0.transform.localPosition = new Vector3(0, 0, 0);
            
            var y1 = new GameObject("VertexProvider", typeof(VertexProvider));
            y1.transform.SetParent(pathY.transform, false);
            y1.transform.localPosition = new Vector3(0, 1, 0);

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