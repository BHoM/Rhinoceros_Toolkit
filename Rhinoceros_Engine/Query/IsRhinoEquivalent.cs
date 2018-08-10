using BH.oM.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Engine.Rhinoceros
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        public static bool IsRhinoEquivalent(Type type)     
        {
            if (typeof(IGeometry).IsAssignableFrom(type))
                return (type != typeof(Extrusion)
                     && type != typeof(Pipe)
                     && type != typeof(Loft)
                     && type != typeof(PolySurface)
                     && type != typeof(CompositeGeometry)
                     && type != typeof(Quaternion));
            else
                return false;
        }


        /***************************************************/
    }
}
