using Silksprite.MeshWeaver.Models;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public interface IModifierProvider
    {
        int Revision { get; }
        LodMask LodMask { get; set; }

        // ReSharper disable once InconsistentNaming
        bool enabled { get; } // This is for MonoBehaviour
    }
}