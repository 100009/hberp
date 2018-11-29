using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    public partial class frmOptions : Form
    {

        public static Color ALTERNATING_BACKCOLOR = Color.AliceBlue;
        public static Color BACKCOLOR = Color.AliceBlue;
        public static Color BASECOLOR = Color.LightCyan;
        public static Color BORDERCOLOR = Color.MediumTurquoise;

        public static Color ITEMCOLOR_CREATE;
        public static Color ITEMCOLOR_PMC;
        public static Color ITEMCOLOR_PRODUCT;
        public static Color ITEMCOLOR_AUDIT;
        public static Color ITEMCOLOR_CANCELLED;
        public static Color ITEMCOLOR_COMPLETE;

        public static Color URGENTCOLOR_TODAYCOMPLETE;
        public static Color URGENTCOLOR_TOMORROWCOMPLETE;

        public static bool SHOW_MAIN_TOOLBAR;

        INIClass ini = new INIClass(Util.INI_FILE);
        dalSysParameters _dal = new dalSysParameters();
        public frmOptions()
        {
            InitializeComponent();
        }

        private void frmSkins_Load(object sender, EventArgs e)
        {
            LoadColorOptions();
            InitParameter(false);
            clsTranslate.InitLanguage(this);
            rbEnglish.Checked = Util.language == "0" ? true : false;
            rbChinese.Checked = Util.language == "1" ? true : false;
        }

        private void LoadColorOptions()
        {
            cboColorOptions.Items.Clear();
            cboColorOptions.Items.Add("天空蓝");
            cboColorOptions.Items.Add("金甲黄");
            cboColorOptions.Items.Add("军装绿");
            cboColorOptions.Items.Add("皮革紫");
            cboColorOptions.Items.Add("桔皮红");
            cboColorOptions.SelectedIndex = -1;
        }

        private void cboColorOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboColorOptions.Text)
            {
                case "天空蓝":
                    cboAlternatingColor.Text = "AliceBlue";
                    cboBackColor.Text = "AliceBlue";
                    cboBaseColor.Text = "LightCyan";
                    cboBorderColor.Text = "MediumTurquoise";
                    break;
                case "金甲黄":
                    cboAlternatingColor.Text = "LightGoldenrodYellow";
                    cboBackColor.Text = "LightGoldenrodYellow";
                    cboBaseColor.Text = "Khaki";
                    cboBorderColor.Text = "DarkKhaki";
                    break;
                case "军装绿":
                    cboAlternatingColor.Text = "Honeydew";
                    cboBackColor.Text = "Honeydew";
                    cboBaseColor.Text = "DarkSeaGreen";
                    cboBorderColor.Text = "DarkGreen";
                    break;
                case "皮革紫":
                    cboAlternatingColor.Text = "PapayaWhip";
                    cboBackColor.Text = "PapayaWhip";
                    cboBaseColor.Text = "Tan";
                    cboBorderColor.Text = "SaddleBrown";
                    break;
                case "桔皮红":
                    cboAlternatingColor.Text = "LavenderBlush";
                    cboBackColor.Text = "LavenderBlush";
                    cboBaseColor.Text = "LightSalmon";
                    cboBorderColor.Text = "Tomato";
                    break;
                default:
                    cboAlternatingColor.Text = "LightGoldenrodYellow";
                    cboBackColor.Text = "LightGoldenrodYellow";
                    cboBaseColor.Text = "Khaki";
                    cboBorderColor.Text = "DarkKhaki";
                    break;
            }
        }

        public static Color ConvertColor(string colorstr, Color defcolor)
        {
            if (string.IsNullOrEmpty(colorstr)) return defcolor;
            Color retcolor = new Color();
            System.Drawing.ColorConverter colConvert = new System.Drawing.ColorConverter();
            try
            {
                retcolor = (Color)colConvert.ConvertFromString("#" + colorstr);
            }
            catch
            {
                try
                {
                    retcolor = (Color)colConvert.ConvertFromString(colorstr);
                }
                catch
                {
                    retcolor = defcolor;
                }
            }
            return retcolor;
        }

        public static void LoadDefaultColor(bool isdef)
        {
            INIClass ini = new INIClass(Util.INI_FILE);
            if (isdef)
            {
                ALTERNATING_BACKCOLOR = Color.LightGoldenrodYellow;
                BACKCOLOR = Color.LightGoldenrodYellow;
                BASECOLOR = Color.Khaki;
                BORDERCOLOR = Color.DarkKhaki;

                ITEMCOLOR_CREATE = Color.Black;
                ITEMCOLOR_PMC = Color.DeepSkyBlue;
                ITEMCOLOR_PRODUCT = Color.RoyalBlue;
                ITEMCOLOR_AUDIT = Color.Green;
                ITEMCOLOR_CANCELLED = Color.DarkGoldenrod;
                ITEMCOLOR_COMPLETE = Color.DarkGray;
                URGENTCOLOR_TODAYCOMPLETE = Color.Red;
                URGENTCOLOR_TOMORROWCOMPLETE = Color.Orange;

                SHOW_MAIN_TOOLBAR = true;
                Util.language = "1";
            }
            else
            {
                string language = ini.IniReadValue("LANGUAGE", "LANGUAGE");
                if (!string.IsNullOrEmpty(language))
                    Util.language = language;

                string iniAlterColor = ini.IniReadValue("SKIN_COLOR", "ALTERNATING_BACKCOLOR");
                if (!string.IsNullOrEmpty(iniAlterColor))
                    ALTERNATING_BACKCOLOR = Color.FromName(iniAlterColor);

                string iniBackColor = ini.IniReadValue("SKIN_COLOR", "BACKCOLOR");
                if (!string.IsNullOrEmpty(iniBackColor))
                    BACKCOLOR = Color.FromName(iniBackColor);

                string iniBaseColor = ini.IniReadValue("SKIN_COLOR", "BASECOLOR");
                if (!string.IsNullOrEmpty(iniBaseColor))
                    BASECOLOR = Color.FromName(iniBaseColor);

                string iniBorderColor = ini.IniReadValue("SKIN_COLOR", "BORDERCOLOR");
                if (!string.IsNullOrEmpty(iniBorderColor))
                    BORDERCOLOR = Color.FromName(iniBorderColor);

                string iniShowToolBar = ini.IniReadValue("MAIN_FORM", "SHOW_TOOLBAR");
                if (string.IsNullOrEmpty(iniShowToolBar) || iniShowToolBar.CompareTo("1") == 0)
                    SHOW_MAIN_TOOLBAR = true;
                else
                    SHOW_MAIN_TOOLBAR = false;

                ITEMCOLOR_CREATE = Color.FromName(clsLxms.GetParameterValue("ITEMCOLOR_CREATE"));
                ITEMCOLOR_PMC = Color.FromName(clsLxms.GetParameterValue("ITEMCOLOR_PMC"));
                ITEMCOLOR_PRODUCT = Color.FromName(clsLxms.GetParameterValue("ITEMCOLOR_PRODUCT"));
                ITEMCOLOR_AUDIT = Color.FromName(clsLxms.GetParameterValue("ITEMCOLOR_AUDIT"));
                ITEMCOLOR_CANCELLED = Color.FromName(clsLxms.GetParameterValue("ITEMCOLOR_CANCELLED"));
                ITEMCOLOR_COMPLETE = Color.FromName(clsLxms.GetParameterValue("ITEMCOLOR_COMPLETE"));

                URGENTCOLOR_TODAYCOMPLETE = ConvertColor(clsLxms.GetParameterValue("URGENTCOLOR_TODAYCOMPLETE"), Color.Red);
                URGENTCOLOR_TOMORROWCOMPLETE = ConvertColor(clsLxms.GetParameterValue("URGENTCOLOR_TOMORROWCOMPLETE"), Color.Orange);

            }
        }

        private void InitParameter(bool isdef)
        {
            LoadDefaultColor(isdef);

            cboAlternatingColor.Text = ALTERNATING_BACKCOLOR.Name;
            cboBackColor.Text = BACKCOLOR.Name;
            cboBaseColor.Text = BASECOLOR.Name;
            cboBorderColor.Text = BORDERCOLOR.Name;

            chkShowToolbar.Checked = SHOW_MAIN_TOOLBAR;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ini.IniWriteValue("SKIN_COLOR", "ALTERNATING_BACKCOLOR", cboAlternatingColor.Text);
                ALTERNATING_BACKCOLOR = cboAlternatingColor.SelectedColor;

                ini.IniWriteValue("SKIN_COLOR", "BACKCOLOR", cboBackColor.Text);
                BACKCOLOR = cboBackColor.SelectedColor;

                ini.IniWriteValue("SKIN_COLOR", "BASECOLOR", cboBaseColor.Text);
                BASECOLOR = cboBaseColor.SelectedColor;

                ini.IniWriteValue("SKIN_COLOR", "BORDERCOLOR", cboBorderColor.Text);
                BORDERCOLOR = cboBorderColor.SelectedColor;

                Util.language = rbEnglish.Checked ? "0" : "1";
                ini.IniWriteValue("LANGUAGE", "LANGUAGE", Util.language);

                ini.IniWriteValue("MAIN_FORM", "SHOW_TOOLBAR", chkShowToolbar.Checked ? "1" : "0");
                SHOW_MAIN_TOOLBAR = chkShowToolbar.Checked;

                clsTranslate.InitLanguage(this);
                MessageBox.Show("Success!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControl1.Refresh();
                this.BackColor = BACKCOLOR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to restore to default setting?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            InitParameter(true);
            btnApply_Click(null, null);
        }

        private void btnRemoveExcelInfo_Click(object sender, EventArgs e)
        {
            RWReg reg = new RWReg("CURRENT_USER");
            bool ret = reg.SetRegValue(@"Software\Microsoft\Office\14.0\Excel\Security", "ExtensionHardening", "00000000", "DWORD");
            if (ret)
                MessageBox.Show("Success to remove the information!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Fail to remove the information!", clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnShow3_Click(object sender, EventArgs e)
        {
            RWReg reg = new RWReg("CURRENT_USER");
            bool ret = reg.DelRegKeyValue(@"Software\Microsoft\Office\14.0\Excel\Security", "ExtensionHardening");
            if (ret)
                MessageBox.Show("Changed to show the information!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Fail to show the information!", clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmOptions_Shown(object sender, EventArgs e)
        {
            this.BackColor = BACKCOLOR;
        }
    }
}