using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MatData2Keyword
{
    class CSVRead
    {
        public List<TestData> testSamples { private set; get ;}

        public String[] Headers { private set; get; }
        public String[] SubHeaders { private set; get; }  // subheaders contains the units.
        public int NumColumns { private set; get; }
        public int NumLines { private set; get; }

        private char[] delimiterChars = { ',', ';', ':' };

        public CSVRead(StreamReader fileData)
        {
            testSamples = new List<TestData>();

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

                TestData testData = new TestData
                {
                    Index = i,
                    Time = double.Parse(aux[(int)TestDataIndices.Time]),
                    Extension = double.Parse(aux[(int)TestDataIndices.Extension]),
                    TensileExtension = double.Parse(aux[(int)TestDataIndices.TensileExtension]),
                    Load = double.Parse(aux[(int)TestDataIndices.Load]),
                    TensileStrain = double.Parse(aux[(int)TestDataIndices.TensileStrain]),
                    TensileStress = double.Parse(aux[(int)TestDataIndices.TensileStress])
                };

                testSamples.Add(testData);

            }
            

        }

    }
}
