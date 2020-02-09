using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkMove
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtSourcePath.Text))
                return;

            if (string.IsNullOrEmpty(this.txtDestPath.Text))
                return;

            if (!Directory.Exists(txtSourcePath.Text))
                Directory.CreateDirectory(txtDestPath.Text);

            this.SourcePath = txtSourcePath.Text;
            this.DestinationPath = txtDestPath.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        public string SourcePath { get; private set; }
        public string DestinationPath { get; private set; }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            var txt = sender as TextBox;

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.None;
            else
                e.Effect = DragDropEffects.Move;
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            var txt = sender as TextBox;
            var dir = ((string[]) e.Data.GetData(DataFormats.FileDrop)).FirstOrDefault();

            if (!Directory.Exists(dir))
                e.Effect = DragDropEffects.None;
            else
            {
                e.Effect = DragDropEffects.Move;
                txt.Text = dir;
            }
        }
    }
}
