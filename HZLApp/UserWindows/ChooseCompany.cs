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
    public partial class ChooseCompany : Form
    {
        DAL.DBAccessHelper db = new DAL.DBAccessHelper();

        

        public ChooseCompany()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                MainForm frm1 = (MainForm)this.Owner;
                //((TextBox)frm1.Controls["textBox1"]).Text
                frm1.ChooseCompanyID = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); ;
                this.DialogResult = DialogResult.OK;
                //this.Close();
            
            }
            catch { }
        }

        private void ChooseCompany_Load(object sender, EventArgs e)
        {
            DataSet ds = db.GetDSCompany("");
            dataGridView1.DataSource=ds.Tables[0];
          //  dataGridView1.DataBindings();
        }
    }
}
