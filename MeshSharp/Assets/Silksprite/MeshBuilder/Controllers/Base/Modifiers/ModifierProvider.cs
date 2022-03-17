using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class ModifierProvider : MonoBehaviour
    {
        public LodMask lodMask = LodMask.All;
        
        public LodMask LodMask
        {
            get => lodMask;
            set => lodMask = value;
        }

        // ReSharper disable once Unity.RedundantEventFunction
        void Start()
        {
            // The sole reason for this empty method is for showing enabled checkbox
        }
    }
    public interface IModifierProvider
    {
        LodMask LodMask { get; set; }

        // ReSharper disable once InconsistentNaming
        bool enabled { get; } // This is for MonoBehaviour
    }
}