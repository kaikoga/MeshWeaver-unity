using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    // Shamelessly copied from https://edom18.hateblo.jp/entry/2018/03/25/100234
    public class PolygonMeshieFactory2 : IMeshieFactory
    {
        readonly Pathie _pathie;

        public PolygonMeshieFactory2(Pathie pathie)
        {
            _pathie = pathie;
        }

        public Meshie Build()
        {
            return BuildInternal();
        }

        readonly List<int> _triangles = new List<int>();
        readonly List<Vector3> _vertices = new List<Vector3>();
        readonly List<bool> _verticesBuffer = new List<bool>();

        Vector3 _prevDirection = Vector3.zero;

        bool _isIncluding = false;
        int _curIndex;
        int _nextIndex;
        int _prevIndex;

        Vector3 CurrentPoint => _vertices[_curIndex];
        Vector3 PreviousPoint => _vertices[_prevIndex];
        Vector3 NextPoint => _vertices[_nextIndex];

        /// <summary>
        /// Clear buffers.
        /// </summary>
        void Clear()
        {
            _vertices.Clear();
            _verticesBuffer.Clear();
            _triangles.Clear();
        }

        void Initialize(IReadOnlyCollection<Vector3> vertices)
        {
            Clear();

            // 設定された頂点を保持しておく
            _vertices.AddRange(vertices);

            // 全頂点のインデックスを保持、使用済みフラグをfalseで初期化
            _verticesBuffer.AddRange(vertices.Select(_ => false));
        }

        /// <summary>
        /// Create mesh by vertices.
        /// </summary>
        Meshie BuildInternal()
        {
            var vertices = _pathie.DedupLoop((a, b) => a.VertexEquals(b)).ToArray();
            Initialize(vertices.Select(v => v.Vertex).ToList());

            var i = 0;
            for (; i < 65536; i++)
            {
                if (_verticesBuffer.Count(buf => !buf) <= 3)
                {
                    break;
                }
                DetectTriangle();
            }
            // Debug.Log(i);

            var unusedIndices = _verticesBuffer.Select((b, index) => new KeyValuePair<int, bool>(index, b)).Where(kv => !kv.Value).Select(kv => kv.Key);
            _triangles.AddRange(unusedIndices);

            return Meshie.Builder(vertices, _triangles, true).ToMeshie();
        }

        /// <summary>
        /// Detect triangle from far point.
        /// </summary>
        void DetectTriangle()
        {
            if (!_isIncluding)
            {
                FindFarPoint();
            }

            var a = CurrentPoint;
            var b = NextPoint;
            var c = PreviousPoint;

            // var edge1 = b - a;
            // var edge2 = c - a;
            //
            // var angle = Vector3.Angle(edge1, edge2);
            // if (angle >= 180)
            // {
            //     Debug.LogError("Something was wrong.");
            //     return;
            // }

            if (_isIncluding)
            {
                if (Vector3.Dot(_prevDirection, GetCurrentDirection()) < 0f)
                {
                    MoveToNext();
                    return;
                }
            }

            if (IsIncludePoint())
            {
                // Debug.Log("Point is including.");

                // Store current triangle direction.
                if (!_isIncluding) _prevDirection = GetCurrentDirection();

                // try to find other point.
                _isIncluding = true;

                MoveToNext();

                return;
            }

            _isIncluding = false;

            _triangles.Add(_curIndex);
            _triangles.Add(_nextIndex);
            _triangles.Add(_prevIndex);

            _verticesBuffer[_curIndex] = true; 
        }

        /// <summary>
        /// Check to include point in the triangle.
        /// </summary>
        /// <returns></returns>
        bool IsIncludePoint()
        {
            for (var index = 0; index < _vertices.Count; index++)
            {
                if (_verticesBuffer[index])
                {
                    continue;
                }

                // skip if index in detected three points.
                if (index == _curIndex || index == _nextIndex || index == _prevIndex)
                {
                    continue;
                }

                if (CheckInPoint(_vertices[index]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get current triangle direction.
        /// </summary>
        /// <returns>Triangle direction normal.</returns>
        Vector3 GetCurrentDirection()
        {
            var edge1 = (NextPoint - CurrentPoint).normalized;
            var edge2 = (PreviousPoint - CurrentPoint).normalized;

            return Vector3.Cross(edge1, edge2);
        }

        /// <summary>
        /// Check including point.
        /// </summary>
        /// <param name="target">Target point.</param>
        /// <returns>return true if point is including.</returns>
        bool CheckInPoint(Vector3 target)
        {
            // Triangle points.
            Vector3[] tp =
            {
                CurrentPoint,
                NextPoint,
                PreviousPoint,
            };

            var prevNormal = default(Vector3);
            for (var i = 0; i < tp.Length; i++)
            {
                var edge1 = (target - tp[i]);
                var edge2 = (target - tp[(i + 1) % tp.Length]);

                var normal = Vector3.Cross(edge1, edge2).normalized;

                if (prevNormal == default)
                {
                    prevNormal = normal;
                    continue;
                }

                // If not same direction, the point out of a triangle.
                if (Vector3.Dot(prevNormal, normal) <= -0.01f)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Position reference move to next.
        /// </summary>
        void MoveToNext()
        {
            _curIndex = FindNextIndex(_curIndex);
            _nextIndex = FindNextIndex(_curIndex);
            _prevIndex = FindPrevIndex(_curIndex);
        }

        /// <summary>
        /// 原点から最も遠い点を探す
        /// </summary>
        void FindFarPoint()
        {
            var farIndex = -1;
            var maxDist = float.MinValue;

            for (var index = 0; index < _vertices.Count; index++)
            {
                if (_verticesBuffer[index])
                {
                    continue;
                }

                var dist = Vector3.Distance(Vector3.zero, _vertices[index]);
                if (dist > maxDist)
                {
                    maxDist = dist;
                    farIndex = index;
                }
            }

            _curIndex = farIndex;
            _nextIndex = FindNextIndex(_curIndex);
            _prevIndex = FindPrevIndex(_curIndex);
        }

        /// <summary>
        /// 指定インデックスから調べて次の有効頂点インデックスを探す
        /// </summary>
        int FindNextIndex(int start)
        {
            var i = start;
            while (true)
            {
                i = (i + 1) % _vertices.Count;
                if (!_verticesBuffer[i])
                {
                    return i;
                }
            }
        }

        /// <summary>
        /// 指定インデックスから調べて前の有効頂点インデックスを探す
        /// </summary>
        int FindPrevIndex(int start)
        {
            var i = start;
            while (true)
            {
                i = (i - 1) >= 0 ? i - 1 : _vertices.Count - 1;
                if (!_verticesBuffer[i])
                {
                    return i;
                }
            }
        }
    }
}
