using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HZLApp.DAL;
using HZLApp.Entity;

namespace HZLApp
{
    public partial class ChangePWD : Form
    {
        DAL.DBAccessHelper db = new DBAccessHelper();

        public ChangePWD()
        {
            InitializeComponent();
        }

        private void btnqd_Click(object sender, EventArgs e)
        {
            if (txtpwd.Text.Trim() == "" || txttpwd.Text.Trim() == "" || txtoldpwd.Text.Trim() == "")
            {
                txtpwd.Text = ""; txttpwd.Text = ""; txtoldpwd.Text = "";
                MessageBox.Show("输入的密码不能为空！");
                return;
            }
            if (txtpwd.Text.Trim() != txttpwd.Text.Trim())
            {
                txtpwd.Text = ""; txttpwd.Text = ""; txtoldpwd.Text = "";
                MessageBox.Show("新输入的2次密码不一样，请重新输入！");
                return;
            }
            string oldpwd = UserPublicClass.MD5(txtoldpwd.Text.Trim());

            if (UserPublicClass.PWDValue != oldpwd)
            {
                txtpwd.Text = ""; txttpwd.Text = ""; txtoldpwd.Text = "";
                MessageBox.Show("原密码错误，请重新输入！");
                return;
            }
             string newpwd = UserPublicClass.MD5(txtpwd.Text.Trim());
           
            if(UserPublicClass.PWDValue==newpwd)
            {
                txtpwd.Text = ""; txttpwd.Text = ""; txtoldpwd.Text = "";
                MessageBox.Show("新旧密码一样，请重新输入！");
                return;
            }

            if (db.UpdatePWD(UserPublicClass.LoginValue, newpwd) == 0)
                MessageBox.Show("修改错误！");
            else
            {
                MessageBox.Show("修改成功！");
                this.Hide();
            }
        }
    }
}
