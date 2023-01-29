using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class CompositeMeshProvider : MeshProvider
    {
        readonly MeshieCollector _childrenCollector = new MeshieCollector();

        protected override IMeshieFactory CreateFactory()
        {
            return _childrenCollector.CollectMeshies(this.GetComponentsInDirectChildren<MeshProvider>());
        }
    }
}