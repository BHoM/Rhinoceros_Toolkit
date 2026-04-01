/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2026, the respective contributors. All rights reserved.
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
using System.ComponentModel;
using BH.oM.Base.Attributes;
using Rhino.Geometry;

namespace BH.Engine.Adapters.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        [Description("Creates a random Rhino Point3d using the provided Random instance.")]
        [Input("random", "The Random instance used to generate coordinate values.")]
        [Output("point", "A random Rhino Point3d with X, Y, Z coordinates between 0 and 1.")]
        public static Point3d RandomPoint3d(Random random)
        {
            return new Point3d(random.NextDouble(), random.NextDouble(), random.NextDouble());
        }

        /***************************************************/

        [Description("Creates a random Rhino Point3d using a seed value.")]
        [Input("seed", "Seed value for the Random instance. Defaults to 0.")]
        [Output("point", "A random Rhino Point3d with X, Y, Z coordinates between 0 and 1.")]
        public static Point3d RandomPoint3d(int seed = 0)
        {
            return RandomPoint3d(new Random(seed));
        }

        /***************************************************/

        [Description("Creates a list of 10 random Rhino Point3d objects using the provided Random instance.")]
        [Input("random", "The Random instance used to generate coordinate values.")]
        [Output("points", "A list of 10 random Rhino Point3d objects with X, Y, Z coordinates between 0 and 1.")]
        public static List<Point3d> RandomPoint3dList(Random random)
        {
            List<Point3d> points = new List<Point3d>();
            for (int i = 0; i < 10; i++)
                points.Add(RandomPoint3d(random));
            return points;
        }

        /***************************************************/

        [Description("Creates a list of 10 random Rhino Point3d objects using a seed value.")]
        [Input("seed", "Seed value for the Random instance. Defaults to 0.")]
        [Output("points", "A list of 10 random Rhino Point3d objects with X, Y, Z coordinates between 0 and 1.")]
        public static List<Point3d> RandomPoint3dList(int seed = 0)
        {
            return RandomPoint3dList(new Random(seed));
        }

        /***************************************************/
    }
}







