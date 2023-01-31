using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Meshes;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class ImportMeshProvider : MeshProvider
    {
        public Mesh mesh;
        public Material[] materials;

        protected override IMeshieFactory CreateFactory()
        {
            return new ImportMeshieFactory(mesh, materials.ToArray());
        }
    }
}