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
                string targetPath = System.IO.Path.Combine(Environment.CurrentDirectory, file);
                System.IO.File.Delete(targetPath);
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
