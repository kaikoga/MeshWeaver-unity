using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Meshes;
using Silksprite.MeshBuilder.Models.Meshes.Core;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using Silksprite.MeshBuilder.Models.Paths;
using Silksprite.MeshBuilder.Models.Paths.Core;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;

namespace Silksprite.MeshBuilder.Models.Extensions
{
    public static class FactoryExtension
    {
        public static IMeshieFactory WithModifiers(this IMeshieFactory factory, params IMeshieModifier[] modifiers)
        {
            return modifiers.Aggregate(ModifiedMeshieFactory.Builder(factory), (builder, modifier) => builder.Concat(modifier)).ToFactory();
        }

        public static IMeshieFactory WithModifiers(this IMeshieFactory factory, IEnumerable<IMeshieModifier> modifiers)
        {
            return modifiers.Aggregate(ModifiedMeshieFactory.Builder(factory), (builder, modifier) => builder.Concat(modifier)).ToFactory();
        }

        public static IPathieFactory WithModifiers(this IPathieFactory factory, params IPathieModifier[] modifiers)
        {
            return modifiers.Aggregate(ModifiedPathieFactory.Builder(factory), (builder, modifier) => builder.Concat(modifier)).ToFactory();
        }

        public static IPathieFactory WithModifiers(this IPathieFactory factory, IEnumerable<IPathieModifier> modifiers)
        {
            return modifiers.Aggregate(ModifiedPathieFactory.Builder(factory), (builder, modifier) => builder.Concat(modifier)).ToFactory();
        }
    }
}