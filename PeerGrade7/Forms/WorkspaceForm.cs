using System.ComponentModel;
using System.Windows.Forms;

namespace Forms
{

    public partial class WorkspaceForm : Form
    {
        public WorkspaceForm()
        {
            InitializeComponent();
            IsMdiContainer = true;
        }

        /// <summary>
        /// You can not close this.
        /// </summary>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
        }
    }
}
