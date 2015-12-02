using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HZLApp
{
    public partial class WindowAdd : Form
    {
        public string ChooseID = "";
        public string pic = "";

        public WindowAdd()
        {
            InitializeComponent();
        }

        private void btntlc_Click(object sender, EventArgs e)
        {
            UserWindows.tlc gdc = new UserWindows.tlc();
            gdc.Owner = this;
            gdc.ShowDialog();
            if (gdc.DialogResult == DialogResult.OK)
            {
                MainForm frm1 = (MainForm)this.Owner;
                 frm1.ChooseParaID = ChooseID;
                 frm1.Pic = pic;
                this.DialogResult = DialogResult.OK;
            }

        }

        private void btnpkc_Click(object sender, EventArgs e)
        {
            UserWindows.pkc gdc = new UserWindows.pkc();
            gdc.Owner = this;
            gdc.ShowDialog(); 
            if (gdc.DialogResult == DialogResult.OK)
            {

                MainForm frm1 = (MainForm)this.Owner;
                frm1.ChooseParaID = ChooseID;
                frm1.Pic = pic;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btntlm_Click(object sender, EventArgs e)
        {
            UserWindows.tlm gdc = new UserWindows.tlm();
            gdc.Owner = this;
            gdc.ShowDialog();
            if (gdc.DialogResult == DialogResult.OK)
            {

                MainForm frm1 = (MainForm)this.Owner;
                frm1.ChooseParaID = ChooseID;
                frm1.Pic = pic;
                this.DialogResult = DialogResult.OK;
            }

        }

        private void btnpkm_Click(object sender, EventArgs e)
        {
            UserWindows.pkm gdc = new UserWindows.pkm();
            gdc.Owner = this;
            gdc.ShowDialog();
            if (gdc.DialogResult == DialogResult.OK)
            {
                MainForm frm1 = (MainForm)this.Owner;
                frm1.ChooseParaID = ChooseID;
                frm1.Pic = pic;
                this.DialogResult = DialogResult.OK;

            }
        }

        private void btngdc_Click(object sender, EventArgs e)
        {
            UserWindows.gdc gdc = new UserWindows.gdc();
            gdc.Owner = this;
            gdc.ShowDialog();
            if (gdc.DialogResult == DialogResult.OK)
            {

                MainForm frm1 = (MainForm)this.Owner;
                frm1.ChooseParaID = ChooseID;
                frm1.Pic = pic;
                this.DialogResult = DialogResult.OK;

            }
        }
    }
}
