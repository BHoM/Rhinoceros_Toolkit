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
using System.Linq;
using BH.Engine.Geometry;
using BH.oM.Base.Attributes;
using BHG = BH.oM.Geometry;
using RHG = Rhino.Geometry;

namespace BH.Engine.Adapters.Rhinoceros
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        [Description("Checks whether a Rhino Brep represents a planar surface, i.e. all its faces are planar and all its vertices are coplanar.")]
        [Input("brep", "The Rhino Brep to check.")]
        [Output("isPlanarSurface", "True if all faces of the Brep are planar and all vertices are coplanar, false otherwise.")]
        public static bool IsPlanarSurface(this RHG.Brep brep)
        {
            bool isPlanarSurface = true;
            isPlanarSurface &= brep.Surfaces.All(s => s.IsPlanar(BHG.Tolerance.Distance));
            isPlanarSurface &= brep.Vertices.Select(x => x.FromRhino()).ToList().IsCoplanar();

            return isPlanarSurface;
        }

        /***************************************************/
    }
}







