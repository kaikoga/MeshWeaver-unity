using System;
using Silksprite.MeshWeaver.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class LocButton : GUIAction
    {
        readonly LocalizedContent _loc;
        readonly Action _onClick;

        public LocButton(LocalizedContent loc, Action onClick)
        {
            _loc = loc;
            _onClick = onClick;
        }
        
        public override void OnGUI()
        {
            if (GUILayout.Button(_loc.Tr)) _onClick?.Invoke();
        }
    }
}