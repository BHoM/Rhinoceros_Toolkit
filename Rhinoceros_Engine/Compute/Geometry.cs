/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2021, the respective contributors. All rights reserved.
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

namespace BH.Engine.Rhinoceros
{
    public static partial class Compute
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static object RandomGeometry(int seed = 0)
        {
            return RandomGeometry(new Random(seed));
        }

        /***************************************************/

        public static object RandomGeometry(Random random)
        {
            int throwDice = random.Next(15);
            switch (throwDice)
            {
                case 0:
                    return Create.RandomArc(random);
                case 1:
                    return Create.RandomArcCurve(random);
                case 2:
                    return Create.RandomCircle(random);
                case 3:
                    return Create.RandomLine(random);
                case 4:
                    return Create.RandomLineCurve(random);
                case 5:
                    return Create.RandomNurbsCurve(random);
                case 6:
                    return Create.RandomPoint3d(random);
                case 7:
                    return Create.RandomPoint3f(random);
                case 8:
                    return Create.RandomControlPoint(random);
                case 9:
                    return Create.RandomPolyCurve(random);
                case 10:
                    return Create.RandomPolyline(random);
                case 11:
                    return Create.RandomPolylineCurve(random);
                case 12:
                    return Create.RandomVector3d(random);
                case 13:
                    return Create.RandomPoint3f(random);
                case 14:
                    return Create.RandomCurves(random);
                case 15:
                    return Create.RandomPoint3dList(random);
                default:
                    return null;
            }
        }

        /***************************************************/

        public static object RandomGeometry(Type type, Random random)
        {
            switch(type.Name.ToLower())
            {
                case "arc":
                    return Create.RandomArc(random);
                case "arccurve":
                    return Create.RandomArcCurve(random);
                case "circle":
                    return Create.RandomCircle(random);
                case "line":
                    return Create.RandomLine(random);
                case "linecurve":
                    return Create.RandomLineCurve(random);
                case "nurbscurve":
                case "nurbcurve":
                    return Create.RandomNurbsCurve(random);
                case "point":
                case "point3d":
                    return Create.RandomPoint3d(random);
                case "point3f":
                    return Create.RandomPoint3f(random);
                case "controlpoint":
                    return Create.RandomControlPoint(random);
                case "polycurve":
                    return Create.RandomPolyCurve(random);
                case "polyline":
                    return Create.RandomPolyline(random);
                case "polylinecurve":
                    return Create.RandomPolylineCurve(random);
                case "vector":
                case "vector3d":
                    return Create.RandomVector3d(random);
                case "vector3f":
                    return Create.RandomPoint3f(random);
                case "curvelist":
                case "curves":
                    return Create.RandomCurves(random);
                case "pointlist":
                case "point3dlist":
                case "points":
                    return Create.RandomPoint3dList(random);
                default:
                    return null;
            }
        }
        
        /***************************************************/
    }
}

