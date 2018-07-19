using System;
using System.Collections.Generic;
using System.Linq;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BH.Engine.Rhinoceros;

namespace BH.Test.Rhinoceros
{
    public partial class Convert
    {
        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        [TestMethod]
        public void Point3dToBHoM()
        {
            RHG.Point3d rhinoPoint = Create.RandomPoint3d(m_random);
            BHG.Point bhPoint = Engine.Rhinoceros.Convert.ToBHoM(rhinoPoint);
            Assert.IsTrue(bhPoint.IsEqual(rhinoPoint));
        }

        /***************************************************/

        [TestMethod]
        public void Point3fToBHoM()
        {
            RHG.Point3f rhinoPoint = Create.RandomPoint3f(m_random);
            BHG.Point bhPoint = Engine.Rhinoceros.Convert.ToBHoM(rhinoPoint);
            Assert.IsTrue(bhPoint.IsEqual(rhinoPoint));
        }

        /***************************************************/

        [TestMethod]
        public void ControlPointToBHoM()
        {
            RHG.ControlPoint rhinoPoint = Create.RandomControlPoint(m_random);
            BHG.Point bhPoint = Engine.Rhinoceros.Convert.ToBHoM(rhinoPoint);
            Assert.IsTrue(bhPoint.IsEqual(rhinoPoint));
        }

        /***************************************************/

        [TestMethod]
        public void Vector3dToBHoM()
        {
            RHG.Vector3d rhVector = Create.RandomVector3d(m_random);
            BHG.Vector bhVector = Engine.Rhinoceros.Convert.ToBHoM(rhVector);
            Assert.IsTrue(bhVector.IsEqual(rhVector));
        }

        /***************************************************/

        [TestMethod]
        public void Vector3fToBHoM()
        {
            RHG.Vector3f rhVector = Create.RandomVector3f(m_random);
            BHG.Vector bhVector = Engine.Rhinoceros.Convert.ToBHoM(rhVector);
            Assert.IsTrue(bhVector.IsEqual(rhVector));
        }


        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        [TestMethod]
        public void ArcToBHoM()
        {
            RHG.Arc rhArc = Create.RandomArc(m_random);
            BHG.Arc bhArc = Engine.Rhinoceros.Convert.ToBHoM(rhArc);
            Assert.IsTrue(bhArc.IsEqual(rhArc));
        }

        /***************************************************/

        [TestMethod]
        public void ArcCurveToBHoM()
        {
            RHG.ArcCurve rhArc = Create.RandomArcCurve(m_random);
            BHG.Arc bhArc = Engine.Rhinoceros.Convert.ToBHoM(rhArc) as BHG.Arc;
            Assert.IsTrue(bhArc.IsEqual(rhArc));
        }

        /***************************************************/

        [TestMethod]
        public void CircleToBHoM()
        {
            RHG.Circle rhCircle = Create.RandomCircle(m_random);
            BHG.Circle bhCircle = Engine.Rhinoceros.Convert.ToBHoM(rhCircle);
            Assert.IsTrue(bhCircle.IsEqual(rhCircle));
        }

        /***************************************************/

        [TestMethod]
        public void LineToBHoM()
        {
            RHG.Line rhLine = Create.RandomLine(m_random);
            BHG.Line bhLine = Engine.Rhinoceros.Convert.ToBHoM(rhLine);
            Assert.IsTrue(bhLine.IsEqual(rhLine));
        }

        /***************************************************/

        [TestMethod]
        public void NurbCurveToBHoM()
        {
            RHG.NurbsCurve rhCurve = Create.RandomNurbsCurve(m_random);
            BHG.NurbCurve bhCurve = rhCurve.ToBHoM() as BHG.NurbCurve;
            Assert.IsTrue(bhCurve.IsEqual(rhCurve));

            // Checking null return
            rhCurve = null;
            bhCurve = rhCurve.ToBHoM() as BHG.NurbCurve;
            Assert.IsTrue(bhCurve.IsEqual(rhCurve));
        }

        /***************************************************/

        [TestMethod]
        public void PolyCurveToBHoM()
        {
            RHG.PolyCurve rhPolyCurve = Create.RandomPolyCurve(m_random);
            BHG.PolyCurve bhPolyCurve = rhPolyCurve.ToBHoM() as BHG.PolyCurve;

            rhPolyCurve.RemoveNesting();
            RHG.Curve[] rhCurves = rhPolyCurve.Explode();
            List<BHG.ICurve> bhCurves = bhPolyCurve.Curves;

            Assert.IsTrue(rhCurves.Length == bhCurves.Count);

            for (int i = 0; i < bhCurves.Count; i++)
                Assert.IsTrue(bhCurves[i].IIsEqual(rhCurves[i]));
        }

        /***************************************************/

        [TestMethod]
        public void PolylineToBHoM()
        {
            RHG.Polyline rhPolyline = Create.RandomPolyline(m_random);
            BHG.Polyline bhPolyline = rhPolyline.ToBHoM();
            Assert.IsTrue(bhPolyline.IsEqual(rhPolyline));
        }

        /***************************************************/

        [TestMethod]
        public void PolylineCurveToBHoM()
        {
            RHG.PolylineCurve rhPolylineCurve = Create.RandomPolylineCurve(m_random);
            BHG.Polyline bhPolyline = rhPolylineCurve.ToBHoM();
            Assert.IsTrue(bhPolyline.IsEqual(rhPolylineCurve));

            rhPolylineCurve = null;
            bhPolyline = rhPolylineCurve.ToBHoM();
            Assert.IsTrue(bhPolyline.IsEqual(rhPolylineCurve));
        }

        /***************************************************/

        [TestMethod]
        public void CurveToBHoM()
        {
            List<RHG.Curve> rhCurves = Create.RandomCurves(m_random);
            List<BHG.ICurve> bhCurves = rhCurves.Select(c => c.ToBHoM()).ToList();
            bool typeMatches = false;
            bool geometryMatches = false;
            for (int i = 0; i < rhCurves.Count; i++)
            {
                typeMatches = m_CurveTypesConvention[rhCurves[i].GetType()] == bhCurves[i].GetType();
                geometryMatches = bhCurves[i].IIsEqual(rhCurves[i]);
            }
            Assert.IsTrue(typeMatches && geometryMatches);
        }

        /***************************************************/
    }
}
