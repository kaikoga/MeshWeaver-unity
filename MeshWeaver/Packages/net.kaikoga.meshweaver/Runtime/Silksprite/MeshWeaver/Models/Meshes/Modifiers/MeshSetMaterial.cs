using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshSetMaterial : IMeshieModifier
    {
        readonly Material _material;

        public MeshSetMaterial(Material material)
        {
            _material = material;
        }

        public Meshie Modify(Meshie meshie)
        {
            return new Meshie(meshie.Vertices, meshie.Gons.Select(gon => gon.WithMaterial(_material)));
        }
    }
}