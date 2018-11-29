using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class LxFlexGrid : AxMSFlexGridLib.AxMSFlexGrid
    {        
        public LxFlexGrid()
        {
            InitializeComponent();
        }
        
        private void mnuCopy_Click(object sender, EventArgs e)
        {
            string cellvalue = this.get_TextMatrix(this.Row, this.Col).Trim();            
            Clipboard.SetText(cellvalue);
        }

        private void mnuFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                if (this.Rows == 0) return;
                if (string.IsNullOrEmpty(mnuFind.Text.Trim()))
                {
                    MessageBox.Show(this.mnuFind.ToolTipText, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                for (int i = 0; i < this.Rows; i++)
                {
                    for (int j = 0; j < this.Cols; j++)
                    {
                        string cellvalue = this.get_TextMatrix(i, j).Trim();
                        if (!string.IsNullOrEmpty(cellvalue) && cellvalue.ToUpper().IndexOf(mnuFind.Text.Trim().ToUpper()) >= 0)
                        {
                            this.Row = i;
                            this.Col = j;                            
                            return;
                        }
                    }
                }
            }
        }
        
        private void mnuFilterEqual_Click(object sender, EventArgs e)
        {            
            if (this.FixedRows >= this.Rows) return;
            
            string cellvalue = this.get_TextMatrix(this.Row, this.Col).Trim();
            for (int i = this.Rows; i >=0 ; i--)
            {
                if (i > this.FixedRows)
                {
                    if (this.get_TextMatrix(i, this.Col).CompareTo(cellvalue) != 0)
                        this.RemoveItem(i);
                }
                else
                    break;
            }
        }

        private void mnuFilterLarger_Click(object sender, EventArgs e)
        {
            if (this.FixedRows >= this.Rows) return;
            
            string cellvalue = this.get_TextMatrix(this.Row, this.Col).Trim();
            for (int i = this.Rows; i >= 0; i--)
            {
                if (i > this.FixedRows)
                {
                    if (this.get_TextMatrix(i, this.Col).CompareTo(cellvalue) < 0)
                        this.RemoveItem(i);
                }
                else
                    break;
            }
        }

        private void mnuFilterLess_Click(object sender, EventArgs e)
        {
            if (this.FixedRows >= this.Rows) return;

            string cellvalue = this.get_TextMatrix(this.Row, this.Col).Trim();
            for (int i = this.Rows; i >= 0; i--)
            {
                if (i > this.FixedRows)
                {
                    if (this.get_TextMatrix(i, this.Col).CompareTo(cellvalue) > 0)
                        this.RemoveItem(i);
                }
                else
                    break;
            }
        }
    }
}
