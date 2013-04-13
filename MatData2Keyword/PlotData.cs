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

        public PlotData(List<TestData> testData, Chart chart)
        {
            chart.Series.Clear(); //ensure that the chart is empty
            chart.Series.Add("Series0");
            chart.Series[0].ChartType = SeriesChartType.Line;
            chart.Legends.Clear();

            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{F2}";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "{F2}";
            chart.ChartAreas[0].AxisY.Minimum = 0.0;

            foreach (TestData data in testData)
            {
                chart.Series[0].Points.AddXY(data.TensileStrain,data.TensileStress);
            }
        
        }

    }
}
