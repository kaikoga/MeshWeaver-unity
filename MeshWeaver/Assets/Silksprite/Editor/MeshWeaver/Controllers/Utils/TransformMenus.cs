using Silksprite.MeshWeaver.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Utils
{
    public static class TransformMenus
    {
        public static readonly ChildComponentPopupMenu<Transform> Menu = new ChildComponentPopupMenu<Transform>
        (
            typeof(Transform)
        );
    }
}