using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Utils;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Commands
{
    public class ExplodePath : ICommand<PathProvider>
    {
        public LocalizedContent Name => Loc("Explode Path");

        public void Invoke(PathProvider target)
        {
            var transform = target.transform;
            foreach (var lod in LodMaskLayers.Values)
            {
                var pathie = target.ToFactory().Build(lod);
                if (pathie.Vertices.Count == 0) continue;
                var lodPath = transform.parent.AddChildComponent<CompositePathProvider>(target.gameObject.name);
                lodPath.lodMask = lod.ToLodMask();
                foreach (var vertie in pathie.Vertices)
                {
                    var vertex = lodPath.AddChildComponent<VertexProvider>();
                    vertex.transform.position = vertie.Vertex;
                    vertex.uvs = Vector2MuxData.FromMux(vertie.Uvs);
                }
            }
        }
    }
}