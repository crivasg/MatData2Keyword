using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatData2Keyword
{
    class Convert
    {
        public List<StressStrain> EngineeringData { set; private get; }
        public List<StressStrain> TrueData { private set; get; }

        public Convert()
        {
            this.TrueData = new List<StressStrain>();
        }

        public void Run()
        { 
        
        }

    }
}
