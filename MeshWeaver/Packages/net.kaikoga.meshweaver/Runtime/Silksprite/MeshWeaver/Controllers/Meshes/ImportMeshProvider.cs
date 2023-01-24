using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.Meshes;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class ImportMeshProvider : MeshProvider
    {
        public Mesh mesh;
        public Material[] materials;

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            foreach (var material in materials) context.GetMaterialIndex(material); 

            return new ImportMeshieFactory(mesh, materials.ToArray());
        }
    }
}