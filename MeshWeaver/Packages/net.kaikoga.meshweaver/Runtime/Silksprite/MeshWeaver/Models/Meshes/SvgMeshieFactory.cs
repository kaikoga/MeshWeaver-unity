using System.IO;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;
#if MW_VECTORGRAPHICS
using System.Xml;
using Unity.VectorGraphics;
#endif

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class SvgMeshieFactory : IMeshieFactory
    {
#if MW_VECTORGRAPHICS
        readonly SVGParser.SceneInfo _sceneInfo;
        readonly VectorUtils.TessellationOptions _tessellationOptions;
#endif
        readonly Material _material;

        public SvgMeshieFactory(string svg, Material material)
        {
            _material = material;
#if MW_VECTORGRAPHICS
            try
            {
                using (var stream = new StringReader(svg))
                {
                    _sceneInfo = SVGParser.ImportSVG(stream, ViewportOptions.DontPreserve, 72f);
                }
            }
            catch (XmlException)
            {
                _sceneInfo = default;
            }
            
            _tessellationOptions = new VectorUtils.TessellationOptions
            {
                StepDistance = float.MaxValue,
                MaxCordDeviation = 0.02f,
                MaxTanAngleDeviation = Mathf.PI * 0.25f,
                SamplingStepSize = 0.1f
            };
#endif
        }

#if MW_VECTORGRAPHICS
        public Meshie Build(LodMaskLayer lod)
        {
            if (_sceneInfo.Scene == null) return Meshie.Empty();

            var geometry = VectorUtils.TessellateScene(_sceneInfo.Scene, _tessellationOptions);
            var builder = Meshie.Builder();
            foreach (var g in geometry)
            {
                var m = new Meshie(
                    g.Vertices.Select(v => new Vertie(v)),
                    g.Indices.EachTrio((a, b, c) => new Gon(new[] { (int)a, b, c }, _material)));
                builder.Concat(m, Matrix4x4.identity, 0);
            }

            return builder.ToMeshie();
        }

#else
        public Meshie Build(LodMaskLayer lod) => Meshie.Empty(); 
#endif
        public Pathie Extract(string pathName, LodMaskLayer lod) => Pathie.Empty();
    }
}