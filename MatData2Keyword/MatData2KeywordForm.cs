using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MatData2Keyword
{
    public partial class MatData2KeywordForm : Form
    {

        Stream fileData = null;

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

                foreach (String filename in csvImportFileDialog.FileNames)
                {
                    OpenAndReadFile(filename);
                }
                
            }
        }

        private void OpenAndReadFile(String filename)
        {
            fileData = null;

            fileData = null;
        }
    }
}
