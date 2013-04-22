using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatData2Keyword
{

    enum TestDataIndices
    {
        Time = 0,
        Extension,
        TensileExtension,
        Load,
        TensileStrain,
        TensileStress
    };
    
    class TestData
    {

        public int Index { get; set; }
        public double Time { get; set; }
        public double Extension { get; set; }
        public double TensileExtension { get; set; }
        public double Load { get; set; }
        public double TensileStrain { get; set; }
        public double TensileStress { get; set; }

        public double this[TestDataIndices index]
        {
            get
            {
                double v = 0.0;
                switch (index)
                {
                    case TestDataIndices.Time:
                        v = this.Time;
                        break;
                    case TestDataIndices.Extension:
                        v = this.Extension;
                        break;
                    case TestDataIndices.TensileExtension:
                        v = this.TensileExtension;
                        break;
                    case TestDataIndices.Load:
                        v = this.Load;
                        break;
                    case TestDataIndices.TensileStrain:
                        v = this.TensileStrain;
                        break;
                    case TestDataIndices.TensileStress:
                        v = this.TensileStress;
                        break;
                    default:
                        break;
                }
                return v;
            }

        }

    }
}
