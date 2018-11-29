using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LXMS.DBUtility;

namespace LXMS
{
    public partial class frmDatabase : Form
    {
        public frmDatabase()
        {
            InitializeComponent();
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmDatabase_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
        }

        private void toolOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Support File(*.txt;*.sql)|*.txt;*.sql|Text File(*.txt)|*.txt|Script File(*.sql)|*.sql";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtSql.Text = Util.GetTextFile(ofd.FileName);
            }
        }

        private void toolExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSql.Text.Trim())) return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                CloseAll();
                string[] sql;
                if(txtSql.SelectionLength==0)
                    sql = txtSql.Text.Trim().Split(';');
                else
                    sql = txtSql.SelectedText.Trim().Split(';');
                for (int i = 0; i < sql.Length; i++)
                {
                    if (!string.IsNullOrEmpty(sql[i].Trim()))
                    {
                        string[] f = sql[i].Split(' ');
                        string tablename = string.Empty;
                        switch (f[0].ToUpper().Trim())
                        {
                            case "SELECT":
                                try
                                {
                                    for (int k = 0; k < f.Length; k++)
                                    {
                                        if (f[k].ToUpper().Trim() == "FROM")
                                        {
                                            if (f.Length > k + 1)
                                                tablename = f[k + 1];
                                            break;
                                        }
                                    }
                                    DataSet ds = SqlHelper.ExecuteDs(sql[i]);
                                    if (ds != null)
                                    {
                                        frmViewList frm = new frmViewList((i + 1).ToString() + ".Select " + tablename, ds);
                                        ShowMDIChild(frm, frm);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    frmViewText frm = new frmViewText((i + 1).ToString() + ".Select " + tablename, sql[i] + "\r\n\r\n" + ex.Message.ToString());
                                    ShowMDIChild(frm, frm);
                                }
                                break;
                            case "DELETE":
                            case "TRUNCATE":
                            case "DROP":
                                frmViewText frmt = new frmViewText(i.ToString(), "You are not permitted to operater like this!");
                                ShowMDIChild(frmt, frmt);
                                break;
                            case "INSERT":
                            case "UPDATE":
                                try
                                {
                                    for (int k = 0; k < f.Length; k++)
                                    {
                                        if (f[k].ToUpper().Trim() == "FROM")
                                        {
                                            if (f.Length > k + 1)
                                                tablename = f[k + 1];
                                            break;
                                        }
                                    }
                                    int iRet = SqlHelper.ExecuteNonQuery(sql[i]);
                                    if (iRet >= 0)
                                    {
                                        frmViewText frme = new frmViewText((i + 1).ToString() + "." + f[0] + " " + tablename, sql[i] + "\r\n\r\n Execute Success");
                                        ShowMDIChild(frme, frme);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    frmViewText frme = new frmViewText((i + 1).ToString() + "." + f[0] + " " + tablename, sql[i] + "\r\n\r\n" + ex.Message.ToString());
                                    ShowMDIChild(frme, frme);
                                }
                                break;
                            default:
                                frmViewText frmd = new frmViewText(i.ToString(), "You are not permitted to operater like this!");
                                ShowMDIChild(frmd, frmd);                                
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        public bool ShowMDIChild(Form frmchild, object tag)
        {
            try
            {
                if (!IsAlreadyOpen(frmchild, tag))
                {
                    AddMDIChildToTabCtrl(frmchild, this.tabControl1);
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public bool IsAlreadyOpen(Form frmchild, object tag)
        {
            try
            {
                int intCount = this.MdiChildren.Length;
                if (intCount > 0)
                {
                    Form[] frmArray = this.MdiChildren;
                    foreach (Form frmMdi in frmArray)
                    {
                        if ((Convert.ToString(frmMdi.Tag)) == (Convert.ToString(tag)))
                        {
                            frmMdi.Activate();
                            frmMdi.BringToFront();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void AddMDIChildToTabCtrl(Form frmChild, TabControl tab)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                //frmChild.Parent = tp;
                frmChild.Disposed += new EventHandler(frmChild_Closed);
                frmChild.Closed += new EventHandler(frmChild_Closed);
                //this.MdiChildActivate += new EventHandler(frmMain_MdiChildActivate);
                //tab.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
                //tab.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
                frmChild.TopLevel = false;
                TabPage tp = new TabPage();
                tp.Parent = tabControl1;
                tp.Text = clsTranslate.TranslateString(frmChild.Text);
                //tp.Tag = frmChild.Name;
                tp.Tag = frmChild;
                tp.ToolTipText = frmChild.Name;
                
                frmChild.Parent = tp;
                //tp.ContextMenuStrip = contextMenuStrip2;
                frmChild.Dock = DockStyle.Fill;
                tp.Show();

                //child Form will now hold a reference value to a tabpage

                this.tabControl1.SelectedTab = tp;
                frmChild.FormBorderStyle = FormBorderStyle.None;
                frmChild.WindowState = FormWindowState.Maximized;

                frmChild.TopMost = false;
                frmChild.Show();                
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

        void frmChild_Closed(object sender, EventArgs e)
        {
            this.RemoveTabPageFromTabCtrl();
        }

        /**/
        /// <summary>
        /// 在子窗体关闭时移除对应的TabPage
        /// </summary>
        private void RemoveTabPageFromTabCtrl()
        {
            try
            {
                for (int idx = 0; idx < tabControl1.TabCount; idx++)
                {
                    if (tabControl1.TabPages[idx].HasChildren == false)
                    {                        
                        tabControl1.TabPages[idx].Tag = null;
                        tabControl1.TabPages[idx].Dispose();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void CloseAll()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (tabControl1.TabPages.Count == 0) return;
                for (int i = tabControl1.TabPages.Count - 1; i >= 0; i--)
                {
                    TabPage tp = tabControl1.TabPages[i];
                    if (tp.Tag != null)
                    {
                        Form frmChild = (Form)tp.Tag;
                        if (frmChild != null)
                            frmChild.Dispose();
                    }
                    else
                        tp.Dispose();
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
