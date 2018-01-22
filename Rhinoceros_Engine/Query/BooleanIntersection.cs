using BH.oM.Geometry;
using RG = Rhino.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace BH.Engine.Rhinoceros
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static List<ICurve> IApplyBooleanIntersection(this ICurve curveA, ICurve curveB)
        {           
            RG.Curve[] rCurves = RG.Curve.CreateBooleanIntersection(curveA.IToRhino(), curveB.IToRhino());
            return rCurves.Select(x => x.ToBHoM()).ToList();
        }
    }
}