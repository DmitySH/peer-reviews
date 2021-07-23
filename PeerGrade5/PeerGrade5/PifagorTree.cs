using System;
using System.Collections.Generic;
using System.Drawing;

namespace PeerGrade5
{
    class PifagorTree : Fractal
    {
        private readonly double leftAngle;
        private readonly double rightAngle;
        private readonly double koef;
        private readonly List<(PointF, PointF, Color)> points = new List<(PointF, PointF, Color)>();

        public PifagorTree(double length, int depth, double leftAngle, double rightAngle, double koef) : base(length, depth)
        {
            this.rightAngle = rightAngle;
            this.leftAngle = leftAngle;
            this.koef = koef;
        }

        /// <summary>
        /// Overrided painter. 
        /// </summary>
        /// <param name="gr"> Graphics. </param>
        /// <param name="canvasForm"> Form where to draw. </param>
        public override void Painter(Graphics gr, CanvasForm canvasForm)
        {
            PointF startPoint = new PointF(canvasForm.canvas.Width / 2, canvasForm.canvas.Height);
            Recurs(0, startPoint, 0, Length);

            foreach (var point in points)
            {
                pen.Color = point.Item3;
                gr.DrawLine(pen, point.Item1, point.Item2);
            }
        }

        /// <summary>
        /// Recursive method.
        /// </summary>
        /// <param name="counter"> Current step of recursion. </param>
        /// <param name="curPoint"> Current point. </param>
        /// <param name="angle"> Angle. </param>
        /// <param name="length"> Length. </param>
        private void Recurs(int counter, PointF curPoint, double angle, double length)
        {
            PointF nextPoint = new PointF((float)(curPoint.X - length * Math.Sin(MyStaticMethods.ToRad(angle))),
                    (float)(curPoint.Y - length * Math.Cos(MyStaticMethods.ToRad(angle))));
            points.Add((curPoint, nextPoint, colors[counter]));

            length *= koef;
            counter++;

            if (counter < Depth)
            {
                Recurs(counter, nextPoint, angle - rightAngle, length);
                Recurs(counter, nextPoint, angle + leftAngle, length);
            }
        }
    }
}
