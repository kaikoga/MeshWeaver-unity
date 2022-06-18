using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshCutoutColliderProvider : MeshModifierProviderBase
    {
        public Collider predicate;
        public bool inside;
        [Range(0, 3)]
        public int numVertex = 1;

        protected override IMeshieModifier CreateModifier()
        {
            if (predicate == null) return new MeshCutout(_ => false, inside, numVertex);

            var localToWorldMatrix = transform.localToWorldMatrix;
            return new MeshCutout(v =>
            {
                var local = localToWorldMatrix.MultiplyPoint(v);
                return predicate.ClosestPoint(local) == local;
            }, inside, numVertex);
        }

        protected override void RefreshUnityReferences() => AddUnityReference(predicate);
    }
}