/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2020, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

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
        public void PointToRhino()
        {
            BHG.Point bhPoint = Engine.Geometry.Create.RandomPoint(m_random);
            RHG.Point3d rhinoPoint = bhPoint.ToRhino();
            Assert.IsTrue(bhPoint.IsEqual(rhinoPoint));

            // Checking null return
            bhPoint = null;
            rhinoPoint = bhPoint.ToRhino();
            Assert.IsTrue(bhPoint.IsEqual(rhinoPoint));
        }

        /***************************************************/

        [TestMethod]
        public void VectorToRhino()
        {
            BHG.Vector bhVector = Engine.Geometry.Create.RandomVector(m_random);
            RHG.Vector3d rhVector = bhVector.ToRhino();
            Assert.IsTrue(bhVector.IsEqual(rhVector));

            // Checking null return
            bhVector = null;
            rhVector = bhVector.ToRhino();
            Assert.IsTrue(bhVector.IsEqual(rhVector));
        }


        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        [TestMethod]
        public void ArcToRhino()
        {
            BHG.Arc bhArc = Engine.Geometry.Create.RandomArc(m_random);
            RHG.Arc rhArc = bhArc.ToRhino();
            Assert.IsTrue(bhArc.IsEqual(rhArc));

            // Checking null return
            bhArc = null;
            rhArc = bhArc.ToRhino();
            Assert.IsTrue(bhArc.IsEqual(rhArc));
        }

        /***************************************************/

        [TestMethod]
        public void CircleToRhino()
        {
            BHG.Circle bhCircle = Engine.Geometry.Create.RandomCircle(m_random);
            RHG.Circle rhCircle = bhCircle.ToRhino();
            Assert.IsTrue(bhCircle.IsEqual(rhCircle));

            // Checking null return
            bhCircle = null;
            rhCircle = bhCircle.ToRhino();
            Assert.IsTrue(bhCircle.IsEqual(rhCircle));
        }

        /***************************************************/

        [TestMethod]
        public void LineToRhino()
        {
            BHG.Line bhLine = Engine.Geometry.Create.RandomLine(m_random);
            RHG.Line rhLine = bhLine.ToRhino();
            Assert.IsTrue(bhLine.IsEqual(rhLine));

            // Checking null return
            bhLine = null;
            rhLine = bhLine.ToRhino();
            Assert.IsTrue(bhLine.IsEqual(rhLine));
        }

        /***************************************************/

        [TestMethod]
        public void NurbCurveToRhino()
        {
            BHG.NurbsCurve bhCurve = Engine.Geometry.Create.RandomNurbsCurve(m_random);
            RHG.NurbsCurve rhCurve = bhCurve.ToRhino();
            Assert.IsTrue(bhCurve.IsEqual(rhCurve));

            // Checking null return
            bhCurve = null;
            rhCurve = bhCurve.ToRhino();
            Assert.IsTrue(bhCurve.IsEqual(rhCurve));
        }

        /***************************************************/

        [TestMethod]
        public void PolyCurveToRhino()
        {
            BHG.PolyCurve bhPolyCurve = Engine.Geometry.Create.RandomPolyCurve(m_random);
            RHG.PolyCurve rhPolyCurve = bhPolyCurve.ToRhino();
            Assert.IsTrue(bhPolyCurve.IsEqual(rhPolyCurve));

            // Random null check
            List<BHG.ICurve> bhCurves = bhPolyCurve.Curves;
            RHG.Curve[] rhCurves = rhPolyCurve.Explode();
            for (int i = 0; i < bhCurves.Count; i++)
            {
                if (m_random.Next(0, 2) != 0)
                    bhCurves[i] = null;
                Assert.IsTrue(bhCurves[i].IIsEqual(rhCurves[i]));
            }

            // Checking null return
            bhPolyCurve = null;
            rhPolyCurve = bhPolyCurve.ToRhino();
            Assert.IsTrue(rhPolyCurve == null);
        }

        /***************************************************/

        [TestMethod]
        public void PolylineToRhino()
        {
            BHG.Polyline bhPolyline = Engine.Geometry.Create.RandomPolyline(m_random);
            RHG.PolylineCurve rhPolyline = bhPolyline.ToRhino();
            Assert.IsTrue(bhPolyline.IsEqual(rhPolyline));

            // Checking null return
            bhPolyline = null;
            rhPolyline = bhPolyline.ToRhino();
            Assert.IsTrue(bhPolyline.IsEqual(rhPolyline));
        }

        /***************************************************/

        [TestMethod]
        public void PolylineCurveToRhino()
        {
            BHG.Polyline bhPolyline = Engine.Geometry.Create.RandomPolyline(m_random);
            RHG.PolylineCurve rhPolylineCurve = bhPolyline.ToRhino();
            Assert.IsTrue(bhPolyline.IsEqual(rhPolylineCurve));

            // Checking null return
            rhPolylineCurve = null;
            bhPolyline = rhPolylineCurve.ToBHoM();
            Assert.IsTrue(bhPolyline.IsEqual(rhPolylineCurve));
        }

        /***************************************************/

        [TestMethod]
        public void ICurveToRhino()
        {
            List<BHG.ICurve> bhCurves = Enumerable.Repeat(Engine.Geometry.Create.RandomCurve(m_random), 15).ToList();
            List<RHG.Curve> rhCurves = bhCurves.Select(x => x.IToRhino()).ToList();

            bool typeMatches = true;
            bool geometryMatches = true;

            for (int i = 0; i < rhCurves.Count; i++)
            {
                typeMatches &= m_CurveTypesConvention[rhCurves[i].GetType()] == bhCurves[i].GetType();
                geometryMatches &= bhCurves[i].IIsEqual(rhCurves[i]);
            }

            Assert.IsTrue(typeMatches & geometryMatches);
        }

        /***************************************************/
    }
}

