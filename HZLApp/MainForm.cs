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
        DAL.DBAccessHelper db = new DAL.DBAccessHelper();
        private string CompanyID="001";
        public string ChooseCompanyID = "";
        public string ChooseParaID = "";
        private static int PageInt = 1;
        public string Pic = "";
        ZoomPic zp = new ZoomPic();
        LogHelper log = new LogHelper();

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
            }
            catch{
                strname=AppDomain.CurrentDomain.BaseDirectory + "ShareImage" ;
            }
            if (!Directory.Exists(strname))
            { Directory.CreateDirectory(strname); }
            strname += "\\" + txtname.Text + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            
            _NewBitmap.Save(strname);
            KJVisable(true);
        }

        #region 控件属性
        void VisableNextPer(bool per, bool next)
        { llnext.Enabled = next; llper.Enabled = per; }
        void KJVisable(bool bl)
        {

            btnsave.Visible = bl; button1.Visible = bl;
            bt1delete.Visible = bl; bt2delete.Visible = bl; bt3delete.Visible = bl;
            bt4delete.Visible = bl; bt5delete.Visible = bl; bt6delete.Visible = bl;

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
                DataTable lsdt = db.ExecutePager(BeginPage, 6, "ID", strShow, strSql, strWhere, " ID DESC ", out PageCount, out RecordCount);
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
                    p1.Visible = true; p11.Visible = true;
                    txt1Cd.Text = dw["CWidth"].ToString();
                    txt1gd.Text = dw["CHigh"].ToString();
                    txt1dh.Text = dw["CDetailID"].ToString();
                    txt1ts.Text = dw["CTS"].ToString();
                    txt1pfs.Text = dw["CPFS"].ToString();
                    pictureBox1.Tag = dw["CDAdress"].ToString();
                    pictureBox1.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                case 2:
                    p2.Visible = true; p21.Visible = true;
                    txt2cd.Text = dw["CWidth"].ToString();
                    txt2gd.Text = dw["CHigh"].ToString();
                    txt2dh.Text = dw["CDetailID"].ToString();
                    txt2ts.Text = dw["CTS"].ToString();
                    txt2pfs.Text = dw["CPFS"].ToString();
                    pictureBox2.Tag = dw["CDAdress"].ToString();
                    pictureBox2.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                case 3:
                    p3.Visible = true; p31.Visible = true;
                    txt3cd.Text = dw["CWidth"].ToString();
                    txt3gd.Text = dw["CHigh"].ToString();
                    txt3dh.Text = dw["CDetailID"].ToString();
                    txt3ts.Text = dw["CTS"].ToString();
                    txt3pfs.Text = dw["CPFS"].ToString();
                    pictureBox3.Tag = dw["CDAdress"].ToString();
                    pictureBox3.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                case 4:
                    p4.Visible = true; p41.Visible = true;
                    txt4cd.Text = dw["CWidth"].ToString();
                    txt4gd.Text = dw["CHigh"].ToString();
                    txt4dh.Text = dw["CDetailID"].ToString();
                    txt4ts.Text = dw["CTS"].ToString();
                    txt4pfs.Text = dw["CPFS"].ToString();
                    pictureBox4.Tag = dw["CDAdress"].ToString();
                    pictureBox4.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                case 5:
                    p5.Visible = true; p51.Visible = true;
                    txt5cd.Text = dw["CWidth"].ToString();
                    txt5gd.Text = dw["CHigh"].ToString();
                    txt5dh.Text = dw["CDetailID"].ToString();
                    txt5ts.Text = dw["CTS"].ToString();
                    txt5pfs.Text = dw["CPFS"].ToString();
                    pictureBox5.Tag = dw["CDAdress"].ToString();
                    pictureBox5.Load(GetStrPicPath(dw["CDAdress"].ToString()));
                    break;
                case 6:
                    p6.Visible = true; p61.Visible = true;
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
        }

        /// <summary>
        /// 保存打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsave_Click(object sender, EventArgs e)
        {

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
                     DetailID = txt1dh.Text.Trim();
                 if (i == 3 && txt3dh.Text.Trim() != "")
                     DetailID = txt1dh.Text.Trim();
                 if (i == 4 && txt4dh.Text.Trim() != "")
                     DetailID = txt1dh.Text.Trim();
                 if (i == 5 && txt5dh.Text.Trim() != "")
                     DetailID = txt1dh.Text.Trim();
                 if (i == 6 && txt6dh.Text.Trim() != "")
                     DetailID = txt1dh.Text.Trim();
                 if (DetailID == "") continue;
                     if (!InsertBusinessDetail(DetailID))
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
        /// 新增明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
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
                p21.Visible = true; p2.Visible = true;
                txt2dh.Text = ChooseParaID;
                pictureBox2.Load(GetStrPicPath(Pic));

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

        

        bool InsertBusinessDetail(string cdetailid)
        {
            try
            {
                H_Bus_DetailEntity HBD = new H_Bus_DetailEntity();
                HBD.CCID = CompanyID;
                HBD.CDetailID = txt2dh.Text.Trim();
                HBD.CWidth = TxtToDec(txt2cd.Text.Trim());
                HBD.CHigh = TxtToDec(txt2gd.Text.Trim());
                HBD.CTS = TxtToDec(txt2ts.Text.Trim());
                HBD.CPFS = TxtToDec(txt2pfs.Text.Trim());
                HBD.CDAdress = pictureBox4.Tag.ToString();
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

        #region 删除
        private void bt1delete_Click(object sender, EventArgs e)
        {

        }
        private void bt2delete_Click(object sender, EventArgs e)
        {
            txt2dh.Text = "";
            // DelteDetail(txt2dh.Text.Trim());
            pictureBox2.Load();
            p2.Visible = false; p21.Visible = false;

        }
        private void bt3delete_Click(object sender, EventArgs e)
        {

        }

        private void bt4delete_Click(object sender, EventArgs e)
        {

        }

        private void bt5delete_Click(object sender, EventArgs e)
        {

        }

        private void bt6delete_Click(object sender, EventArgs e)
        {

        }
        #endregion


        #region  翻页
        private void llnext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PageInt++;
            ShowAPPDetail(PageInt);
        }

        private void llper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PageInt--;
            if (PageInt <= 0) PageInt = 1;
            ShowAPPDetail(PageInt);
        }
        #endregion
      

    }
}
