using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class StitchMeshProvider : MeshProvider
    {
        public PathProvider pathProviderA;
        readonly PathieCollector _pathProviderACollector = new PathieCollector();

        public PathProvider pathProviderB;
        readonly PathieCollector _pathProviderBCollector = new PathieCollector();

        public Material material;

        public IPathieFactory LastPathieA { get; private set; }
        public IPathieFactory LastPathieB { get; private set; }

        protected override int SyncReferences()
        {
            return _pathProviderACollector.Sync(pathProviderA) |
                   _pathProviderBCollector.Sync(pathProviderB);
        }

        protected override IMeshieFactory CreateFactory()
        {
            LastPathieA = _pathProviderACollector.SingleValue();
            LastPathieB = _pathProviderBCollector.SingleValue();
            return new StitchMeshieFactory(LastPathieA, LastPathieB, material);
        }
    }
}