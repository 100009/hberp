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
    public partial class QRY_ASSET_LOG : Form
    {
        public QRY_ASSET_LOG()
        {
            InitializeComponent();
        }

        private void QRY_ASSET_DATA_Load(object sender, EventArgs e)
        {
            DBGrid1.Tag = this.Name+"1";
            DBGrid2.Tag = this.Name + "2";
            DBGrid3.Tag = this.Name + "3";
            clsTranslate.InitLanguage(this);
            LoadData1();
            LoadData2();
            LoadData3();
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData1();
            LoadData2();
            LoadData3();
        }

        private void LoadData1()
        {
            try
            {
                dalAssetList dal = new dalAssetList();
                BindingCollection<modAssetDepreList> list = dal.GetDepreList(out Util.emsg);
                DBGrid1.DataSource = list;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadData2()
        {
            try
            {
                dalAssetWorkQty dal = new dalAssetWorkQty();
                BindingCollection<modAssetWorkQty> list = dal.GetIList(out Util.emsg);
                DBGrid2.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadData3()
        {
            try
            {
                dalAssetEvaluate dal = new dalAssetEvaluate();
                BindingCollection<modAssetEvaluate> list = dal.GetIList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
                DBGrid3.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
