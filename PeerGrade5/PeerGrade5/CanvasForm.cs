using System;
using System.Drawing;
using System.Windows.Forms;

namespace PeerGrade5
{
    public partial class CanvasForm : Form
    {
        public static Bitmap bmp;
        public Graphics gr;

        /// <summary>
        /// Constructor for canvas form.
        /// </summary>
        public CanvasForm()
        {
            InitializeComponent();

            MinimumSize = new System.Drawing.Size(
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width / 2,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height / 2);
            MaximumSize = new System.Drawing.Size(
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height);
        }

        /// <summary>
        /// Method for automatic resize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasForm_ResizeEnd(object sender, EventArgs e)
        {
            Fractal fractal = new KantorSet(Form1.globalLength, Form1.globalDepth, Form1.globalHeight, Form1.globalDistance);
            switch (Form1.globalId)
            {
                case 0:
                    fractal = new PifagorTree(Form1.globalLength,Form1.globalDepth,
                        Form1.globalLangle,Form1.globalRangle,
                        Form1.globalKoef);
                    break;
                case 1:
                    fractal = new KochCurve(Form1.globalLength,Form1.globalDepth);
                    break;
                case 2:
                    fractal = new SierpinskiCarpet(Form1.globalLength,Form1.globalDepth);
                    break;
                case 3:
                    fractal = new SierpinskiTriangle(Form1.globalLength, Form1.globalDepth);
                    break;
                case 4:
                    fractal = new KantorSet(Form1.globalLength,Form1.globalDepth,Form1.globalHeight,Form1.globalDistance);
                    break;
            }

            bmp = new Bitmap(canvas.Width, canvas.Height);
            gr = Graphics.FromImage(bmp);
            fractal.Painter(gr, this);
            canvas.Image = bmp;
        }

        /// <summary>
        /// Saves picture as .PNG.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Picture .JPG|*.JPG";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    canvas.Image.Save(sfd.FileName);
                }
                catch
                {
                    MessageBox.Show("Cannot save image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
