using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZLApp.Entity
{
  public  class H_CompanyEntity
    {
        [DataField("CID")]
      public string CID { set; get; }
        [DataField("CName")]
        public string CName { set; get; }
        [DataField("CTelphone")]
        public string CTelphone { set; get; }

        [DataField("CColor")]
        public string CColor { set; get; }
        [DataField("CGlass")]
        public string CGlass { set; get; }
        [DataField("CIsGlass")]
        public bool CIsGlass { set; get; }
       
        [DataField("CRemarks")]
        public string CRemarks { set; get; }
        [DataField("CAdress")]
        public string CAdress { set; get; }
        [DataField("DoTime")]
        public string DoTime { set; get; }

        public override string ToString()
        {
            return string.Format("CID:{1}{0}CName:{2}{0}CTelphone:{3}{0}CColor:{4}{0}CGlass:{5}{0}CIsGlass:{6}{0}CRemarks:{7}{0}CAdress:{8}{0}DoTime:{9}",
                Environment.NewLine, CID,CName,CTelphone,CColor,CGlass,
                                 CIsGlass, CRemarks,CAdress, DoTime);
        } 
    }
}
