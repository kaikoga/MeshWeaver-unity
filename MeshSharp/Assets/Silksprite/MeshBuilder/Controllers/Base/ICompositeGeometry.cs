using System.Collections.Generic;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public interface ICompositeGeometry<T>
    {
        List<T> PrimaryElements { set; }
    }
}