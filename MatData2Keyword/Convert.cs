using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatData2Keyword
{
    class Convert
    {
        public List<TestData> EngineeringData { set; private get; }
        public List<TestData> TrueData { private set; get; }

        public Convert()
        {
            this.TrueData = new List<TestData>();
        }

        public void Run()
        { 
        
        }

    }
}
