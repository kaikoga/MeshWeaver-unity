using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;

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

        public int materialIndexBody;
        public int materialIndexBottom;
        public int materialIndexTop;

        public PathProvider pathProviderX;
        public PathProvider pathProviderY;

        public MatrixMeshieFactory.OperatorKind operatorKind = MatrixMeshieFactory.OperatorKind.ApplyX;
        public MatrixMeshieFactory.CellPatternKind defaultCellPatternKind = MatrixMeshieFactory.CellPatternKind.Default;
        public List<MatrixMeshieFactory.CellPatternOverride> cellPatternOverrides;

        public PillarMeshieFactory.LongitudeAxisKind longitudeAxisKind = PillarMeshieFactory.LongitudeAxisKind.Y;

        public IPathieFactory LastPathieX { get; private set; }
        public IPathieFactory LastPathieY { get; private set; }

        protected override IMeshieFactory CreateFactory()
        {
            LastPathieX = CollectPathie(pathProviderX);
            LastPathieY = CollectPathie(pathProviderY);
            return new PillarMeshieFactory(LastPathieX,
                LastPathieY,
                operatorKind,
                defaultCellPatternKind,
                cellPatternOverrides,
                longitudeAxisKind,
                fillBody,
                fillBottom,
                fillTop,
                uvChannelBody,
                uvChannelBottom,
                uvChannelTop,
                materialIndexBody,
                materialIndexBottom,
                materialIndexTop);
        }
    }
}