using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZLApp.Entity
{
    public class H_Bus_DetailEntity
    {
        [DataField("CCID")]
        public string CCID { set; get; }
        [DataField("CDetailID")]
        public string CDetailID { set; get; }
        [DataField("CWidth")]
        public decimal CWidth { set; get; }

        [DataField("CHigh")]
        public decimal CHigh { set; get; }
        [DataField("CTS")]
        public decimal CTS { set; get; }
        [DataField("CPFS")]
        public decimal CPFS { set; get; }
        [DataField("CDAdress")]
        public string CDAdress { set; get; }

     
        [DataField("DoTime")]
        public string DoTime { set; get; }

        public override string ToString()
        {
            return string.Format("CCID:{1}{0}CDetailID:{2}{0}CWidth:{3}{0}CHigh:{4}{0}CTS:{5}{0}CPFS:{6}{0}DoTime:{7}{0}DoTime:{8}",
                Environment.NewLine, CCID, CDetailID, CWidth, CHigh, CTS,
                                 CPFS,CDAdress, DoTime);
        } 
    }
}
