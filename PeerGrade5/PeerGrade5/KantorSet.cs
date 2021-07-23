using System.Collections.Generic;
using System.Drawing;

namespace PeerGrade5
{
    class KantorSet : Fractal
    {
        private readonly List<RectangleF> rects = new List<RectangleF>();
        private readonly double distance;
        private readonly double height;

        /// <summary>
        /// Constructor for set. 
        /// </summary>
        /// <param name="length"> Length. </param>
        /// <param name="depth"> Depth. </param>
        /// <param name="height"> Height. </param>
        /// <param name="distance"> Distance. </param>
        public KantorSet(double length, int depth, double height, double distance) : base(length, depth)
        {
            this.height = height;
            this.distance = distance;
        }

        /// <summary>
        /// Overrided painter.
        /// </summary>
        /// <param name="gr"> Graphics. </param>
        /// <param name="canvasForm"> Form where to draw. </param>
        public override void Painter(Graphics gr, CanvasForm canvasForm)
        {
            RectangleF rectMain = new RectangleF(10,
                0, (float)Length, (float)height);
            Recurs(0, rectMain);

            foreach (var rect in rects)
            {
                gr.FillRectangle(Brushes.Black, rect);
            }
        }
        
        /// <summary>
        /// Recursive method.
        /// </summary>
        /// <param name="counter"> Current recursion step. </param>
        /// <param name="rta"> Previous rectangle. </param>
        private void Recurs(int counter, RectangleF rta)
        {
            counter++;
            rects.Add(rta);
            RectangleF nextRta1 = new RectangleF(rta.X, (float)(rta.Y + distance), rta.Width / 3, rta.Height);
            RectangleF nextRta2 = new RectangleF(rta.X + rta.Width / 3 * 2, nextRta1.Y, rta.Width / 3, rta.Height);

            if (counter < Depth)
            {
                Recurs(counter, nextRta1);
                Recurs(counter, nextRta2);
            }
        }
    }
}
