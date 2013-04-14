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

        StreamReader fileData = null;

        public MatData2KeywordForm()
        {
            InitializeComponent();
            ChartReset();

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

        private void ChartReset()
        {
            matChart.Series.Clear();
        }

        private void OpenAndReadFile(String filename)
        {
            fileData = null;

            try
            {
                if(  (fileData = File.OpenText(filename)) != null )
                {
                    using(fileData)
                    {
                        CSVRead csvFile = new CSVRead(fileData);
                        fileData.Close();

                        Headers chartHeaders = new Headers()
                        {
                            Titles = csvFile.Headers,
                            Units = csvFile.SubHeaders
                        };

                        PlotData plt = new PlotData(csvFile.Samples,matChart, chartHeaders,
                            Path.GetFileNameWithoutExtension(filename));
                    }
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                fileData = null;
            }
            
        }


        private void ProcessDataFromChart()
        { 
        
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartReset();
        }

        private void processStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessDataFromChart();
        }
    }
}
