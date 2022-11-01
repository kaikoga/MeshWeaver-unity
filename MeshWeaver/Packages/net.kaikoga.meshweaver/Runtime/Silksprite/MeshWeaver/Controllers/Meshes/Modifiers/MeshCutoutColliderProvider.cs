using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshCutoutColliderProvider : MeshModifierProviderBase
    {
        [SerializeField] [HideInInspector] bool hasLegacyPredicate = true;
        [FormerlySerializedAs("predicate")]
        [SerializeField] [HideInInspector] Collider legacyPredicate;
        
        public List<Collider> predicates = new List<Collider>();

        public bool inside;
        [Range(0, 3)]
        public int numVertex = 1;

        protected override IMeshieModifier CreateModifier()
        {
            if (hasLegacyPredicate)
            {
                predicates = legacyPredicate ? new List<Collider> { legacyPredicate } : new List<Collider>();
                hasLegacyPredicate = false;
            }

            // NOTE: We don't create a MeshCutoutCollider Model here,
            // because Collider mono objects could easily get stale or missing and break things 
            var predicatesArray = predicates.Where(p => p != null).ToArray();

            var localToWorldMatrix = transform.localToWorldMatrix;
            return new MeshCutout(v =>
            {
                var local = localToWorldMatrix.MultiplyPoint(v);
                return predicatesArray.Any(predicate => predicate && predicate.ClosestPoint(local) == local);
            }, inside, numVertex);
        }

        // FIXME: the whole Modifier caching system should be reworked 
        protected override void RefreshUnityReferences()
        {
            foreach (var predicate in predicates) AddUnityReference(predicate);
        }
    }
}