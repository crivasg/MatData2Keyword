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
        public double[,] Data { private set; get; }
        public int NumColumns { private set; get; }
        public int NumLines { private set; get; }

        private char[] delimiterChars = { ',', ';', ':' };

        public CSVRead(StreamReader fileData)
        {
            this.NumColumns = 0;
            this.NumLines = 0;

            bool hasUnits = false;

            String temp = fileData.ReadLine();
            this.Headers = temp.Split(delimiterChars);
            this.NumColumns = Headers.Count();
            
            // read again the csv to check if there are subheaders.

            temp = fileData.ReadLine();
            String[] aux = temp.Split(delimiterChars);
            double myDouble = 0.0;
            bool status = double.TryParse(aux[0], out myDouble);  // check if the 2nd line contains
                                                                  // a string or a double. 
            if (!status)
            {
                SubHeaders = temp.Split(delimiterChars);
                hasUnits = true;
            }
            else
            {
                ++this.NumLines;
            }

            while( (temp = fileData.ReadLine() ) != null )
            {
                if (temp.Length > 0)
                {
                    ++this.NumLines;
                }
            }

            // Init the data array.
            this.Data = new double[this.NumLines, this.NumColumns];
            fileData.BaseStream.Seek(0,0);

            temp = fileData.ReadLine();
            if(hasUnits)
            {
                temp = fileData.ReadLine();
            }

            for ( int i = 0 ; i < this.NumLines ; ++i )
            {
                temp = fileData.ReadLine();
                aux = temp.Split(delimiterChars);
                for (int j = 0; j < this.NumColumns; ++j )
                {
                    double tmpDouble = 0.0;
                    bool try2parse = double.TryParse(aux[j], out tmpDouble);
                    if (try2parse)
                    {
                        this.Data[i, j] = tmpDouble;
                    }
                }
            }
            

        }

    }
}
