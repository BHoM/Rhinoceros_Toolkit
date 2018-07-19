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

        private static string[] m_RequiredFiles = new string[]
        {
            "Ltdis15x.dll", "Ltfil15x.dll", "Ltkrn15x.dll",
            "openNURBSx64.dll", "rhcommon_c.dll", "Rhino.exe",
            "Rhino_DotNet.dll", "RhinoCommon.dll", "tlx64.dll"
        };
    }
}
