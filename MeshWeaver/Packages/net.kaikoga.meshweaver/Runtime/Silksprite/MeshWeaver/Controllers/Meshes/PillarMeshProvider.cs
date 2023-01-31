using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class PillarMeshProvider : MeshProvider
    {
        public bool fillBody = true;
        public bool fillBottom;
        public bool fillTop;

        public int uvChannelBody;
        public int uvChannelBottom;
        public int uvChannelTop;

        public Material materialBody;
        public Material materialBottom;
        public Material materialTop;

        public PathProvider pathProviderX;
        readonly PathieCollector _pathProviderXCollector = new PathieCollector();

        public PathProvider pathProviderY;
        readonly PathieCollector _pathProviderYCollector = new PathieCollector();

        public MatrixMeshieFactory.OperatorKind operatorKind = MatrixMeshieFactory.OperatorKind.ApplyX;
        public MatrixMeshieFactory.CellPatternKind defaultCellPatternKind = MatrixMeshieFactory.CellPatternKind.Default;
        public List<MatrixMeshProvider.CellOverrideData> cellPatternOverrides;

        public PillarMeshieFactory.LongitudeAxisKind longitudeAxisKind = PillarMeshieFactory.LongitudeAxisKind.Y;
        public bool reverseLids;

        public IPathieFactory LastPathieX { get; private set; }
        public IPathieFactory LastPathieY { get; private set; }

        protected override int SyncReferences()
        {
            return _pathProviderXCollector.Sync(pathProviderX) |
                   _pathProviderYCollector.Sync(pathProviderY);
        }

        protected override IMeshieFactory CreateFactory()
        {
            LastPathieX = _pathProviderXCollector.SingleValue();
            LastPathieY = _pathProviderYCollector.SingleValue();
            return new PillarMeshieFactory(LastPathieX,
                LastPathieY,
                operatorKind,
                defaultCellPatternKind,
                MatrixMeshProvider.ResolveCellPatternOverrides(cellPatternOverrides),
                longitudeAxisKind,
                reverseLids,
                fillBody,
                fillBottom,
                fillTop,
                uvChannelBody,
                uvChannelBottom,
                uvChannelTop,
                materialBody,
                materialBottom,
                materialTop);
        }
    }
}