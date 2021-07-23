using System;
using System.Collections.Generic;
using System.Drawing;

namespace PeerGrade5
{
    class SierpinskiTriangle : Fractal
    {
        // List of Tuples with threes of points and color.
        private readonly List<(PointF, PointF, PointF, Color)> points = new List<(PointF, PointF, PointF, Color)>();

        public SierpinskiTriangle(double length, int depth) : base(length, depth)
        {
        }

        public override void Painter(Graphics gr, CanvasForm canvasForm)
        {
            // First three.
            PointF starPoint1 = new PointF(canvasForm.canvas.Width / 2, 50);
            PointF starPoint2 = new PointF((float)(starPoint1.X + Length * Math.Sin(MyStaticMethods.ToRad(330))),
                (float)(starPoint1.Y + Length * Math.Cos(MyStaticMethods.ToRad(330))));
            PointF starPoint3 = new PointF((float)(starPoint1.X + Length * Math.Sin(MyStaticMethods.ToRad(30))),
                (float)(starPoint1.Y + Length * Math.Cos(MyStaticMethods.ToRad(30))));

            // Start of collecting points.
            Recurs(0, starPoint1, starPoint2, starPoint3);

            // Connecting threes of points to draw triangles.
            foreach (var point in points)
            {
                pen.Color = point.Item4;
                gr.DrawLine(pen, point.Item1, point.Item2);
                gr.DrawLine(pen, point.Item1, point.Item3);
                gr.DrawLine(pen, point.Item2, point.Item3);
            }
        }

        /// <summary>
        /// Recursive method to collect all the threes of points.
        /// </summary>
        /// <param name="counter"> Depth-counter. </param>
        /// <param name="point1"> First point. </param>
        /// <param name="point2"> Second point. </param>
        /// <param name="point3"> Third point. </param>
        private void Recurs(int counter, PointF point1, PointF point2, PointF point3)
        {
            points.Add((point1, point2, point3, colors[counter]));

            // Finding new three points and go to the next step of recursion.
            PointF nextPoint1 = MyStaticMethods.Middle(point1, point2);
            PointF nextPoint2 = MyStaticMethods.Middle(point1, point3);
            PointF nextPoint3 = MyStaticMethods.Middle(point2, point3);
            counter++;

            if (counter < Depth)
            {
                Recurs(counter, point1, nextPoint1, nextPoint2);
                Recurs(counter, point2, nextPoint3, nextPoint1);
                Recurs(counter, point3, nextPoint2, nextPoint3);
            }
        }
    }
}