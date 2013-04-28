﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

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

                this.AddTwoPercentOffset();
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

                        Filter xFilter = new Filter()
                        {
                            Input = csvFile.Samples,
                        };
                        xFilter.SkipIfLess(TestDataIndices.TensileStrain);
                        List<TestData> tmpList = new List<TestData>(xFilter.Output);
                        xFilter.Input = tmpList;
                        xFilter.LevelIfLess(TestDataIndices.TensileStress);

                        PlotData plt = new PlotData(xFilter.Output,matChart, chartHeaders,
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
        
            if(matChart.Series.Count == 0)
            {
                MessageBox.Show(@"No data in memory. Please, Import some test data");
                return;
            }

            GetMaxValuesFromChart();

            ProcessData process = new ProcessData(matChart);
        }

        /// <summary>
        /// Get the max values from the x and y axis, and resize the chart.
        /// </summary>
        private void GetMaxValuesFromChart()
        {
            double xMax = double.MinValue;
            double yMax = double.MinValue;

            foreach (Series s in matChart.Series.Where(s => s.Name.ToUpper() != @"2% OFFSET"))
            {
                DataPoint maxDataPointX = s.Points.FindMaxByValue("X");
                DataPoint maxDataPointY = s.Points.FindMaxByValue("Y");

                if (maxDataPointX.XValue.CompareTo(xMax) > 0 )
                {
                    xMax = maxDataPointX.XValue;
                }

                if (maxDataPointY.YValues[0].CompareTo(yMax) > 0 )
                {
                    yMax = maxDataPointY.YValues[0];
                }

                double yy = Interpolation.Linear(s,100.0);
                yy = Interpolation.Linear(s, -100.0);
                yy = Interpolation.Linear(s, 0.001);

            }

            MessageBox.Show(String.Format(@"{0} {1}",xMax,yMax));
        }

        private void AddTwoPercentOffset()
        {

            int series = matChart.Series.Count;

            matChart.Series.Add(@"2% Offset");
            matChart.Series[series].ChartType = SeriesChartType.Line;
            matChart.Series[series].BorderWidth = 1;
            matChart.Series[series].Color = Color.Black;

            matChart.Series[series].Points.AddXY(0.2 / 100.0, 0.0);
            matChart.Series[series].Points.AddXY(0.35 / 100.0, 210.0E3*0.15 / 100.0);
        
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
