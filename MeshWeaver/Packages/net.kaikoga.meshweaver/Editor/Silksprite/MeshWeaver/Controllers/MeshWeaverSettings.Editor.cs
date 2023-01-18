using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers
{
    [CustomEditor(typeof(MeshWeaverSettings))]
    public class MeshWeaverSettingsEditor : MeshWeaverEditorBase
    {
        protected override bool IsMainComponentEditor => true;

        protected sealed override void PopulateInspectorGUI(GUIContainer container)
        {
            container.Add(new LocHelpBox(Loc("This asset contains global settings of MeshWeaver. This file can be moved anywhere inside Assets directory."), MessageType.Info));
        }

        [SettingsProvider]
        public static SettingsProvider SettingsProvider()
        {
            var guiAction = new MainProviderHeader();

            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Project Settings window.
            var provider = new SettingsProvider("Project/MeshWeaver ", SettingsScope.Project)
            {
                // By default the last token of the path is used as display name if no label is provided.
                label = "MeshWeaver",
                // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
                guiHandler = searchContext => guiAction.OnGUI(),

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<string>(new[] { "MeshWeaver" })
            };

            return provider;
        }
    }
}