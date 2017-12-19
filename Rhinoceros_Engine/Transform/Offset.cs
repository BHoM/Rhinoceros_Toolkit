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
    public static partial class Transform
    {
        /***************************************************/
        /**** Public Methods - Curve                    ****/
        /***************************************************/

        public static List<ICurve> Offset(this ICurve polyline, Plane plane, double distance, int corner)
        {
            return polyline.IToRhino().Offset(plane.ToRhino(), distance, Tolerance.Distance, (RG.CurveOffsetCornerStyle)corner).Select(crv => crv.ToBHoM()).ToList();
        }
    }
}
