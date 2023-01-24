using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class ExtractPathProvider : PathProvider
    {
        public MeshProvider meshProvider;
        public string pathName;

        protected override IPathieFactory CreateFactory(IMeshContext context)
        {
            return new ExtractPathieFactory(meshProvider.ToFactory(context), pathName);
        }
    }
}