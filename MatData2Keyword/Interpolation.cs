using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;


namespace MatData2Keyword
{
    public static class Interpolation
    {
        public static double Linear( Series series, double xValue )
        {
            double yValue = 0.0;

            double xMinValue = series.Points.FindMinByValue("X").XValue;
            if( xValue < xMinValue )
            {
                IEnumerable<DataPoint> points = series.Points.Where(p => p.XValue == xMinValue).Take(1);
                foreach (DataPoint point in points)
                {
                    yValue = point.YValues[0];
                }
                return yValue;
            }

            double xMaxValue = series.Points.FindMaxByValue("X").XValue;
            if (xValue > xMaxValue)
            {
                
                IEnumerable<DataPoint> points = series.Points.Where(p => p.XValue == xMaxValue).Take(1);
                foreach (DataPoint point in points)
                {
                    yValue = point.YValues[0];
                }
                return yValue;
            }
            
            

            return yValue;
        }
    }
}
