using System;
using Silksprite.MeshBuilder.Controllers.Meshes;
using Silksprite.MeshBuilder.Controllers.Utils;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.DataObjects;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    [CustomEditor(typeof(MeshProvider), true, isFallback = true)]
    public class MeshProviderEditor : Editor
    {
        bool _isExpanded;

        public override void OnInspectorGUI()
        {
            var meshProvider = (MeshProvider)target;
            base.OnInspectorGUI();
            MeshBuilderGUI.DumpFoldout("Mesh data", ref _isExpanded, () => meshProvider.LastMeshie);

            MeshModifierProviderMenus.Menu.ModifierPopup(meshProvider);

            if (GUILayout.Button("Bake"))
            {
                var transform = meshProvider.transform;
                var baked = transform.parent.AddChildComponent<BakedMeshProvider>();
                baked.meshData = MeshieData.FromMeshie(meshProvider.ToMeshie());
                var bakedTransform = baked.transform;
                bakedTransform.localPosition = transform.localPosition;
                bakedTransform.localRotation = transform.localRotation;
                bakedTransform.localScale = transform.localScale;
                baked.RefreshElementsOfParent();
            }
        }
    }
}