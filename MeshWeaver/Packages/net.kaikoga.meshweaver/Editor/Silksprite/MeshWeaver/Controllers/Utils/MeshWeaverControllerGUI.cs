using Silksprite.MeshWeaver.Models;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Utils
{
    public static class MeshWeaverControllerGUI
    {
        public static void LodSelectorGUI(Object target)
        {
            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                MeshWeaverSettings.Current.currentLodMaskLayer = (LodMaskLayer)EditorGUILayout.EnumPopup("Current LOD (Global)", MeshWeaverSettings.Current.currentLodMaskLayer);
                if (changedScope.changed)
                {
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }
}