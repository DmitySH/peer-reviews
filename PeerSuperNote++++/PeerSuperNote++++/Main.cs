using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace PeerSuperNote____
{

    public partial class Main : Form
    {
        private int numberOfTab = 1;
        private readonly List<RichTextBox> textBoxes = new List<RichTextBox>();
        private readonly List<string> paths = new List<string>();

        public Main()
        {
            InitializeComponent();

            InitializeContextMenu();
            InitializeTab();
        }


        private void InitializeContextMenu()
        {
            ToolStripMenuItem selectAll = new ToolStripMenuItem("Select all");
            ToolStripMenuItem cutSelected = new ToolStripMenuItem("Cut");
            ToolStripMenuItem copySelected = new ToolStripMenuItem("Copy");
            ToolStripMenuItem paste = new ToolStripMenuItem("Paste");
            ToolStripMenuItem chooseFormat = new ToolStripMenuItem("Format");

            contextMenu.Items.AddRange(new ToolStripItem[] { selectAll, cutSelected, copySelected, paste, chooseFormat });

            selectAll.Click += (sender, e) => textBoxes[tabs.SelectedIndex].SelectAll();
            cutSelected.Click += (sender, e) => textBoxes[tabs.SelectedIndex].Cut();
            copySelected.Click += (sender, e) => textBoxes[tabs.SelectedIndex].Copy();
            paste.Click += (sender, e) => textBoxes[tabs.SelectedIndex].Paste();

            chooseFormat.DropDownItems.AddRange(new ToolStripItem[] { italicContext, boldContext, underlinedContext, crossedContext, removeFormatContext });
            chooseFormat.Click += format_Click;
        }

        private void InitializeTab()
        {
            RichTextBox rb = new RichTextBox();
            rb.Dock = DockStyle.Fill;

            rb.ContextMenuStrip = contextMenu;
            textBoxes.Add(rb);
            tabs.TabPages.Add($"New file {numberOfTab}");
            tabs.TabPages[tabs.TabPages.Count - 1].Controls.Add(rb);
            tabs.SelectedTab = tabs.TabPages[tabs.TabPages.Count - 1];

            paths.Add("notsaved");
            numberOfTab++;
        }

        private void closeSelected_Click(object sender, EventArgs e)
        {
            if (tabs.TabPages.Count > 0)
            {
                textBoxes.RemoveAt(tabs.SelectedIndex);
                paths.RemoveAt(tabs.SelectedIndex);
                tabs.TabPages.RemoveAt(tabs.SelectedIndex);
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.FileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RichTextBox rb = new RichTextBox();
                    rb.Dock = DockStyle.Fill;
                    if (openFileDialog.FileName.EndsWith("txt"))
                    {
                        rb.Text = File.ReadAllText(openFileDialog.FileName);
                    }
                    else if (openFileDialog.FileName.EndsWith("rtf"))
                    {
                        rb.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        MessageBox.Show("Error! Can't open the file.");
                        return;
                    }

                    textBoxes.Add(rb);
                    tabs.TabPages.Add(openFileDialog.SafeFileName);
                    tabs.TabPages[tabs.TabPages.Count - 1].Controls.Add(rb);
                    tabs.SelectedTab = tabs.TabPages[tabs.TabPages.Count - 1];

                    paths.Add(openFileDialog.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error! Can't open the file.");
            }
        }

        static string FindName(string path)
        {
            string[] lines = path.Split(Path.DirectorySeparatorChar);
            return lines[lines.Length - 1];
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            if (tabs.TabPages.Count > 0)
            {
                try
                {
                    if (!paths[tabs.SelectedIndex].Equals("notsaved"))
                    {
                        if (paths[tabs.SelectedIndex].EndsWith(".txt"))
                        {
                            File.WriteAllText(paths[tabs.SelectedIndex], textBoxes[tabs.SelectedIndex].Text);
                        }
                        else
                        {
                            textBoxes[tabs.SelectedIndex].SaveFile(paths[tabs.SelectedIndex], RichTextBoxStreamType.RichText);
                        }
                    }
                    else
                    {
                        saveFileAs_Click(sender, e);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error! Can't save the file.");
                }
            }
        }

        private void saveFileAs_Click(object sender, EventArgs e)
        {
            if (tabs.TabPages.Count > 0)
            {
                saveFileDialog.FileName = tabs.TabPages[tabs.SelectedIndex].Text;
                saveFileDialog.Filter = "txt files (*.txt)|*.txt| rtf files (*.rtf)|*.rtf";
                saveFileDialog.FilterIndex = 1;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName.EndsWith("txt"))
                    {
                        File.WriteAllText(saveFileDialog.FileName, textBoxes[tabs.SelectedIndex].Text);
                    }
                    else
                    {
                        textBoxes[tabs.SelectedIndex].SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    tabs.TabPages[tabs.SelectedIndex].Text = FindName(saveFileDialog.FileName);
                    paths[tabs.SelectedIndex] = saveFileDialog.FileName;
                }
            }
        }

        private void saveAllFiles_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabs.TabPages.Count; i++)
            {
                tabs.SelectedTab = tabs.TabPages[i];
                saveFile_Click(sender, e);
            }
        }

        private void createFileTab_Click(object sender, EventArgs e) => InitializeTab();

        private void createFileWindow_Click(object sender, EventArgs e) => new Main().Show();

        private void SetFont(Font font, string name)
        {
            if (tabs.TabPages.Count <= 0 || textBoxes[tabs.SelectedIndex].SelectionFont == null)
            {
                return;
            }

            if (textBoxes[tabs.SelectedIndex].SelectionFont.Italic && name == "Italic")
            {
                textBoxes[tabs.SelectedIndex].SelectionFont =
                    new Font(textBoxes[tabs.SelectedIndex].SelectionFont,
                        textBoxes[tabs.SelectedIndex].SelectionFont.Style ^ FontStyle.Italic);
                return;
            }

            if (textBoxes[tabs.SelectedIndex].SelectionFont.Bold && name == "Bold")
            {
                textBoxes[tabs.SelectedIndex].SelectionFont =
                    new Font(textBoxes[tabs.SelectedIndex].SelectionFont,
                        textBoxes[tabs.SelectedIndex].SelectionFont.Style ^ FontStyle.Bold);
                return;
            }

            if (textBoxes[tabs.SelectedIndex].SelectionFont.Underline && name == "Underline")
            {
                textBoxes[tabs.SelectedIndex].SelectionFont =
                    new Font(textBoxes[tabs.SelectedIndex].SelectionFont,
                        textBoxes[tabs.SelectedIndex].SelectionFont.Style ^ FontStyle.Underline);
                return;
            }

            if (textBoxes[tabs.SelectedIndex].SelectionFont.Strikeout && name == "Strikeout")
            {
                textBoxes[tabs.SelectedIndex].SelectionFont =
                    new Font(textBoxes[tabs.SelectedIndex].SelectionFont,
                        textBoxes[tabs.SelectedIndex].SelectionFont.Style ^ FontStyle.Strikeout);
                return;
            }

            textBoxes[tabs.SelectedIndex].SelectionFont = font;
        }

        private void italic_Click(object sender, EventArgs e)
        {
            if (tabs.TabPages.Count <= 0 || textBoxes[tabs.SelectedIndex].SelectionFont == null)
            {
                return;
            }

            SetFont(new Font(textBoxes[tabs.SelectedIndex].SelectionFont,
                FontStyle.Italic | textBoxes[tabs.SelectedIndex].SelectionFont.Style), "Italic");
        }

        private void bold_Click(object sender, EventArgs e)
        {
            if (tabs.TabPages.Count <= 0 || textBoxes[tabs.SelectedIndex].SelectionFont == null)
            {
                return;
            }

            SetFont(new Font(textBoxes[tabs.SelectedIndex].SelectionFont,
                    FontStyle.Bold | textBoxes[tabs.SelectedIndex].SelectionFont.Style), "Bold");
        }

        private void underlined_Click(object sender, EventArgs e)
        {
            if (tabs.TabPages.Count <= 0 || textBoxes[tabs.SelectedIndex].SelectionFont == null)
            {
                return;
            }

            SetFont(new Font(textBoxes[tabs.SelectedIndex].SelectionFont,
                    FontStyle.Underline | textBoxes[tabs.SelectedIndex].SelectionFont.Style), "Underline");
        }

        private void crossed_Click(object sender, EventArgs e)
        {
            if (tabs.TabPages.Count <= 0 || textBoxes[tabs.SelectedIndex].SelectionFont == null)
            {
                return;
            }

            SetFont(
                new Font(textBoxes[tabs.SelectedIndex].SelectionFont,
                    FontStyle.Strikeout | textBoxes[tabs.SelectedIndex].SelectionFont.Style), "Strikeout");
        }

        private void removeFormat_Click(object sender, EventArgs e)
        {
            if (tabs.TabPages.Count <= 0 || textBoxes[tabs.SelectedIndex].SelectionFont == null)
            {
                return;
            }

            textBoxes[tabs.SelectedIndex].SelectionFont =
                new Font(textBoxes[tabs.SelectedIndex].SelectionFont, FontStyle.Regular);
        }

        //TODO REMOVE
        private void test_Click(object sender, EventArgs e)
        {
            MessageBox.Show(textBoxes[0].Text.ToString());
        }
        //TODO REMOVE

        private void format_Click(object sender, EventArgs e)
        {
            if (tabs.TabPages.Count <= 0 || textBoxes[tabs.SelectedIndex].SelectionFont == null)
            {
                return;
            }

            italic.Checked = textBoxes[tabs.SelectedIndex].SelectionFont.Italic;

            bold.Checked = textBoxes[tabs.SelectedIndex].SelectionFont.Bold;

            underlined.Checked = textBoxes[tabs.SelectedIndex].SelectionFont.Underline;

            crossed.Checked = textBoxes[tabs.SelectedIndex].SelectionFont.Strikeout;

            italicContext.Checked = textBoxes[tabs.SelectedIndex].SelectionFont.Italic;

            boldContext.Checked = textBoxes[tabs.SelectedIndex].SelectionFont.Bold;

            underlinedContext.Checked = textBoxes[tabs.SelectedIndex].SelectionFont.Underline;

            crossedContext.Checked = textBoxes[tabs.SelectedIndex].SelectionFont.Strikeout;
        }

        private void colorScheme_Click(object sender, EventArgs e)
        {
            ForeColor = Color.Black;
            foreach (TabPage tab in tabs.TabPages)
            {
                tab.BackColor = Color.Black;
                tab.ForeColor = Color.Black;
            }
        }

        private void closeApp_Click(object sender, EventArgs e) => Close();

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < tabs.TabPages.Count; i++)
            {
                tabs.SelectedTab = tabs.TabPages[i];
                try
                {
                    if (paths[tabs.SelectedIndex].Equals("notsaved"))
                    {
                        DialogResult res =
                            MessageBox.Show($"{tabs.TabPages[i].Text} has not been saved. Do you want to save it?", "Not saved",
                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (res == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            break;
                        }
                        if (res == DialogResult.Yes)
                        {
                            saveFileAs_Click(sender, e);
                        }
                    }
                    else
                    {
                        if (tabs.TabPages[tabs.SelectedIndex].Text.EndsWith("txt"))
                        {
                            if (!textBoxes[tabs.SelectedIndex].Text.Equals(File.ReadAllText(paths[tabs.SelectedIndex])))
                            {
                                DialogResult res =
                                    MessageBox.Show($"File {tabs.TabPages[i].Text} has unsaved changes. Do you want to save it?", "Not saved changes",
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                if (res == DialogResult.Cancel)
                                {
                                    e.Cancel = true;
                                    break;
                                }
                                if (res == DialogResult.Yes)
                                {
                                    saveFile_Click(sender, e);
                                }
                            }
                        }
                        else
                        {
                            RichTextBox rTemp = new RichTextBox();
                            rTemp.LoadFile(paths[tabs.SelectedIndex], RichTextBoxStreamType.RichText);

                            if (!rTemp.Rtf.Equals(textBoxes[tabs.SelectedIndex].Rtf))
                            {
                                DialogResult res =
                                    MessageBox.Show($"File {tabs.TabPages[i].Text} has unsaved changes. Do you want to save it?", "Not saved changes",
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                if (res == DialogResult.Cancel)
                                {
                                    e.Cancel = true;
                                    break;
                                }
                                if (res == DialogResult.Yes)
                                {
                                    saveFile_Click(sender, e);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error! Can't save the file.");
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < textBoxes.Count; i++)
            {
                if (!paths[i].Equals("notsaved"))
                {
                    try
                    {
                        if (paths[i].EndsWith(".txt"))
                        {
                            File.WriteAllText(paths[i], textBoxes[i].Text);
                        }
                        else
                        {
                            textBoxes[i].SaveFile(paths[i], RichTextBoxStreamType.RichText);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Error! Can't save the file {tabs.TabPages[i].Text}.");
                    }
                }
            }
        }

        private void Uncheck()
        {
            foreach (var item in autosave.DropDownItems)
            {
                (item as ToolStripMenuItem).Checked = false;
            }
        }

        private void autosave_Click(object sender, EventArgs e)
        {
            switch (timer.Interval)
            {
                case 10000:
                    Uncheck();
                    noSave.Checked = true;
                    break;
                case 15000:
                    Uncheck();
                    sec15.Checked = true;
                    break;
                case 30000:
                    Uncheck();
                    sec30.Checked = true;
                    break;
                case 60000:
                    Uncheck();
                    sec60.Checked = true;
                    break;
                case 300000:
                    Uncheck();
                    sec300.Checked = true;
                    break;
                case 600000:
                    Uncheck();
                    sec600.Checked = true;
                    break;
            }
        }

        private void noSave_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer.Interval = 10000;
        }

        private void sec15_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Interval = 15000;
        }

        private void sec30_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Interval = 30000;
        }

        private void sec60_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Interval = 60000;
        }

        private void sec300_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Interval = 300000;
        }

        private void sec600_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Interval = 600000;
        }
    }
}

    
