using Silksprite.MeshWeaver.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Commands
{
    public interface ICommand<in T>
    where T : MonoBehaviour
    {
        LocalizedContent Name { get; }
        void Invoke(T target);
    }
}