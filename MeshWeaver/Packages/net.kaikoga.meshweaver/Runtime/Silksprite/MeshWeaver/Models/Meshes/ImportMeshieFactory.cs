using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class ImportMeshieFactory : IMeshieFactory
    {
        readonly Mesh _mesh;
        readonly Material[] _materials;

        public ImportMeshieFactory(Mesh mesh, Material[] materials)
        {
            _mesh = mesh;
            _materials = materials;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            if (_mesh == null) return Meshie.Empty();

            var builder = Meshie.Builder();
            var rawVertices = _mesh.vertices;
            var rawUvs = _mesh.uv;
            for (var i = 0; i < rawVertices.Length; i++)
            {
                builder.Vertices.Add(new Vertie(Matrix4x4.Translate(rawVertices[i]), Mux.Single(rawUvs[i])));
            }
            for (var subMeshIndex = 0; subMeshIndex < _mesh.subMeshCount; subMeshIndex++)
            {
                var material = _materials[subMeshIndex];
                var subMesh = _mesh.GetTriangles(subMeshIndex);
                builder.Gons.AddRange(subMesh.EachTrio((a, b, c) => new Gon(new []{a, b, c}, material)));
            }
            return builder.ToMeshie();
        }

        public Pathie Extract(string pathName, LodMaskLayer lod) => Pathie.Empty();
    }
}