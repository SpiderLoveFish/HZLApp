using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZLApp.Entity
{
    public class H_Bus_MainEntity
    {
        [DataField("CCID")]
        public string CCID { set; get; }
        [DataField("ZTS")]
        public decimal ZTS { set; get; }
        [DataField("TotalSquare")]
        public decimal TotalSquare { set; get; }

        [DataField("Price")]
        public decimal Price { set; get; }
        [DataField("TotalPrice")]
        public decimal TotalPrice { set; get; }
        
        [DataField("DoTime")]
        public string DoTime { set; get; }

        public override string ToString()
        {
            return string.Format("CCID:{1}{0}ZTS:{2}{0}TotalSquare:{3}{0}Price:{4}{0}TotalPrice:{5}{0}DoTime:{6}",
                Environment.NewLine, CCID, ZTS, TotalSquare, Price, TotalPrice, DoTime);
        } 
    }
}
