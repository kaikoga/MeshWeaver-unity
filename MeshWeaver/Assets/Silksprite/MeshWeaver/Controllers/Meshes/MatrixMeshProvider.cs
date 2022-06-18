using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class MatrixMeshProvider : MeshProvider
    {
        protected override bool RefreshAlways => true;

        public PathProvider pathProviderX;
        public PathProvider pathProviderY;
        public MatrixMeshieFactory.OperatorKind operatorKind = MatrixMeshieFactory.OperatorKind.ApplyX;
        public MatrixMeshieFactory.CellPatternKind defaultCellPatternKind = MatrixMeshieFactory.CellPatternKind.Default;
        public List<MatrixMeshieFactory.CellPatternOverride> cellPatternOverrides;

        public Material material;

        public IPathieFactory LastPathieX { get; private set; }
        public IPathieFactory LastPathieY { get; private set; }

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            LastPathieX = pathProviderX.CollectPathie();
            LastPathieY = pathProviderY.CollectPathie();
            return new MatrixMeshieFactory(LastPathieX,
                LastPathieY,
                operatorKind,
                defaultCellPatternKind,
                cellPatternOverrides ?? new List<MatrixMeshieFactory.CellPatternOverride>(),
                context.GetMaterialIndex(material));
        }
    }
}