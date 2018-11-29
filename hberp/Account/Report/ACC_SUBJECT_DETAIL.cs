using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class ACC_SUBJECT_DETAIL : Form
    {
        public ACC_SUBJECT_DETAIL()
        {
            InitializeComponent();
        }

        private void ACC_SUBJECT_DETAIL_Load(object sender, EventArgs e)
        {
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            FillControl.FillPeriodList(cboAccName);
            cboSubjectType.Items.Add(clsTranslate.TranslateString("Cash and Bank"));
            cboSubjectType.Items.Add(clsTranslate.TranslateString("Account Subject List"));
            cboSubjectType.SelectedIndex = 0;
        }

        private void cboAccName_SelectedIndexChanged(object sender, EventArgs e)
        { 
            TreeViewEventArgs arg=new TreeViewEventArgs(tvSubject.SelectedNode);
            tvSubject_AfterSelect(null, arg);
        }

        private void cboSubjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTree();
        }

        private void LoadTree()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                tvSubject.ImageList = clsLxms.GetImageList();
                DBGrid.DataSource = null;
                tvSubject.Nodes.Clear();
                tvSubject.BackColor = frmOptions.BACKCOLOR;
                switch (cboSubjectType.SelectedIndex)
                {
                    case 0:
                        dalAccBankAccount dal0 = new dalAccBankAccount();
                        BindingCollection<modAccBankAccount> list0 = dal0.GetIList(out Util.emsg);
                        if (list0 != null && list0.Count > 0)
                        {
                            foreach (modAccBankAccount mod in list0)
                            {
                                tvSubject.Nodes.Add(mod.AccountNo, mod.AccountNo + "(" + mod.BankName + ")", 0, 1);
                            }
                        }
                        break;
                    case 1:
                        dalAccSubjectList dal1 = new dalAccSubjectList();
                        BindingCollection<modAccSubjectList> list1 = dal1.GetChildrenList(out Util.emsg);
                        if (list1 != null && list1.Count > 0)
                        {
                            foreach (modAccSubjectList mod in list1)
                            {
                                tvSubject.Nodes.Add(mod.SubjectId, mod.SubjectName, 0, 1);
                            }
                        }
                        break;
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

        private void tvSubject_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (tvSubject.SelectedNode == null) return;

                //decimal startsum = 0;
                //decimal borrowsum = 0;
                //decimal lendsum = 0;
                //decimal endsum = 0;
                dalAccReport dal = new dalAccReport();
                switch (cboSubjectType.SelectedIndex)
                {
                    case 0:
                        BindingCollection<modAccCredenceDetail> list0 = dal.GetCashAndBankDetail(cboAccName.SelectedValue.ToString(), tvSubject.SelectedNode.Name, Util.IsTrialBalance, out Util.emsg);
                        DBGrid.DataSource = list0;                        
                        if (list0 == null && !string.IsNullOrEmpty(Util.emsg))
                        {
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if(list0.Count>0)
                        {							
							DBGrid.Columns["LastBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
							DBGrid.Columns["ThisBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
							DBGrid.Columns["LendMoney"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            DBGrid.Columns["BorrowMoney"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

							Status1.Text = clsTranslate.TranslateString("Start Money") + " :  " + string.Format("{0:C2}", list0[list0.Count-1].LastBalance);
							Status2.Text = clsTranslate.TranslateString("Borrow Money") + " :  " + string.Format("{0:C2}", list0[list0.Count - 1].BorrowMoney);
							Status3.Text = clsTranslate.TranslateString("Lend Money") + " :  " + string.Format("{0:C2}", list0[list0.Count - 1].LendMoney);
							Status4.Text = clsTranslate.TranslateString("End Money") + " :  " + string.Format("{0:C2}", list0[list0.Count - 1].ThisBalance);
						}
                        break;
                    case 1:
                        BindingCollection<modAccCredenceDetail> list1 = new BindingCollection<modAccCredenceDetail>();
                        dal.GetCredenceDetail(true, cboAccName.SelectedValue.ToString(), tvSubject.SelectedNode.Name, Util.IsTrialBalance, ref list1, out Util.emsg);
                        DBGrid.DataSource = list1;                        
                        if (list1 == null && !string.IsNullOrEmpty(Util.emsg))
                        {
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (list1.Count > 0)
                        {
							DBGrid.Columns["LastBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
							DBGrid.Columns["ThisBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
							DBGrid.Columns["LendMoney"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            DBGrid.Columns["BorrowMoney"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

							Status1.Text = clsTranslate.TranslateString("Start Money") + " :  " + string.Format("{0:C2}", list1[list1.Count - 1].LastBalance);
							Status2.Text = clsTranslate.TranslateString("Borrow Money") + " :  " + string.Format("{0:C2}", list1[list1.Count - 1].BorrowMoney);
							Status3.Text = clsTranslate.TranslateString("Lend Money") + " :  " + string.Format("{0:C2}", list1[list1.Count - 1].LendMoney);
							Status4.Text = clsTranslate.TranslateString("End Money") + " :  " + string.Format("{0:C2}", list1[list1.Count - 1].ThisBalance);
						}
                        break;
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

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                EditAccCredenceList frm = new EditAccCredenceList();
                frm.EditItem(cboAccName.SelectedValue.ToString(), Convert.ToInt32(DBGrid.CurrentRow.Cells["accseq"].Value));
                frm.ShowDialog();
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
