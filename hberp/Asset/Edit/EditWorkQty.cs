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
    public partial class EditWorkQty : Form
    {
        string _action;
        dalAssetWorkQty _dal = new dalAssetWorkQty();
        public EditWorkQty()
        {
            InitializeComponent();
        }

        private void EditWorkQty_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
        }

        public void InitData(modAssetList mod)
        {            
            modAssetWorkQty modw = _dal.GetItem(mod.AssetId, Util.modperiod.AccName, out Util.emsg);
            if (modw != null)
            {
                txtAssetId.Text = modw.AssetId;
                txtAssetName.Text = modw.AssetName;
                txtQty.Text = modw.WorkQty.ToString();
                txtRemark.Text = modw.Remark;
                _action = "EDIT";
            }
            else
            {
                txtAssetId.Text = mod.AssetId;
                txtAssetName.Text = mod.AssetName;                
                _action = "NEW";
            }
            Status1.Text = _action;
            //Status2.Text = "工量余存：" + mod.LeftQty;            
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Util.modperiod.LockFlag==1)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                dalAccCredenceList dalcre = new dalAccCredenceList();
                if (dalcre.ExistDepre(Util.modperiod.AccName, out Util.emsg))
                {
                    MessageBox.Show("本月已做资产折旧凭证，工作量数据不能更改!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtQty.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Work Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtQty.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Work Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Focus();
                    return;
                }
                else if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Work Qty") + clsTranslate.TranslateString(" must >= 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Focus();
                    return;
                }


                modAssetWorkQty mod = new modAssetWorkQty();
                mod.AssetId = txtAssetId.Text.Trim();
                mod.AssetName = txtAssetName.Text.Trim();
                mod.AccName = Util.modperiod.AccName;
                mod.WorkQty = Convert.ToDecimal(txtQty.Text);
                mod.Remark = txtRemark.Text.Trim();
                mod.UpdateUser = Util.UserId;
                bool ret;
                if (mod.WorkQty > 0)
                {
                    if (_action == "ADD" || _action == "NEW")
                        ret = _dal.Insert(mod, out Util.emsg);
                    else
                        ret = _dal.Update(mod.AssetId, Util.modperiod.AccName, mod, out Util.emsg);
                }
                else
                    ret = _dal.Delete(mod.AssetId, Util.modperiod.AccName, out Util.emsg);
                if (ret)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        private void toolCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}
