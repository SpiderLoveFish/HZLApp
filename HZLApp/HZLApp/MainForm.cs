using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HZLApp.Entity;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.Configuration;
using System.IO;

namespace HZLApp
{
    public partial class MainForm : Form
    {
        #region 初始化参数
        
       
        DAL.DBAccessHelper db = new DAL.DBAccessHelper();
        private string CompanyID="001";
        public string ChooseCompanyID = "";
        public string ChooseParaID = "";
        private static int PageInt = 1;
        public string Pic = "";
        ZoomPic zp = new ZoomPic();
        LogHelper log = new LogHelper();
        private decimal StausSum = 0;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            BindCombox();
            VisableNextPer(false,false);
            InitAPP("Y");
           
        }

        private void btnsaveprint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.ShowDialog(); 
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            KJVisable(false);
            Bitmap _NewBitmap = new Bitmap(groupBox1.Width, groupBox1.Height);
            groupBox1.DrawToBitmap(_NewBitmap, new Rectangle(0, 0, _NewBitmap.Width, _NewBitmap.Height));
            e.Graphics.DrawImage(_NewBitmap, 0, 0, _NewBitmap.Width, _NewBitmap.Height);
            string strname = "";
            try{
                strname = ConfigurationManager.AppSettings.Get("SaveFilePath");
                if (!Directory.Exists(strname))
                { Directory.CreateDirectory(strname); }
            }
            catch{
                strname=AppDomain.CurrentDomain.BaseDirectory + "ShareImage" ;
                if (!Directory.Exists(strname))
                { Directory.CreateDirectory(strname); }
            }
            
