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

        [Description("Creates a random Rhino Vector3d using the provided Random instance.")]
        [Input("random", "The Random instance used to generate component values.")]
        [Output("vector", "A random Rhino Vector3d with X, Y, Z components between 0 and 1.")]
        public static Vector3d RandomVector3d(Random random)
        {
            return new Vector3d(random.NextDouble(), random.NextDouble(), random.NextDouble());
        }

        /***************************************************/

        [Description("Creates a random Rhino Vector3d using a seed value.")]
        [Input("seed", "Seed value for the Random instance. Defaults to 0.")]
        [Output("vector", "A random Rhino Vector3d with X, Y, Z components between 0 and 1.")]
        public static Vector3d RandomVector3d(int seed = 0)
        {
            return RandomVector3d(new Random(seed));
        }

        /***************************************************/
    }
}







