using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    public partial class QRY_LOGS : Form
    {
        
        public QRY_LOGS()
        {
            InitializeComponent();
        }

        private void QRY_LOGS_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            
        }

        private void toolExit1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolRefresh1_Click(object sender, EventArgs e)
        {
            LoadData1();
        }

        private void LoadData1()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtItemNo1.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Please input ") + clsTranslate.TranslateString("item no"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemNo1.Focus();
                    return;
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

        private void txtItemNo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                LoadData1();
            }
            else
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtEpibolyId2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                //LoadData2();
            }
            else
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void toolRefresh2_Click(object sender, EventArgs e)
        {
            //LoadData2();
        }

        private void txtItemNo3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                //LoadData3();
            }
            else
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }
        
        private void toolRefresh3_Click(object sender, EventArgs e)
        {
            //LoadData3();
        }

        private void toolRefresh4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalLogLoginHost bll = new dalLogLoginHost();
                BindingCollection<modLogLoginHost> list = bll.GetIList(out Util.emsg);
                DBGrid4.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    DBGrid4.Columns["registercode"].Width = 360;
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

        private void toolRefresh5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
