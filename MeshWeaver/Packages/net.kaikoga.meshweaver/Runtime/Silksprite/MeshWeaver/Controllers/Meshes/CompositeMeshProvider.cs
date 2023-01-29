using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class CompositeMeshProvider : MeshProvider
    {
        readonly MeshieCollector _childrenCollector = new MeshieCollector();

        protected override bool Sync() => _childrenCollector.Sync(this.GetComponentsInDirectChildren<MeshProvider>());

        protected override IMeshieFactory CreateFactory() => _childrenCollector.Value;
    }
}