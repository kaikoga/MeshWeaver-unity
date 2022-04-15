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
            var meshBehaviour = MeshWeaverPrimitives.CreateMeshBehaviour(false);

            DoUnityThings(meshBehaviour.gameObject, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Mesh Behaviour With Renderer", false, 10)]
        public static void CreateMeshBehaviourWithRenderer(MenuCommand menuCommand)
        {
            var meshBehaviour = MeshWeaverPrimitives.CreateMeshBehaviour(true);

            DoUnityThings(meshBehaviour.gameObject, menuCommand.context as GameObject);
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
            var parent = menuCommand.context as GameObject;
            var cubeProvider = MeshWeaverPrimitives.CreateCubePrimitive();
            DoUnityThings(MeshWeaverPrimitives.WrapPrimitiveIfNeeded(cubeProvider, parent), parent);
        }

        static void DoUnityThings(GameObject gameObject, GameObject parent)
        {
            GameObjectUtility.SetParentAndAlign(gameObject, parent);
            Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
            Selection.activeObject = gameObject;
        }
    }
}