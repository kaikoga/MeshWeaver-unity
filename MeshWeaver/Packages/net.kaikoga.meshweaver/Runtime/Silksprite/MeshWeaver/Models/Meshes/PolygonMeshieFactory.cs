using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    // Ref: https://edom18.hateblo.jp/entry/2018/03/25/100234
    public class PolygonMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathie;
        readonly Material _material;

        public PolygonMeshieFactory(IPathieFactory pathie, Material material)
        {
            _pathie = pathie;
            _material = material;
        }

        const int InitialSize = 4;
        const int InitialFail = 0x100000;

        int _workSize = InitialSize;
        int[] _workPrev = new int[InitialSize];
        int[] _workNext = new int[InitialSize];
        float[] _workSqrDistances = new float[InitialSize];

        public Meshie Build(LodMaskLayer lod)
        {
            var pathie = _pathie.Build(lod);

            var vertices = pathie.DedupLoop((a, b) => a.VertexEquals(b)).ToArray();;
            var count = vertices.Length;
            if (count < 3) return Meshie.Empty();

            var fail = Math.Min(InitialFail, count * count * count);

            var first = 0;
            if (_workSize < count)
            {
                while (_workSize < count) _workSize <<= 2;
                Array.Resize(ref _workPrev, _workSize);
                Array.Resize(ref _workNext, _workSize);
                Array.Resize(ref _workSqrDistances, _workSize);
            }

            var prev = _workPrev;
            var next = _workNext;
            var sqrDistances = _workSqrDistances;

            for (var i = 0; i < count; i++)
            {
                var ii = (i + 1) % count; 
                prev[ii] = i;
                next[i] = ii;
                sqrDistances[i] = (vertices[i].Vertex /* - vertices[0].Vertex*/ ).sqrMagnitude;
            }

            int FindFarthest()
            {
                var result = first;
                var sqrDistance = sqrDistances[first];
                for (var i = next[first]; i != first; i = next[i])
                {
                    if (--fail < 0) throw new Exception();
                    var sqrD = sqrDistances[i];
                    if (sqrDistance > sqrD) continue;
                    sqrDistance = sqrD;
                    result = i;
                }
                return result;
            }

            bool ContainsOtherPoint(int index)
            {
                var prevIndex = prev[index];
                var nextIndex = next[index];
                for (var i = next[nextIndex]; i != prevIndex; i = next[i])
                {
                    if (--fail < 0) throw new Exception();
                    var local = vertices[i].Vertex;
                    var localI = vertices[index].Vertex - local;
                    var localNext = vertices[nextIndex].Vertex - local;
                    var localPrev = vertices[prevIndex].Vertex - local;
                    var normalPrev = Vector3.Cross(localPrev, localI); //.normalized;
                    var normalNext = Vector3.Cross(localI, localNext); //.normalized;
                    var normalBeam = Vector3.Cross(localNext, localPrev); //.normalized;
                    if (normalPrev.sqrMagnitude > 0f &&
                        normalNext.sqrMagnitude > 0f &&
                        normalBeam.sqrMagnitude > 0f &&
                        Vector3.Dot(normalPrev, normalNext) >= 0f &&
                        Vector3.Dot(normalNext, normalBeam) >= 0f &&
                        Vector3.Dot(normalBeam, normalPrev) >= 0f)
                    {
                        return true;
                    }
                }
                return false;
            }

            void RemovePoint(int index)
            {
                if (first == index) first = next[index];
                next[prev[index]] = next[index];
                prev[next[index]] = prev[index];
                prev[index] = -1;
                next[index] = -1;
            }

            var gons = new List<Gon>();

            for (var limit = 2; limit < count; limit++)
            {
                if (next[first] == prev[first]) break;
                var i = FindFarthest();
                while (ContainsOtherPoint(i)) i = next[i];
                gons.Add(new Gon(new[] { prev[i], i, next[i] }, _material));
                RemovePoint(i);
            }

            return Meshie.Builder(vertices, gons, true).ToMeshie();
        }

        public Pathie Extract(string pathName, LodMaskLayer lod) => _pathie.Build(lod);
    }
}