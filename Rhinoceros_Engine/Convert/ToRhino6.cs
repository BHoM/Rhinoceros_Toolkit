/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
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

using System.Collections.Generic;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;
using BH.Engine.Geometry;

namespace BH.Engine.Rhinoceros
{
    public static partial class Convert
    {
        public static RHG.NurbsCurve ToRhino6(this BHG.NurbsCurve bCurve)
        {
            if (bCurve == null) return null;

            List<double> knots = bCurve.Knots;
            List<double> weights = bCurve.Weights;
            List<BHG.Point> ctrlPts = bCurve.ControlPoints;

            RHG.NurbsCurve rCurve = new RHG.NurbsCurve(3, false, bCurve.Degree() + 1, ctrlPts.Count);

            for (int i = 0; i < knots.Count; i++)
                rCurve.Knots[i] = knots[i];

            for (int i = 0; i < ctrlPts.Count; i++)
            {
                BHG.Point pt = ctrlPts[i] * weights[i];
                rCurve.Points.SetPoint(i, pt.X, pt.Y, pt.Z, weights[i]);
            }

            return rCurve;
        }
    }
}

