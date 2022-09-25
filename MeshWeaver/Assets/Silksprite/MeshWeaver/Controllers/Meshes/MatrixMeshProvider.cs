using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.CustomDrawers;
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
        public List<CellOverrideData> cellPatternOverrides;

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
                ResolveCellPatternOverrides(cellPatternOverrides, context),
                context.GetMaterialIndex(material));
        }

        public static MatrixMeshieFactory.CellOverride[] ResolveCellPatternOverrides(IReadOnlyCollection<CellOverrideData> data, IMeshContext context)
        {
            if (data == null || data.Count == 0)
            {
                return Array.Empty<MatrixMeshieFactory.CellOverride>();
            }
            return data.Select(cell => cell.ResolveMaterials(context)).ToArray();
        }

        [Serializable]
        public class CellOverrideData
        {
            public MatrixMeshieFactory.CellPatternKind cellPatternKind;
            [RectIntCustom]
            public RectInt cellRange = new RectInt(0, 0, 1, 1);

            public Material material;

            public MatrixMeshieFactory.CellOverride ResolveMaterials(IMeshContext context)
            {
                return new MatrixMeshieFactory.CellOverride
                {
                    cellPatternKind = cellPatternKind,
                    cellRange = cellRange,
                    materialIndex = material ? context.GetMaterialIndex(material) : -1,
                };
            }
        }
    }
}