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
    public partial class frmGridColVisible : Form
    {
        string _gridname=string.Empty;
        string _cols=string.Empty;
        INIClass ini = new INIClass(Util.INI_FILE);
        public frmGridColVisible()
        {
            InitializeComponent();
        }

        private void frmGridColVisible_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            string strHeader = "Header Text,360,0;Column Name,0,0";
            Util.InitListView(lvData, strHeader);
            lvData.View = View.Details;
            lvData.CheckBoxes = true;
        }

        public void InitColumn(string gridname, string cols)
        {
            _gridname = gridname;
            _cols = cols;            
        }

        private void LoadData()
        {
            string[] col = _cols.Split(',');
            for (int i = 0; i < col.Length; i++)
            {
                string[] str = new string[2];
                str[0] = clsTranslate.TranslateString(col[i].ToString());
                str[1] = col[i].ToString();
                ListViewItem item = new ListViewItem(str);
                lvData.Items.Add(item);
            }
            if (lvData.Items.Count > 0)
            {
                string shows = ini.IniReadValue("GRID_COLUMN", _gridname);
                if (string.IsNullOrEmpty(shows.Trim()))
                    checkAllToolStripMenuItem_Click(null, null);
                else
                {
                    checkNoneToolStripMenuItem_Click(null, null);
                    string[] show = shows.Split(',');
                    for (int i = 0; i < lvData.Items.Count; i++)
                    {
                        for (int j = 0; j < show.Length; j++)
                        {
                            if (show[j].ToLower() == lvData.Items[i].SubItems[1].Text.Trim().ToLower())
                            {
                                lvData.Items[i].Checked = true;
                                break;
                            }
                        }
                    }
                }
            }   
        }

        private void frmGridColVisible_Shown(object sender, EventArgs e)
        {
            LoadData();
        }

        private void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvData.Items.Count == 0) return;
            for (int i = 0; i < lvData.Items.Count; i++)
            {
                lvData.Items[i].Checked = true;
            }
        }

        private void checkNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvData.Items.Count == 0) return;
            for (int i = 0; i < lvData.Items.Count; i++)
            {
                lvData.Items[i].Checked = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lvData.Items.Count == 0) return;

            string shows = string.Empty;
            for (int i = 0; i < lvData.CheckedItems.Count; i++)
            {
                if (i == 0)
                    shows = lvData.CheckedItems[i].SubItems[1].Text;
                else
                    shows += "," + lvData.CheckedItems[i].SubItems[1].Text;
            }
            ini.IniWriteValue("GRID_COLUMN", _gridname, shows);
            DialogResult = DialogResult.OK;
        }
                
    }
}
