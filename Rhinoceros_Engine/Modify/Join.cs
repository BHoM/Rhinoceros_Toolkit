using RG = Rhino.Geometry;
using BH.oM.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace BH.Engine.Rhinoceros
{
    public static partial class Modify
    {
        /***************************************************/
        /**** Public Methods - Interfaces               ****/
        /***************************************************/

        public static IEnumerable<ICurve> IJoin(this IEnumerable<ICurve> curves)      // ALWAYS USE THIS METHOD IN ITS EXTENSION FORM   //TODO: Why???? (AD)
        {
            IEnumerable<RG.Curve> rCurves = curves.Select(x => x.IToRhino() as RG.Curve);
            return RG.Curve.JoinCurves(rCurves).Select(x => x.ToBHoM());
        }


        /***************************************************/
    }
}