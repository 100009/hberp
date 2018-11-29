using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class LxListBox : ListBox
    {
        public LxListBox()
        {
            InitializeComponent();
            this.BackColor = frmOptions.BACKCOLOR;
            clsTranslate.TranslateMenu(contextMenuStrip1);
            //txtFind.ToolTipText = clsTranslate.TranslateString("Please input find text");
        }

        public LxListBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            this.BackColor = frmOptions.BACKCOLOR;
            clsTranslate.TranslateMenu(contextMenuStrip1);
            //txtFind.ToolTipText = clsTranslate.TranslateString("Please input find text");
            SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
        }

        private void toolSelAllMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Items.Count == 0) return;

            for (int i = 0; i < this.Items.Count; i++)
            {
                SetSelected(i, true);
            }
        }

        private void toolSelNoneMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Items.Count == 0) return;

            for (int i = 0; i < this.Items.Count; i++)
            {
                SetSelected(i, false);
                txtFind.Text = string.Empty;
            }
        }

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                if (txtFind.Text.Trim().IndexOf(",") < 0)
                {
                    int i = this.FindString(txtFind.Text.Trim());
                    if (i > 0)
                    {
                        this.TopIndex = i;
                        SetSelected(i, true);
                    }
                }
                else
                {
                    this.SelectedItems.Clear();
                    string[] temp = txtFind.Text.Trim().Split(',');
                    for (int i = 0; i < temp.Length; i++)
                    {
                        int f = this.FindString(temp[i]);
                        if (f > 0)
                        {
                            this.TopIndex = f;
                            SetSelected(f, true);
                        }
                    }
                }
                contextMenuStrip1.Hide();
            }            
        }
    }
}
