using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    public partial class SEC_TASK_GRANT : Form
    {
        dalTaskGrant _dal = new dalTaskGrant();
        public SEC_TASK_GRANT()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SEC_TASK_GRANT_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalRoleList bll = new dalRoleList();
                //DataSet ds = bll.GetRoleList(true, out Util.emsg);
                tvUser2.ImageList = Util.GetImageList();
                BindingCollection<modRoleList> rolelist = new BindingCollection<modRoleList>();
                rolelist = bll.GetIList(true, out Util.emsg);
                if (rolelist != null)
                {
                    comboBox1.ValueMember = "RoleId";
                    comboBox1.DisplayMember = "RoleDesc";
                    comboBox1.DataSource = rolelist;
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                        //LoadListBox();
                    }

                    foreach (modRoleList role in rolelist)
                    {
                        dalUserList _ubll = new dalUserList();
                        BindingCollection<modUserList> userlist = _ubll.GetIList(role.RoleId, out Util.emsg);
                        if (userlist != null)
                        {
                            TreeNode tn = tvUser2.Nodes.Add(role.RoleId, role.RoleDesc, 0, 1);
                            foreach (modUserList user in userlist)
                            {
                                TreeNode node = tn.Nodes.Add(user.UserId, user.UserName, 2, 3);
                                node.ToolTipText = node.Name;
                            }
                        }
                    }
                    tvUser2.ShowNodeToolTips = true;
                    dalTaskList task = new dalTaskList();
                    BindingCollection<modTaskList> tasklist = task.GetIList(string.Empty, true, false, out Util.emsg);
                    DBGrid2.DataSource = tasklist;                                        
                    for (int j = 0; j < DBGrid2.RowCount; j++)
                    {
                        DBGrid2.Rows[j].Cells["Taskname"].Value = clsTranslate.TranslateString(DBGrid2.Rows[j].Cells["Taskname"].Value.ToString());
                    }
                    
                    DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
                    checkboxColumn.HeaderText = clsTranslate.TranslateString("Select");
                    checkboxColumn.DataPropertyName = "Select";
                    DBGrid2.Columns.Insert(0, checkboxColumn);
                                        
                    DBGrid2.Columns[9].Visible = false;
                    DBGrid2.Columns[8].Visible = false;
                    DBGrid2.Columns[7].Visible = false;
                    DBGrid2.Columns[6].Visible = false;
                    DBGrid2.Columns[5].Visible = false;
                    DBGrid2.Columns[4].Visible = false;
                    DBGrid2.Columns[3].Visible = false;

                    //DBGrid2.Columns[8].ReadOnly = true;
                    //DBGrid2.Columns[7].ReadOnly = true;
                    //DBGrid2.Columns[6].ReadOnly = true;
                    //DBGrid2.Columns[5].ReadOnly = true;
                    //DBGrid2.Columns[4].ReadOnly = true;
                    //DBGrid2.Columns[3].ReadOnly = true;
                    DBGrid2.Columns[2].ReadOnly = true;
                    DBGrid2.Columns[1].ReadOnly = true;
                    //Util.AutoSetColWidth(1, DBGrid2);
                    DBGrid2.AllowUserToAddRows = false;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
                LoadListBox();
        }

        private void LoadListBox()
        {
            this.Cursor = Cursors.WaitCursor;
                                                
            string strColHeader = "Task Code,Task Name";
            Util.Init_Lv_Header(strColHeader, lvDest);            
            Util.Init_Lv_Header(strColHeader, lvSource);
            BindingCollection<modTaskGrant> grant1 = _dal.GetGrantData(true, false, comboBox1.SelectedValue.ToString(), out Util.emsg);
            if (grant1 != null)
            {
                foreach (modTaskGrant mod in grant1)
                {
                    string[] str = new string[2];
                    str[0] = mod.TaskCode;
                    str[1] = clsTranslate.TranslateString(mod.TaskName);
                    ListViewItem item = new ListViewItem(str);
                    lvDest.Items.Add(item);
                }
            }
            
            BindingCollection<modTaskGrant> grant2 = _dal.GetReadyData(true, false, comboBox1.SelectedValue.ToString(), out Util.emsg);
            if (grant2 != null)
            {
                foreach (modTaskGrant mod in grant2)
                {
                    string[] str = new string[2];
                    str[0] = mod.TaskCode;
                    str[1] = clsTranslate.TranslateString(mod.TaskName);
                    ListViewItem item = new ListViewItem(str);
                    lvSource.Items.Add(item);
                }
            }
            this.Cursor = Cursors.Default;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            for (int iIndex = lvSource.Items.Count - 1; iIndex >= 0; iIndex--)
            {
                if (lvSource.Items[iIndex].Selected == true)
                {                    
                    ListViewItem tempItem = (ListViewItem)lvSource.Items[iIndex].Clone();
                    lvDest.Items.Remove(tempItem);
                    lvSource.Items.RemoveAt(iIndex);
                    lvDest.Items.Add(tempItem);                    
                }
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int iIndex = lvDest.Items.Count - 1; iIndex >= 0; iIndex--)
            {
                if (lvDest.Items[iIndex].Selected == true)
                {
                    ListViewItem tempItem = (ListViewItem)lvDest.Items[iIndex].Clone();
                    lvSource.Items.Remove(tempItem);
                    lvDest.Items.RemoveAt(iIndex);
                    lvSource.Items.Add(tempItem);                    
                }
            }            
        }
        
        private void toolSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ArrayList arrtask = new ArrayList();
            for(int i=0;i<lvDest.Items.Count;i++)
            {
                arrtask.Add(lvDest.Items[i].Text.Trim().ToString());                       
            }
            bool ret = _dal.SaveTaskGrant(comboBox1.SelectedValue.ToString(), arrtask, Util.UserId, out Util.emsg);
            if (!ret)
            {
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Success!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }
        private void lvSource_DoubleClick(object sender, EventArgs e)
        {
            button2_Click(null, null);
        }

        private void lvDest_DoubleClick(object sender, EventArgs e)
        {
            button3_Click(null, null);
        }

        private void lvSource_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lvSource_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //invoke   the   drag   and   drop   operation   
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void lvSource_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem tempItem = new ListViewItem();
            ListViewItem DestItem;
            Point pt;
            pt = lvSource.PointToClient(new Point(e.X, e.Y));
            DestItem = lvSource.GetItemAt(pt.X, pt.Y);
            if (DestItem == null)
            {
                ListViewItem newItem = (ListViewItem)e.Data.GetData("System.Windows.Forms.ListViewItem");
                tempItem = (ListViewItem)newItem.Clone();
                lvDest.Items.Remove(newItem);
                lvSource.Items.Remove(newItem);
                lvSource.Items.Add(tempItem);
            }
            else
            {
                ListViewItem newItem = (ListViewItem)e.Data.GetData("System.Windows.Forms.ListViewItem");
                if (!DestItem.Equals(newItem))
                {
                    tempItem = (ListViewItem)newItem.Clone();
                    lvDest.Items.Remove(newItem);
                    lvSource.Items.Remove(newItem);                    
                    lvSource.Items.Insert(DestItem.Index, tempItem);
                }
            }
        }

        private void lvDest_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lvDest_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem tempItem = new ListViewItem();
            ListViewItem DestItem;
            Point pt;
            pt = lvDest.PointToClient(new Point(e.X, e.Y));
            DestItem = lvDest.GetItemAt(pt.X, pt.Y);
            if (DestItem == null)
            {
                ListViewItem newItem = (ListViewItem)e.Data.GetData("System.Windows.Forms.ListViewItem");
                tempItem = (ListViewItem)newItem.Clone();
                lvSource.Items.Remove(newItem);
                lvDest.Items.Remove(newItem);
                lvDest.Items.Add(tempItem);
            }
            else
            {
                ListViewItem newItem = (ListViewItem)e.Data.GetData("System.Windows.Forms.ListViewItem");
                if (!DestItem.Equals(newItem))
                {
                    tempItem = (ListViewItem)newItem.Clone();
                    lvSource.Items.Remove(newItem);
                    lvDest.Items.Remove(newItem);
                    lvDest.Items.Insert(DestItem.Index, tempItem);
                }
            }
        }

        private void lvDest_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //invoke   the   drag   and   drop   operation   
                DoDragDrop(e.Item, DragDropEffects.Move);

            }
        }
        
        private void tvUser2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (DBGrid2.RowCount == 0) return;
            this.Cursor = Cursors.WaitCursor;
            for (int j = 0; j < DBGrid2.RowCount; j++)
            {
                DBGrid2.Rows[j].Cells[0].Value = 0;
            }
            if (e.Node.Level == 1)
            {
                //toolSave2.Enabled = true;
                DBGrid2.ReadOnly = false;
                DBGrid2.Columns[0].ReadOnly = false;
                BindingCollection<modTaskGrant> grantlist = _dal.GetUserGrantData(true, false, e.Node.Name.ToString(), string.Empty, string.Empty, out Util.emsg);
                if (grantlist != null)
                {
                    foreach (modTaskGrant mod in grantlist)
                    {
                        for (int j = 0; j < DBGrid2.RowCount; j++)
                        {
                            if (mod.TaskCode == DBGrid2.Rows[j].Cells[1].Value.ToString())
                            {
                                DBGrid2.Rows[j].Cells[0].Value = 1;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                DBGrid2.ReadOnly = true;
                //toolSave2.Enabled = false;
                BindingCollection<modTaskGrant> grantlist = _dal.GetGrantData(true, false, e.Node.Name.ToString(), out Util.emsg);
                if (grantlist != null)
                {
                    foreach (modTaskGrant mod in grantlist)
                    {
                        for (int j = 0; j < DBGrid2.RowCount; j++)
                        {
                            if (mod.TaskCode == DBGrid2.Rows[j].Cells[1].Value.ToString())
                            {
                                DBGrid2.Rows[j].Cells[0].Value = 1;
                                break;
                            }
                        }
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            Util.AutoSetColWidth(2, DBGrid2);
        }

        private void widthToHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.AutoSetColWidth(1, DBGrid2);
        }

        private void widthToContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.AutoSetColWidth(3, DBGrid2);
        }

        private void toolSave2_Click(object sender, EventArgs e)
        {
            bool ret;
            this.Cursor = Cursors.WaitCursor;
            DBGrid2.EndEdit();
            ArrayList arrtask = new ArrayList();
            for (int i = 0; i < DBGrid2.RowCount; i++)
            {
                if (Convert.ToBoolean(DBGrid2.Rows[i].Cells[0].Value) == true)
                {
                    arrtask.Add(DBGrid2.Rows[i].Cells[1].Value.ToString());
                }
            }

            if (tvUser2.SelectedNode.Level == 0)
            {
                ret = _dal.SaveTaskGrant(tvUser2.SelectedNode.Name.ToString(), arrtask, Util.UserId, out Util.emsg);
            }
            else
            {
                ret = _dal.SaveUserTaskGrant(tvUser2.SelectedNode.Name.ToString(), arrtask, Util.UserId, out Util.emsg);
            }            
            if (!ret)
            {
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Success!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 32)
            {
                if (string.IsNullOrEmpty(txtFind2.Text.Trim()))
                {
                    MessageBox.Show("Please enter the text you want to find!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dalUserList bll = new dalUserList();
                modUserList mod = bll.GetItem(txtFind2.Text.Trim().ToUpper());
                if (mod == null)
                {
                    MessageBox.Show("No data found!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    for (int i = 0; i < tvUser2.Nodes.Count; i++)
                    {
                        if (tvUser2.Nodes[i].GetNodeCount(false) > 0)
                        {
                            for (int j = 0; j < tvUser2.Nodes[i].GetNodeCount(false); j++)
                            {
                                if (mod.UserId == tvUser2.Nodes[i].Nodes[j].Name)
                                {
                                    tvUser2.Nodes[i].Expand();
                                    tvUser2.SelectedNode = tvUser2.Nodes[i].Nodes[j];
                                    TreeViewEventArgs ea = new TreeViewEventArgs(tvUser2.SelectedNode);
                                    tvUser2_AfterSelect(null, ea);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }   
    }
}