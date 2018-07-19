using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Point3d RandomPoint3d(Random random)
        {
            return new Point3d(random.NextDouble(), random.NextDouble(), random.NextDouble());
        }

        /***************************************************/

        public static Point3f RandomPoint3f(Random random)
        {
            return new Point3f((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        }

        /***************************************************/


        public static ControlPoint RandomControlPoint(Random random)
        {
            return new ControlPoint(random.NextDouble(), random.NextDouble(), random.NextDouble());
        }

        /***************************************************/

        public static Point3d RandomPoint3d(int seed = 0)
        {
            return RandomPoint3d(new Random(seed));
        }

        /***************************************************/

        public static Point3f RandomPoint3f(int seed = 0)
        {
            return RandomPoint3f(new Random(seed));

        }

        /***************************************************/

        public static ControlPoint RandomControlPoint(int seed = 0)
        {
            return RandomControlPoint(new Random(seed));
        }

        /***************************************************/
    }
}
