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
    public partial class frmMsgLabelSetting : Form
    {
        INIClass ini = new INIClass(Util.INI_FILE);
        string _sectionname = string.Empty;
        public frmMsgLabelSetting(string sectionname)
        {
            InitializeComponent();
            cboBackcolor.Text = "DarkGreen";
            cboForecolor.Text = "Yellow";
            cboFontSize.Text = "12";
            cboSpeed.Text = "10";
            _sectionname = sectionname;
            InitSetting();
        }
                
        private void frmMsgLabelSetting_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
        }

        private void InitSetting()
        {
            if (!string.IsNullOrEmpty(_sectionname))
            {
                string bkcolor = ini.IniReadValue(_sectionname, "BACKCOLOR");
                if (!string.IsNullOrEmpty(bkcolor))
                    cboBackcolor.Text = bkcolor;

                string forecolor = ini.IniReadValue(_sectionname, "FORECOLOR");
                if (!string.IsNullOrEmpty(forecolor))
                    cboForecolor.Text = forecolor;

                string fontsize = ini.IniReadValue(_sectionname, "FONTSIZE");
                if (!string.IsNullOrEmpty(fontsize))
                    cboFontSize.Text = fontsize;

                string speed = ini.IniReadValue(_sectionname, "SPEED");
                if (!string.IsNullOrEmpty(speed))
                    cboSpeed.Text = speed;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_sectionname))
            {
                ini.IniWriteValue(_sectionname, "BACKCOLOR", cboBackcolor.Text.ToString());
                ini.IniWriteValue(_sectionname, "FORECOLOR", cboForecolor.Text.ToString());
                ini.IniWriteValue(_sectionname, "FONTSIZE", cboFontSize.Text);
                ini.IniWriteValue(_sectionname, "SPEED", cboSpeed.Text);                
            }
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            cboBackcolor.Text = "DarkGreen";
            cboForecolor.Text = "Yellow";
            cboFontSize.Text = "12";
            cboSpeed.Text = "10";
            btnOK_Click(null, null);
        }
    }
}