            strname += "\\" + txtname.Text + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            
            _NewBitmap.Save(strname);
            KJVisable(true);
        }

        #region 控件属性
        void VisableNextPer(bool per, bool next)
        { llnext.Enabled = next; llper.Enabled = per; }
        /// <summary>
        /// 打印控件属性控制
        /// </summary>
        /// <param name="bl"></param>
        void KJVisable(bool bl)
        {
            panelNextPer.Visible=
            btnsave.Visible = btnAdd.Visible =
            bt1delete.Visible =  bt2delete.Visible =  bt3delete.Visible =
            bt4delete.Visible =  bt5delete.Visible =  bt6delete.Visible = bl;
                         
        }
        #endregion
        

        void BindCombox()
        {
            DataSet ds = db.GetDSPara();
            try {
                DataTable dtglass = new DataTable();
                dtglass.Columns.Add("SID");
                dtglass.Columns.Add("SName");
                foreach (DataRow dw in ds.Tables[0].Select("Style='Glass'"))
                {
                    DataRow dr1 = dtglass.NewRow();
                    dr1["SID"] = dw["SID"];
                    dr1["SName"] = dw["SName"];
                    dtglass.Rows.Add(dr1);
                }
                DataTable dtremarks = new DataTable();
                dtremarks.Columns.Add("SID");
                dtremarks.Columns.Add("SName");
                foreach (DataRow dw in ds.Tables[0].Select("Style='Remarks'"))
                {
                    DataRow dr1 = dtremarks.NewRow();
                    dr1["SID"] = dw["SID"];
                    dr1["SName"] = dw["SName"];
                    dtremarks.Rows.Add(dr1);
                }
                

                cbglass.DataSource = dtglass;
                cbglass.ValueMember = "SID";
                cbglass.DisplayMember = "SName";

                cbadress.DataSource = dtremarks;
                cbadress.ValueMember = "SID";
                cbadress.DisplayMember = "SName";
            }
            catch (Exception ex)
            {
                log.wrirteLog("主页面", "绑定combox-style", ex.Message);
            }
        
        }

        void ShowAPP()
        {
            
            DataSet ds = db.GetDSCompany(CompanyID);
            if (ds == null) return;
            else {
                if (ds.Tables[0].Rows.Count <= 0) return;
                else
                {
                    foreach (DataRow dw in ds.Tables[0].Rows)
                    {
                       
                        txtcolor.Text =dw["CColor"].ToString();
                        cbadress.SelectedValue = dw["CAdress"].ToString();
                        cbglass.SelectedValue = dw["CGlass"].ToString();
                        txtbz.Text = dw["CRemarks"].ToString();
                        txtphone.Text=dw["CTelphone"].ToString();
                        txtname.Text = dw["CName"].ToString();
                      for (int i = 0; i < cksf.Items.Count; i++) 
                        {
                            string flag = "否";
                            if (dw["CIsGlass"].ToString() == "True") flag = "是";
                            else flag = "否";
                            if (cksf.Items[i].ToString() == flag)
                          cksf.SetItemChecked(i, true);  
     
                        }
                      CompanyID = dw["CID"].ToString();
                    }
                }
            }
            

        }

        void ShowAPPDetail(int BeginPage)
        {
            try {
                string strSql = " Select ID, CCID,CDetailID,CWidth,CHigh,CTS,CPFS,CDAdress From H_Business_Detail  ";
                string strShow = "ID,CCID,CDetailID,CWidth,CHigh,CTS,CPFS,CDAdress";
                int PageCount, RecordCount;
                string strWhere = " where CCID='" + CompanyID + "'";
                DataTable lsdt = db.ExecutePager(BeginPage, 6, "ID", strShow, strSql, strWhere, " CDetailID DESC ", out PageCount, out RecordCount);
                if (PageCount > 1)
                {
                    if (BeginPage == 1)
                        VisableNextPer(false, true);
                    else
                    {
                        if (BeginPage == PageCount)
                            VisableNextPer(true, false);
                        else VisableNextPer(true, true);
                    }
                }
                if (PageCount == 1)
                    VisableNextPer(false, false);
                lbcount.Text = "一共有" + PageCount + "页，" + RecordCount + "项";
                int i = 1;
                foreach (DataRow dw in lsdt.Rows)
                {
                    loadpic(i, dw);
                    i++;

                }
            }
            catch (Exception ex)
            {
                log.wrirteLog("主页面", "显示明细", ex.Message);
            }
        
        }

        void ShowAppMain()
        {
            DataSet ds = db.GetDSBusiness_Main(CompanyID);
            if (ds == null) return;
            else
            {
                if (ds.Tables[0].Rows.Count <= 0) return;
                else
                {
                    foreach (DataRow dw in ds.Tables[0].Rows)
                    {

                        txtzts.Text = dw["ZTS"].ToString();
                        txtdj.Text = dw["Price"].ToString();
                        txtzje.Text = dw["TotalPrice"].ToString();
                        txtzpfs.Text = dw["TotalSquare"].ToString();
                        
                        
                    }
                }
            }
        }
        

        void loadpic(int index,DataRow dw)
        {
            switch (index)
            {
                case 1:
                    p1.Visible = true; p11.Visible = true;bt1delete.Visible=true;
                    txt1cd.Text = dw["CWidth"].ToString();
                    txt1gd.Text = dw["CHigh"].ToString();
                    txt1dh.Text = dw["CDetailID"].ToString();
                    txt1ts.Text = dw["CTS"].ToString();
                    txt1pfs.Text = dw["CPFS"].ToString();
                    pictureBox1.Tag = dw["CDAdress"].ToString();
                    pictureBox1.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    
                    break;
                case 2:
                    p2.Visible = true; p21.Visible = true;bt2delete.Visible=true;
                    txt2cd.Text = dw["CWidth"].ToString();
                    txt2gd.Text = dw["CHigh"].ToString();
                    txt2dh.Text = dw["CDetailID"].ToString();
                    txt2ts.Text = dw["CTS"].ToString();
                    txt2pfs.Text = dw["CPFS"].ToString();
                    pictureBox2.Tag = dw["CDAdress"].ToString();
                    pictureBox2.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                case 3:
                    p3.Visible = true; p31.Visible = true;bt3delete.Visible=true;
                    txt3cd.Text = dw["CWidth"].ToString();
                    txt3gd.Text = dw["CHigh"].ToString();
                    txt3dh.Text = dw["CDetailID"].ToString();
                    txt3ts.Text = dw["CTS"].ToString();
                    txt3pfs.Text = dw["CPFS"].ToString();
                    pictureBox3.Tag = dw["CDAdress"].ToString();
                    pictureBox3.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                case 4:
                    p4.Visible = true; p41.Visible = true;bt4delete.Visible=true;
                    txt4cd.Text = dw["CWidth"].ToString();
                    txt4gd.Text = dw["CHigh"].ToString();
                    txt4dh.Text = dw["CDetailID"].ToString();
                    txt4ts.Text = dw["CTS"].ToString();
                    txt4pfs.Text = dw["CPFS"].ToString();
                    pictureBox4.Tag = dw["CDAdress"].ToString();
                    pictureBox4.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                case 5:
                    p5.Visible = true; p51.Visible = true;bt5delete.Visible=true;
                    txt5cd.Text = dw["CWidth"].ToString();
                    txt5gd.Text = dw["CHigh"].ToString();
                    txt5dh.Text = dw["CDetailID"].ToString();
                    txt5ts.Text = dw["CTS"].ToString();
                    txt5pfs.Text = dw["CPFS"].ToString();
                    pictureBox5.Tag = dw["CDAdress"].ToString();
                    pictureBox5.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                case 6:
                    p6.Visible = true; p61.Visible = true;bt6delete.Visible=true;
                    txt6cd.Text = dw["CWidth"].ToString();
                    txt6gd.Text = dw["CHigh"].ToString();
                    txt6dh.Text = dw["CDetailID"].ToString();
                    txt6ts.Text = dw["CTS"].ToString();
                    txt6pfs.Text = dw["CPFS"].ToString();
                    pictureBox6.Tag = dw["CDAdress"].ToString();
                    pictureBox6.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                default: break;
            }
            
        }



        void InitAPP(string IsInit)
        {
            bool YesInit=false;
            if (IsInit == "Y") YesInit = false;
            else YesInit = true;
            p1.Visible = YesInit; p11.Visible = YesInit;
            p2.Visible = YesInit; p21.Visible = YesInit;
            p3.Visible = YesInit; p31.Visible = YesInit;
            p4.Visible = YesInit; p41.Visible = YesInit;
            p5.Visible = YesInit; p51.Visible = YesInit;
            p6.Visible = YesInit; p61.Visible = YesInit;

            txt1dh.Text = txt2dh.Text = txt3dh.Text =
                                      txt4dh.Text = txt5dh.Text = txt6dh.Text = "";
                               
        }

        /// <summary>
        /// 基本数据格式验证
        /// </summary>
        /// <returns></returns>
        string vailedata()
        {
            string MSG = "";
            if (!IsNumber(txt1cd.Text.Trim()) || !IsNumber(txt1gd.Text.Trim())||
              !IsNumber(txt2cd.Text.Trim()) || !IsNumber(txt2gd.Text.Trim())||
               !IsNumber(txt3cd.Text.Trim()) || !IsNumber(txt3gd.Text.Trim())||
               !IsNumber(txt4cd.Text.Trim()) || !IsNumber(txt4gd.Text.Trim())||
               !IsNumber(txt5cd.Text.Trim()) || !IsNumber(txt5gd.Text.Trim())||
               !IsNumber(txt6cd.Text.Trim()) || !IsNumber(txt6gd.Text.Trim()))
                 {
                MSG = "数字！";
            }

            if (!IsHandsetTelpone(txtphone.Text.Trim()))
            {
               MSG="电话号码不对，请输入13，14，15，17开头。";
            }
            return MSG;
        }

        /// <summary>
        /// 保存打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsave_Click(object sender, EventArgs e)
        {
            //再算一次
            decimal bb = StausSum + TxtToDec(txt1pfs.Text) + TxtToDec(txt2pfs.Text)
                   + TxtToDec(txt3pfs.Text) + TxtToDec(txt4pfs.Text) + TxtToDec(txt5pfs.Text) +
                   TxtToDec(txt6pfs.Text) ;
            txtzpfs.Text = bb.ToString();
            txtzje.Text = ComputerTotalSum(bb, TxtToDec(txtdj.Text.Trim()));
               

            string messge = vailedata();
            if (messge != "")
            {
                MessageBox.Show(messge);
                return;
            }

            if (!Insert_Company())
            {
                MessageBox.Show("公司信息保存失败！请查看LOG文件反馈!");
                return;
            }  
            string DetailID = "";
            for (int i = 1; i <= 6; i++)
            {
               
                 if(i==1&&txt1dh.Text.Trim()!="")
                     DetailID=txt1dh.Text.Trim();
                 if (i == 2 && txt2dh.Text.Trim() != "")
                     DetailID = txt2dh.Text.Trim();
                 if (i == 3 && txt3dh.Text.Trim() != "")
                     DetailID = txt3dh.Text.Trim();
                 if (i == 4 && txt4dh.Text.Trim() != "")
                     DetailID = txt4dh.Text.Trim();
                 if (i == 5 && txt5dh.Text.Trim() != "")
                     DetailID = txt5dh.Text.Trim();
                 if (i == 6 && txt6dh.Text.Trim() != "")
                     DetailID = txt6dh.Text.Trim();
                 if (DetailID == "") continue;
                     if (!InsertBusinessDetail(i))
                    {
                        MessageBox.Show(DetailID+"明细保存失败！请查看LOG文件反馈!");
                        return;
                    }
                     DetailID = "";

            } 
            if (!InsertBusinessMain())
            {
                MessageBox.Show("BusinessMain保存失败！请查看LOG文件反馈!");
                return;
            }

                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
        }

        /// <summary>
        /// 选取数据临时存放
        /// </summary>
        void LoadTemppic(int index)
        {
            switch (index)
            {
                case 1:
                     p11.Visible = true; p1.Visible = true;
                    txt1dh.Text = ChooseParaID;
                    pictureBox1.Tag = Pic;
                    pictureBox1.Load(GetStrPicPath(Pic));
                    break;
                case 2: p21.Visible = true; p2.Visible = true;
                    txt2dh.Text = ChooseParaID;
                    pictureBox2.Tag = Pic;
                    pictureBox2.Load(GetStrPicPath(Pic)); 
                    break;
                case 3: p31.Visible = true; p3.Visible = true;
                    txt3dh.Text = ChooseParaID;
                     pictureBox3.Tag = Pic;
                     pictureBox3.Load(GetStrPicPath(Pic)); 
                    break;
                case 4: p41.Visible = true; p4.Visible = true;
                    txt4dh.Text = ChooseParaID;
                    pictureBox4.Tag = Pic;
                    pictureBox4.Load(GetStrPicPath(Pic)); 
                    break;
                case 5: p51.Visible = true; p5.Visible = true;
                    txt5dh.Text = ChooseParaID;
                    pictureBox5.Tag = Pic;
                    pictureBox5.Load(GetStrPicPath(Pic)); 
                    break;
                case 6: p61.Visible = true; p6.Visible = true;
                    txt6dh.Text = ChooseParaID;
                    pictureBox6.Tag = Pic;
                    pictureBox6.Load(GetStrPicPath(Pic)); break;
                default: break;
            
            }
        }

        private void cksf_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (cksf.CheckedItems.Count > 0)
            {

                for (int i = 0; i < cksf.Items.Count; i++)
                {

                    if (i != e.Index)
                    {

                        this.cksf.SetItemCheckState(i,

                        System.Windows.Forms.CheckState.Unchecked);

                    }

                }

            }

        }

     
        /// <summary>
        /// 获取控件内容
        /// </summary>
        bool Insert_Company()
        {
            if(CompanyID=="")
            CompanyID = db.GetMaxID("Cpy", "H_Company");
            H_CompanyEntity hce = new H_CompanyEntity();
            hce.CID = CompanyID;
            hce.CName = txtname.Text.Trim();
            bool iscksf = false;
            try
            {
                if (cksf.SelectedItem.ToString() == "是")
                    iscksf = true;
                else if (cksf.SelectedItem.ToString() == "否")
                    iscksf = false;
            }
            catch { }
            hce.CIsGlass = iscksf;
            hce.CColor = txtcolor.Text.Trim();
            hce.CAdress = cbadress.SelectedValue.ToString();
            hce.CGlass = cbglass.SelectedValue.ToString();
            hce.CRemarks = txtbz.Text.Trim();
            hce.CTelphone = txtphone.Text.Trim();

            db.DeleteCompany(hce);
              
                    if (!db.InsertCompany(hce))
                    {
                        MessageBox.Show("公司资料保存失败！！");
                        return false;
                    }
                    else
                        return true;
                
             
        }

        private void btnnext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        /// <summary>
        /// 选择客户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lkkh_Click(object sender, EventArgs e)
        {

            UserWindows.ChooseCompany cc = new UserWindows.ChooseCompany();
            cc.Owner = this;
            cc.ShowDialog();
            if(cc.DialogResult == DialogResult.OK)
            if (ChooseCompanyID != CompanyID)
            {
                CompanyID = ChooseCompanyID;
                ChooseCompanyID = "";
                ShowAPP();
                ShowAPPDetail(1);
                ShowAppMain();
            }


        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            CompanyID = "";
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            //db.DeleteBusiness_Detail(CompanyID,txt1dh.Text);

            if (txtname.Text.Trim() == "")
            {
                MessageBox.Show("请选择公司或者新增公司再选择！");
                return;
            }
            WindowAdd wa = new WindowAdd();
            wa.Owner = this;
            wa.ShowDialog();
            if (wa.DialogResult == DialogResult.OK)
            {
                int i = 1;
                while (i == 1)
                {
                    try
                    {
                        if (txt1dh.Text == "")
                            LoadTemppic(1);
                        else if (txt2dh.Text == "")
                            LoadTemppic(2);
                        else if (txt3dh.Text == "")
                            LoadTemppic(3);
                        else if (txt4dh.Text == "")
                            LoadTemppic(4);
                        else if (txt5dh.Text == "")
                            LoadTemppic(5);
                        else if (txt6dh.Text == "")
                            LoadTemppic(6);
                        else
                        {
                            if (MessageBox.Show("当前页数据已满，请先保存当前数据，如果已经保存，请点确定继续？" + pictureBox1.Tag.ToString(), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                InitAPP("Y");
                                i = 1; continue;
                            }
                        }
                        i = 2;
                    }
                    catch { i = 2; }
                }


            }
        }


        bool InsertBusinessDetail(int index)
        {
            try
            {
                H_Bus_DetailEntity HBD = new H_Bus_DetailEntity();
                HBD.CCID = CompanyID;
                switch (index)
                {
                    #region 保存明细
                    
                  
                    case 1: 
                         HBD.CDetailID = txt1dh.Text.Trim();
                HBD.CWidth = TxtToDec(txt1cd.Text.Trim());
                HBD.CHigh = TxtToDec(txt1gd.Text.Trim());
                HBD.CTS = TxtToDec(txt1ts.Text.Trim());
                HBD.CPFS = TxtToDec(txt1pfs.Text.Trim());
                HBD.CDAdress = pictureBox1.Tag.ToString();
                        break;
                    case 2: 
                         HBD.CDetailID = txt2dh.Text.Trim();
                HBD.CWidth = TxtToDec(txt2cd.Text.Trim());
                HBD.CHigh = TxtToDec(txt2gd.Text.Trim());
                HBD.CTS = TxtToDec(txt2ts.Text.Trim());
                HBD.CPFS = TxtToDec(txt2pfs.Text.Trim());
                HBD.CDAdress = pictureBox2.Tag.ToString();
                        break;
                    case 3: 
                        HBD.CDetailID = txt3dh.Text.Trim();
                        HBD.CWidth = TxtToDec(txt3cd.Text.Trim());
                        HBD.CHigh = TxtToDec(txt3gd.Text.Trim());
                        HBD.CTS = TxtToDec(txt3ts.Text.Trim());
                        HBD.CPFS = TxtToDec(txt3pfs.Text.Trim());
                        HBD.CDAdress = pictureBox3.Tag.ToString();
                        break;
                    case 4:
                           HBD.CDetailID = txt4dh.Text.Trim();
                HBD.CWidth = TxtToDec(txt4cd.Text.Trim());
                HBD.CHigh = TxtToDec(txt4gd.Text.Trim());
                HBD.CTS = TxtToDec(txt4ts.Text.Trim());
                HBD.CPFS = TxtToDec(txt4pfs.Text.Trim());
                HBD.CDAdress = pictureBox4.Tag.ToString();
                        break;
                    case 5:
                           HBD.CDetailID = txt5dh.Text.Trim();
                HBD.CWidth = TxtToDec(txt5cd.Text.Trim());
                HBD.CHigh = TxtToDec(txt5gd.Text.Trim());
                HBD.CTS = TxtToDec(txt5ts.Text.Trim());
                HBD.CPFS = TxtToDec(txt5pfs.Text.Trim());
                HBD.CDAdress = pictureBox5.Tag.ToString();
                        break;
                    case 6: 
                           HBD.CDetailID = txt6dh.Text.Trim();
                HBD.CWidth = TxtToDec(txt6cd.Text.Trim());
                HBD.CHigh = TxtToDec(txt6gd.Text.Trim());
                HBD.CTS = TxtToDec(txt6ts.Text.Trim());
                HBD.CPFS = TxtToDec(txt6pfs.Text.Trim());
                HBD.CDAdress = pictureBox6.Tag.ToString();
                        break;
                    default: break;
                    #endregion
                }
               
                db.DeleteBusiness_Detail(HBD);
                if (!db.InsertBusiness_Detail(HBD))
                    return false;
                else return true;
            }
            catch (Exception ex)
            {
                log.wrirteLog("主页面", "增加明细", ex.Message); return false;
            }
        
        }

        bool InsertBusinessMain()
        {
            try
            {
                H_Bus_MainEntity HBD = new H_Bus_MainEntity();
                HBD.CCID = CompanyID;
                HBD.ZTS = TxtToDec(txtzts.Text.Trim());
                HBD.TotalSquare = TxtToDec(txtzpfs.Text.Trim());
                HBD.Price = TxtToDec(txtdj.Text.Trim());
                HBD.TotalPrice = TxtToDec(txtzje.Text.Trim());
                db.DeleteBusiness_Main(HBD);
                if (!db.InsertBusiness_Main(HBD))
                    return false;
                else return true;
            }
            catch (Exception ex)
            {
                log.wrirteLog("主页面", "增加明细主表", ex.Message); return false;
            }

        }


        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        decimal TxtToDec(string txt)
        {
            decimal dec = 0;
            try { dec = decimal.Parse(txt); }
            catch { }

            return dec;
        }

        /// <summary>
        /// 获取预览图片
        /// </summary>
        /// <param name="FILENAME"></param>
        /// <returns></returns>
        string GetStrPicPath(string FILENAME)
        {
            string filepic = AppDomain.CurrentDomain.BaseDirectory + "image\\" + FILENAME;
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "imageZoom\\" + FILENAME;
            zp.GetThumbnail(filepic, filepath, ImageFormat.Jpeg, 200, true);
            return filepath;
        }

       // 验证手机号码的主要代码如下：
         public bool IsHandsetTelpone(string str_handset)
    {
      return System.Text.RegularExpressions.Regex.IsMatch(str_handset,@"^[1]+[3,5,7,4]+\d{9}");
    }

         private bool IsNumber(string str_handset)
         {
             if (!System.Text.RegularExpressions.Regex.IsMatch(str_handset, "^([0-9]{1,})$"))
             {
                 if (!System.Text.RegularExpressions.Regex.IsMatch(str_handset, "^([0-9]{1,}[.][0-9]*)$"))
                     return false;
                 else return true; 
             }
             else return true;
            
             //return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$");
      
         }


        #region 删除
        private void bt1delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(txt1dh.Text + "数据删除不能恢复，请点确定继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                db.DeleteBusiness_Detail(CompanyID, txt1dh.Text.Trim());
                txt1dh.Text = "";
                pictureBox1.Load();
                p1.Visible = false; p11.Visible = false;
            }
        }
        private void bt2delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(txt2dh.Text+"数据删除不能恢复，请点确定继续？" , "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               
                db.DeleteBusiness_Detail(CompanyID,txt2dh.Text.Trim());
                txt2dh.Text = "";
                pictureBox2.Load();
                p2.Visible = false; p21.Visible = false;
            }

        }
        private void bt3delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(txt3dh.Text + "数据删除不能恢复，请点确定继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                db.DeleteBusiness_Detail(CompanyID, txt3dh.Text.Trim());
                txt3dh.Text = "";
                pictureBox3.Load();
                p3.Visible = false; p31.Visible = false;
            }
        }

        private void bt4delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(txt4dh.Text + "数据删除不能恢复，请点确定继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                db.DeleteBusiness_Detail(CompanyID, txt4dh.Text.Trim());
                txt4dh.Text = "";
                pictureBox4.Load();
                p4.Visible = false; p41.Visible = false;
            }
        }

        private void bt5delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(txt5dh.Text + "数据删除不能恢复，请点确定继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                db.DeleteBusiness_Detail(CompanyID, txt5dh.Text.Trim());
                txt5dh.Text = "";
                pictureBox5.Load();
                p5.Visible = false; p51.Visible = false;
            }
        }

        private void bt6delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(txt6dh.Text + "数据删除不能恢复，请点确定继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                db.DeleteBusiness_Detail(CompanyID, txt6dh.Text.Trim());
                txt6dh.Text = "";
                pictureBox6.Load();
                p6.Visible = false; p61.Visible = false;
            }
        }
        #endregion


        #region  翻页
        private void llnext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InitAPP("Y");
            PageInt++;
            ShowAPPDetail(PageInt);
        }

        private void llper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InitAPP("Y");
            PageInt--;
            if (PageInt <= 0) PageInt = 1;
            ShowAPPDetail(PageInt);
        }
        #endregion

       
        #region 预览
       
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
              if (e.Button == MouseButtons.Right)
            {
                UserWindows.PreView pv = new UserWindows.PreView();
                pv.PicName = pictureBox1.Tag.ToString();
                pv.ShowDialog();
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                UserWindows.PreView pv = new UserWindows.PreView();
                pv.PicName = pictureBox2.Tag.ToString();
                pv.ShowDialog();
            }

        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                UserWindows.PreView pv = new UserWindows.PreView();
                pv.PicName = pictureBox3.Tag.ToString();
                pv.ShowDialog();
            }
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                UserWindows.PreView pv = new UserWindows.PreView();
                pv.PicName = pictureBox4.Tag.ToString();
                pv.ShowDialog();
            }
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                UserWindows.PreView pv = new UserWindows.PreView();
                pv.PicName = pictureBox1.Tag.ToString();
                pv.ShowDialog();
            }
        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                UserWindows.PreView pv = new UserWindows.PreView();
                pv.PicName = pictureBox6.Tag.ToString();
                pv.ShowDialog();
            }
        }

        #endregion

        string ComputerSum(decimal cd, decimal gd, decimal ts)
        {
            try
            {
                decimal sum = 0;
                sum = cd * gd * ts;
                 string[] str   =new string[6];
                if(txt1dh.Text.Trim()!="")str[0]=txt1dh.Text.Trim();
                if(txt2dh.Text.Trim()!="")str[1]=txt2dh.Text.Trim();
               if(txt3dh.Text.Trim()!="")str[2]=txt3dh.Text.Trim();
               if(txt4dh.Text.Trim()!="")str[3]=txt4dh.Text.Trim();
               if(txt5dh.Text.Trim()!="")str[4]=txt5dh.Text.Trim(); 
                if(txt6dh.Text.Trim()!="")str[5]=txt6dh.Text.Trim();
               
                DataSet ds = db.GetDSBusiness_Detail(CompanyID,str);
                if (ds == null) StausSum = 0;
                else { if (ds.Tables[0].Rows.Count <= 0)StausSum = 0;
                else
                {
                    decimal aa=0;
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                     aa=aa+TxtToDec(dr["CPFS"].ToString() );
                    }
                    StausSum = aa;
                    }
                }
                decimal bb=StausSum + TxtToDec(txt1pfs.Text)+TxtToDec(txt2pfs.Text)
                    +TxtToDec(txt3pfs.Text)+TxtToDec(txt4pfs.Text)+TxtToDec(txt5pfs.Text)+
                    TxtToDec(txt6pfs.Text)+sum;
                txtzpfs.Text = bb.ToString();
                txtzje.Text = ComputerTotalSum(bb,TxtToDec(txtdj.Text.Trim()));
                return sum.ToString().TrimEnd('.', '0');
               
            }
            catch {
                return "0";
            }
        }
        string ComputerTotalSum(decimal zpfs, decimal dj)
        {
            try
            {
                decimal sum = 0;
                sum = zpfs * dj;
                return sum.ToString().TrimEnd('.', '0');
            }
            catch
            {
                return "0";
            }
        }

        #region 计算
        
      
        private void txt1cd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt1pfs.Text = ComputerSum(TxtToDec(txt1cd.Text.Trim()), TxtToDec(txt1gd.Text.Trim()), TxtToDec(txt1ts.Text.Trim()));
                }
                catch { }
                }
        }

        private void txt1gd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt1pfs.Text = ComputerSum(TxtToDec(txt1cd.Text.Trim()), TxtToDec(txt1gd.Text.Trim()), TxtToDec(txt1ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt1ts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt1pfs.Text = ComputerSum(TxtToDec(txt1cd.Text.Trim()), TxtToDec(txt1gd.Text.Trim()), TxtToDec(txt1ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt2cd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt2pfs.Text = ComputerSum(TxtToDec(txt2cd.Text.Trim()), TxtToDec(txt2gd.Text.Trim()), TxtToDec(txt2ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt2gd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt2pfs.Text = ComputerSum(TxtToDec(txt2cd.Text.Trim()), TxtToDec(txt2gd.Text.Trim()), TxtToDec(txt2ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt2ts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt2pfs.Text = ComputerSum(TxtToDec(txt2cd.Text.Trim()), TxtToDec(txt2gd.Text.Trim()), TxtToDec(txt2ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt3cd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt3pfs.Text = ComputerSum(TxtToDec(txt3cd.Text.Trim()), TxtToDec(txt3gd.Text.Trim()), TxtToDec(txt3ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt3gd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt3pfs.Text = ComputerSum(TxtToDec(txt3cd.Text.Trim()), TxtToDec(txt3gd.Text.Trim()), TxtToDec(txt3ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt3ts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt3pfs.Text = ComputerSum(TxtToDec(txt3cd.Text.Trim()), TxtToDec(txt3gd.Text.Trim()), TxtToDec(txt3ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt6ts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt6pfs.Text = ComputerSum(TxtToDec(txt6cd.Text.Trim()), TxtToDec(txt6gd.Text.Trim()), TxtToDec(txt6ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt6cd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt6pfs.Text = ComputerSum(TxtToDec(txt6cd.Text.Trim()), TxtToDec(txt6gd.Text.Trim()), TxtToDec(txt6ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt6gd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt6pfs.Text = ComputerSum(TxtToDec(txt6cd.Text.Trim()), TxtToDec(txt6gd.Text.Trim()), TxtToDec(txt6ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt5ts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt5pfs.Text = ComputerSum(TxtToDec(txt5cd.Text.Trim()), TxtToDec(txt5gd.Text.Trim()), TxtToDec(txt5ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt5cd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt5pfs.Text = ComputerSum(TxtToDec(txt5cd.Text.Trim()), TxtToDec(txt5gd.Text.Trim()), TxtToDec(txt5ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt5gd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt5pfs.Text = ComputerSum(TxtToDec(txt5cd.Text.Trim()), TxtToDec(txt5gd.Text.Trim()), TxtToDec(txt5ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt4ts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt4pfs.Text = ComputerSum(TxtToDec(txt4cd.Text.Trim()), TxtToDec(txt4gd.Text.Trim()), TxtToDec(txt4ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt4cd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt4pfs.Text = ComputerSum(TxtToDec(txt4cd.Text.Trim()), TxtToDec(txt4gd.Text.Trim()), TxtToDec(txt4ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txt4gd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt4pfs.Text = ComputerSum(TxtToDec(txt4cd.Text.Trim()), TxtToDec(txt4gd.Text.Trim()), TxtToDec(txt4ts.Text.Trim()));
                }
                catch { }
            }
        }

        private void txtdj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txtzje.Text = ComputerTotalSum(TxtToDec(txtdj.Text.Trim()), TxtToDec(txtzpfs.Text.Trim()) );
                }
                catch { }
            }
        }
        #endregion


    }
}
