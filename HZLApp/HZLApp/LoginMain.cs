using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using HZLApp.DAL;
using HZLApp.Entity;
using System.Configuration;
 

namespace HZLApp
{
    public partial class LoginMain : Form
    {
        DAL.DBAccessHelper db = new DBAccessHelper();

        public LoginMain()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

            BtnLogin();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            txtlogin.Text = "";
            txtpwd.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            try
            {
               
               UserPublicClass.CompanyValue = ConfigurationManager.AppSettings.Get("CompnyName");
               txtTitle.Text = UserPublicClass.CompanyValue;
            }
            catch { }
            txtlogin.Focus();
        }

        string CheckVaile(string Userlogin,string UserPWD)
        {
            string MSG="";
            if (Userlogin == "" || UserPWD == "")
                MSG = "请输入正确的用户名和密码！";
            H_UserEntity users = new H_UserEntity();
            users.UserLoginName = Userlogin;
            users.UserPWD = UserPWD;
           // var users = DataConvert<H_UserEntity>.ToList(dt);
            if (!db.IsVaileLogin(users))
                MSG = "用户名密码错误！！！";

            return MSG;
        
        }

        

        void BtnLogin()
        {
            if (db.strCon() == "")
                MessageBox.Show("数据库连接错误！请检查！");
            string UserLogin = txtlogin.Text.Trim();
            string UserPwd = UserPublicClass.MD5(txtpwd.Text.Trim());
            string ErrorMsg = CheckVaile(UserLogin, UserPwd);
            if (ErrorMsg != "")
                MessageBox.Show(ErrorMsg);
            else
            {

                HZLMain m = new HZLMain();
                m.Show();
                this.Hide();
            }
        }

       
 
 

        private void txtpwd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //Code
                BtnLogin();
        }
    }
}
