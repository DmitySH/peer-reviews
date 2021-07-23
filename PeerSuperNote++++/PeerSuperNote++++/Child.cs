using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeerSuperNote____
{
    public partial class Child : Form
    {
        private int childFormNumber;
        public Child()
        {
            InitializeComponent();
        }

        private void ShowNewForm(Form childForm)
        {
            childForm.MdiParent = this;
            childForm.Text = "Окно " + childFormNumber;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }
    }
}
