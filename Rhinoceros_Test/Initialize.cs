/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2020, the respective contributors. All rights reserved.
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BH.Test.Rhinoceros
{
    [TestClass]
    public static class Initialize
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext contex)
        {
            string programFilesDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string rhinoDir = System.IO.Path.Combine(programFilesDir, "Rhinoceros 5 (64-bit)", "System");

            foreach (string file in m_RequiredFiles)
            {
                try
                {
                    string sourcePath = System.IO.Path.Combine(rhinoDir, file);
                    string targetPath = System.IO.Path.Combine(Environment.CurrentDirectory, file);
                    System.IO.File.Copy(sourcePath, targetPath);
                }
                catch
                {

                }
            }
        }

        /***************************************************/

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            foreach (string file in m_RequiredFiles)
            {
                try
                {
                    string targetPath = System.IO.Path.Combine(Environment.CurrentDirectory, file);
                    System.IO.File.Delete(targetPath);
                }
                catch
                {

                }
            }
        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        /// <summary>
        /// <see cref="https://github.com/BuroHappoldEngineering/Rhinoceros_Toolkit/pull/52#pullrequestreview-138914924"/> 
        /// From Steve Baer in McNeel:
        /// The majority of RhinoCommon depends on functionality exported from the rhcommon_c.dll
        /// The rhcommon_c.dll depends on Rhino.exe
        /// Due to this dependency chain, Rhino.exe needs to be the host process.
        /// <see cref="https://discourse.mcneel.com/t/unit-testing-plugin-code/39830"/>
        /// <see cref="https://discourse.mcneel.com/t/unit-testing-plug-in-code-with-nunit/3367/6"/>
        /// 
        /// Once we upgrade to Rhino v7 we can take advantage of:
        /// <see cref="https://discourse.mcneel.com/t/rhinocommon-unit-testing/61317"/>
        /// </summary>
        private static string[] m_RequiredFiles = new string[]
        {
            "Ltdis15x.dll", "Ltfil15x.dll", "Ltkrn15x.dll", // Needed due to the LeadTools framework
            "openNURBSx64.dll", // Required as standard library of geometrical objects
            "rhcommon_c.dll", "RhinoCommon.dll", // Actual Rhinoceros SDKs
            "Rhino.exe", // Rhino host process. Any dll call must come from here.
            "Rhino_DotNet.dll", // Not sure, but it will be disposed later on.
            "tlx64.dll" // Not sure, licensing?
        };
    }
}

