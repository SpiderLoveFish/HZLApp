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
    public partial class PreView : Form
    {
        public string PicName = "";
        LogHelper log = new LogHelper();

        public PreView()
        {
            InitializeComponent();
        }

        private void PreView_Load(object sender, EventArgs e)
        {
            try
            {
                string url = AppDomain.CurrentDomain.BaseDirectory + "image\\" + PicName;
                pictureBox1.Load(url);
            }
            catch (Exception ex){
                log.wrirteLog("预览","-预览",ex.Message);
            }
        }
    }
}
