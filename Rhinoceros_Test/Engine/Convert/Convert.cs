using System;
using System.Collections.Generic;
using System.Linq;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BH.Engine.Rhinoceros;

namespace BH.Test.Rhinoceros
{
    [TestClass]
    public partial class Convert
    {
        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        private Random m_random = new Random();

        private Dictionary<Type, Type> m_CurveTypesConvention = new Dictionary<Type, Type>
        {
            { typeof(RHG.Arc), typeof(BHG.Arc) },
            { typeof(RHG.ArcCurve), typeof(BHG.Arc) },
            { typeof(RHG.Circle), typeof(BHG.Circle) },
            { typeof(RHG.Line), typeof(BHG.Line) },
            { typeof(RHG.LineCurve), typeof(BHG.Line) },
            { typeof(RHG.NurbsCurve), typeof(BHG.NurbsCurve) },
            { typeof(RHG.PolyCurve), typeof(BHG.PolyCurve) },
            { typeof(RHG.Polyline), typeof(BHG.Polyline) },
            { typeof(RHG.PolylineCurve), typeof(BHG.Polyline) },
        };

        /***************************************************/
    }
}
