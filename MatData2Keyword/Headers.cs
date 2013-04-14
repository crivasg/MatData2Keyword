using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatData2Keyword
{
    class Headers
    {

        public String[] Titles { set; private get; }
        public String[] Units { set; private get;}

        public String this[TestDataIndices index]
        {
            get
            {
                if (this.Units.Length == 0 )
                {
                    return String.Copy(this.Titles[(int)index]);   
                }
                return String.Concat(this.Titles[(int)index], " ", this.Units[(int)index]);
            }
            
        }

        public Headers()
        { 
        
        }

    }
}
