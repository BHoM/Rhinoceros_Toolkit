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

using BH.oM.Base;
using BH.oM.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.oM.Adapters.Rhinoceros
{
    [Description("Set up the objects to add to the Rhinoceros file.")]
    public class RhinocerosDocumentBuilder : BHoMObject
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        [Description("RhinoObjects to add to a Rhinoceros file. Layer and colour attributes are defined by the RhinoObject.")]
        public virtual List<RhinoObject> RhinoObjects { get; set; } = new List<RhinoObject>();

        [Description("IGeometry to add to a Rhinoceros file on the default layer and with colour by the default layer.")]
        public virtual List<IGeometry> Geometry { get; set; } = new List<IGeometry>();

        /*******************************************/

    }
}
