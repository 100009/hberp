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
    public partial class MTN_EMPLOYEE_LIST : BaseFormEdit
    {
        dalAdminEmployeeList _dal = new dalAdminEmployeeList();
        string _deptid = string.Empty;
        public MTN_EMPLOYEE_LIST()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void MTN_EMPLOYEE_LIST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            FillControl.FillStatus(cboStatus, false);
            FillControl.FillAdminDutyList(cboDuty, false, true);
            FillControl.FillAdminDegreeList(cboDegree, false, true);
            FillControl.FillAdminDeptList(cboDept, false, true);
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                BindingCollection<modAdminEmployeeList> list = _dal.GetIList(1, 10000, "0,1", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
                DBGrid.DataSource = list;
                DBGrid.Enabled = true;
                if (list != null && list.Count > 0)
                {
                    AddComboBoxColumns();                    
                    Status1 = DBGrid.Rows.Count.ToString();
                    Status2 = clsTranslate.TranslateString("Refresh");
                }
                else
                {
                    Util.EmptyFormBox(this);
                    imgEmployee.ImageLocation = string.Empty;
                    if (!string.IsNullOrEmpty(Util.emsg))
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //else
                    //    MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }                    
                Status3 = _deptid;
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

        private void AddComboBoxColumns()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Status");
            checkboxColumn.DataPropertyName = "Status";
            checkboxColumn.Name = "Status";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(2);
            DBGrid.Columns.Insert(2, checkboxColumn);
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modAdminEmployeeList mod = (modAdminEmployeeList)DBGrid.Rows[i].DataBoundItem;
                if (mod.EmployeeId.CompareTo(FindText) == 0 || mod.EmployeeName.IndexOf(FindText) > 0)
                {
                    DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                    DBGrid_SelectionChanged(null, null);
                    return;
                }
            }
        }

        protected override void Refresh()
        {
            LoadData();
        }

        protected override void Select()
        {
            if (DBGrid.CurrentRow == null) return;

            Util.retValue1 = DBGrid.CurrentRow.Cells[0].Value.ToString();
            Util.retValue2 = DBGrid.CurrentRow.Cells[1].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        protected override void New()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            Util.EmptyFormBox(this);
            cboStatus.SelectedIndex = 1;
            cboSex.SelectedIndex = -1;
            cboDegree.SelectedIndex = -1;
            cboDept.SelectedIndex = -1;
            imgEmployee.ImageLocation = string.Empty;
            txtEmployeeId.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            txtEmployeeId.ReadOnly = true;
            txtEmployeeName.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtEmployeeId.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                if (string.IsNullOrEmpty(txtEmployeeId.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Employee id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEmployeeId.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtEmployeeName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Employee name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEmployeeName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtIdCard.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Id Card") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtIdCard.Focus();
                    return false;
                }
                else if (txtIdCard.Text.Trim().Length == 15)
                {
                    string birth = txtIdCard.Text.Substring(6, 2) + "-" + txtIdCard.Text.Substring(8, 2) + "-" + txtIdCard.Text.Substring(10, 2);
                    if (DateTime.Parse(birth) != dtpBirthday.Value)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Birthday") + clsTranslate.TranslateString(" value is not correct with id card!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtIdCard.Focus();
                        return false;
                    }
                }
                else if (txtIdCard.Text.Trim().Length == 18)
                {
                    string birth = txtIdCard.Text.Substring(6, 4) + "-" + txtIdCard.Text.Substring(10, 2) + "-" + txtIdCard.Text.Substring(12, 2);
                    if (DateTime.Parse(birth).ToString("MM-dd-yyyy") != dtpBirthday.Value.ToString("MM-dd-yyyy"))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Birthday") + clsTranslate.TranslateString(" value is not correct with id card!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtIdCard.Focus();
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(clsTranslate.TranslateString("Id Card") + clsTranslate.TranslateString(" length is not correct!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtIdCard.Focus();
                    return false;
                }                
                if (cboSex.SelectedIndex < 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sex") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboSex.Focus();
                    return false;
                }
                if (cboDegree.SelectedIndex < 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Edu degree") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboDegree.Focus();
                    return false;
                }
                if (cboDept.SelectedValue == null)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Dept Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboDept.Focus();
                    return false;
                }
                
                int? status = cboStatus.SelectedIndex;
                DateTime outdate = DateTime.Parse("0001-1-1");
                modAdminEmployeeList mod = new modAdminEmployeeList();
                mod.EmployeeId = txtEmployeeId.Text.Trim();
                mod.EmployeeName = txtEmployeeName.Text.Trim();
                mod.Status = cboStatus.SelectedIndex;
                mod.DeptId = cboDept.SelectedValue.ToString();                
                mod.Duty = cboDuty.SelectedValue.ToString();
                mod.JoinDate = dtpInDate.Value;
                mod.LeaveDate = outdate;
                mod.Salary = 0;
                mod.Sex = cboSex.Text;
                mod.IdCard = txtIdCard.Text.Trim();
                mod.Birthday = dtpBirthday.Value;
                mod.School = txtSchool.Text.Trim();
                mod.Speciality = txtSpeciality.Text.Trim();
                mod.Addr = txtAddr.Text.Trim();
                mod.EduDegree = cboDegree.SelectedValue.ToString();
                mod.Phone = txtPhone.Text.Trim();
                mod.Email = txtEmail.Text.Trim();
                mod.QQ = txtQQ.Text.Trim();
                mod.Remark = txtRemark.Text.Trim();
                mod.ImgPath = txtImgPath.Text.Trim();
                mod.UpdateUser = Util.UserId;
                mod.UpdateTime = DateTime.Now;
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtEmployeeId.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    LoadData();
                    FindText = mod.EmployeeId;
                    Find();
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        protected override void Cancel()
        {
            Util.ChangeStatus(this, true);
            DBGrid.Enabled = true;
            DBGrid_SelectionChanged(null, null);
        }

        protected override bool Inactive()
        {
            if(DBGrid.CurrentRow==null) return false;
            DateTime outdate = DateTime.Today;
            if(cboStatus.SelectedIndex==1)
                outdate = DateTime.Parse("0001-1-1");
            bool ret = _dal.Inactive(txtEmployeeId.Text.Trim(), outdate, out Util.emsg);
            if (ret)
            {
                cboStatus.SelectedIndex = 1 - cboStatus.SelectedIndex;
                DBGrid.CurrentRow.Cells["status"].Value = cboStatus.SelectedIndex;
            }
            return ret;
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modAdminEmployeeList mod = (modAdminEmployeeList)DBGrid.CurrentRow.DataBoundItem;
                txtEmployeeId.Text = mod.EmployeeId;
                txtEmployeeName.Text = mod.EmployeeName;
                cboDept.SelectedValue = mod.DeptId;
                cboStatus.SelectedIndex = Convert.ToInt32(mod.Status);
                cboSex.Text = mod.Sex;
                cboDuty.SelectedValue = mod.Duty;
                cboDegree.SelectedValue = mod.EduDegree;
                txtIdCard.Text = mod.IdCard;
                dtpBirthday.Value = mod.Birthday;
                txtSchool.Text = mod.School;
                txtSpeciality.Text = mod.Speciality;
                txtEmail.Text = mod.Email;
                txtPhone.Text = mod.Phone;
                txtQQ.Text = mod.QQ;
                txtAddr.Text = mod.Addr;                
                txtRemark.Text = mod.Remark;
                txtImgPath.Text = mod.ImgPath;
                imgEmployee.ImageLocation = clsLxms.GetEmployeeImagePath(mod.ImgPath);
                FindText = mod.EmployeeId;
            }
            else
            {
                Util.EmptyFormBox(this);
            }
        }


        private void btnImgPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtImgPath.Text = ofd.FileName;
            }
        }

        private void DBGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtEmployeeId.ReadOnly == true && e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo info = DBGrid.HitTest(e.X, e.Y);
                if (info.RowIndex >= 0)
                {
                    DataGridViewRow dr = (DataGridViewRow)DBGrid.Rows[info.RowIndex];
                    if (dr != null)
                        DBGrid.DoDragDrop(dr, DragDropEffects.Copy);
                }
            }
        }

        private void tvLeft_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void txtEmployeeId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtIdCard_Validated(object sender, EventArgs e)
        {
            if (txtIdCard.Text.Trim().Length == 15)
            {
                string birth = txtIdCard.Text.Substring(6, 2) + "-" + txtIdCard.Text.Substring(8, 2) + "-" + txtIdCard.Text.Substring(10, 2);
                dtpBirthday.Value = DateTime.Parse(birth);                
            }
            else if (txtIdCard.Text.Trim().Length == 18)
            {
                string birth = txtIdCard.Text.Substring(6, 4) + "-" + txtIdCard.Text.Substring(10, 2) + "-" + txtIdCard.Text.Substring(12, 2);
                dtpBirthday.Value = DateTime.Parse(birth);
            }
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (SelectVisible)
                Select();
        }

        private void cboDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDegree.SelectedValue == null) return;
            if (cboDegree.SelectedValue.ToString().CompareTo("New...") != 0) return;

            MTN_DEGREE_LIST frm = new MTN_DEGREE_LIST();
            frm.ShowDialog();
            FillControl.FillAdminDegreeList(cboDegree, false, true);
        }

        private void cboDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDuty.SelectedValue == null) return;
            if (cboDuty.SelectedValue.ToString().CompareTo("New...") != 0) return;

            MTN_DUTY_LIST frm = new MTN_DUTY_LIST();
            frm.ShowDialog();
            FillControl.FillAdminDutyList(cboDuty, false, true);
        }

        private void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDept.SelectedValue == null) return;
            if (cboDept.SelectedValue.ToString().CompareTo("New...") != 0) return;

            MTN_DEPT_LIST frm = new MTN_DEPT_LIST();
            frm.ShowDialog();
            FillControl.FillAdminDeptList(cboDept, false, true);
        }        
    }
}