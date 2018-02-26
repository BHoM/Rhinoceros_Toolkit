using BH.oM.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace BH.Engine.Rhinoceros
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        public static List<Polyline> ExternalEdges(this Mesh mesh)      // ALWAYS USE THIS METHOD IN ITS EXTENSION FORM   //TODO: Why???? (AD)
        {
            return mesh.ToRhino().GetNakedEdges().Select(crv => crv.ToBHoM()).ToList();
        }


        /***************************************************/
    }
}
