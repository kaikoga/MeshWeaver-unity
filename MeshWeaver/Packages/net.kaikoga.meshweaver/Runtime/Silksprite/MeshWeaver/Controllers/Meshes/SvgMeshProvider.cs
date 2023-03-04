using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Meshes;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class SvgMeshProvider : MeshProvider
    {
        [TextArea(minLines: 4, maxLines: 10)]
        public string svg;
        public Sprite svgSprite;

        public Material material;

        protected override IMeshieFactory CreateFactory()
        {
            return new SvgMeshieFactory(svg, material);
        }
    }
}