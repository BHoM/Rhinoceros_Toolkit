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

        /// <summary>
        /// ALWAYS USE THIS METHOD IN ITS EXTENSION FORM
        /// </summary>
        public static List<ICurve> GetBooleanUnion(this List<ICurve> curves)
        {
            // BHoM method to implement is at https://hal.inria.fr/inria-00517670/document
            RG.Curve[] rCurves = RG.Curve.CreateBooleanUnion(curves.Select(crv => crv.IToRhino()));
            return rCurves.Select(x=>x.ToBHoM()).ToList();
        }
    }
}
