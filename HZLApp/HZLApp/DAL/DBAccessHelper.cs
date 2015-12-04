using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using HZLApp.Entity;
using System.Data;

namespace HZLApp.DAL
{
  public  class DBAccessHelper
    {

      LogHelper log = new LogHelper();
      private static readonly string StrCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.ConnectionStrings["HZLACCESSDB"].ConnectionString + ";Persist Security Info=True;Jet OLEDB:Database Password=hzl123";

      
      
        public  string strCon()
      {
          string strCon = "";
        try
        {
            strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.ConnectionStrings["HZLACCESSDB"].ConnectionString + ";Persist Security Info=True;Jet OLEDB:Database Password=hzl123";

        }
        catch (Exception ex) { log.wrirteLog("数据库操作","链接是否有效",ex.Message); }
        return strCon;
    }
         // string strCon = StrCon();
      //AccessHelper.conn_str;

      /// <summary>
      /// 登录是否有效
      /// </summary>
      /// <param name="ue"></param>
      /// <returns></returns>
      public bool IsVaileLogin(H_UserEntity ue)
      {
           bool result = true;
           try
           {


               string SqlStr = "SELECT * FROM H_User where UserLoginName=@ul and UserPWD=@pwd";
               OleDbParameter[] prams = {
                                                 new OleDbParameter("@ul", ue.UserLoginName),
                                                 new OleDbParameter("@pwd", ue.UserPWD)
                                             };
               DataSet ds = AccessHelper.ExecuteDataSet(strCon(), SqlStr, prams);
               if (ds == null) result = false;
               else if (ds.Tables[0].Rows.Count == 0) result = false;
               else result = true;
               UserPublicClass.LoginValue = ue.UserLoginName;
               UserPublicClass.NameValue = ds.Tables[0].Rows[0]["UserName"].ToString();
               UserPublicClass.PWDValue = ds.Tables[0].Rows[0]["UserPWD"].ToString();
           }
           catch (Exception ex)
           {
               log.wrirteLog("数据库操作", "登录是否有效", ex.Message); 
               result = false;
           }
           return result;

      }

