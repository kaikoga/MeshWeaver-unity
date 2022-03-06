using Silksprite.MeshBuilder.Controllers.Base;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class GeometryProviderExtensions
    {
        public static void RefreshElementsOfParent(this GeometryProvider self)
        {
            self.transform.parent.GetComponent<GeometryProvider>()?.RefreshElements();
        }

        public static void RefreshElements(this GeometryProvider self)
        {
            switch (self)
            {
                case ICompositeGeometry<MeshProvider> mesh:
                {
                    self.CollectDirectChildren<MeshProvider>(out var list);
                    mesh.PrimaryElements = list;
                    break;
                }
                case ICompositeGeometry<PathProvider> path:
                {
                    self.CollectDirectChildren<PathProvider>(out var list);
                    path.PrimaryElements = list;
                    break;
                }
            }
        }
    }
}