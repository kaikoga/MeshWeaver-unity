using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Controllers.Modifiers;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Controllers.Paths.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;
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

        static VertexProvider AddVertexProvider(Transform parent)
        {
            var gameObject = new GameObject("VertexProvider");
            gameObject.transform.SetParent(parent, false);
            var vertexProvider = gameObject.AddComponent<VertexProvider>();
            return vertexProvider;
        }

        static UvGeneratorProvider AddUvGeneratorProvider(PathProvider pathProvider, Vector2 min, Vector2 max, int uvChannel, float topologicalWeight = 1f)
        {
            var uvGenerator = pathProvider.gameObject.AddComponent<UvGeneratorProvider>();
            uvGenerator.min = min;
            uvGenerator.max = max;
            uvGenerator.uvChannel = uvChannel;
            uvGenerator.topologicalWeight = topologicalWeight;
            return uvGenerator;
        }

        static UvProjectorProvider AddUvProjectorProvider(PathProvider pathProvider, int uvChannel)
        {
            var uvProjector = pathProvider.gameObject.AddComponent<UvProjectorProvider>();
            uvProjector.uvChannel = uvChannel;
            var reference = new GameObject("ReferenceTranslation");
            var referenceTransform = reference.AddComponent<Transform>();
            referenceTransform.SetParent(uvProjector.transform, false);
            uvProjector.referenceTranslation = referenceTransform;
            return uvProjector;
        }

        public static GameObject WrapPrimitiveIfNeeded(MeshProvider meshProvider, GameObject parent)
        {
            if (parent && parent.GetComponentInParent<CustomMeshBehaviour>()) return meshProvider.gameObject;

            var meshBehaviour = CreateMeshBehaviour(true);
            meshProvider.transform.SetParent(meshBehaviour.transform, false);
            return meshBehaviour.gameObject;
        }

        public static MeshProvider CreateQuadXYPrimitive()
        {
            var gameObject = new GameObject("Quad Provider");
            var polygon = gameObject.AddComponent<PolygonMeshProvider>();

            var path = polygon.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path");
            polygon.pathProvider = path;

            path.CreateVertex(new Vector3(0f, 0f, 0f), false, new Vector2(0f, 0f));
            path.CreateVertex(new Vector3(0f, 1f, 0f), false, new Vector2(0f, 1f));
            path.CreateVertex(new Vector3(1f, 1f, 0f), false, new Vector2(1f, 1f));
            path.CreateVertex(new Vector3(1f, 0f, 0f), false, new Vector2(1f, 0f));
            path.CreateVertex(new Vector3(0f, 0f, 0f), false, new Vector2(0f, 0f));

            return polygon;
        }

        public static MeshProvider CreateQuadXZPrimitive()
        {
            var gameObject = new GameObject("Quad Provider");
            var polygon = gameObject.AddComponent<PolygonMeshProvider>();

            var path = polygon.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path");
            polygon.pathProvider = path;

            path.CreateVertex(new Vector3(0f, 0f, 0f), false, new Vector2(0f, 0f));
            path.CreateVertex(new Vector3(0f, 0f, 1f), false, new Vector2(0f, 1f));
            path.CreateVertex(new Vector3(1f, 0f, 1f), false, new Vector2(1f, 1f));
            path.CreateVertex(new Vector3(1f, 0f, 0f), false, new Vector2(1f, 0f));
            path.CreateVertex(new Vector3(0f, 0f, 0f), false, new Vector2(0f, 0f));

            return polygon;
        }

        public static MeshProvider CreatePlanePrimitive()
        {
            var gameObject = new GameObject("Plane Provider");
            var matrix = gameObject.AddComponent<MatrixMeshProvider>();

            var pathX = matrix.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path X");
            matrix.pathProviderX = pathX;

            pathX.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathX.CreateVertex(new Vector3(1f, 0f, 0f), false);
            AddUvGeneratorProvider(pathX, new Vector2(0f, 0f), new Vector2(1f, 0f), 0);
            var subdivisionX = pathX.gameObject.AddComponent<PathSubdivisionProvider>();
            subdivisionX.maxCount = 4;
            subdivisionX.maxLength = 0.25f;

            var pathY = matrix.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            matrix.pathProviderY = pathY;
            pathY.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathY.CreateVertex(new Vector3(0f, 0f, 1f), false);
            AddUvGeneratorProvider(pathY, new Vector2(0f, 0f), new Vector2(0f, 1f), 0);
            var subdivisionY = pathY.gameObject.AddComponent<PathSubdivisionProvider>();
            subdivisionY.maxCount = 4;
            subdivisionY.maxLength = 0.25f;
            return matrix;
        }

        public static MeshProvider CreateCubePrimitive()
        {
            var gameObject = new GameObject("Cube Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;
            pillar.uvChannelBottom = 1;
            pillar.uvChannelTop = 1;

            var pathX = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path X");
            pillar.pathProviderX = pathX;
            pathX.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathX.CreateVertex(new Vector3(1f, 0f, 0f), true);
            pathX.CreateVertex(new Vector3(1f, 0f, 1f), true);
            pathX.CreateVertex(new Vector3(0f, 0f, 1f), true);
            pathX.CreateVertex(new Vector3(0f, 0f, 0f), false);
            AddUvGeneratorProvider(pathX, new Vector2(0f, 0f), new Vector2(4f, 0f), 0);
            var uvX2 = AddUvProjectorProvider(pathX, 1);
            uvX2.referenceTranslation.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            var pathY = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            pillar.pathProviderY = pathY;
            pathY.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathY.CreateVertex(new Vector3(0f, 1f, 0f), false);
            AddUvGeneratorProvider(pathY, new Vector2(0f, 0f), new Vector2(0f, 1f), 0);
            return pillar;
        }

        public static MeshProvider CreateSpherePrimitive()
        {
            var gameObject = new GameObject("Sphere Provider");
            var matrix = gameObject.AddComponent<MatrixMeshProvider>();

            var pathX = matrix.AddChildComponent<RevolutionPathProvider>("RevolutionPathProvider_Path X");
            pathX.axis = RevolutionPathieFactory.Axis.Y;
            pathX.min = 0f;
            pathX.max = 360f;
            pathX.radius = 0f;
            pathX.steps = 17;
            matrix.pathProviderX = pathX;
            AddUvGeneratorProvider(pathX, new Vector2(0f, 0f), new Vector2(2f, 0f), 0, 0f);

            var pathY = matrix.AddChildComponent<RevolutionPathProvider>("RevolutionPathProvider_Path Y");
            pathY.axis = RevolutionPathieFactory.Axis.X;
            pathY.min = 0f;
            pathY.max = 180f;
            pathY.radius = 0.5f;
            pathY.steps = 9;
            matrix.pathProviderY = pathY;
            AddUvGeneratorProvider(pathY, new Vector2(0f, 1f), new Vector2(0f, 0f), 0);

            return matrix;
        }

        public static MeshProvider CreateCylinderPrimitive()
        {
            var gameObject = new GameObject("Cylinder Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;
            pillar.uvChannelBottom = 1;
            pillar.uvChannelTop = 1;
            pillar.operatorKind = MatrixMeshieFactory.OperatorKind.ApplyY;

            var pathX = pillar.AddChildComponent<RevolutionPathProvider>("RevolutionPathProvider_Path X");
            pathX.axis = RevolutionPathieFactory.Axis.Y;
            pathX.min = 360f;
            pathX.max = 0f;
            pathX.radius = 0.5f;
            pathX.steps = 9;
            pillar.pathProviderX = pathX;
            AddUvGeneratorProvider(pathX, new Vector2(0f, 0f), new Vector2(2f, 0f), 0);
            var uvX2 = AddUvProjectorProvider(pathX, 1);
            uvX2.referenceTranslation.transform.position = new Vector3(-0.5f, -0.5f, 0f);
            uvX2.referenceTranslation.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            var pathY = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            pathY.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathY.CreateVertex(new Vector3(0f, 1f, 0f), false);
            pillar.pathProviderY = pathY;
            AddUvGeneratorProvider(pathY, new Vector2(0f, 0f), new Vector2(0f, 1f), 0);

            return pillar;
        }

        public static MeshProvider CreateStairsPrimitive()
        {
            var gameObject = new GameObject("Stairs Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;
            pillar.uvChannelBottom = 1;
            pillar.uvChannelTop = 1;

            var pathX = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path X");
            pillar.pathProviderX = pathX;

            pathX.CreateVertex(new Vector3(0f, 0f, 0f), false);

            var child = pathX.CreateChildPathProvider();
            child.CreateVertex(new Vector3(0f, 0f, 0f), false);
            child.CreateVertex(new Vector3(0f, 1f, 0f), true);
            child.CreateVertex(new Vector3(1f, 1f, 0f), false);
            var repeat = child.gameObject.AddComponent<PathRepeatProvider>();
            repeat.count = 8;
            repeat.offsetByPath = true;
            var normalize = child.gameObject.AddComponent<VertwiseNormalizeProvider>();
            normalize.min = new Vector3(0f, 0f, 0f);
            normalize.max = new Vector3(1f, 1f, 1f);
            pathX.CreateVertex(new Vector3(1f, 1f, 0f), true);
            pathX.CreateVertex(new Vector3(1f, 0f, 0f), true);
            pathX.CreateVertex(new Vector3(0f, 0f, 0f), false);
            AddUvGeneratorProvider(pathX, new Vector2(0f, 0f), new Vector2(2f, 0f), 0);
            AddUvProjectorProvider(pathX, 1);

            var pathY = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            pillar.pathProviderY = pathY;
            pathY.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathY.CreateVertex(new Vector3(0f, 0f, 1f), false);
            AddUvGeneratorProvider(pathY, new Vector2(0f, 0f), new Vector2(0f, 1f), 0);

            return pillar;
        }
    }
}