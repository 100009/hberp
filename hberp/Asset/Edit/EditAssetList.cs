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
    public partial class EditAssetList : Form
    {
        string _action;
        dalAssetList _dal = new dalAssetList();        
        INIClass ini = new INIClass(Util.INI_FILE);
        public EditAssetList()
        {
            InitializeComponent();
            FillControl.FillDepreMethod(cboDepreMethod, false);
        }

        private void EditAssetList_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);            
            txtAssetId.ReadOnly = true;
        }
        
        public void EditItem(string assetid)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";                
                modAssetList mod = _dal.GetItem(assetid, out Util.emsg);
                if (mod != null)
                {
                    txtAssetId.Text = mod.AssetId;
                    txtAssetName.Text = mod.AssetName;
                    txtAssetProperty.Text = mod.AssetProperty;
                    dtpSignDate.Value = mod.SignDate;
                    dtpPurchaseDate.Value = mod.PurchaseDate;
                    txtControlDepart.Text = mod.ControlDepart;
                    txtUsingDepart.Text = mod.UsingDepart;
                    cboDepreMethod.SelectedValue = mod.DepreMethod;
                    txtRawMny.Text = mod.RawMny.ToString();
                    txtLastMny.Text = mod.LastMny.ToString();
                    txtRawQty.Text = mod.RawQty.ToString();
                    txtDepreUnit.Text = mod.DepreUnit;
                    txtRemark.Text = mod.Remark;
                    if (mod.Status == 2)
                    {
                        status4.Image = Properties.Resources.audited;
                        Util.ChangeStatus(this, true);
                        toolSave.Enabled = false;
                    }
                    else
                    {
                        status4.Image = null;
                        Util.ChangeStatus(this, false);
                        toolSave.Enabled = true;
                    }
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

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dtpSignDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpSignDate.Focus();
                    return;
                }                
                if (cboDepreMethod.SelectedValue == null || string.IsNullOrEmpty(cboDepreMethod.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Depre Method") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboDepreMethod.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtRawMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Start Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRawMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtRawMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Start Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRawMny.Focus();
                    return;
                }
                else if (Convert.ToDecimal(txtRawMny.Text.Trim()) <= 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Start Mny") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRawMny.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtLastMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Last Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtLastMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtLastMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Last Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtLastMny.Focus();
                    return;
                }
                else if (Convert.ToDecimal(txtLastMny.Text.Trim()) < 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Last Mny") + clsTranslate.TranslateString(" must >= 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtLastMny.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtRawQty.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Raw Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRawQty.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtRawQty.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Raw Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRawQty.Focus();
                    return;
                }
                else if (Convert.ToDecimal(txtRawQty.Text.Trim()) <= 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Raw Qty") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRawQty.Focus();
                    return;
                }
                
                if (string.IsNullOrEmpty(txtAssetName.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Asset Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAssetName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDepreUnit.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Depre Unit") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDepreUnit.Focus();
                    return;
                }
                if (cboDepreMethod.SelectedValue==null)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Depre Method") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboDepreMethod.Focus();
                    return;
                }
                
                modAssetList mod = new modAssetList();
                mod.AssetId = txtAssetId.Text.Trim();
                mod.AssetName = txtAssetName.Text.Trim();
                mod.AssetProperty = txtAssetProperty.Text.Trim();
                mod.ControlDepart = txtControlDepart.Text.Trim();
                mod.UsingDepart = txtUsingDepart.Text.Trim();
                mod.SignDate = dtpSignDate.Value;
                mod.PurchaseDate = dtpPurchaseDate.Value;
                mod.DepreMethod = cboDepreMethod.SelectedValue.ToString();
                mod.DepreUnit = txtDepreUnit.Text.Trim();
                mod.RawMny = Convert.ToDecimal(txtRawMny.Text);
                mod.LastMny = Convert.ToDecimal(txtLastMny.Text);
                mod.RawQty = Convert.ToDecimal(txtRawQty.Text);
                mod.Remark = txtRemark.Text.Trim();
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;
                bool ret;
                if (_action == "ADD" || _action == "NEW")
                    ret = _dal.Insert(mod, out Util.emsg);
                else
                    ret = _dal.Update(mod.AssetId, mod, out Util.emsg);
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

        private void btnControlDepart_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MTN_DEPT_LIST frm = new MTN_DEPT_LIST();
                frm.SelectVisible = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtControlDepart.Text = Util.retValue1;
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

        private void btnUsingDepart_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MTN_DEPT_LIST frm = new MTN_DEPT_LIST();
                frm.SelectVisible = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtUsingDepart.Text = Util.retValue1;
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

        private void cboDepreMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDepreMethod.SelectedValue == null)
            {
                txtRawQty.Text = "0";
                return;
            }
            switch (cboDepreMethod.SelectedValue.ToString())
            {
                case "平均年限法":
                    txtDepreUnit.Text = "月";
                    break;
                case "双倍余额递减法":
                    txtDepreUnit.Text = "月";
                    break;
                case "工作量法":
                    txtDepreUnit.Text = "";
                    break;
            }            
        }
    }
}
