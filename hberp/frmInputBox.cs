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
    public partial class frmInputBox : Form
    {
        public frmInputBox(string title,string initValue)
        {
            InitializeComponent();
            this.Text = title;
            textBox1.Text = initValue;
        }

        private void frmInputBox_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Util.retValue1 = "";
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please input data!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();                
                return;
            }
            Util.retValue1 = textBox1.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                btnOk_Click(null, null);
        }

    }
}
