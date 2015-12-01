using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZLApp.Entity
{
    [Serializable]
    public class H_UserEntity : DataEntityBase
    {
        [DataField("UserLoginName")]
        public string UserLoginName { set; get; }
        [DataField("UserName")]
        public string UserName { set; get; }
        [DataField("UserPWD")]
        public string UserPWD { set; get; }
        [DataField("DoTime")]
        public string DoTime { set; get; }
        
        public override string ToString()
         {
             return string.Format("UserLoginName:{1}{0}UserName:{2}{0}UserPWD:{3}{0}DoTime:{4}", Environment.NewLine, UserLoginName,
                                  UserName, UserPWD, DoTime);
         } 
    }
}
