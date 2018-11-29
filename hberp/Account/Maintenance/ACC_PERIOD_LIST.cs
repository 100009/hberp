using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class ACC_PERIOD_LIST : Form
    {
        dalAccPeriodList _dal = new dalAccPeriodList();
        public ACC_PERIOD_LIST()
        {
            InitializeComponent();
        }

        private void ACC_PERIOD_LIST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modAccPeriodList> list = _dal.GetIList(out Util.emsg);
            DBGrid.DataSource = list;
            if (list == null)
            {                
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            EditAccPeriodList frm = new EditAccPeriodList();
            frm.AddItem(Util.UserId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void toolEdit_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            EditAccPeriodList frm = new EditAccPeriodList();
            frm.EditItem((modAccPeriodList)DBGrid.CurrentRow.DataBoundItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}
