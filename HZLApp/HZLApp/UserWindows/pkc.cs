using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace HZLApp.UserWindows
{
    public partial class pkc : Form
    {
        DAL.DBAccessHelper db = new DAL.DBAccessHelper();

        private string pic = "";
        private static int PageInt=1;

        public pkc()
        {
            InitializeComponent();
        }

        ZoomPic zp = new ZoomPic();
        LogHelper logh = new LogHelper();

        string[] strpathname= new string[15];


        private void pkc_Load(object sender, EventArgs e)
        {
         //string NewID=  db.GetMaxID("pkc","H_pkc");

            
            try
            {
                VisableNextPer(false,false);
                InitShow(false);
                ShowDt(1);
              
              
            }
            catch { }
        }

        void InitShow(bool flag)
        {
            pictureBox1.Visible = pictureBox2.Visible = pictureBox3.Visible
                = pictureBox4.Visible = pictureBox5.Visible = pictureBox6.Visible
                = pictureBox7.Visible = pictureBox8.Visible = pictureBox9.Visible
                 = pictureBox10.Visible = pictureBox11.Visible = pictureBox12.Visible
        = pictureBox13.Visible = pictureBox14.Visible = pictureBox15.Visible = flag;
        }

        //动态加载
        void loadpic(int index, string hid,string path)
        {
            switch (index)
            { 
                case 1:
                    pictureBox1.Visible = true;
                pictureBox1.Tag = hid;
                pictureBox1.Load(path);
                    break;
                case 2:
                    pictureBox2.Visible = true;
                 pictureBox2.Tag = hid;
                pictureBox2.Load(path);
                break;
                case 3:
                pictureBox3.Visible = true;
                pictureBox3.Tag = hid;
                pictureBox3.Load(path);
                break;
                case 4:
                pictureBox4.Visible = true;
                pictureBox4.Tag = hid;
                pictureBox4.Load(path);
                break;
                case 5:
                pictureBox5.Visible = true;
                pictureBox5.Tag = hid;
                pictureBox5.Load(path);
                break;
                case 6:
                pictureBox6.Visible = true;
                pictureBox6.Tag = hid;
                pictureBox6.Load(path);
                break;
                case 7:
                pictureBox7.Visible = true;
                pictureBox7.Tag = hid;
                pictureBox7.Load(path);
                break;
                case 8:
                pictureBox8.Visible = true;
                pictureBox8.Tag = hid;
                pictureBox8.Load(path);
                break;
                case 9:
                pictureBox9.Visible = true;
                pictureBox9.Tag = hid;
                pictureBox9.Load(path);
                break;
                case 10:
                pictureBox10.Visible = true;
                pictureBox10.Tag = hid;
                pictureBox10.Load(path);
                break;
                case 11:
                pictureBox11.Visible = true;
                pictureBox11.Tag = hid;
                pictureBox11.Load(path);
                break;
                case 12:
                pictureBox12.Visible = true;
                pictureBox12.Tag = hid;
                pictureBox12.Load(path);
                break;
                case 13:
                pictureBox13.Visible = true;
                pictureBox13.Tag = hid;
                pictureBox13.Load(path);
                break;
                case 14:
                pictureBox14.Visible = true;
                pictureBox14.Tag = hid;
                pictureBox14.Load(path);
                break;
                case 15:
                pictureBox15.Visible = true;
                pictureBox15.Tag = hid;
                pictureBox15.Load(path);
                break;
                default: break;
            }
             
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // 获取文件夹绝对路径    显示在 txtbox 控件里

            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();

            string StrFileName = "";
            // 获取文件和路径名 一起显示在 txtbox 控件里

            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string SavePathFile = AppDomain.CurrentDomain.BaseDirectory + "\\image\\pkc" +DateTime.Now.ToString("yyyyMMddHHmmss")+ ".jpg";
                StrFileName = dialog.SafeFileName;
                if (File.Exists(SavePathFile))
                {
                    File.Delete(SavePathFile);
                } File.Copy( dialog.FileName,SavePathFile);
                if (!db.InsertH_Para("H_pkc", "pkc" +DateTime.Now.ToString("yyyyMMddHHmmss")+ ".jpg", db.GetMaxID("pkc", "H_pkc")))
                {
                    MessageBox.Show("保存失败！"); return;
                }
                else
                {
                    MessageBox.Show("保存成功！");
                    ShowDt(PageInt);
                }
            }

            
        }

        /// <summary>
        /// Button属性
        /// </summary>
        /// <param name="Per"></param>
        /// <param name="Next"></param>
        void VisableNextPer(bool Per, bool Next)
        {
            btnnext.Enabled = Next;
            btnPer.Enabled = Per;
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="BeginPage"></param>
        void ShowDt(int BeginPage)
        {
            try
            {
                InitShow(false);
                string strSql = " Select ID, HID,HName,HAdress  From H_pkc ";
                string strShow = "ID, HID,HName,HAdress";
                int PageCount, RecordCount;
                string strWhere = "  ";
                DataTable lsdt = db.ExecutePager(BeginPage, 15, "ID", strShow, strSql, strWhere, " ID DESC ", out PageCount, out RecordCount);
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
                lbcount.Text = "一共有"+PageCount+"页，"+RecordCount+"项";
                strpathname = null;
                strpathname=new string[15];
                for (int i = 0; i < lsdt.Rows.Count; i++)
                {
                    strpathname[i] = lsdt.Rows[i]["HAdress"].ToString();

                    pic = lsdt.Rows[i]["HAdress"].ToString(); //
                     
                    string path = AppDomain.CurrentDomain.BaseDirectory + "imageZoom\\" + lsdt.Rows[i]["HAdress"].ToString();
                    zp.GetThumbnail(AppDomain.CurrentDomain.BaseDirectory + "image\\" + pic, path, ImageFormat.Jpeg, 200, true);
                    loadpic(i + 1, lsdt.Rows[i]["HID"].ToString(), path);
                }
            }
            catch (Exception ex)
            {
                logh.wrirteLog("显示pkc","查询",ex.Message);
            }
        }

        

        
        private void btnPer_Click(object sender, EventArgs e)
        {
            PageInt--;
            if (PageInt <= 0) PageInt = 1;
            ShowDt(PageInt);
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            PageInt++;
            ShowDt(PageInt);
            
        }

        #region 双击
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox1.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }
       
        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox2.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox3.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox4.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox5_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox5.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox6_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox6.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox7_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox7.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox8_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox8.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox9_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox9.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox10_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox10.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;

        }

        private void pictureBox11_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox11.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;

        }

        private void pictureBox12_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox12.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox13_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox13.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox14_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox14.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox15_DoubleClick(object sender, EventArgs e)
        {
            WindowAdd frm1 = (WindowAdd)this.Owner;
            frm1.ChooseID = pictureBox15.Tag.ToString();
            frm1.pic = pic;
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region 鼠标
        
     

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Tag.ToString() == "") return;
            if (e.Button == MouseButtons.Right)
            {
                PreView pv = new PreView();
                pv.PicName = strpathname[0];
                pv.ShowDialog();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (MessageBox.Show("确认删除？" + pictureBox1.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    db.DeleteH_Para("H_pkc", pictureBox1.Tag.ToString());
                    ShowDt(PageInt);
                }
                else return;
            }

        }


        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[1] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[1];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox2.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox2.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[2] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[2];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox3.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox3.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }

        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[3] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[3];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox4.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox4.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[4] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[4];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox5.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox5.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[5] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[5];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox6.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox6.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }

        }

        private void pictureBox7_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[6] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[6];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox7.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox7.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }

        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[7] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[7];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox8.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox8.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        private void pictureBox9_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[8] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[8];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox9.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox9.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        private void pictureBox10_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[9] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[9];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox10.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox10.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        private void pictureBox11_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[10] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[10];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox11.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox11.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        private void pictureBox12_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[11] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[11];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox12.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox12.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        private void pictureBox13_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[11] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[12];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox13.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox13.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        private void pictureBox14_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[13] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[13];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox14.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox14.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }

        }

        private void pictureBox15_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (strpathname[14] == "") return;
                if (e.Button == MouseButtons.Right)
                {
                    PreView pv = new PreView();
                    pv.PicName = strpathname[14];
                    pv.ShowDialog();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (MessageBox.Show("确认删除？" + pictureBox15.Tag.ToString(), "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        db.DeleteH_Para("H_pkc", pictureBox15.Tag.ToString());
                        ShowDt(PageInt);
                    }
                    else return;
                }
            }
            catch { }
        }

        #endregion
    }
}
