using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class BaseFormEdit : Form
    {
        protected int _status = 0;
        public BaseFormEdit()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void BaseFormEdit_Load(object sender, EventArgs e)
        {
            toolRefresh.Enabled = true;
            toolSelect.Enabled = true;
            toolFind.Enabled = true;

            toolNew.Enabled = true;
            toolEdit.Enabled = true;
            toolDel.Enabled = true;

            toolSave.Enabled = false;
            toolCancel.Enabled = false;
            toolInactive.Enabled = true;
            _status = 0;
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected void HideEditButtons()
        {
            //toolRefresh.Visible = false;
            //lblFind.Visible = false;
            //txtFind.Visible = false;
            //toolFind.Visible = false;

            toolNew.Visible = false;
            toolEdit.Visible = false;
            toolDel.Visible = false;

            toolSave.Visible = false;
            toolCancel.Visible = false;
            toolInactive.Visible = false;

            sepDel.Visible = false;
            //toolStripSeparator2.Visible = false;
            toolStripSeparator3.Visible = false;
            sepCancel.Visible = false;
        }

        protected void ShowEditButtons()
        {
            //toolRefresh.Visible = true;
            //lblFind.Visible = true;
            //txtFind.Visible = true;
            //toolFind.Visible = true;

            toolNew.Visible = true;
            toolEdit.Visible = true;
            toolDel.Visible = true;

            toolSave.Visible = true;
            toolCancel.Visible = true;
            toolInactive.Visible = true;

            sepDel.Visible = true;
            //toolStripSeparator2.Visible = true;
            toolStripSeparator3.Visible = true;
            sepCancel.Visible = true;
        }

        public void ShowHideSelection(bool visible)
        {
            toolSelect.Visible = visible;
        }

        #region refresh
        private void toolRefresh_Click(object sender, EventArgs e)
        {
            Refresh();           
        }

        protected virtual void Refresh()
        {
        }
        #endregion

        #region select
        private void toolSelect_Click(object sender, EventArgs e)
        {
            Select();
        }

        protected virtual void Select()
        {
        }
        #endregion

        #region find
        private void toolFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                MessageBox.Show(clsTranslate.TranslateString("Please input find text"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Find();
        }

        protected virtual void Find()
        {
        }

        public string FindText
        {
            get
            {
                return txtFind.Text.Trim();
            }
            set
            {
                txtFind.Text = value;
            }
        }

        #endregion

        #region new
        private void toolNew_Click(object sender, EventArgs e)
        {
            toolRefresh.Enabled = false;
            toolSelect.Enabled = false;
            toolFind.Enabled = false;
            
            toolNew.Enabled = false;
            toolEdit.Enabled = false;
            toolDel.Enabled = false;

            toolSave.Enabled = true;
            toolCancel.Enabled = true;
            toolInactive.Enabled = false;

            _status = 1;
            New();
        }

        protected virtual void New()
        {

        }
        #endregion

        #region edit
        private void toolEdit_Click(object sender, EventArgs e)
        {
            toolRefresh.Enabled = false;
            toolSelect.Enabled = false;
            toolFind.Enabled = false;
            
            toolNew.Enabled = false;
            toolEdit.Enabled = false;
            toolDel.Enabled = false;

            toolSave.Enabled = true;
            toolCancel.Enabled = true;
            toolInactive.Enabled = false;

            _status = 2;
            Edit();
        }

        protected virtual void Edit()
        {
        }
        #endregion

        #region delete
        private void toolDel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"),clsTranslate.TranslateString("Confirm"),MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No) return;
            
            if (Delete())
                MessageBox.Show(clsTranslate.TranslateString("Delete Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (!string.IsNullOrEmpty(Util.emsg))
                    MessageBox.Show(clsTranslate.TranslateString(Util.emsg), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected virtual bool Delete()
        {
            return false;
        }
        #endregion

        #region save
        private void toolSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                toolRefresh.Enabled = true;
                toolSelect.Enabled = true;
                toolFind.Enabled = true;
                
                toolNew.Enabled = true;
                toolEdit.Enabled = true;
                toolDel.Enabled = true;

                toolSave.Enabled = false;
                toolCancel.Enabled = false;
                toolInactive.Enabled = true;

                _status = 0;
            }
            else
            {
                if(!string.IsNullOrEmpty(Util.emsg))
                    MessageBox.Show(clsTranslate.TranslateString(Util.emsg), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected virtual bool Save()
        {
            return false;
        }
        #endregion

        #region cancel
        private void toolCancel_Click(object sender, EventArgs e)
        {            
            toolRefresh.Enabled = true;
            toolSelect.Enabled = true;
            toolFind.Enabled = true;
                
            toolNew.Enabled = true;
            toolEdit.Enabled = true;
            toolDel.Enabled = true;

            toolSave.Enabled = false;
            toolCancel.Enabled = false;
            toolInactive.Enabled = true;

            _status = 0;
            Cancel();
        }

        protected virtual void Cancel()
        {
        }

        #endregion

        #region inactive
        private void toolInactive_Click(object sender, EventArgs e)
        {
            if (Inactive())
                MessageBox.Show(clsTranslate.TranslateString("Inactive Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
                
        protected virtual bool Inactive()
        {
            return false;
        }
        #endregion

        #region audit
        private void toolAudit_Click(object sender, EventArgs e)
        {
            if (Audit())
                MessageBox.Show(clsTranslate.TranslateString("Audit Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected virtual bool Audit()
        {
            return false;
        }
        #endregion

        #region reset
        private void toolReset_Click(object sender, EventArgs e)
        {
            if (Reset())
                MessageBox.Show(clsTranslate.TranslateString("Reset Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show(clsTranslate.TranslateString(Util.emsg), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected virtual bool Reset()
        {
            return false;
        }
        #endregion

        #region Status
        public string Status1
        {
            get
            {
                return StatusLabel1.Text;
            }
            set
            {
                StatusLabel1.Text = value + " Row(s)";
            }
        }

        public string Status2
        {
            get
            {
                return StatusLabel2.Text;
            }
            set
            {
                StatusLabel2.Text = value;
            }
        }

        public string Status3
        {
            get
            {
                return StatusLabel3.Text;
            }
            set
            {
                StatusLabel3.Text = value;
            }
        }

        public string Status4
        {
            get
            {
                return StatusLabel4.Text;
            }
            set
            {
                StatusLabel4.Text = value;
            }
        }
        #endregion

        #region Enabled
        public bool AuditEnabled
        {
            get { return toolAudit.Enabled; }
            set { toolAudit.Enabled = value; }
        }
        public bool ResetEnabled
        {
            get { return toolReset.Enabled; }
            set { toolReset.Enabled = value; }
        }
        #endregion

        #region Visible
        public bool SelectVisible
        {
            get { return toolSelect.Visible; }
            set { toolSelect.Visible = value; }
        }
        public bool NewVisible
        {
            get { return toolNew.Visible; }
            set { toolNew.Visible = value; }
        }
        public bool EditVisible
        {
            get { return toolEdit.Visible; }
            set { toolEdit.Visible = value; }
        }
        public bool DelVisible
        {
            get { return toolDel.Visible; }
            set 
            { 
                toolDel.Visible = value;
                sepDel.Visible = value;
            }
        }
        public bool SaveVisible
        {
            get { return toolSave.Visible; }
            set { toolSave.Visible = value; }
        }
        public bool CancelVisible
        {
            get { return toolCancel.Visible; }
            set 
            { 
                toolCancel.Visible = value;
                sepCancel.Visible = value;
            }
        }
        public bool InactiveVisible
        {
            get { return toolInactive.Visible; }
            set { toolInactive.Visible = value; }
        }
        public bool AuditVisible
        {
            get { return toolAudit.Visible; }
            set { toolAudit.Visible = value; }
        }
        public bool ResetVisible
        {
            get { return toolReset.Visible; }
            set { toolReset.Visible = value; }
        }
        #endregion

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
                toolFind_Click(null, null);
        }

        private void BaseFormEdit_Shown(object sender, EventArgs e)
        {
            Util.HideToolStripSeparator(toolStrip1);
        }
    }
}