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
    public partial class EditAssetEvaluate : Form
    {
        dalAssetEvaluate _dal = new dalAssetEvaluate();
        string _action;
        public EditAssetEvaluate()
        {
            InitializeComponent();
        }

        private void EditAssetEvaluate_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            txtFormId.ReadOnly = true;
            if (clsLxms.GetParameterValue("NEED_ASSET_NO").CompareTo("T") == 0)
                lblStar.Visible = true;
            else
                lblStar.Visible = false;
        }

        public void AddItem()
        {
            _action = "NEW";
            dtpFormDate.Value = DateTime.Today;
            txtFormId.Text = _dal.GetNewId(dtpFormDate.Value);
            txtAssetId.ReadOnly = true;
            txtAssetName.ReadOnly = true;
            txtNetMny.ReadOnly = true;
            status4.Image = null;
        }

        public void EditItem(string formid)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                modAssetEvaluate mod = _dal.GetItem(formid, out Util.emsg);
                if (mod != null)
                {
                    txtFormId.Text = formid;
                    dtpFormDate.Value = mod.FormDate;
                    txtNo.Text = mod.No;
                    txtAssetId.Text = mod.AssetId;
                    txtAssetName.Text = mod.AssetName;
                    txtNetMny.Text = mod.NetMny.ToString();
                    txtEvaluateMny.Text = mod.EvaluateMny.ToString();
                    txtRemark.Text = mod.Remark;
                    if (mod.Status == 1)
                    {
                        status4.Image = Properties.Resources.audited;
                        Util.ChangeStatus(this, true);
                        toolSave.Enabled = false;
                    }
                    else
                    {
                        status4.Image = null;
                        toolSave.Visible = true;
                        Util.ChangeStatus(this, false);
                        txtFormId.ReadOnly = true;
                        toolSave.Enabled = true;                        
                    }                    
                    txtAssetId.ReadOnly = true;
                    txtAssetName.ReadOnly = true;
                    txtNetMny.ReadOnly = true;                    
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

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dtpFormDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFormDate.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtNetMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Net Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNetMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtNetMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Net Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNetMny.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtEvaluateMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sale Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEvaluateMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtEvaluateMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sale Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEvaluateMny.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAssetName.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Asset name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAssetName.Focus();
                    return;
                }
                
                if (clsLxms.GetParameterValue("NEED_ASSET_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
                    return;
                }

                modAssetEvaluate mod = new modAssetEvaluate();
                mod.FormId = txtFormId.Text;
                mod.FormDate = dtpFormDate.Value;
                mod.No = txtNo.Text.Trim();
                mod.AssetId = txtAssetId.Text;
                mod.AssetName = txtAssetName.Text.Trim();
                mod.NetMny = Convert.ToDecimal(txtNetMny.Text);
                mod.EvaluateMny = Convert.ToDecimal(txtEvaluateMny.Text);                
                mod.Remark = txtRemark.Text.Trim();
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;
                bool ret;
                if (_action == "ADD" || _action == "NEW")
                    ret = _dal.Insert(mod, out Util.emsg);
                else
                    ret = _dal.Update(mod.FormId, mod, out Util.emsg);
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
        
        private void btnAssetId_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAssetList dal = new dalAssetList();
                BindingCollection<modAssetList> list = dal.GetIList("1", string.Empty, string.Empty, string.Empty, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList("请选择资产：", list);
                    frm.Selection = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        modAssetList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                        if (mod != null)
                        {
                            txtAssetId.Text = mod.AssetId;
                            txtAssetName.Text = mod.AssetName;
                            txtNetMny.Text = mod.NetMny.ToString();
                        }
                        else
                        {
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}

