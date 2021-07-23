using System;
using System.Drawing;
using System.Windows.Forms;

namespace PeerGrade5
{
    public partial class Form1 : Form
    {
        public static double globalLength;
        public static int globalDepth;
        
        public static double globalDistance;
        public static double globalHeight;
        public static double globalLangle;
        public static double globalRangle;
        public static int globalId;
        public static double globalKoef;
        string[] fractalNames = { "Pifagor's tree", "Koch's curve",
            "Sierpinski's carpet", "Sierpinski's triangle", "Kantor's set"};

        private int id;
        public static Bitmap bmp;
        public Graphics gr;
        public CanvasForm canvasForm;
        /// <summary>
        /// Form1 constructor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            
            HideAll();
            MinimumSize = new Size(
                Screen.PrimaryScreen.Bounds.Size.Width / 2,
                Screen.PrimaryScreen.Bounds.Size.Height / 2);
            MaximumSize = new Size(
                Screen.PrimaryScreen.Bounds.Size.Width,
                Screen.PrimaryScreen.Bounds.Size.Height);

            fractalList.Items.AddRange(fractalNames);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void fractalList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Hides all labels and boxes.
        /// </summary>
        private void HideAll()
        {
            labelParams.Visible = false;
            buttonDrawIt.Visible = false;
            angleRightBox.Visible = false;
            angleLeftBox.Visible = false;
            koefBox.Visible = false;
            depthBox.Visible = false;
            lengthBox.Visible = false;
            distanceBox.Visible = false;
            heightBox.Visible = false;
            depthLabel.Visible = false;
            distanceLabel.Visible = false;
            heightLabel.Visible = false;
            leftBranchAngleLabel.Visible = false;
            rightBranchAngleLabel.Visible = false;
            lengthLabel.Visible = false;
        }

        /// <summary>
        /// Open elements to create Tree.
        /// </summary>
        private void OpenTree()
        {
            HideAll();
            labelParams.Text = "Enter all parameters to draw this fractal";
            labelParams.Visible = true;
            buttonDrawIt.Visible = true;
            angleLeftBox.Visible = true;
            angleRightBox.Visible = true;
            koefBox.Visible = true;
            depthBox.Visible = true;
            lengthBox.Visible = true;

            depthLabel.Visible = true;
            heightLabel.Visible = true;
            leftBranchAngleLabel.Visible = true;
            lengthLabel.Visible = true;
            rightBranchAngleLabel.Visible = true;
            id = 0;
        }

        /// <summary>
        /// Open elements to create Curve.
        /// </summary>
        private void OpenCurve()
        {
            HideAll();
            labelParams.Text = "Enter all parameters to draw this fractal";
            labelParams.Visible = true;
            depthBox.Visible = true;
            lengthBox.Visible = true;
            buttonDrawIt.Visible = true;

            lengthLabel.Visible = true;
            depthLabel.Visible = true;
            id = 1;
        }

        /// <summary>
        /// Open elements to create Carpet.
        /// </summary>
        private void OpenCarpet()
        {
            HideAll();
            labelParams.Text = "Enter all parameters to draw this fractal";
            labelParams.Visible = true;
            buttonDrawIt.Visible = true;
            depthBox.Visible = true;
            lengthBox.Visible = true;

            lengthLabel.Visible = true;
            depthLabel.Visible = true;
            id = 2;
        }

        /// <summary>
        /// Open elements to create Triangle.
        /// </summary>
        private void OpenTriangle()
        {
            HideAll();
            labelParams.Text = "Enter all parameters to draw this fractal";
            labelParams.Visible = true;
            buttonDrawIt.Visible = true;
            depthBox.Visible = true;
            lengthBox.Visible = true;

            lengthLabel.Visible = true;
            depthLabel.Visible = true;
            id = 3;
        }

        /// <summary>
        /// Open elements to create Set.
        /// </summary>
        private void OpenKantor()
        {
            HideAll();
            labelParams.Text = "Enter all parameters to draw this fractal";
            labelParams.Visible = true;
            buttonDrawIt.Visible = true;
            heightBox.Visible = true;
            distanceBox.Visible = true;
            depthBox.Visible = true;
            lengthBox.Visible = true;

            lengthLabel.Visible = true;
            depthLabel.Visible = true;
            heightLabel.Visible = true;
            distanceLabel.Visible = true;
            id = 4;
            id = 4;
        }

