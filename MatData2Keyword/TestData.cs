﻿using System;
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

    enum TestDataColumns
    {
        Time = "Time",
        Extension = "Extension",
        TensileExtension = "Tensile Extension",
        Load = "Load",
        TensileStrain = " Tensile Strain",
        TensileStress = " Tensile Stress"
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

    }
}
