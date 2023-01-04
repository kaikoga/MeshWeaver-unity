using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    [CustomEditor(typeof(ProviderBase), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class SubProviderBaseEditor : ProviderBaseEditor
    {
    }
}