        /// <summary>
        /// Button of choosing fractal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChoose_Click(object sender, EventArgs e)
        {
            int n = fractalList.SelectedIndex;
            switch (n)
            {
                case 0:
                    OpenTree();
                    break;
                case 1:
                    OpenCurve();
                    break;
                case 2:
                    OpenCarpet();
                    break;
                case 3:
                    OpenTriangle();
                    break;
                case 4:
                    OpenKantor();
                    break;
            }
        }

        /// <summary>
        /// Buttoon to draw selected fractal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDrawIt_Click(object sender, EventArgs e)
        {
            canvasForm = new CanvasForm();
            globalId = id;
            bmp = new Bitmap(canvasForm.canvas.Width, canvasForm.canvas.Height);
            gr = Graphics.FromImage(bmp);
            switch (id)
            {
                case 0:
                    DrawTree(canvasForm, gr, bmp);
                    break;
                case 1:
                    DrawCurve(canvasForm, gr, bmp);
                    break;
                case 2:
                    DrawCarpet(canvasForm, gr, bmp);
                    break;
                case 3:
                    DrawTriangle(canvasForm, gr, bmp);
                    break;
                case 4:
                    DrawSet(canvasForm, gr, bmp);
                    break;
            }
        }

        /// <summary>
        /// Method to draw Set.
        /// </summary>
        /// <param name="canvasForm"> Form where to draw. </param>
        /// <param name="gr"> Graphics to draw. </param>
        /// <param name="bmp"> Bitmap to draw. </param>
        private void DrawSet(CanvasForm canvasForm, Graphics gr, Bitmap bmp)
        {
            int depth;
            double length;
            double distance;
            double height;

            length = MyStaticMethods.ParseLength(lengthBox.Text, 800);
            if (length < 0)
            {
                MessageBox.Show("Length was not correct!");
                return;
            }

            depth = MyStaticMethods.ParseDepth(depthBox.Text, 11);
            if (depth < 0)
            {
                MessageBox.Show("Depth was not correct!");
                return;
            }

            distance = MyStaticMethods.ParseDistance(distanceBox.Text);
            if (distance < 0)
            {
                MessageBox.Show("Distance was not correct!");
                return;
            }

            height = MyStaticMethods.ParseHeight(heightBox.Text);
            if (height < 0)
            {
                MessageBox.Show("Height was not correct!");
                return;
            }

            globalLength = length;
            globalDepth = depth;
            globalDistance = distance;
            globalHeight = height;

            canvasForm.Text = "FRACTAL||Kantor's set";
            canvasForm.Show();

            KantorSet fractal = new KantorSet(length, depth, height, distance);
            fractal.Painter(gr, canvasForm);
            canvasForm.canvas.Image = bmp;
        }

        /// <summary>
        /// Method to draw Triangle.
        /// </summary>
        /// <param name="canvasForm"> Form where to draw. </param>
        /// <param name="gr"> Graphics to draw. </param>
        /// <param name="bmp"> Bitmap to draw. </param>
        private void DrawTriangle(CanvasForm canvasForm, Graphics gr, Bitmap bmp)
        {
            int depth;
            double length;

            length = MyStaticMethods.ParseLength(lengthBox.Text, 800);
            if (length < 0)
            {
                MessageBox.Show("Length was not correct!");
                return;
            }

            depth = MyStaticMethods.ParseDepth(depthBox.Text, 11);
            if (depth < 0)
            {
                MessageBox.Show("Depth was not correct!");
                return;
            }

            globalLength = length;
            globalDepth = depth;

            canvasForm.Text = "FRACTAL||Sierpinski's triangle";
            canvasForm.Show();

            SierpinskiTriangle fractal = new SierpinskiTriangle(length, depth);
            fractal.Painter(gr, canvasForm);
            canvasForm.canvas.Image = bmp;
        }

        /// <summary>
        /// Method to draw Carpet.
        /// </summary>
        /// <param name="canvasForm"> Form where to draw. </param>
        /// <param name="gr"> Graphics to draw. </param>
        /// <param name="bmp"> Bitmap to draw. </param>
        private void DrawCarpet(CanvasForm canvasForm, Graphics gr, Bitmap bmp)
        {
            int depth;
            double length;

            length = MyStaticMethods.ParseLength(lengthBox.Text, 800);
            if (length < 0)
            {
                MessageBox.Show("Length was not correct!");
                return;
            }

            depth = MyStaticMethods.ParseDepth(depthBox.Text, 9);
            if (depth < 0)
            {
                MessageBox.Show("Depth was not correct!");
                return;
            }

            globalLength = length;
            globalDepth = depth;

            canvasForm.Text = "FRACTAL||Sierpinski's carpet";
            canvasForm.Show();

            SierpinskiCarpet fractal = new SierpinskiCarpet(length, depth);
            fractal.Painter(gr, canvasForm);
            canvasForm.canvas.Image = bmp;
        }

