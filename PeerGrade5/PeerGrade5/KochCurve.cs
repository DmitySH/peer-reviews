using System.Collections.Generic;
using System.Drawing;

namespace PeerGrade5
{
    class KochCurve : Fractal
    {
        private readonly List<(PointF, PointF, Color)> points = new List<(PointF, PointF, Color)>();
        
        /// <summary>
        /// Constructor for Curve.
        /// </summary>
        /// <param name="length"> Length. </param>
        /// <param name="depth"> Depth. </param>
        public KochCurve(double length, int depth) : base(length, depth) { }

        /// <summary>
        /// Overrided painter.
        /// </summary>
        /// <param name="gr"> Graphics. </param>
        /// <param name="canvasForm"> Form where to draw. </param>
        public override void Painter(Graphics gr, CanvasForm canvasForm)
        {
            PointF startPointRight = new PointF((float)(canvasForm.canvas.Width / 2 + Length / 2), canvasForm.canvas.Height - 100);
            PointF startPointLeft = new PointF((float)(canvasForm.canvas.Width / 2 - Length / 2), canvasForm.canvas.Height - 100);
            Recurs(startPointLeft, startPointRight, Depth);

            foreach (var point in points)
            {
                pen.Color = point.Item3;
                gr.DrawLine(pen, point.Item1, point.Item2);
            }
        }

        /// <summary>
        /// Recursive method.
        /// </summary>
        /// <param name="begin"> First point. </param>
        /// <param name="end"> End point. </param>
        /// <param name="counter"> Current step of recursion. </param>
        private void Recurs(PointF begin, PointF end, int counter)
        {
            double cos = 0.5;
            double sin = -0.866;

            if (counter == 0)
            {
                points.Add((begin, end, colors[counter]));
                return;
            }
            
            PointF goUp = new PointF(begin.X + (end.X - begin.X) / 3,
                begin.Y + (end.Y - begin.Y) / 3);
            PointF goDown = new PointF(begin.X + (end.X - begin.X) * 2 / 3,
                begin.Y + (end.Y - begin.Y) * 2 / 3);
            PointF middle = new PointF((float)(goUp.X + (goDown.X - goUp.X) * cos - sin * (goDown.Y - goUp.Y)),
                (float)(goUp.Y + (goDown.X - goUp.X) * sin + cos * (goDown.Y - goUp.Y)));

            Recurs(begin, goUp, counter - 1);
            Recurs(goUp, middle, counter - 1);
            Recurs(middle, goDown, counter - 1);
            Recurs(goDown, end, counter - 1);
        }
    }
}
