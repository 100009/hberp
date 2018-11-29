using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class frmSingleSelect : Form
    {
        string valuetype = string.Empty;
        public frmSingleSelect()
        {
            InitializeComponent();
        }

        private void frmSingleSelect_Load(object sender, EventArgs e)
        {
            Util.retValue1 = string.Empty;
        }

        public void InitData(string title, string data, ComboBoxStyle style)
        {
            this.Text = title;
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(data)) return;
            string[] temp = data.Split(',');
            for (int i = 0; i < temp.Length; i++)
                cbo.Items.Add(temp[i]);

            cbo.DropDownStyle = style;
            valuetype = "TEXT";
        }

        public void InitData(string title, string data, string defultitem, ComboBoxStyle style)
        {
            this.Text = title;
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(data)) return;
            string[] temp = data.Split(',');
            for (int i = 0; i < temp.Length; i++)
                cbo.Items.Add(temp[i]);

            cbo.DropDownStyle = style;
            cbo.Text = defultitem;
            valuetype = "TEXT";
        }

        public void InitData(string title, ArrayList arr, ComboBoxStyle style)
        {
            this.Text = title;
            cbo.Items.Clear();
            if (arr == null || arr.Count == 0) return;
            for (int i = 0; i < arr.Count; i++)
                cbo.Items.Add(arr[i]);

            cbo.DropDownStyle = style;
            valuetype = "TEXT";
        }

        public void InitData(string title, ArrayList arr, string defultitem, ComboBoxStyle style)
        {
            this.Text = title;
            cbo.Items.Clear();
            if (arr == null || arr.Count == 0) return;
            for (int i = 0; i < arr.Count; i++)
                cbo.Items.Add(arr[i]);

            cbo.DropDownStyle = style;
            cbo.Text = defultitem;
            valuetype = "TEXT";
        }

        public void InitViewList<T>(string strTitle, IList<T> list, string valuemember, string displaymember, ComboBoxStyle style)
        {
            this.Text = strTitle;
            cbo.DataSource = list;
            cbo.ValueMember = valuemember;
            cbo.DisplayMember = displaymember;
            valuetype = "LIST";
            cbo.DropDownStyle = style;
            cbo.SelectedIndex = -1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbo.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                if (cbo.SelectedIndex == -1) return;
            }
            else
            {
                if (string.IsNullOrEmpty(cbo.Text.Trim())) return;                
            }
            Util.retValue1 = cbo.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}
