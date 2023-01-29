using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Controllers.Modifiers;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Controllers.Paths.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Primitives
{
    public static class MeshWeaverPrimitives
    {
        public static MeshBehaviour CreateMeshBehaviour(bool hasRenderer)
        {
            var gameObject = new GameObject("Mesh Behaviour");
            var meshBehaviour = gameObject.AddComponent<MeshBehaviour>();
            if (hasRenderer) MeshBehaviourBaseEditor.SetupAsMeshRenderer(meshBehaviour);
            return meshBehaviour;
        }

        static UvGeneratorProvider AddUvGeneratorProvider(PathProvider pathProvider, Rect uvArea, int uvChannel, float topologicalWeight = 1f)
        {
            var uvGenerator = pathProvider.gameObject.AddComponent<UvGeneratorProvider>();
            uvGenerator.uvArea = uvArea;
            uvGenerator.uvChannel = uvChannel;
            uvGenerator.topologicalWeight = topologicalWeight;
            return uvGenerator;
        }

        static UvProjectorProvider AddUvProjectorProvider(PathProvider pathProvider, int uvChannel)
        {
            var uvProjector = pathProvider.gameObject.AddComponent<UvProjectorProvider>();
            uvProjector.projection = UvProjector.ProjectionKind.Normalized;
            uvProjector.uvChannel = uvChannel;
            return uvProjector;
        }

        public static MeshProvider CreateQuadXYPrimitive(Material material, bool hasUv)
        {
            var gameObject = new GameObject("Quad Provider");
            var polygon = gameObject.AddComponent<PolygonMeshProvider>();
            polygon.material = material;

            var path = polygon.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path");
            polygon.pathProvider = path;

            if (hasUv)
            {
                path.CreateVertex(new Vector3(0f, 0f, 0f), false, new Vector2(0f, 0f));
                path.CreateVertex(new Vector3(0f, 1f, 0f), false, new Vector2(0f, 1f));
                path.CreateVertex(new Vector3(1f, 1f, 0f), false, new Vector2(1f, 1f));
                path.CreateVertex(new Vector3(1f, 0f, 0f), false, new Vector2(1f, 0f));
            }
            else
            {
                path.CreateVertex(new Vector3(0f, 0f, 0f), false);
                path.CreateVertex(new Vector3(0f, 1f, 0f), false);
                path.CreateVertex(new Vector3(1f, 1f, 0f), false);
                path.CreateVertex(new Vector3(1f, 0f, 0f), false);
            }
            path.isLoop = true;

            return polygon;
        }

        public static MeshProvider CreateQuadXZPrimitive(Material material, bool hasUv)
        {
            var gameObject = new GameObject("Quad Provider");
            var polygon = gameObject.AddComponent<PolygonMeshProvider>();
            polygon.material = material;

            var path = polygon.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path");
            polygon.pathProvider = path;

            if (hasUv)
            {
                path.CreateVertex(new Vector3(0f, 0f, 0f), false, new Vector2(0f, 0f));
                path.CreateVertex(new Vector3(0f, 0f, 1f), false, new Vector2(0f, 1f));
                path.CreateVertex(new Vector3(1f, 0f, 1f), false, new Vector2(1f, 1f));
                path.CreateVertex(new Vector3(1f, 0f, 0f), false, new Vector2(1f, 0f));
            }
            else
            {
                path.CreateVertex(new Vector3(0f, 0f, 0f), false);
                path.CreateVertex(new Vector3(0f, 0f, 1f), false);
                path.CreateVertex(new Vector3(1f, 0f, 1f), false);
                path.CreateVertex(new Vector3(1f, 0f, 0f), false);
            }
            path.isLoop = true;

            return polygon;
        }

        public static MeshProvider CreatePlanePrimitive(Material material, bool hasUv)
        {
            var gameObject = new GameObject("Plane Provider");
            var matrix = gameObject.AddComponent<MatrixMeshProvider>();
            matrix.material = material;

            var pathX = matrix.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path X");
            matrix.pathProviderX = pathX;

            pathX.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathX.CreateVertex(new Vector3(1f, 0f, 0f), false);
            if (hasUv)
            {
                AddUvGeneratorProvider(pathX, new Rect(0f, 0f, 1f, 0f), 0);
            }

            var subdivisionX = pathX.gameObject.AddComponent<PathSubdivisionProvider>();
            subdivisionX.maxCount = 4;
            subdivisionX.maxLength = 0.25f;

            var pathY = matrix.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            matrix.pathProviderY = pathY;
            pathY.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathY.CreateVertex(new Vector3(0f, 0f, 1f), false);
            if (hasUv)
            {
                AddUvGeneratorProvider(pathY, new Rect(0f, 0f, 0f, 1f), 0);
            }

            var subdivisionY = pathY.gameObject.AddComponent<PathSubdivisionProvider>();
            subdivisionY.maxCount = 4;
            subdivisionY.maxLength = 0.25f;
            return matrix;
        }

        public static MeshProvider CreateCubePrimitive(Material material, bool hasUv)
        {
            var gameObject = new GameObject("Cube Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;
            pillar.materialBody = material;
            pillar.materialBottom = material;
            pillar.materialTop = material;
            if (hasUv)
            {
                pillar.uvChannelBottom = 1;
                pillar.uvChannelTop = 1;
            }

            var pathX = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path X");
            pillar.pathProviderX = pathX;
            pathX.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathX.CreateVertex(new Vector3(1f, 0f, 0f), true);
            pathX.CreateVertex(new Vector3(1f, 0f, 1f), true);
            pathX.CreateVertex(new Vector3(0f, 0f, 1f), true);
            pathX.isLoop = true;
            if (hasUv)
            {
                AddUvGeneratorProvider(pathX, new Rect(0f, 0f, 4f, 0f), 0);
                var uvX2 = AddUvProjectorProvider(pathX, 1);
                uvX2.axisX = UvProjector.ProjectionAxisKind.XPlus;
                uvX2.axisY = UvProjector.ProjectionAxisKind.ZPlus;
            }

            var pathY = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            pillar.pathProviderY = pathY;
            pathY.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathY.CreateVertex(new Vector3(0f, 1f, 0f), false);
            if (hasUv)
            {
                AddUvGeneratorProvider(pathY, new Rect(0f, 0f, 0f, 1f), 0);
            }

            return pillar;
        }

        public static MeshProvider CreateSpherePrimitive(Material material, bool hasUv)
        {
            var gameObject = new GameObject("Sphere Provider");
            var matrix = gameObject.AddComponent<MatrixMeshProvider>();
            matrix.material = material;

            var pathX = matrix.AddChildComponent<RevolutionPathProvider>("RevolutionPathProvider_Path X");
            pathX.axis = RevolutionPathieFactory.Axis.Y;
            pathX.min = 0f;
            pathX.max = 360f;
            pathX.vector = Vector3.zero;
            pathX.subdivision = 16;
            pathX.hasLegacyRadius = false;
            matrix.pathProviderX = pathX;
            if (hasUv)
            {
                AddUvGeneratorProvider(pathX, new Rect(0f, 0f, 2f, 0f), 0, 0f);
            }

            var pathY = matrix.AddChildComponent<RevolutionPathProvider>("RevolutionPathProvider_Path Y");
            pathY.axis = RevolutionPathieFactory.Axis.X;
            pathY.min = 0f;
            pathY.max = 180f;
            pathY.vector = Vector3.up * 0.5f;
            pathY.subdivision = 8;
            pathY.isLoop = false;
            pathY.hasLegacyRadius = false;
            matrix.pathProviderY = pathY;
            if (hasUv)
            {
                AddUvGeneratorProvider(pathY, new Rect(0f, 1f, 0f, 0f), 0);
            }

            return matrix;
        }

        public static MeshProvider CreateExtrudedCylinderPrimitive(Material material, bool hasUv)
        {
            var gameObject = new GameObject("Cylinder Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;
            pillar.materialBody = material;
            pillar.materialBottom = material;
            pillar.materialTop = material;
            if (hasUv)
            {
                pillar.uvChannelBottom = 1;
                pillar.uvChannelTop = 1;
            }

            pillar.operatorKind = MatrixMeshieFactory.OperatorKind.ApplyY;

            var pathX = pillar.AddChildComponent<RevolutionPathProvider>("RevolutionPathProvider_Path X");
            pathX.axis = RevolutionPathieFactory.Axis.Y;
            pathX.min = 360f;
            pathX.max = 0f;
            pathX.vector = Vector3.forward * 0.5f;
            pathX.subdivision = 16;
            pathX.hasLegacyRadius = false;
            pillar.pathProviderX = pathX;
            if (hasUv)
            {
                AddUvGeneratorProvider(pathX, new Rect(0f, 0f, 2f, 0f), 0);
                var uvX2 = AddUvProjectorProvider(pathX, 1);
                uvX2.axisX = UvProjector.ProjectionAxisKind.XPlus;
                uvX2.axisY = UvProjector.ProjectionAxisKind.ZPlus;
            }

            var pathY = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            pathY.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathY.CreateVertex(new Vector3(0f, 1f, 0f), false);
            pillar.pathProviderY = pathY;
            if (hasUv)
            {
                AddUvGeneratorProvider(pathY, new Rect(0f, 0f, 0f, 1f), 0);
            }

            return pillar;
        }

        public static MeshProvider CreateRotatedCylinderPrimitive(Material material, bool hasUv)
        {
            var gameObject = new GameObject("Cylinder Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;
            pillar.materialBody = material;
            pillar.materialBottom = material;
            pillar.materialTop = material;
            if (hasUv)
            {
                pillar.uvChannelBottom = 1;
                pillar.uvChannelTop = 1;
            }

            pillar.operatorKind = MatrixMeshieFactory.OperatorKind.ApplyX;

            var pathX = pillar.AddChildComponent<RevolutionPathProvider>("RevolutionPathProvider_Path X");
            pathX.axis = RevolutionPathieFactory.Axis.Y;
            pathX.min = 360f;
            pathX.max = 0f;
            pathX.vector = Vector3.zero;
            pathX.subdivision = 16;
            pathX.hasLegacyRadius = false;
            pillar.pathProviderX = pathX;
            if (hasUv)
            {
                AddUvGeneratorProvider(pathX, new Rect(0f, 0f, 2f, 0f), 0);
                var uvX2 = AddUvProjectorProvider(pathX, 1);
                uvX2.axisX = UvProjector.ProjectionAxisKind.XPlus;
                uvX2.axisY = UvProjector.ProjectionAxisKind.ZPlus;
            }

            var pathY = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            pathY.CreateVertex(new Vector3(0f, 0f, 0f), false).gameObject.SetActive(false);
            pathY.CreateVertex(new Vector3(0.5f, 0f, 0f), false);
            pathY.CreateVertex(new Vector3(0.5f, 1f, 0f), false);
            pathY.CreateVertex(new Vector3(0f, 1f, 0f), false).gameObject.SetActive(false);
            pillar.pathProviderY = pathY;
            if (hasUv)
            {
                AddUvGeneratorProvider(pathY, new Rect(0f, 0f, 0f, 1f), 0);
            }

            return pillar;
        }

        public static MeshProvider CreateStairsPrimitive(Material material, bool hasUv)
        {
            var gameObject = new GameObject("Stairs Provider");
            var pillar = gameObject.AddComponent<PillarMeshProvider>();
            pillar.fillBottom = true;
            pillar.fillTop = true;
            pillar.materialBody = material;
            pillar.materialBottom = material;
            pillar.materialTop = material;
            if (hasUv)
            {
                pillar.uvChannelBottom = 1;
                pillar.uvChannelTop = 1;
            }

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
            normalize.bounds = new Bounds(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1f, 1f, 1f));
            pathX.CreateVertex(new Vector3(1f, 1f, 0f), true);
            pathX.CreateVertex(new Vector3(1f, 0f, 0f), true);
            pathX.CreateVertex(new Vector3(0f, 0f, 0f), false);
            if (hasUv)
            {
                AddUvGeneratorProvider(pathX, new Rect(0f, 0f, 2f, 0f), 0);
                AddUvProjectorProvider(pathX, 1);
            }

            var pathY = pillar.AddChildComponent<CompositePathProvider>("CompositePathProvider_Path Y");
            pillar.pathProviderY = pathY;
            pathY.CreateVertex(new Vector3(0f, 0f, 0f), false);
            pathY.CreateVertex(new Vector3(0f, 0f, 1f), false);
            if (hasUv)
            {
                AddUvGeneratorProvider(pathY, new Rect(0f, 0f, 0f, 1f), 0);
            }

            return pillar;
        }
    }
}