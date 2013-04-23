using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatData2Keyword
{
    class Filter
    {
        public List<TestData> Input { set; private get; }
        public List<TestData> Output { private set; get; }

        public Filter()
        {
            this.Output = new List<TestData>();
        
        }

        public void SkipIfLess(TestDataIndices index)
        {

            double prvValue = double.MinValue;

            // removes the negative value of the stain data using a cool Linq query
            foreach (TestData data in this.Input.Where(p => p[index] > 0.0))
            {
                if (data[index] > prvValue)
                {
                    prvValue = data[index];
                    this.Output.Add(data);
                }
            }
        }

        public void LevelIfLess(TestDataIndices index)
        {
            double prvValue = double.MinValue;

            foreach (TestData data in this.Input)
            {
                if (data[index] > prvValue)
                {
                    prvValue = data[index];
                }
                else
                {
                    data[index] = prvValue;
                }
                this.Output.Add(data);
            
            }
        }

    }
}
