using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;
using LXMS.DBUtility;

namespace LXMS
{
    public partial class frmMain : Form
    {
        public static bool bLogined;
        ArrayList _arrtab = new ArrayList();
        int _currtabindex = 0;
        int _timeinfo = 0;
        public static bool _createshipment = false;
        INIClass ini = new INIClass(Util.INI_FILE);        
        public frmMain()
        {
            InitializeComponent();            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {                
                this.Cursor = Cursors.WaitCursor;
                this.Visible = false;
                lblTrialBalance.Visible = false;
                InitColor();
                clsTranslate.InitLanguage(this);
                frmLogin fLogin = new frmLogin();
                fLogin.BackColor = frmOptions.BACKCOLOR;
                fLogin.ShowDialog();
                if (fLogin.DialogResult == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }
                else
                {                    
                    LoadLoginData();                    
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

        private void frmMain_Shown(object sender, EventArgs e)
        {
            RefreshColor();
            LoadDefaultBar();           
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            //string msg = GetMessage();
            //if (string.IsNullOrEmpty(GetMessage()))
            //    msgLabel1.Visible = false;
            //else
            //{
            //    msgLabel1.Message = msg;
            //    msgLabel1.GetSetting();
            //    msgLabel1.Visible = true;
            //}
        }

        //private string GetMessage()
        //{
        //    string msg = string.Empty;
        //    SysBulletin bll2 = new SysBulletin();
        //    BindingCollection<modSysBulletin> list2 = bll2.GetIList(false, out Util.emsg);
        //    if (list2 != null && list2.Count > 0)
        //    {
        //        msg = "公告: ";
        //        for (int i = 0; i < list2.Count; i++)
        //        {
        //            if (i == 0)
        //                msg += list2[i].Msg.Replace("\r\n", " ");
        //            else
        //                msg += ";    " + list2[i].Msg.Replace("\r\n", " ");
        //        }
        //    }
        //    else
        //    {
        //        SysModifyLog bll = new SysModifyLog();
        //        BindingCollection<modSysModifyLog> list = bll.GetIList(out Util.emsg);
        //        if (list != null && list.Count > 0)
        //        {
        //            if(list[0].UpdateTime>=DateTime.Now.AddDays(-7))
        //                msg = "程序最新修改：" + list[0].ModifyContent.Replace("\r\n", " ");
        //            else
        //                msg = string.Empty;
        //        }
        //        else
        //        {
        //            msg = string.Empty;
        //        }
        //    }
        //    return msg;
        //}
        private void InitColor()
        {
            frmOptions.LoadDefaultColor(false);
            RefreshColor();
        }

        private void RefreshColor()
        {
            statusStrip1.BackColor = frmOptions.BACKCOLOR;
            menuMain.BackColor = frmOptions.BACKCOLOR;
            splitContainer1.Panel1.BackColor = frmOptions.BORDERCOLOR;
            this.Refresh();
        }

        private void LoadLoginData()
        {
            bLogined = true;
            closeAllToolStripMenuItem_Click(null, null);
            StatusLabel2.Text = Util.UserName;
            StatusLabel3.Text = Util.RoleDesc;
            StatusLabel4.Text = Util.modperiod.AccName;
            StatusLabel5.Text = Application.ExecutablePath;
            SetMenuPower();
            //msgLabel1.InitLanguage();
            timerInfo.Enabled = true;
            menuMain.BackColor = Color.WhiteSmoke;
            //LoadBackground();            
            tabControl1.SelectedIndex = 0;
            string strHostName = Environment.MachineName;
            //if (Util.UserId == "SYSADMIN" && (strHostName.ToUpper() == "30-0001-14544" || strHostName.ToUpper() == "LUJ-PC"))
            if (Util.UserId == "SYSADMIN")
            {
                sepClear.Visible = true;
                mnuDatabaseOperation.Visible = true;
            }
            else
            {
                sepClear.Visible = false;
                mnuDatabaseOperation.Visible = false;
            }
            Util.AlarmTimeInterval =Convert.ToInt32(clsLxms.GetParameterValue("ALARM_TIME_INTERVAL"));
            
            string companyname = clsLxms.GetParameterValue("COMPANY_NAME");
            string systemname = clsLxms.GetParameterValue("SYSTEM_NAME");
            StatusLabel1.Text = companyname;
            
            if (!Util.SOFT_REGISTER)
                this.Text = systemname + " - [" + clsTranslate.TranslateString("Unregister") + "]";
            else
                this.Text = systemname;
            this.Visible = true;
        }

        private void LoadDefaultBar()
        {
            string defaultbar = ini.IniReadValue("LXERP", "DEFAULT_BAR");
            switch (defaultbar)
            {   
                case "Sales":
                    btnSales_Click(null, null);
                    btnSales.Focus();
                    break;
                case "Purchase":
                    btnPurchase_Click(null, null);
                    btnPurchase.Focus();
                    break;
                case "Production":
                    btnProduction_Click(null, null);
                    btnProduction.Focus();
                    break;
                case "Warehouse":
                    btnWarehouse_Click(null, null);
                    btnWarehouse.Focus();
                    break;
                case "Account":
                    btnAccount_Click(null, null);
                    btnAccount.Focus();
                    break;
                case "Asset":
                    btnAsset_Click(null, null);
                    btnAsset.Focus();
                    break;
                case "Maintenance":
                    btnMaintenance_Click(null, null);
                    btnMaintenance.Focus();
                    break;
                case "Security":
                    btnSecurity_Click(null, null);
                    btnSecurity.Focus();
                    break;
                case "CRM":
                    btnCrm_Click(null, null);
                    btnCrm.Focus();
                    break;
                default:                    
                    break;
            }
        }
        
        private void timerInfo_Tick(object sender, EventArgs e)
        {

        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe"); 
        }
               
        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            GuiWarehouse frmgui = new GuiWarehouse();
            AddFormToPanel(frmgui, pnlMain);
            ini.IniWriteValue("LXERP", "DEFAULT_BAR", "Warehouse");
            btnWarehouse.Focus();
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            GuiMaintenance frmgui = new GuiMaintenance();
            AddFormToPanel(frmgui, pnlMain);
            ini.IniWriteValue("LXERP", "DEFAULT_BAR", "Maintenance");
            btnMaintenance.Focus();
        }

        private void btnSecurity_Click(object sender, EventArgs e)
        {
            GuiSecurity frmgui = new GuiSecurity();
            AddFormToPanel(frmgui, pnlMain);
            ini.IniWriteValue("LXERP", "DEFAULT_BAR", "Security");
            btnSecurity.Focus();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            GuiSales frmgui = new GuiSales();
            AddFormToPanel(frmgui, pnlMain);
            ini.IniWriteValue("LXERP", "DEFAULT_BAR", "Sales");
            btnSales.Focus();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            GuiPurchase frmgui = new GuiPurchase();
            AddFormToPanel(frmgui, pnlMain);
            ini.IniWriteValue("LXERP", "DEFAULT_BAR", "Purchase");
            btnPurchase.Focus();
        }

        private void btnProduction_Click(object sender, EventArgs e)
        {
            GuiProduction frmgui = new GuiProduction();
            AddFormToPanel(frmgui, pnlMain);
            ini.IniWriteValue("LXERP", "DEFAULT_BAR", "Production");
            btnProduction.Focus();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            GuiAccount frmgui = new GuiAccount();
            AddFormToPanel(frmgui, pnlMain);
            ini.IniWriteValue("LXERP", "DEFAULT_BAR", "Account");
            btnAccount.Focus();
        }

        private void btnAsset_Click(object sender, EventArgs e)
        {
            GuiAsset frmgui = new GuiAsset();
            AddFormToPanel(frmgui, pnlMain);
            ini.IniWriteValue("LXERP", "DEFAULT_BAR", "Asset");
            btnAsset.Focus();
        }


        private void btnCrm_Click(object sender, EventArgs e)
        {
            GuiCRM frmgui = new GuiCRM();
            AddFormToPanel(frmgui, pnlMain);
            ini.IniWriteValue("LXERP", "DEFAULT_BAR", "CRM");
            btnCrm.Focus();
        }

        public void SetMenuPower()
        {
            int i;

            dalTaskGrant dal = new dalTaskGrant();
            BindingCollection<modTaskGrant> grantlist = dal.GetUserGrantData(true, false, Util.UserId, string.Empty, string.Empty, out Util.emsg);

            if (grantlist != null)
            {
                for (i = 0; i < menuMain.Items.Count; i++)
                {               
                    GetSubMenu((ToolStripMenuItem)menuMain.Items[i], grantlist);
                }                
            }
            
            //toolPieceworkCreate.Enabled = OPA_PIECEWORK_LIST_CREATE.Enabled;
            //toolPieceworkWorkAudit.Enabled = OPA_PIECEWORK_LIST_AUDIT.Enabled;
            //toolPieceworkBatchAdd.Enabled = OPA_PIECEWORK_BATCH_ADD.Enabled;
            if (Util.UserId.CompareTo("READ") != 0)
                SEC_CHANGE_PASSWORD.Enabled = true;
            else
                SEC_CHANGE_PASSWORD.Enabled = false;
        }

        private void GetSubMenu(ToolStripMenuItem tsmItem, BindingCollection<modTaskGrant> grantlist)
        {
            int i;
            if (tsmItem.DropDownItems.Count > 0)
            {
                for (i = 0; i < tsmItem.DropDownItems.Count; i++)
                {
                    if ((tsmItem.DropDownItems[i].Tag == null || tsmItem.DropDownItems[i].Tag.ToString() != "T") && tsmItem.DropDownItems[i].GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                    {
                        ToolStripMenuItem subitem = (ToolStripMenuItem)tsmItem.DropDownItems[i];
                        if (subitem.DropDownItems.Count > 0)
                        {
                            GetSubMenu(subitem, grantlist);
                        }
                        else
                        {
                            subitem.Enabled = false;
                            if (subitem.Name != null)
                            {
                                foreach (modTaskGrant mod in grantlist)
                                {
                                    if (mod.Url == subitem.Name.ToString())
                                    {
                                        subitem.Enabled = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }                    
                }
            }
        }
        
        public bool CheckChildFrmExist(string childFrmName)
        {
            try
            {
                foreach (TabPage page in tabControl1.TabPages)
                {
                    if (page.ToolTipText == childFrmName) //用子窗体的Name进行判断，如果存在则将他激活
                    {
                        tabControl1.SelectedTab = page;                        
                        return true;
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

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            LoadDefaultBar();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tileHorizonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void casecadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void arrangeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bLogined && Util.modperiod.LockFlag==0)
            {
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to exit the system?"), clsTranslate.TranslateString("Information"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    //Application.Exit();

                }
            }
        }
                
        private int MdiChildrenCount()
        {
            int i = 0;
            foreach (Form f in this.MdiChildren)
                i++;
            return i;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void mnuTempletes_Click(object sender, EventArgs e)
        {
            string templetefile = clsLxms.GetParameterValue("EXCEL_TEMPLETE_FILE");
            if (File.Exists(templetefile))
            {
                Process.Start(templetefile);
            }
        }

        private void userGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SEC_ROLE_LIST") == true)
            {
                return;
            }
            SEC_ROLE_LIST newFrm = new SEC_ROLE_LIST();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void userListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SEC_USER_LIST") == true)
            {
                return;
            }
            SEC_USER_LIST newFrm = new SEC_USER_LIST();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void taskListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SEC_TASK_LIST") == true)
            {
                return;
            }
            SEC_TASK_LIST newFrm = new SEC_TASK_LIST();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void taskGrantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SEC_TASK_GRANT") == true)
            {
                return;
            }
            SEC_TASK_GRANT newFrm = new SEC_TASK_GRANT();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void frmMain_MdiChildActivate(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild != null)
                {
                    this.tabControl1.Visible = true;
                    for (int i = 0; i < this.tabControl1.TabCount; i++)
                    {
                        if (this.ActiveMdiChild.Equals(tabControl1.TabPages[i].Tag))
                        {
                            this.tabControl1.SelectedTab = this.tabControl1.TabPages[i];
                            break;
                        }
                    }
                }
                else
                {
                    this.tabControl1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                StatusLabel1.Text = ex.Message;
            }    
        }


        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (tabControl1.SelectedIndex == 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("This is the home page, you can not close it!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                TabPage tp = tabControl1.TabPages[tabControl1.SelectedIndex];
                Form frmChild = (Form)tp.Tag;
                if (frmChild != null)
                {
                    if (_arrtab.Count > 0)
                    {
                        for (int i = _arrtab.Count-1; i >=0 ; i--)
                        {
                            if (_arrtab[i].ToString() == frmChild.Text)
                                _arrtab.RemoveAt(i);
                        }
                    }
                    frmChild.Dispose();
                    if (_arrtab.Count > 0)
                    {
                        for (int i = 0; i < tabControl1.TabPages.Count; i++)
                        {
                            if (tabControl1.TabPages[i].Text == _arrtab[0].ToString())
                            {
                                tabControl1.SelectedIndex = i;
                                break;
                            }
                        }
                    }
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

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (tabControl1.TabPages.Count == 0) return;
                for (int i = tabControl1.TabPages.Count - 1; i >= 1; i--)
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
                _arrtab.Clear();
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout fAbout = new frmAbout();
            fAbout.ShowDialog();
        }
        
        private void taskGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SEC_TASK_GROUP") == true)
            {
                return;
            }
            SEC_TASK_GROUP newFrm = new SEC_TASK_GROUP();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void skinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOptions fSkins = new frmOptions();
            fSkins.ShowDialog();
            InitColor();
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == null) return;
                if (_arrtab.Count == 0 || (_arrtab.Count > 0 && tabControl1.SelectedTab.Text != _arrtab[0].ToString()))
                    _arrtab.Insert(0, tabControl1.SelectedTab.Text);

                if (tabControl1.SelectedIndex == 0 && _currtabindex != tabControl1.SelectedIndex)
                {
                    LoadDefaultBar();
                }
                _currtabindex = tabControl1.SelectedIndex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddMDIChildToTabCtrl(Form frmChild, TabControl tab)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmChild.MdiParent = this;
                frmChild.Disposed += new EventHandler(frmChild_Closed);
                frmChild.Closed += new EventHandler(frmChild_Closed);
                this.MdiChildActivate += new EventHandler(frmMain_MdiChildActivate);
                tab.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
                tab.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);

                TabPage tp = new TabPage();
                tp.Parent = tabControl1;
                tp.Text =clsTranslate.TranslateString(frmChild.Text);
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
                if (_arrtab.Count == 0 || (_arrtab.Count > 0 && frmChild.Text != _arrtab[0].ToString()))
                    _arrtab.Insert(0, frmChild.Text);
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

        private void AddFormToPanel(Form frmChild, Panel pl)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmChild.MdiParent = this;
                frmChild.Disposed += new EventHandler(frmChild_Closed);
                frmChild.Closed += new EventHandler(frmChild_Closed);
                this.MdiChildActivate += new EventHandler(frmMain_MdiChildActivate);                
                pl.Tag = frmChild;
                frmChild.Parent = pl;
                frmChild.Dock = DockStyle.Fill;
                pl.Show();
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
                        if (_arrtab.Count > 0)
                        {
                            for (int i = _arrtab.Count - 1; i >= 0; i--)
                            {
                                if (_arrtab[i].ToString() == tabControl1.TabPages[idx].Text)
                                    _arrtab.RemoveAt(i);
                            }
                        }
                        if (_arrtab.Count > 0)
                        {
                            for (int i = 0; i < tabControl1.TabPages.Count; i++)
                            {
                                if (tabControl1.TabPages[i].Text == _arrtab[0].ToString())
                                {
                                    tabControl1.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
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

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    Rectangle rct;
                    for (int i = 0; i < tabControl1.TabPages.Count; i++)
                    {
                        rct = tabControl1.GetTabRect(i);

                        if (rct.Contains(e.X, e.Y))
                        {
                            tabControl1.SelectedTab = tabControl1.TabPages[i];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StatusLabel1.Text = ex.Message;
            }
        }
        
        private void mnuClearOperationData_Click(object sender, EventArgs e)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want clear database operation data?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want clear database operation data?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want clear database operation data?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                dalSysModifyLog dal = new dalSysModifyLog();
                bool ret = dal.ClearOperattionData(out Util.emsg);
                if (ret)
                    MessageBox.Show(clsTranslate.TranslateString("Success"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(Util.emsg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                
        private void SEC_CHANGE_PASSWORD_Click(object sender, EventArgs e)
        {            
            SEC_CHANGE_PASSWORD frm = new SEC_CHANGE_PASSWORD();
            frm.ShowDialog();
        }
        
        private void OPA_RELOGIN_Click(object sender, EventArgs e)
        {
            try
            {
                frmLogin fLogin = new frmLogin();
                fLogin.BackColor = frmOptions.BACKCOLOR;
                fLogin.ShowDialog();
                if (fLogin.DialogResult != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
                else
                {
                    LoadLoginData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }        
        
        private void SEC_USER_LIST_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SEC_USER_LIST") == true)
            {
                return;
            }
            SEC_USER_LIST newFrm = new SEC_USER_LIST();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }
        
        private void SEC_SYSTEM_PARAMETERS_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SEC_SYSTEM_PARAMETERS") == true)
            {
                return;
            }
            SEC_SYSTEM_PARAMETERS newFrm = new SEC_SYSTEM_PARAMETERS();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void SYS_MODIFY_LOG_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SYS_MODIFY_LOG") == true)
            {
                return;
            }
            SYS_MODIFY_LOG newFrm = new SYS_MODIFY_LOG();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void SYS_BULLETIN_FORM_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SYS_BULLETIN_FORM") == true)
            {
                return;
            }
            SYS_BULLETIN_FORM newFrm = new SYS_BULLETIN_FORM();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void mnuDatabaseOperation_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("frmDatabase") == true)
            {
                return;
            }
            frmDatabase newFrm = new frmDatabase();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void mnuSoftRegister_Click(object sender, EventArgs e)
        {
            frmSoftRegister frm = new frmSoftRegister();
            frm.ShowDialog();
            string systemname = clsLxms.GetParameterValue("SYSTEM_NAME");            
            if (!Util.SOFT_REGISTER)
                this.Text = systemname + " - [" + clsTranslate.TranslateString("Unregister") + "]";
            else
                this.Text = systemname;
        }

        private void QRY_TASK_GRANT_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("QRY_TASK_GRANT") == true)
            {
                return;
            }
            QRY_TASK_GRANT newFrm = new QRY_TASK_GRANT();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_PERIOD_LIST_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("ACC_PERIOD_LIST") == true)
            {
                return;
            }
            ACC_PERIOD_LIST newFrm = new ACC_PERIOD_LIST();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void SYS_GUIDE_Click(object sender, EventArgs e)
        {
            if (this.CheckChildFrmExist("SYS_GUIDE") == true)
            {
                return;
            }
            SYS_GUIDE newFrm = new SYS_GUIDE();
            if (newFrm != null && !ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void lblTrialBalance_DoubleClick(object sender, EventArgs e)
        {
            Util.IsTrialBalance = false;
            lblTrialBalance.Visible = false;
        }
    }
}