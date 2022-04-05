using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Meshes.Core;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using Silksprite.MeshWeaver.Models.Paths;
using Silksprite.MeshWeaver.Models.Paths.Core;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Models.Extensions
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