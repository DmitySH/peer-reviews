using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PeerGrade5
{
    public abstract class Fractal
    {
        /// <summary>
        /// Constructor for fractals.
        /// </summary>
        /// <param name="length"> Length. </param>
        /// <param name="depth"> Depth. </param>
        public Fractal(double length, int depth)
        {
            Length = length;
            Depth = depth;
            foreach (var gradient in MyStaticMethods.GetGradients((Application.OpenForms["Form1"] as Form1).colorDialogStart.Color,
                (Application.OpenForms["Form1"] as Form1).colorDialogEnd.Color, Depth))
            {
                colors.Add(gradient);
            }
        }

        private double length;
        private int depth;

        public double Length
        {
            set => length = value;
            get => length;
        }
        public int Depth
        {
            set => depth = value;
            get => depth;
        }

        protected readonly Pen pen = new Pen(Color.BlueViolet, 2);
        protected List<Color> colors = new List<Color>();

        /// <summary>
        /// Abstract method for drawing fractals.
        /// </summary>
        /// <param name="gr"> Graphics. </param>
        /// <param name="canvasForm"> Form where to draw. </param>
        public abstract void Painter(Graphics gr, CanvasForm canvasForm);
    }
}