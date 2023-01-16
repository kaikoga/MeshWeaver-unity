using System.Linq;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Commands
{
    public class UpgradeByWrapWithCompositeEntirely<T, TComposite> : ICommand<T>
        where T : MonoBehaviour
        where TComposite : MonoBehaviour
    {
        public LocalizedContent Name => Loc("Wrap with Composite/With Modifiers");

        public void Invoke(T target)
        {
            var child = new GameObject("Content");
            child.transform.SetParent(target.transform, false);
            child.transform.localPosition = Vector3.zero;
            child.transform.localRotation = Quaternion.identity;
            child.transform.localScale = Vector3.one;

            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(target), child.AddComponent<T>());
            
            var serializedObject = new SerializedObject(target);
            var property = serializedObject.GetIterator();
            while (property.Next(true))
            {
                switch (property.propertyType)
                {
                    case SerializedPropertyType.ObjectReference:
                    case SerializedPropertyType.ExposedReference:
                        break;
                    default:
                        continue;
                }

                if (!(property.objectReferenceValue is Component referencedComponent)) continue;
                if (referencedComponent.transform.parent != target.transform) continue;
                referencedComponent.transform.SetParent(child.transform, false);
            }
            
            var scriptAsset = Resources.FindObjectsOfTypeAll<MonoScript>().First(script => script.GetClass() == typeof(TComposite));
            serializedObject.FindProperty("m_Script").objectReferenceValue = scriptAsset;
            serializedObject.ApplyModifiedProperties();
        }
    }
}