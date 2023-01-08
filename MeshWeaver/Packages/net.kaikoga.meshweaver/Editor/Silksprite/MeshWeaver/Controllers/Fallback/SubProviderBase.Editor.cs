using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Fallback
{
    [CustomEditor(typeof(ProviderBase), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class SubProviderBaseEditor : ProviderEditorBase
    {
    }
}