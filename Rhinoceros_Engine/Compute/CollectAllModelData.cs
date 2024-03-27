/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2024, the respective contributors. All rights reserved.
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
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using BH.oM.Base.Attributes;
using BH.oM.Base;
using Rhino.FileIO;
using System.IO;
using System.Drawing;
using Rhino.Geometry;

namespace BH.Engine.Adapters.Rhinoceros
{
    public static partial class Compute
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        [Description("Opens a series of rhino models and fetches all the data from them.")]
        [Input("fileNames", "Full path filenames to the files to be extracted.")]
        [Input("go", "Toggles on and off the execution of the method.")]
        [MultiOutput(0, "layerName", "The name of the model and the layer name from the opened model separated with '::'. Can be used with the Human grasshopper plugin to generate new layers.")]
        [MultiOutput(1, "colour", "The colour of the layer.")]
        [MultiOutput(2, "geometry", "The geometry from the models.")]
        public static Output<List<string>, List<Color>, List<GeometryBase>> CollectAllModelData(List<string> fileNames, bool go = false)
        {
            if (!go)
                return new Output<List<string>, List<Color>, List<GeometryBase>>();

            List<string> layerNames = new List<string>();
            List<Color> colours = new List<Color>();
            List<GeometryBase> geometries = new List<GeometryBase>();

            foreach (string fileName in fileNames)
            {
                File3dm file3Dm = File3dm.Read(fileName);
                string name = Path.GetFileName(fileName);

                foreach (File3dmObject item in file3Dm.Objects)
                {
                    Rhino.DocObjects.Layer layer = file3Dm.Layers[item.Attributes.LayerIndex];
                    layerNames.Add(name + "::" + layer.Name);
                    colours.Add(layer.Color);
                    geometries.Add(item.Geometry.Duplicate());

                }
            }

            return new Output<List<string>, List<Color>, List<GeometryBase>>
            {
                Item1 = layerNames,
                Item2 = colours,
                Item3 = geometries
            };

        }

        /***************************************************/
    }
}

