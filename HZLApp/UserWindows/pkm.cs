using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HZLApp.UserWindows
{
    public partial class pkm : Form
    {
        DAL.DBAccessHelper db = new DAL.DBAccessHelper();

        private string pic = "";


        ZoomPic zp = new ZoomPic();
        public pkm()
        {
            InitializeComponent();
        }

        private void pdm_Load(object sender, EventArgs e)
        {

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
                StrFileName = dialog.SafeFileName;
                if (!db.InsertH_Para("H_pkm", StrFileName, db.GetMaxID("pkm", "H_pkm")))
                    MessageBox.Show("保存失败！");
                else MessageBox.Show("保存成功！");
            }
        }

        private void pkm_Load(object sender, EventArgs e)
        {

        }
    }
}
