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
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static List<ICurve> IGetBooleanIntersection(this ICurve curveA, ICurve curveB)
        {
            RG.Curve[] rCurves = RG.Curve.CreateBooleanIntersection(curveA.IToRhino(), curveB.IToRhino());
            return rCurves.Select(x => x.ToBHoM()).ToList();
        }

        /***************************************************/

        public static List<Polyline> GetBooleanIntersection(this Polyline curveA, Polyline curveB)
        {
            RG.Curve[] rCurves = RG.Curve.CreateBooleanIntersection(curveA.ToRhino(), curveB.ToRhino());
            return rCurves.Select(x => x.ToBHoM() as Polyline).ToList();
        }
    }
}
