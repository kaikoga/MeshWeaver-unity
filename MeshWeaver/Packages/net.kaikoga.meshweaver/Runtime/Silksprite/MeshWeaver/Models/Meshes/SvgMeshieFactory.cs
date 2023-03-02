using System.IO;
using System.Linq;
using System.Xml;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;
#if MW_VECTORGRAPHICS
using Unity.VectorGraphics;
#endif

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class SvgMeshieFactory : IMeshieFactory
    {
        readonly string _svg;
        readonly Material _material;
#if MW_VECTORGRAPHICS
        readonly VectorUtils.TessellationOptions _tesselationOptions;
#endif

        public SvgMeshieFactory(string svg, Material material)
        {
            _svg = svg;
            _material = material;

#if MW_VECTORGRAPHICS
            _tesselationOptions = new VectorUtils.TessellationOptions
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
            try
            {
                SVGParser.SceneInfo sceneInfo;
                using (var stream = new StringReader(_svg))
                {
                    sceneInfo = SVGParser.ImportSVG(stream, ViewportOptions.DontPreserve, 72f);
                }

                var geometry = VectorUtils.TessellateScene(sceneInfo.Scene, _tesselationOptions);
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
            catch (XmlException)
            {
                return Meshie.Empty();
            }
        }

        public Pathie Extract(string pathName, LodMaskLayer lod)
        {
            return Pathie.Empty();
        }
#else
        public Meshie Build(LodMaskLayer lod) => Meshie.Empty(); 
        public Pathie Extract(string pathName, LodMaskLayer lod) => Pathie.Empty();
#endif
    }
}