using Silksprite.MeshWeaver.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Utils
{
    public static class ColliderMenus
    {
        public static readonly ChildComponentPopupMenu<Collider> Menu = new ChildComponentPopupMenu<Collider>
        (
            typeof(BoxCollider),
            typeof(SphereCollider),
            typeof(CapsuleCollider)
        );
    }
}