using BH.Adapter.Rhinoceros;
using BH.oM.Geometry;
using RG = Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Engine.Rhinoceros
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        /// <summary>
        /// ALWAYS USE THIS METHOD IN ITS EXTENSION FORM
        /// </summary>
        public static List<Polyline> GetExternalEdges(this Mesh mesh)
        {
            return mesh.ToRhino().GetNakedEdges().Select(crv => crv.ToBHoM()).ToList();
        }
    }
}
