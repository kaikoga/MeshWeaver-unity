using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Controllers.Paths;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Primitives
{
    public static class MeshWeaverMenuItems
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

        [MenuItem("GameObject/MeshWeaver/Empty/Vertex Provider", false, 10)]
        public static void CreateVertexProvider(MenuCommand menuCommand)
        {
            var gameObject = new GameObject("Vertex Provider");
            gameObject.AddComponent<VertexProvider>();
            DoUnityThings(gameObject, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Quad XY", false, 10)]
        public static void CreateQuadXY(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateQuadXYPrimitive();
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Quad XZ", false, 10)]
        public static void CreateQuadXZ(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateQuadXZPrimitive();
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Plane", false, 10)]
        public static void CreatePlane(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreatePlanePrimitive(true);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Cube", false, 10)]
        public static void CreateCube(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateCubePrimitive(true);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Sphere", false, 10)]
        public static void CreateSphere(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateSpherePrimitive(true);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Cylinder Extruded", false, 10)]
        public static void CreateExtrudedCylinder(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateExtrudedCylinderPrimitive(true);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Cylinder Rotated", false, 10)]
        public static void CreateRotatedCylinder(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateRotatedCylinderPrimitive(true);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/Stairs", false, 10)]
        public static void CreateStairs(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateStairsPrimitive(true);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/NoUV/Plane", false, 10)]
        public static void CreatePlaneNoUv(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreatePlanePrimitive(false);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/NoUV/Cube", false, 10)]
        public static void CreateCubeNoUv(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateCubePrimitive(false);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/NoUV/Sphere", false, 10)]
        public static void CreateSphereNoUv(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateSpherePrimitive(false);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/NoUV/Cylinder Extruded", false, 10)]
        public static void CreateExtrudedCylinderNoUv(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateExtrudedCylinderPrimitive(false);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/NoUV/Cylinder Rotated", false, 10)]
        public static void CreateRotatedCylinderNoUv(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateRotatedCylinderPrimitive(false);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        [MenuItem("GameObject/MeshWeaver/Primitives/NoUV/Stairs", false, 10)]
        public static void CreateStairsNoUv(MenuCommand menuCommand)
        {
            var primitiveProvider = MeshWeaverPrimitives.CreateStairsPrimitive(false);
            DoUnityThings(primitiveProvider, menuCommand.context as GameObject);
        }

        static void DoUnityThings(MeshProvider meshProvider, GameObject parent) => DoUnityThings(MeshWeaverPrimitives.WrapPrimitiveIfNeeded(meshProvider, parent), parent);

        static void DoUnityThings(GameObject gameObject, GameObject parent)
        {
            GameObjectUtility.SetParentAndAlign(gameObject, parent);
            Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
            Selection.activeObject = gameObject;
        }
    }
}