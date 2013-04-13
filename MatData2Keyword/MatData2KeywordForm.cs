using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatData2Keyword
{
    public partial class MatData2KeywordForm : Form
    {
        public MatData2KeywordForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(csvImportFileDialog.ShowDialog() == DialogResult.OK)
            {
                
            }
        }
    }
}
