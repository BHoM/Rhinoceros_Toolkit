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
using BH.oM.Adapter;
using System.ComponentModel;

namespace BH.oM.Adapters.Rhinoceros
{
    [Description("Define configuration settings for pushing and pulling Rhinoceros files using the Rhinoceros Adapter.")]
    public class RhinocerosConfig : ActionConfig
    {
        /***************************************************/
        /****             Public Properties             ****/
        /***************************************************/

        [Description("Define the version of Rhinoceros the Adapter should be operating with.")]
        public virtual int Version { get; set; } = 6;
        public virtual int Units { get; set; }

        /***************************************************/
    }
}
