using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshSetMaterialProvider : MeshModifierProviderBase
    {
        public Material material;

        protected override IMeshieModifier CreateModifier() => new MeshSetMaterial(material);
    }
}