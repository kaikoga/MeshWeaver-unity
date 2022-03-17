using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public interface IModifierProvider
    {
        LodMask LodMask { get; set; }

        // ReSharper disable once InconsistentNaming
        bool enabled { get; } // This is for MonoBehaviour
    }
}