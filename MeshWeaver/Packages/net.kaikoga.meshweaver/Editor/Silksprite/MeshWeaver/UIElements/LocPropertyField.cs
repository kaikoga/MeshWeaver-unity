using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class LocPropertyField : PropertyField  
    {
        public LocPropertyField(SerializedProperty property, LocalizedContent locLabel) : base(property, Localization.Lang == "en" ? property.displayName : locLabel.Tr)
        {
            style.flexGrow = new StyleFloat(1f);
        }
    }
}