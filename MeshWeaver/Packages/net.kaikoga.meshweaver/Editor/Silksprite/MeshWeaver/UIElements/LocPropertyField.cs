using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEditor.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class LocPropertyField : PropertyField  
    {
        public LocPropertyField(SerializedProperty property, LocalizedContent locLabel) : base(property, MeshWeaverSettings.Current.Lang == "en" ? property.displayName : locLabel.Tr)
        {
        }
    }
}