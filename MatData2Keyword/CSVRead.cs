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
        public String[] SubHeaders { private set; get; }  // subheaders contains the units.
        public float[,] Data { private set; get; }
        public int NumColumns { private set; get; }
        public int NumLines { private set; get; }

        private char[] delimiterChars = { ',', ';', ':' };

        public CSVRead(StreamReader fileData)
        {

            String temp = fileData.ReadLine();
            this.Headers = temp.Split(delimiterChars);
            this.NumColumns = Headers.Count();
            
            // read again the csv to check if there are subheaders.

            temp = fileData.ReadLine();
            String[] aux = temp.Split(delimiterChars);
            double myDouble = 0.0;
            bool status = double.TryParse(aux[0], out myDouble);

            if (!status)
            {
                SubHeaders = temp.Split(delimiterChars);
            }
            

        }

    }
}
