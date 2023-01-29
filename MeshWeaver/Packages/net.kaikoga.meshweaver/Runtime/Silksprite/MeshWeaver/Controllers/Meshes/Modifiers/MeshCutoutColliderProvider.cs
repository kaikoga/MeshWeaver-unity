using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Core;
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
        readonly UnityCollector<Collider> _predicatesCollector = new UnityCollector<Collider>();

        public bool inside;
        [Range(0, 3)]
        public int numVertex = 1;

        protected override void Sync()
        {
            if (hasLegacyPredicate)
            {
                predicates = legacyPredicate ? new List<Collider> { legacyPredicate } : new List<Collider>();
                hasLegacyPredicate = false;
            }

            _predicatesCollector.Sync(predicates);
        }

        protected override IMeshieModifier CreateModifier()
        {
            var predicatesArray = _predicatesCollector.Value;
            var localToWorldMatrix = transform.localToWorldMatrix;

            return new MeshCutout(v =>
            {
                var local = localToWorldMatrix.MultiplyPoint(v);
                return predicatesArray.Any(predicate => predicate && predicate.ClosestPoint(local) == local);
            }, inside, numVertex);
        }
    }
}