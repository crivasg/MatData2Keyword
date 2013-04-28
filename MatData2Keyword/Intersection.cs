using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace MatData2Keyword
{
    public static class Intersection
    {
        public static DataPoint TwoSeries(Series series1, Series series2)
        {
            DataPoint dataPoint = null;

            // where the series 2 is greater than series1

            foreach (DataPoint dp in series1.Points)
            {
                double yy = Interpolation.Linear(series2, dp.XValue);

                if (yy > dp.YValues[0])
                {
                    dataPoint = dp;
                    break;
                }
                
            }


            return dataPoint;
        }
    }
}
