using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.DataObjects;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class BakedPathProvider : PathProvider
    {
        public PathieData pathData;

        protected override Pathie GeneratePathie(LodMaskLayer lod)
        {
            return new BakedPathieFactory(pathData).Build();
        }
    }
}