using System;
using System.Collections.Generic;
using System.Drawing;

namespace PeerGrade5
{
    class MyStaticMethods
    {
        /// <summary>
        /// To radian converter.
        /// </summary>
        /// <param name="angle"> Angle in degrees. </param>
        /// <returns> Angle in radians. </returns>
        public static double ToRad(double angle) => angle * 2 * Math.PI / 360.0;

        /// <summary>
        /// Safe parse for angle.
        /// </summary>
        /// <param name="strAngle"> Angle in string. </param>
        /// <returns> Angle in double. </returns>
        public static double ParseAngle(string strAngle)
        {
            if (double.TryParse(strAngle, out double num) && num >= 0 && num <= 90)
            {
                return num;
            }

            return -1;
        }

        /// <summary>
        /// Safe parse for coefficient.
        /// </summary>
        /// <param name="strKoef"> Coefficient in string. </param>
        /// <returns> Coefficient in double. </returns>
        public static double ParseKoef(string strKoef)
        {
            if (double.TryParse(strKoef, out double num) && num > 0 && num <= 1)
            {
                return num;
            }

            return -1;
        }

        /// <summary>
        /// Safe parse for depth.
        /// </summary>
        /// <param name="strDepth"> Depth in string. </param>
        /// <param name="maxDepth"> Maximal depth. </param>
        /// <returns> Depth in int. </returns>
        public static int ParseDepth(string strDepth, int maxDepth)
        {
            if (int.TryParse(strDepth, out int num) && num > 0 && num <= maxDepth)
            {
                return num;
            }

            return -1;
        }

        /// <summary>
        /// Safe parse for length.
        /// </summary>
        /// <param name="strLength"> Length in string. </param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static double ParseLength(string strLength, double maxLength)
        {
            if (double.TryParse(strLength, out double num) && num > 0 && num <= maxLength)
            {
                return num;
            }

            return -1;
        }

        /// <summary>
        /// Safe parse for height.
        /// </summary>
        /// <param name="strHeight"> Height in string. </param>
        /// <returns></returns>
        public static double ParseHeight(string strHeight)
        {
            if (double.TryParse(strHeight, out double num) && num > 0 && num <= 1000)
            {
                return num;
            }

            return -1;
        }

        /// <summary>
        /// Safe parse for distance.
        /// </summary>
        /// <param name="strDistance"> Distance in string. </param>
        /// <returns> Distance in double. </returns>
        public static double ParseDistance(string strDistance)
        {
            if (double.TryParse(strDistance, out double num) && num > 0 && num <= 100)
            {
                return num;
            }

            return -1;
        }

        /// <summary>
        /// Finds middle of two points.
        /// </summary>
        /// <param name="point1"> First point. </param>
        /// <param name="point2"> Second point. </param>
        /// <returns> Middle point. </returns>
        public static PointF Middle(PointF point1, PointF point2)
           => new PointF((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);

        /// <summary>
        ///  Creating gradient collection.
        /// </summary>
        /// <param name="start"> Start gradient color. </param>
        /// <param name="end"> End gradient color. </param>
        /// <param name="steps"> Number of steps. </param>
        /// <returns> Gradient collection. </returns>
        public static IEnumerable<Color> GetGradients(Color start, Color end, int steps)
        {
            if (steps == 1)
            {
                yield return Color.FromArgb(start.R, start.G, start.B);
            }
            else
            {
                int stepR = (end.R - start.R) / (steps - 1);
                int stepG = (end.G - start.G) / (steps - 1);
                int stepB = (end.B - start.B) / (steps - 1);

                for (int i = 0; i < steps; i++)
                {
                    yield return Color.FromArgb(start.R + stepR * i,
                        start.G + stepG * i,
                        start.B + stepB * i);
                }
            }
        }
    }
}
