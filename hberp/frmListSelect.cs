using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class frmListSelect : Form
    {
        public frmListSelect()
        {
            InitializeComponent();
        }

        public frmListSelect(string p_HeaderList, string p_SourceList, string p_DestList)
        {
            InitializeComponent();
            Util.InitListView(lvSource, p_HeaderList);
            Util.InsertListView(lvSource, p_SourceList);
            Util.InitListView(lvDest, p_HeaderList);
            Util.InsertListView(lvDest, p_DestList);
        }
        
        public frmListSelect(string p_ShowCols, DataSet ds,string p_DestList)
        {
            InitializeComponent();
            Util.Fill_lv(ds, lvSource);
            string[] cols = p_ShowCols.Split(',');
            for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
            {                
                lvSource.Columns[j].Width = 0;               
            }
            for (int i = 0; i < cols.Length; i++)
            {
                for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (cols[i].ToLower() == ds.Tables[0].Columns[j].ColumnName.ToString().ToLower())
                    {
                        lvSource.Columns[j].Width = lvSource.Width / cols.Length;
                        break;
                    }
                }
                lvDest.Columns.Add(clsTranslate.TranslateString(cols[i]));
                lvDest.Columns[i].Width = lvDest.Width / cols.Length;
            }
            if (!string.IsNullOrEmpty(p_DestList))
            {
                string[] dest = p_DestList.Split(',');
                for (int iIndex = lvSource.Items.Count - 1; iIndex >= 0; iIndex--)
                {
                    for (int j = 0; j < dest.Length; j++)
                    {
                        if (lvSource.Items[iIndex].Text == dest[j])
                        {
                            ListViewItem tempitem = new ListViewItem();
                            tempitem = (ListViewItem)lvSource.Items[iIndex].Clone();
                            lvDest.Items.Add(tempitem);
                            lvSource.Items.RemoveAt(iIndex);
                            break;
                        }
                    }
                }
            }
        }

        private void btnSelOne_Click(object sender, EventArgs e)
        {
            for (int iIndex = lvSource.Items.Count - 1; iIndex >= 0; iIndex--)
            {
                if (lvSource.Items[iIndex].Selected == true)
                {
                    ListViewItem tempitem = new ListViewItem();
                    tempitem=(ListViewItem)lvSource.Items[iIndex].Clone();
                    lvDest.Items.Add(tempitem);
                    lvSource.Items.RemoveAt(iIndex);                 
                }
            }            
        }
        
        private void btnDelOne_Click(object sender, EventArgs e)
        {
            for (int iIndex = lvDest.Items.Count-1; iIndex >=0  ; iIndex--)
            {
                if (lvDest.Items[iIndex].Selected == true)
                {
                    ListViewItem tempitem = new ListViewItem();
                    tempitem = (ListViewItem)lvDest.Items[iIndex].Clone();
                    lvSource.Items.Add(tempitem);                    
                    lvDest.Items.RemoveAt(iIndex);
                }
            }           
        }
        
        private void btnSelAll_Click(object sender, EventArgs e)
        {
            for (int iIndex = lvSource.Items.Count - 1; iIndex >= 0; iIndex--)
            {                
                ListViewItem tempitem = new ListViewItem();
                tempitem = (ListViewItem)lvSource.Items[iIndex].Clone();
                lvDest.Items.Add(tempitem);
                lvSource.Items.RemoveAt(iIndex);                     
            }          
        }

        private void btnDelAll_Click(object sender, EventArgs e)
        {
            for (int iIndex = lvDest.Items.Count - 1; iIndex >= 0; iIndex--)
            {                
                ListViewItem tempitem = new ListViewItem();
                tempitem = (ListViewItem)lvDest.Items[iIndex].Clone();
                lvSource.Items.Add(tempitem);
                lvDest.Items.RemoveAt(iIndex);              
            }         
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Util.retValue1 = string.Empty;
            Util.retValue2 = string.Empty;
            for (int iRow = 0; iRow < lvDest.Items.Count; iRow++)
            {
                if (string.IsNullOrEmpty(Util.retValue1))
                    Util.retValue1 += lvDest.Items[iRow].Text.ToString();
                else
                    Util.retValue1 += "," + lvDest.Items[iRow].Text.ToString();
                if (lvDest.Columns.Count >= 2)
                {
                    if (string.IsNullOrEmpty(Util.retValue2))
                        Util.retValue2 += lvDest.Items[iRow].SubItems[1].Text.ToString();
                    else
                        Util.retValue2 += "," + lvDest.Items[iRow].SubItems[1].Text.ToString();
                }
            }
            

            this.DialogResult = DialogResult.OK;
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
                    lvSource.Items.Remove(newItem);
                    lvDest.Items.Remove(newItem);
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

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lvSource.Items.Count == 0) return;
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                lvSource.SelectedItems.Clear();
                for (int i = 0; i < lvSource.Items.Count; i++)
                {
                    if (lvSource.Items[i].Text.ToUpper().IndexOf(txtFind.Text.Trim().ToUpper()) >= 0)
                    {
                        lvSource.Items[i].Selected = true;
                        lvSource.Items[i].EnsureVisible();
                        txtFind.Text = string.Empty;
                        break;
                    }
                }
                contextMenuStrip1.Hide();
            }
        }

        private void frmListSelect_Load(object sender, EventArgs e)
        {
            lvSource.ListViewItemSorter = new ListViewColumnSorter();
            lvSource.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);
            lvDest.ListViewItemSorter = new ListViewColumnSorter();
            lvDest.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick); 
        }
    }
}