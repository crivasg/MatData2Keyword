using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatData2Keyword
{
    class StressStrain
    {
        public double Strain { get; set; }
        public double Stress { get; set; }

        public double TrueStrain {
            get
            {
                return Math.Log(1.0 + this.Strain); 
            }
        }

        public double TrueStress
        {
            get
            {
                return this.Stress * (1.0 + this.Strain);
            }
        }

        public StressStrain()
        { 
        
        }

    }
}
