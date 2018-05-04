using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        public static Point3d RandomPoint3d()
        {
            Random ran = new Random();
            return new Point3d(ran.NextDouble(), ran.NextDouble(), ran.NextDouble());
        }

        /***************************************************/

        public static Point3f RandomPoint3f()
        {
            Random ran = new Random();
            return new Point3f((float)ran.NextDouble(), (float)ran.NextDouble(), (float)ran.NextDouble());
        }

        /***************************************************/


        public static ControlPoint RandomControlPoint()
        {
            Random ran = new Random();
            return new ControlPoint(ran.NextDouble(), ran.NextDouble(), ran.NextDouble());
        }

        /***************************************************/

        public static Vector3d RandomVector3d()
        {
            Random ran = new Random();
            return new Vector3d(ran.NextDouble(), ran.NextDouble(), ran.NextDouble());
        }

        /***************************************************/

        public static Vector3f RandomVector3f()
        {
            Random ran = new Random();
            return new Vector3f((float)ran.NextDouble(), (float)ran.NextDouble(), (float)ran.NextDouble());
        }

        /***************************************************/
    }
}
