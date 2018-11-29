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
    public partial class MTN_WAREHOUSE_INOUT_TYPE : BaseFormEdit
    {
        dalWarehouseInoutType _dal = new dalWarehouseInoutType();
        public MTN_WAREHOUSE_INOUT_TYPE()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void MTN_WAREHOUSE_INOUT_TYPE_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            NewVisible = false;
            EditVisible = false;
            DelVisible = false;
            SaveVisible = false;
            CancelVisible = false;
            InactiveVisible = false;
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modWarehouseInoutType> list = _dal.GetIList(out Util.emsg);
            DBGrid.DataSource = list;
            if (list != null)
            {               
                Status1 = DBGrid.Rows.Count.ToString();
                Status2 = clsTranslate.TranslateString("Refresh");
            }
            else
            {
                DBGrid.DataSource = null;
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected override void Refresh()
        {
            LoadData();
        }

        protected override void Select()
        {
            if (DBGrid.CurrentRow == null) return;

            Util.retValue1 = DBGrid.CurrentRow.Cells[0].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modWarehouseInoutType mod = (modWarehouseInoutType)DBGrid.Rows[i].DataBoundItem;
                if (mod.InoutType.CompareTo(FindText) == 0)
                {
                    DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                    return;
                }
            }
        }

        private void txtInOutType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (SelectVisible)
                Select();
        }
    }
}