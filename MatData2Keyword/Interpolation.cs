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


            // The xVlaue exists on the Series.Points
            IEnumerable<DataPoint> pts = series.Points.Where(p => p.XValue == xValue);
            if(pts.Count() > 0)
            {
                foreach (DataPoint point in pts)
                {
                    yValue = point.YValues[0];
                }
                return yValue;
            }

            // Interpolate between two x values. Since the  xValue was not found.
            // ASSUME: the graph always groups.
            pts = series.Points.Where(p => p.XValue < xValue).OrderByDescending(p => p.XValue).Take(1);

            DataPoint pointN1 = null;
            DataPoint pointN2 = null;

            foreach (DataPoint point in pts)
            {
                pointN1 = point;
            }

            pts = series.Points.Where(p => p.XValue > xValue).OrderBy(p => p.XValue).Take(1);
            foreach (DataPoint point in pts)
            {
                pointN2 = point;
            }

            double x1x0 = pointN2.XValue - pointN1.XValue;
            double xx0 = xValue - pointN1.XValue;
            double y1y0 = pointN2.YValues[0] - pointN1.YValues[0];

            // http://en.wikipedia.org/wiki/Linear_interpolation
            yValue = pointN1.YValues[0] + (y1y0) * (xx0 / x1x0);

            return yValue;
        }
    }
}
