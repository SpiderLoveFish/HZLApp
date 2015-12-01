using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HZLApp
{
    public static class UserPublicClass
    {

      public static string MD5(string pwd)
      {
          MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
          string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(pwd)), 4, 8);
          t2 = t2.Replace("-", "");

          t2 = t2.ToLower();

          return t2;
      
      }

      private static string loginvalue;
      /// <summary>
      /// 传递登录人员账号
      /// </summary>
      public static string LoginValue
      {
          set
          {
              loginvalue = value;
          }
          get
          {
              return loginvalue;
          }
      }

      private static string namevalue;
      /// <summary>
      /// 传递登录人员账号
      /// </summary>
      public static string NameValue
      {
          set
          {
              namevalue = value;
          }
          get
          {
              return namevalue;
          }
      }

      private static string pwdvalue;
      /// <summary>
      /// 传递登录人员账号
      /// </summary>
      public static string PWDValue
      {
          set
          {
              pwdvalue = value;
          }
          get
          {
              return pwdvalue;
          }
      }


      private static string compnyvalue;
      /// <summary>
      /// 传递登录人员账号
      /// </summary>
      public static string CompanyValue
      {
          set
          {
              compnyvalue = value;
          }
          get
          {
              return compnyvalue;
          }
      }
    }
}
