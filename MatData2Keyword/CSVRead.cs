using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MatData2Keyword
{
    class CSVRead
    {

        public String[] Headers { private set; get; }
        public String[] SubHeaders { private set; get; }
        public float[,] Data { private set; get; }
        public int NumColumns { private set; get; }
        public int NumLines { private set; get; }

        public CSVRead(StreamReader fileData)
        {
        
        }

    }
}
