﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatData2Keyword
{
    class Filter
    {
        public List<TestData> Input { set; private get; }
        public List<TestData> Output { private set; get; }
        public TestDataIndices Index { set; private get; }

        public Filter()
        {
            this.Output = new List<TestData>();
        
        }

        public void Run()
        { 
        
        }

    }
}