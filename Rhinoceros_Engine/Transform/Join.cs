using BH.Adapter.Rhinoceros;
using RG = Rhino.Geometry;
using BH.oM.Geometry;
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
        /**** Public Methods - Interfaces               ****/
        /***************************************************/

        public static List<ICurve> IGetJoined(this IEnumerable<ICurve> curves)
        {
            IEnumerable<RG.Curve> rCurves = curves.Select(x => x.IToRhino() as RG.Curve);
            return RG.Curve.JoinCurves(rCurves).Select(x => x.ToBHoM()).ToList();
        }
    }
}