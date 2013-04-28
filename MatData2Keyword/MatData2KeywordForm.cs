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
        private double xMax = double.MinValue;
        private double yMax = double.MinValue;
        private int numberOfDataPoints = 40;

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
                        //xFilter.Input = tmpList;
                        //xFilter.LevelIfLess(TestDataIndices.TensileStress);

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

            this.xMax = double.MinValue;
            this.yMax = double.MinValue;

            GetMaxValuesFromChart();
            GetResultingCurveFromData();
            //GetTrueStrainTrueStress();

            DataPoint dp = Intersection.TwoSeries(matChart.Series[0], matChart.Series[@"2% Offset"]);

            ProcessData process = new ProcessData(matChart);
        }

        /// <summary>
        /// Get the max values from the x and y axis, and resize the chart.
        /// </summary>
        private void GetMaxValuesFromChart()
        {

            foreach (Series s in matChart.Series.Where(s => s.Name.ToUpper() != @"2% OFFSET"))
            {
                DataPoint maxDataPointX = s.Points.FindMaxByValue("X");
                DataPoint maxDataPointY = s.Points.FindMaxByValue("Y");

                if (maxDataPointX.XValue.CompareTo(this.xMax) > 0 )
                {
                    this.xMax = maxDataPointX.XValue;
                }

                if (maxDataPointY.YValues[0].CompareTo(this.yMax) > 0 )
                {
                    this.yMax = maxDataPointY.YValues[0];
                }

            }
        }

        private void AddTwoPercentOffset()
        {

            foreach (Series s in matChart.Series.Where(s => s.Name.ToUpper() == @"2% OFFSET"))
            {
                //MessageBox.Show(@"Already of the chart! :P");
                // the 2% offset series already exists, returning.
                return;
            }

            int series = matChart.Series.Count;

            matChart.Series.Add(@"2% Offset");
            matChart.Series[series].ChartType = SeriesChartType.Line;
            matChart.Series[series].BorderWidth = 1;
            matChart.Series[series].Color = Color.Black;

            double epsilon_start = 0.2, epsilon_end = 0.4;
            double delta = (epsilon_end - epsilon_start) / this.numberOfDataPoints;

            for (int i = 0; i < this.numberOfDataPoints; i++)
            {
                double xv = epsilon_start + i * delta;

                matChart.Series[series].Points.AddXY(xv / 100.0, 210.0E3 * (xv - epsilon_start) / 100.0);   
            }
        
        }

        private void GetTrueStrainTrueStress()
        {
            
            List<StressStrain> ss = new List<StressStrain>();
            Series s = matChart.Series[0];

            foreach(DataPoint point in s.Points)
            {
                ss.Add(new StressStrain { Strain = point.XValue, Stress = point.YValues[0] });
            }

            int series = matChart.Series.Count;

            matChart.Series.Add(@"True Strain True Stress");
            matChart.Series[series].ChartType = SeriesChartType.Line;
            matChart.Series[series].BorderWidth = 5;
            matChart.Series[series].Color = Color.Black;

            foreach (StressStrain xy in ss)
            {
                matChart.Series[series].Points.AddXY(xy.TrueStrain, xy.TrueStress);
            }
            
        }

        private void GetResultingCurveFromData()
        {
            int numOfPoints = int.MinValue;
            List<int> pointCount = new List<int>();

            foreach (Series s in matChart.Series.Where(s => s.Name.ToUpper() != @"2% OFFSET"))
            {
                pointCount.Add(s.Points.Count);
            }

            numOfPoints = 40;
            double delta = this.xMax/(double)numOfPoints;

            List<double> strainData = new List<double>();
            List<double> stressData = new List<double>();

            for (int i = 0; i <= numOfPoints; i++)
            {
                double strainTemp = Math.Round(delta * i, 5);
                List<double> stressTemp = new List<double>();
                strainData.Add(strainTemp);
                foreach (Series s in matChart.Series.Where(s => s.Name.ToUpper() != @"2% OFFSET"))
                {
                    double yy = Interpolation.Linear(s, strainTemp);
                    stressTemp.Add(yy);
                }
                stressData.Add(stressTemp.Max());
            }

            int series = matChart.Series.Count;

            matChart.Series.Add(@"XXXXX");
            matChart.Series[series].ChartType = SeriesChartType.Line;
            matChart.Series[series].BorderWidth = 5;
            matChart.Series[series].Color = Color.Black;

            for (int i = 0; i <= numOfPoints; i++)
            {
                matChart.Series[series].Points.AddXY(strainData[i], stressData[i]);
            }

        
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
