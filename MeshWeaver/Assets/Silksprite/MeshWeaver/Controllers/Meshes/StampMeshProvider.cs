using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class StampMeshProvider : MeshProvider
    {
        public MeshProvider meshProvider;
        public PathProvider pathProvider;

        public IMeshieFactory LastMeshie { get; private set; }
        public IPathieFactory LastPathie { get; private set; }

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            LastMeshie = CollectMeshie(context, meshProvider);
            LastPathie = CollectPathie(pathProvider);
            return new StampMeshieFactory(LastMeshie, LastPathie);
        }
    }
}