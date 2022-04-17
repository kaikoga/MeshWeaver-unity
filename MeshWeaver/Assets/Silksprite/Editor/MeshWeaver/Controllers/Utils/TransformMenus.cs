using Silksprite.MeshWeaver.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Utils
{
    public static class TransformMenus
    {
        // FIXME: Transform is already added, can we skip adding it again? 
        public static readonly ChildComponentPopupMenu<Transform> Menu = new ChildComponentPopupMenu<Transform>
        (
            typeof(Transform)
        );
    }
}