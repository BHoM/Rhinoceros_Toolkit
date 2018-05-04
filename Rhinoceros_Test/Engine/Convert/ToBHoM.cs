using System;
using System.Collections.Generic;
using System.Linq;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BH.Test.Rhinoceros
{
    [TestClass]
    public partial class Convert
    {
        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        [TestMethod]
        public void Point3dToBHoM()
        {
            RHG.Point3d rhinoPoint = Engine.Rhinoceros.Create.RandomPoint3d();
            BHG.Point bhPoint = Engine.Rhinoceros.Convert.ToBHoM(rhinoPoint);
            Assert.IsTrue(bhPoint.X == rhinoPoint.X && bhPoint.Y == rhinoPoint.Y && bhPoint.Z == rhinoPoint.Z);
        }

        /***************************************************/

        [TestMethod]
        public void Point3fToBHoM()
        {
            RHG.Point3f rhinoPoint = Engine.Rhinoceros.Create.RandomPoint3f();
            BHG.Point bhPoint = Engine.Rhinoceros.Convert.ToBHoM(rhinoPoint);
            Assert.IsTrue(bhPoint.X == rhinoPoint.X && bhPoint.Y == rhinoPoint.Y && bhPoint.Z == rhinoPoint.Z);
        }

        /***************************************************/

        [TestMethod]
        public void ControlPointToBHoM()
        {
            RHG.ControlPoint rhinoPoint = Engine.Rhinoceros.Create.RandomControlPoint();
            BHG.Point bhPoint = Engine.Rhinoceros.Convert.ToBHoM(rhinoPoint);
            Assert.IsTrue(bhPoint.X == rhinoPoint.Location.X && bhPoint.Y == rhinoPoint.Location.Y && bhPoint.Z == rhinoPoint.Location.Z);
        }

        /***************************************************/

        [TestMethod]
        public void Vector3dToBHoM()
        {
            RHG.Vector3d rhVector = Engine.Rhinoceros.Create.RandomVector3d();
            BHG.Vector bhVector = Engine.Rhinoceros.Convert.ToBHoM(rhVector);
            Assert.IsTrue(bhVector.X == rhVector.X && bhVector.Y == rhVector.Y && bhVector.Z == rhVector.Z);
        }

        /***************************************************/

        [TestMethod]
        public void Vector3fToBHoM()
        {
            RHG.Vector3f rhVector = Engine.Rhinoceros.Create.RandomVector3f();
            BHG.Vector bhVector = Engine.Rhinoceros.Convert.ToBHoM(rhVector);
            Assert.IsTrue(bhVector.X == rhVector.X && bhVector.Y == rhVector.Y && bhVector.Z == rhVector.Z);
        }

        /***************************************************/
    }
}
