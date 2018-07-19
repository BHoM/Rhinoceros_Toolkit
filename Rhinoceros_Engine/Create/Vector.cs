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

        public static Vector3d RandomVector3d(Random random)
        {
            return new Vector3d(random.NextDouble(), random.NextDouble(), random.NextDouble());
        }

        /***************************************************/

        public static Vector3f RandomVector3f(Random random)
        {
            return new Vector3f((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        }

        /***************************************************/

        public static Vector3d RandomVector3d(int seed = 0)
        {
            return RandomVector3d(new Random(seed));
        }

        /***************************************************/

        public static Vector3f RandomVector3f(int seed = 0)
        {
            return RandomVector3f(new Random(seed));
        }

        /***************************************************/
    }
}
