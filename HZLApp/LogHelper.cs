using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HZLApp
{
  public  class LogHelper
    {

      public void wrirteLog(string loacte, string action, string messsage)
      {
          try
          {
              string LogPath = AppDomain.CurrentDomain.BaseDirectory + "\\LogFile\\" + DateTime.Now.ToString("yyyyMM");
              string logfile = "\\" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
              if (!Directory.Exists(LogPath))
              { Directory.CreateDirectory(LogPath); }

              if (!File.Exists(LogPath + logfile))
              {
                  File.CreateText(LogPath + logfile);
              }
              using (FileStream fs = new FileStream(LogPath + logfile, FileMode.Create))
              {
                  using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                  {
                      DateTime dt = DateTime.Now;
                      sw.WriteLine(dt + loacte + action);
                      sw.WriteLine(messsage);
                      sw.WriteLine("");
                      sw.Flush();
                      sw.Close();
                  }
              }
          }
          catch { }
      }
    }
}
