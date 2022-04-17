using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Meshes;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class ImportMeshProvider : MeshProvider
    {
        public Mesh mesh;
        public int materialIndex;

        protected override IMeshieFactory CreateFactory()
        {
            return new ImportMeshieFactory(mesh, materialIndex);
        }
    }
}