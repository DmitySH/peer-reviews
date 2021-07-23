using System.Collections.Generic;
using System.Drawing;

namespace PeerGrade5
{
    class SierpinskiCarpet : Fractal
    {
        private readonly List<RectangleF> rects = new List<RectangleF>();

        /// <summary>
        /// Constructor for Carpet.
        /// </summary>
        /// <param name="length"> Length. </param>
        /// <param name="depth"> Depth. </param>
        public SierpinskiCarpet(double length, int depth) : base(length, depth)
        {
        }

        /// <summary>
        /// Overrided painter. 
        /// </summary>
        /// <param name="gr"> Graphics. </param>
        /// <param name="canvasForm"> Form where to draw. </param>
        public override void Painter(Graphics gr, CanvasForm canvasForm)
        {
            RectangleF rectMain = new RectangleF(0 , 0, (float)Length, (float)Length);

            if (Depth == 1)
            {
                gr.FillRectangle(Brushes.Blue, rectMain);
                return;
                
            }

            Recurs(Depth, rectMain);

            foreach (var rect in rects)
            {
               
                gr.FillRectangle(Brushes.Blue, rect);
            }
        }

        private void Recurs(int counter, RectangleF rta)
        {

            if (counter == 1)
            {
                rects.Add(rta);
                return;
            }

            SizeF size = new SizeF(rta.Size / 3);
            PointF[] pointAr =
            {
                new PointF(rta.Left, rta.Top) ,
                new PointF(rta.Left+size.Width,rta.Top),
                new PointF(rta.Left+size.Width*2, rta.Top),
                new PointF(rta.Left,rta.Top+size.Width),
                new PointF(rta.Left + size.Width * 2, rta.Top + size.Width),
                new PointF(rta.Left, rta.Top + size.Width*2),
                new PointF(rta.Left + size.Width, rta.Top + size.Width * 2),
                new PointF(rta.Left + size.Width * 2, rta.Top + size.Width * 2)
            };

            foreach (var x in pointAr)
            {
                Recurs(counter - 1, new RectangleF(x, size));
            }
        }
    }
}