        /// <summary>
        /// Method to draw Curve.
        /// </summary>
        /// <param name="canvasForm"> Form where to draw. </param>
        /// <param name="gr"> Graphics to draw. </param>
        /// <param name="bmp"> Bitmap to draw. </param>
        private void DrawCurve(CanvasForm canvasForm, Graphics gr, Bitmap bmp)
        {
            int depth;
            double length;

            length = MyStaticMethods.ParseLength(lengthBox.Text, 800);
            if (length < 0)
            {
                MessageBox.Show("Length was not correct!");
                return;
            }

            depth = MyStaticMethods.ParseDepth(depthBox.Text, 11);
            if (depth < 0)
            {
                MessageBox.Show("Depth was not correct!");
                return;
            }

            globalLength = length;
            globalDepth = depth;
            
            canvasForm.Text = "FRACTAL||Koch's curve";
            canvasForm.Show();

            KochCurve fractal = new KochCurve(length, depth);
            fractal.Painter(gr, canvasForm);
            canvasForm.canvas.Image = bmp;
        }

        /// <summary>
        /// Method to draw Tree.
        /// </summary>
        /// <param name="canvasForm"> Form where to draw. </param>
        /// <param name="gr"> Graphics to draw. </param>
        /// <param name="bmp"> Bitmap to draw. </param>
        private void DrawTree(CanvasForm canvasForm, Graphics gr, Bitmap bmp)
        {
            double lAngle;
            double rAngle;
            double koef;
            int depth;
            double length;

            rAngle = MyStaticMethods.ParseAngle(angleRightBox.Text);
            if (rAngle < 0)
            {
                MessageBox.Show("Right angle was not correct!");
                return;
            }

            lAngle = MyStaticMethods.ParseAngle(angleLeftBox.Text);
            if (lAngle < 0)
            {
                MessageBox.Show("Left angle was not correct!");
                return;
            }

            koef = MyStaticMethods.ParseKoef(koefBox.Text);
            if (koef < 0)
            {
                MessageBox.Show("Coefficient was not correct!");
                return;
            }

            depth = MyStaticMethods.ParseDepth(depthBox.Text, 20);
            if (depth < 0)
            {
                MessageBox.Show("Depth was not correct!");
                return;
            }

            length = MyStaticMethods.ParseLength(lengthBox.Text, 300);
            if (length < 0)
            {
                MessageBox.Show("Length was not correct!");
                return;
            }

            globalLangle = lAngle;
            globalRangle = rAngle;
            globalDepth = depth;
            globalLength = length;
            globalKoef = koef;

            canvasForm.Text = "FRACTAL||Pifagor's tree";
            canvasForm.Show();

            PifagorTree fractal = new PifagorTree(length, depth, lAngle, rAngle, koef);
            fractal.Painter(gr, canvasForm);
            canvasForm.canvas.Image = bmp;
        }

        /// <summary>
        /// Making this box invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void angleLeftBox_Click(object sender, EventArgs e)
        {
            angleLeftBox.Text = string.Empty;
        }

        /// <summary>
        /// Making this box invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void angleRightBox_Click(object sender, EventArgs e)
        {
            angleRightBox.Text = string.Empty;
        }

        /// <summary>
        /// Making this box invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void koefBox_Click(object sender, EventArgs e)
        {
            koefBox.Text = string.Empty;
        }

        /// <summary>
        /// Making this box invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void depthBox_Click(object sender, EventArgs e)
        {
            depthBox.Text = string.Empty;
        }

        /// <summary>
        /// Making this box invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lengthBox_Click(object sender, EventArgs e)
        {
            lengthBox.Text = string.Empty;
        }

        /// <summary>
        /// Making this box invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void heightBox_Click(object sender, EventArgs e)
        {
            heightBox.Text = string.Empty;
        }

        /// <summary>
        /// Making this box invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void distanceBox_Click(object sender, EventArgs e)
        {
            distanceBox.Text = string.Empty;
        }

        /// <summary>
        /// Chooses fist color of gradient.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColorEnd_Click(object sender, EventArgs e)
        {
            colorDialogEnd.ShowDialog();
        }

        /// <summary>
        /// Chooses last color of gradient.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColorStart_Click(object sender, EventArgs e)
        {
            colorDialogStart.ShowDialog();
        }
    }
}
