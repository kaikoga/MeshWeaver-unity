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

        [MenuItem("GameObject/MeshWeaver/Primitives/Quad XY", false, 10)]
        public static void CreateQuadXY(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var cubeProvider = MeshWeaverPrimitives.CreateQuadXYPrimitive();
            DoUnityThings(MeshWeaverPrimitives.WrapPrimitiveIfNeeded(cubeProvider, parent), parent);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Quad XZ", false, 10)]
        public static void CreateQuadXZ(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var cubeProvider = MeshWeaverPrimitives.CreateQuadXZPrimitive();
            DoUnityThings(MeshWeaverPrimitives.WrapPrimitiveIfNeeded(cubeProvider, parent), parent);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Plane", false, 10)]
        public static void CreatePlane(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var cubeProvider = MeshWeaverPrimitives.CreatePlanePrimitive();
            DoUnityThings(MeshWeaverPrimitives.WrapPrimitiveIfNeeded(cubeProvider, parent), parent);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Cube", false, 10)]
        public static void CreateCube(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var cubeProvider = MeshWeaverPrimitives.CreateCubePrimitive();
            DoUnityThings(MeshWeaverPrimitives.WrapPrimitiveIfNeeded(cubeProvider, parent), parent);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Sphere", false, 10)]
        public static void CreateSphere(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var cubeProvider = MeshWeaverPrimitives.CreateSpherePrimitive();
            DoUnityThings(MeshWeaverPrimitives.WrapPrimitiveIfNeeded(cubeProvider, parent), parent);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Cylinder", false, 10)]
        public static void CreateCylinder(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var cubeProvider = MeshWeaverPrimitives.CreateCylinderPrimitive();
            DoUnityThings(MeshWeaverPrimitives.WrapPrimitiveIfNeeded(cubeProvider, parent), parent);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Stairs", false, 10)]
        public static void CreateStairs(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var cubeProvider = MeshWeaverPrimitives.CreateStairsPrimitive();
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