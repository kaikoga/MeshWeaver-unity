using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [ExecuteAlways]
    public class MeshBehaviour : MeshBehaviourBase
    {
        readonly MeshieCollector _childrenCollector = new MeshieCollector();

        protected override Meshie OnPopulateMesh(LodMaskLayer lod)
        {
            _childrenCollector.Sync(this.GetComponentsInDirectChildren<MeshProvider>());
            return _childrenCollector.Value.Build(lod);
        }
    }
}