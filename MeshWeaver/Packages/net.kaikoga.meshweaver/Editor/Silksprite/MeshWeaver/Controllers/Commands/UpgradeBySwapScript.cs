using System.Linq;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Commands
{
    public class UpgradeBySwapScript<T, TOut> : ICommand<T>
        where T : MonoBehaviour
        where TOut : MonoBehaviour
    {
        public LocalizedContent Name => _Loc($"Upgrade to/{typeof(TOut).Name}");

        public void Invoke(T target)
        {
            var serializedObject = new SerializedObject(target);
            var scriptAsset = Resources.FindObjectsOfTypeAll<MonoScript>().First(script => script.GetClass() == typeof(TOut));
            serializedObject.FindProperty("m_Script").objectReferenceValue = scriptAsset;
            serializedObject.ApplyModifiedProperties();
        }
    }
}