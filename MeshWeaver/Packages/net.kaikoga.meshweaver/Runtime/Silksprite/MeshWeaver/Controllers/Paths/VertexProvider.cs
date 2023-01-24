using Silksprite.MeshWeaver.Models.DataObjects.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class VertexProvider : PathProvider
    {
        public bool crease;
        public Vector2MuxData[] uvs;

        protected override IPathieFactory CreateFactory(IMeshContext context)
        {
            return new VertexFactory(crease, uvs.ToMux());
        }
    }
}