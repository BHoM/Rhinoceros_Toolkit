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
using BH.oM.Base.Attributes;
using BH.oM.Geometry;

namespace BH.Engine.Adapters.Rhinoceros
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        [Description("Checks whether a BHoM geometry type has a direct Rhino equivalent, i.e. can be converted to a Rhino geometry object. \n" +
            "Types that implement IGeometry but have no Rhino equivalent (such as Extrusion, Pipe, Loft, and others) return false.")]
        [Input("type", "The BHoM geometry Type to check.")]
        [Output("isRhinoEquivalent", "True if the type implements IGeometry and has a direct Rhino equivalent, false otherwise.")]
        public static bool IsRhinoEquivalent(this Type type)
        {
            if (typeof(IGeometry).IsAssignableFrom(type))
                return (type != typeof(Extrusion)
                     && type != typeof(Pipe)
                     && type != typeof(Loft)
                     && type != typeof(CompositeGeometry)
                     && type != typeof(Quaternion)
                     && type != typeof(Basis)
                     && type != typeof(SurfaceTrim));
            else
                return false;
        }


        /***************************************************/
    }
}







