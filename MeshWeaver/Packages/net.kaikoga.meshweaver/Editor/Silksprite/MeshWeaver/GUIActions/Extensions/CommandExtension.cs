using Silksprite.MeshWeaver.Controllers.Commands;
using UnityEngine;

namespace Silksprite.MeshWeaver.GUIActions.Extensions
{
    public static class CommandExtension
    {
        public static LocMenuItem ToLocMenuItem<T>(this ICommand<T> command, T target)
            where T : MonoBehaviour
        {
            return new LocMenuItem(command.Name, () => command.Invoke(target));
        }
    }
}