      /// <summary>
      /// 修改密码
      /// </summary>
      /// <param name="ue"></param>
      /// <returns></returns>
      public int UpdatePWD(string userlogin,string newpwd)
      {
          int result = 0;
          try
          {


              string SqlStr = "UPDATE   H_User SET UserPWD='" + newpwd + "',DoTime='" + DateTime.Now.ToString() + "' where UserLoginName='" + userlogin + "' ";
              OleDbParameter[] prams = {
                                                 new OleDbParameter("@ul", userlogin),
                                                 new OleDbParameter("@pwd", newpwd),
                                                     new OleDbParameter("@u3", DateTime.Now.ToString())
                                             };
             result=  AccessHelper.ExecuteNonQuery(strCon(), SqlStr, prams);
               }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "修改密码", ex.Message); 
              result = 0;
          }
          return result;

      }

      public DataSet GetDSPara()
      {
          try
          {


              string SqlStr = "SELECT * FROM H_Style ";
              OleDbParameter[] prams = {
                                             
                                             };
              DataSet ds = AccessHelper.ExecuteDataSet(strCon(), SqlStr, null);
              if (ds == null) return null;
              else return ds;
              
              }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "获取类型Style", ex.Message); 
              return null;
          }
      }

      public string GetMaxID(string Perstr,string StrTable)
      {
          try
          {


              string SqlStr = "SELECT Max(HID) as HID FROM " + StrTable + " ";
              OleDbParameter[] prams = {
                                             
                                             };
              DataSet ds = AccessHelper.ExecuteDataSet(strCon(), SqlStr, null);
              if (ds == null) return Perstr+"001";
              else if (ds.Tables[0].Rows.Count <= 0) return Perstr + "001";
              else
              {
                  string StrMaxID = ds.Tables[0].Rows[0][0].ToString().Replace(Perstr,"");
                  int IntMaxID = int.Parse(StrMaxID)+1;
                  return Perstr + IntMaxID.ToString("000");
              }

          }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "获取最大编号", ex.Message); 
              return Perstr+"001";
          }
      }

      /// <summary>
      /// 参数增加
      /// </summary>
      /// <param name="ue"></param>
      /// <returns></returns>
      public bool InsertH_Para(string StrTable,string filename,string HID)
      {
          bool result = true;
          try
          {


              string SqlStr = "Insert into " + StrTable + " (HID,HName,HAdress,DoTime) " +
                  " values('" + HID + "','" + filename + "','" + filename + "','" + DateTime.Now.ToString() + "') ";

            int  intresult = AccessHelper.ExecuteNonQuery(strCon(),SqlStr,null);
            if (intresult == 0) result = false;
            else result = true;

            }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "增加各类基础参数", ex.Message); 
              result = false;
          }
          return result;

      }

      /// <summary>
      /// 参数增加
      /// </summary>
      /// <param name="ue"></param>
      /// <returns></returns>
      public bool DeleteH_Para(string StrTable,   string HID)
      {
          bool result = true;
          try
          {


              string SqlStr = "Delete from " + StrTable + "  where HID='" + HID + "' ";

              int intresult = AccessHelper.ExecuteNonQuery(strCon(), SqlStr, null);
              if (intresult == 0) result = false;
              else result = true;

          }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "删除各类基础参数", ex.Message);
              result = false;
          }
          return result;

      }


     
      /// <summary>
      /// 公司
      /// </summary>
      /// <param name="ue"></param>
      /// <returns></returns>
      public bool InsertCompany(H_CompanyEntity hce)
      {
          bool result = true;
          try
          {
              string SqlStr = "  " +
                  " Insert into H_Company (CID,CName,CTelphone,CColor,CGlass, CIsGlass, CRemarks,CAdress, DoTime) " +
                  " values(@c1,@c2,@c3,@c4,@c5,@c6,@c7,@c8,@c9) " ;
              OleDbParameter[] prams = {
                                                     new OleDbParameter("@c1", hce.CID),
                                                     new OleDbParameter("@c2", hce.CName),
                                                       new OleDbParameter("@c3", hce.CTelphone),
                                                     new OleDbParameter("@c4", hce.CColor),
                                                       new OleDbParameter("@c5", hce.CGlass),
                                                     new OleDbParameter("@c6", hce.CIsGlass),
                                                       new OleDbParameter("@c7", hce.CRemarks),
                                                     new OleDbParameter("@c8", hce.CAdress),
                                                      new OleDbParameter("@c9", DateTime.Now.ToString())
                                                 };


              int intresult = AccessHelper.ExecuteNonQuery(strCon(), SqlStr, prams);
              if (intresult == 0) result = false;
              else result = true;

          }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "公司资料增加", ex.Message); 
              result = false;
          }
          return result;

      }

      public int DeleteCompany(H_CompanyEntity hce)
      {
          int intResult = 0;
          string strSql = "Delete from  H_Company where CID=@c1";
          OleDbParameter[] prams = {
                                             new OleDbParameter("@c1", hce.CID)
                                         };
          intResult = AccessHelper.ExecuteNonQuery(strCon(), strSql, prams);
          return intResult;
      }

      public DataSet GetDSCompany(string CID)
      {
          try
          {
               string SqlStr = "SELECT * FROM H_Company ";
              if(CID!="")
              SqlStr += "  WHERE CID='"+CID+"'";
              OleDbParameter[] prams = {
                                             
                                             };
              DataSet ds = AccessHelper.ExecuteDataSet(strCon(), SqlStr, null);
              if (ds == null) return null;
              else return ds;

          }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "公司资料", ex.Message); 
              return null;
          }
      }

      public int DeleteBusiness_Main(H_Bus_MainEntity hce)
      {
          int intResult = 0;
          string strSql = "Delete from  H_Business_Main where CCID=@c1";
          OleDbParameter[] prams = {
                                             new OleDbParameter("@c1", hce.CCID)
                                         };
          intResult = AccessHelper.ExecuteNonQuery(strCon(), strSql, prams);
          return intResult;
      }

      public int DeleteBusiness_Detail(H_Bus_DetailEntity hce)
      {
          int intResult = 0;
          string strSql = "Delete from  H_Business_Detail where CCID=@c1 AND CDetailID=@c2";
          OleDbParameter[] prams = {
                                             new OleDbParameter("@c1", hce.CCID),
                                              new OleDbParameter("@c2", hce.CDetailID)
                                         };
          intResult = AccessHelper.ExecuteNonQuery(strCon(), strSql, prams);
          return intResult;
      }
      public int DeleteBusiness_Detail(string CCID,string CDetailID)
      {
          int intResult = 0;
          string strSql = "Delete from  H_Business_Detail where CCID=@c1 AND CDetailID=@c2";
          OleDbParameter[] prams = {
                                             new OleDbParameter("@c1", CCID),
                                              new OleDbParameter("@c2", CDetailID)
                                         };
          intResult = AccessHelper.ExecuteNonQuery(strCon(), strSql, prams);
          return intResult;
      }

      /// <summary>
      /// 公司
      /// </summary>
      /// <param name="ue"></param>
      /// <returns></returns>
      public bool InsertBusiness_Detail(H_Bus_DetailEntity hce)
      {
          bool result = true;
          try
          {
              string SqlStr = "  " +
                  " Insert into H_Business_Detail (CCID,CDetailID,CWidth,CHigh,CTS, CPFS,CDAdress, DoTime) " +
                  " values(@c1,@c2,round(@c3,2),round(@c4,2),@c5,@c6,@c7,@8) ";
              OleDbParameter[] prams = {
                                                     new OleDbParameter("@c1", hce.CCID),
                                                     new OleDbParameter("@c2", hce.CDetailID),
                                                       new OleDbParameter("@c3", hce.CWidth),
                                                     new OleDbParameter("@c4", hce.CHigh),
                                                      new OleDbParameter("@c5", hce.CTS),
                                                      new OleDbParameter("@c6", hce.CPFS),
                                                      new OleDbParameter("@c7", hce.CDAdress),
                                                       new OleDbParameter("@c8", DateTime.Now.ToString())   };


              int intresult = AccessHelper.ExecuteNonQuery(strCon(), SqlStr, prams);
              if (intresult == 0) result = false;
              else result = true;

          }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "公司明细", ex.Message); 
              result = false;
          }
          return result;

      }

      /// <summary>
      /// 明细主表
      /// </summary>
      /// <param name="ue"></param>
      /// <returns></returns>
      public bool InsertBusiness_Main(H_Bus_MainEntity hcm)
      {
          bool result = true;
          try
          {
              string SqlStr = "  " +
                  " Insert into H_Business_Main (CCID, ZTS, TotalSquare, Price, TotalPrice, DoTime) " +
                  " values(@c1,@c2,@c3,@c4,@c5,@c6) ";
              OleDbParameter[] prams = {
                                                     new OleDbParameter("@c1", hcm.CCID),
                                                     new OleDbParameter("@c2", hcm.ZTS),
                                                       new OleDbParameter("@c3", hcm.TotalSquare),
                                                     new OleDbParameter("@c4", hcm.Price),
                                                       new OleDbParameter("@c5", hcm.TotalPrice),
                                                       new OleDbParameter("@c6", DateTime.Now.ToString())   };


              int intresult = AccessHelper.ExecuteNonQuery(strCon(), SqlStr, prams);
              if (intresult == 0) result = false;
              else result = true;

          }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "明细主表H_Business_Main", ex.Message);
              result = false;
          }
          return result;

      }


      public DataSet GetDSBusiness_Main(string CCID)
      {
          try
          {
              string SqlStr = "SELECT * FROM H_Business_Main ";
              if (CCID != "")
                  SqlStr += "  WHERE CCID='" + CCID + "'";
             
              DataSet ds = AccessHelper.ExecuteDataSet(strCon(), SqlStr, null);
              if (ds == null) return null;
              else return ds;

          }
          catch (Exception ex)
          {
              log.wrirteLog("数据库操作", "Business_Main" + CCID, ex.Message);
              return null;
          }
      }
      public DataSet GetDSBusiness_Detail(string CCID,
          string[] detailid)
      {
          try
          {
              string di="";
              for (int i = 0; i < 6; i++)
              {
                  if (detailid[i] != "") di = di+"," + detailid[i];
              }
              if (di.Length > 0) di = di.Substring(1);
              string SqlStr = "SELECT * FROM H_Business_Detail ";
              if (CCID != "")
                  SqlStr += "  WHERE CCID='" + CCID + "'";
              if(di.Length>0)
                  SqlStr += "  AND CDetailID NOT IN( '" + di + "')";
            
              DataSet ds = AccessHelper.ExecuteDataSet(strCon(), SqlStr, null);
              if (ds == null) return null;
              else return ds;

          }
          catch (Exception ex)
          {
              log.wrirteLog("数据库查询操作", "Detail" + CCID, ex.Message);
              return null;
          }
      }



     #region 分页
        /// <summary>
        /// 分页使用
        /// </summary>
        /// <param name="query"></param>
        /// <param name="passCount"></param>
        /// <returns></returns>
        private static string recordID(string query, int passCount)
        {
           
          
            using (OleDbConnection m_Conn = new OleDbConnection(StrCon))
            {
                m_Conn.Open();
                OleDbCommand cmd = new OleDbCommand(query, m_Conn);
                string result = string.Empty;
                using (OleDbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (passCount < 1)
                        {
                            result += "," +dr[0];
                                //dr.GetInt32(0);
                        }
                        passCount--;
                    }
                }
                m_Conn.Close();
                m_Conn.Dispose();
                return result.Substring(1);
            }
        }
        /// <summary>
        /// ACCESS高效分页
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页容量</param>
        /// <param name="strKey">主键</param>
        /// <param name="showString">显示的字段</param>
        /// <param name="queryString">查询字符串，支持联合查询</param>
        /// <param name="whereString">查询条件，若有条件限制则必须以where 开头</param>
        /// <param name="orderString">排序规则</param>
        /// <param name="pageCount">传出参数：总页数统计</param>
        /// <param name="recordCount">传出参数：总记录统计</param>
        /// <returns>装载记录的DataTable</returns>
        public  DataTable ExecutePager(int pageIndex, int pageSize, string strKey,string showString, string queryString, string whereString, string orderString, out int pageCount, out int recordCount)
        {
            
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = strKey+" asc ";
            using (OleDbConnection m_Conn = new OleDbConnection(StrCon))
            {
                m_Conn.Open();
                string myVw = string.Format(" ( {0} ) tempVw ", queryString);
                OleDbCommand cmdCount = new OleDbCommand(string.Format(" select count(*) as recordCount from {0} {1}", myVw, whereString), m_Conn);

                recordCount = Convert.ToInt32(cmdCount.ExecuteScalar());

                if ((recordCount % pageSize) > 0)
                    pageCount = recordCount / pageSize + 1;
                else
                    pageCount = recordCount / pageSize;
                OleDbCommand cmdRecord;
                if (pageIndex == 1)//第一页
                {
                    cmdRecord = new OleDbCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, whereString, orderString), m_Conn);
                }
                else if (pageIndex > pageCount)//超出总页数
                {
                    cmdRecord = new OleDbCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, "where 1=2", orderString), m_Conn);
                }
                else
                {
                    int pageLowerBound = pageSize * pageIndex;
                    int pageUpperBound = pageLowerBound - pageSize;
                    string recordIDs = recordID(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageLowerBound, strKey, myVw, whereString, orderString), pageUpperBound);
                    cmdRecord = new OleDbCommand(string.Format("select {0} from {1} where {2} in ({3}) order by {4} ", showString, myVw,strKey, recordIDs, orderString), m_Conn);

                }
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdRecord);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                m_Conn.Close();
                m_Conn.Dispose();
            return dt;
            }
        }
        #endregion

        //public bool InsertDfdjzkOrder(H_CompanyEntity hce, out string errmsg)
      //{
      //    bool result = true;
      //    errmsg = "";
      //    string strCon = strCon();
      //    OleDbConnection con = new OleDbConnection(strCon);
      //    try
      //    {
      //        con.Open();
      //        OleDbTransaction tra = con.BeginTransaction(); //创建事务，开始执行事务
      //        if (DeleteCompany(tra, hce) != 1)
      //        {
      //            tra.Rollback();
      //            return false;
      //        }
      //        DfdjhmDA daDfdjhm = new DfdjhmDA();
      //        if (daDfdjhm.UpdateDfdjhm(tra, etyDfdjhm) != 1)
      //        {
      //            tra.Rollback();
      //            return false;
      //        }

      //        tra.Commit();//关闭事务
      //    }
      //    catch (Exception ex)
      //    {
      //        errmsg = ex.Message;
      //        return false;
      //    }
      //    finally
      //    {
      //        con.Close();
      //    }

      //    return result;

      //}

      //public int InsertCby(CbyEntity objCbyEntity)
      //{
      //    int intResult = 0;
      //    string strSql = "insert into cby (dh,xm)  values (@dh,@xm)";
      //    OleDbParameter[] prams = {
      //                                       new OleDbParameter("@dh", objCbyEntity.Dh),
      //                                       new OleDbParameter("@xm", objCbyEntity.Xm)
      //                                   };
      //    intResult = AccessHelper.ExecuteNonQuery(AccessHelper.conn_str, strSql, prams);
      //    return intResult;
      //}


  }
}
