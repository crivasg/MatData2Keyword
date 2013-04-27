using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatData2Keyword
{
    class Convert
    {
        public List<StressStrian> EngineeringData { set; private get; }
        public List<StressStrian> TrueData { private set; get; }

        public Convert()
        {
            this.TrueData = new List<StressStrian>();
        }

        public void Run()
        { 
        
        }

    }
}
