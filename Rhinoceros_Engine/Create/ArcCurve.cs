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

using Rhino.Geometry;
using System;
using BH.oM.Base.Attributes;
using System.ComponentModel;

namespace BH.Engine.Adapters.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        [Description("Creates a random Rhino ArcCurve using the provided random number generator.")]
        [Input("random", "Random number generator to use for arc curve creation.")]
        [Output("arcCurve", "A randomly generated Rhino ArcCurve.")]
        public static ArcCurve RandomArcCurve(Random random)
        {
            return new ArcCurve(RandomArc(random));
        }

        /***************************************************/

        [Description("Creates a random Rhino ArcCurve using the provided seed.")]
        [Input("seed", "Seed for the random number generator.")]
        [Output("arcCurve", "A randomly generated Rhino ArcCurve.")]
        public static ArcCurve RandomArcCurve(int seed = 0)
        {
            return RandomArcCurve(new Random(seed));
        }

        /***************************************************/
    }
}







