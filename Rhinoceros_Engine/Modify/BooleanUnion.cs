using BH.oM.Geometry;
using RG = Rhino.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace BH.Engine.Rhinoceros
{
    public static partial class Modify
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static IEnumerable<ICurve> IApplyBooleanUnion(this IEnumerable<ICurve> curves)   // ALWAYS USE THIS METHOD IN ITS EXTENSION FORM  //TODO: Why???? (AD)
        {
            // BHoM method to implement is at https://hal.inria.fr/inria-00517670/document
            RG.Curve[] rCurves = RG.Curve.CreateBooleanUnion(curves.Select(crv => crv.IToRhino()));
            return rCurves.Select(x=>x.ToBHoM()).ToList();
        }


        /***************************************************/
    }
}
