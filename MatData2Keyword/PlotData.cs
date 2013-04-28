using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace MatData2Keyword
{
    class PlotData
    {

        public PlotData(List<TestData> testData, Chart chart, Headers headers, String chartLegend)
        {
            // If the legend alread exists on the chart, skip it.
            foreach (Series s in chart.Series.Where(s => s.Name.ToUpper() == chartLegend.ToUpper()))
            {
                return;
            }

            chart.Visible = true;

            int series = chart.Series.Count;

            //chart.Series.Clear(); //ensure that the chart is empty
            chart.Series.Add(chartLegend);
            chart.Series[series].ChartType = SeriesChartType.Line;
            chart.Series[series].BorderWidth = 3;
            //chart.Legends.Clear();

            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{F2}";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "{F2}";
            chart.ChartAreas[0].AxisY.Minimum = 0.0;

            chart.ChartAreas[0].AxisX.Title = headers[TestDataIndices.TensileStrain];
            chart.ChartAreas[0].AxisY.Title = headers[TestDataIndices.TensileStress];

            foreach (TestData data in testData)
            {
                chart.Series[series].Points.AddXY(data.TensileStrain, data.TensileStress);
            }
        
        }

    }
}
