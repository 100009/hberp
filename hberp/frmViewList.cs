using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class frmViewList : Form
    {
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel1;
        private IContainer components;
        private ToolStripStatusLabel statusLabel2;
        private RowMergeView DBGrid;
        private bool _selection = false;
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewList));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DBGrid = new LXMS.RowMergeView();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel1,
            this.statusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 402);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(629, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel1
            // 
            this.statusLabel1.AutoSize = false;
            this.statusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(129, 17);
            this.statusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabel2
            // 
            this.statusLabel2.AutoSize = false;
            this.statusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusLabel2.Name = "statusLabel2";
            this.statusLabel2.Size = new System.Drawing.Size(120, 17);
            this.statusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DBGrid
            // 
            this.DBGrid.AllowUserToAddRows = false;
            this.DBGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.DBGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DBGrid.BackgroundColor = System.Drawing.Color.White;
            this.DBGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBGrid.Location = new System.Drawing.Point(0, 0);
            this.DBGrid.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DBGrid.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid.MergeColumnNames")));
            this.DBGrid.MultiSelect = false;
            this.DBGrid.Name = "DBGrid";
            this.DBGrid.ReadOnly = true;
            this.DBGrid.RowChanged = false;
            this.DBGrid.RowTemplate.Height = 23;
            this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid.Size = new System.Drawing.Size(629, 402);
            this.DBGrid.TabIndex = 9;
            this.DBGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DBGrid_CellLeave);
            this.DBGrid.DoubleClick += new System.EventHandler(this.DBGrid_DoubleClick);
            // 
            // frmViewList
            // 
            this.ClientSize = new System.Drawing.Size(629, 424);
            this.Controls.Add(this.DBGrid);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmViewList_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public frmViewList()
        {
            InitializeComponent();
        }


        private void frmViewList_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
        }

        public bool Selection
        {
            get { return _selection; }
            set { _selection = value; }
        }

        public frmViewList(string strTitle,DataSet ds)
        {
            InitializeComponent();

            if (ds == null)
            {
                MessageBox.Show("No data found!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            this.Text = strTitle;
            DBGrid.DataSource = ds.Tables[0];
            DBGrid.AlternatingRowsDefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid.Tag="View List - " + strTitle;
            statusLabel1.Text = ds.Tables[0].Rows.Count.ToString() + " rows";
            Util.retValue1 = string.Empty;
            Util.retValue2 = string.Empty;
            this.Cursor = Cursors.Default;
        }

        public void InitViewList<T>(string strTitle, IList<T> list)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Text = strTitle;
            DBGrid.DataSource = list;
            DBGrid.AlternatingRowsDefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid.Tag = "View List - " + strTitle;
            if(list!=null)
                statusLabel1.Text = list.Count + " rows";
            Util.retValue1 = string.Empty;
            Util.retValue2 = string.Empty;
            this.Cursor = Cursors.Default;
        }
                 
        private void toolExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void DBGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DBGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.DarkCyan;
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            if (_selection)
            {
                Util.retValue1 = DBGrid.CurrentRow.Cells[0].Value.ToString();
                if(DBGrid.ColumnCount>=2)
                    Util.retValue2 = DBGrid.CurrentRow.Cells[1].Value.ToString();
                if (DBGrid.ColumnCount >= 3)
                    Util.retValue3 = DBGrid.CurrentRow.Cells[2].Value.ToString();
                if (DBGrid.ColumnCount >= 4)
                    Util.retValue4 = DBGrid.CurrentRow.Cells[3].Value.ToString();
                if (DBGrid.ColumnCount >= 5)
                    Util.retValue5 = DBGrid.CurrentRow.Cells[4].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
            else
            {
                if (DBGrid.ColumnCount > 0)
                {
                    bool formtype = false, formid = false;
                    for (int i = 0; i < DBGrid.ColumnCount; i++)
                    {
                        if (DBGrid.Columns[i].Name.ToLower().CompareTo("formtype") == 0)
                            formtype = true;
                        if (DBGrid.Columns[i].Name.ToLower().CompareTo("formid") == 0)
                            formid = true;
                    }

                    if (formtype && formid)
                    {
                        switch (DBGrid.CurrentRow.Cells["formtype"].Value.ToString())
                        {
                            case "送货单":
                            case "收营单":
                            case "退货单":
                                EditSalesShipment frmss = new EditSalesShipment();
                                frmss.EditItem(DBGrid.CurrentRow.Cells["formid"].Value.ToString());
                                frmss.ShowDialog();
                                break;
                            case "采购收货":
                            case "采购退货":
                                EditPurchaseList frmpur = new EditPurchaseList();
                                frmpur.EditItem(DBGrid.CurrentRow.Cells["formid"].Value.ToString());
                                frmpur.ShowDialog();
                                break;
                            case "生产领料出库":
                            case "损耗出库":
                            case "借入物出库":
                            case "借出物出库":
                            case "生产商品入库":
                            case "溢余入库":
                            case "借入物入库":
                            case "借出物入库":
                                EditWarehouseInout frmio = new EditWarehouseInout();
                                frmio.EditItem(Convert.ToInt32(DBGrid.CurrentRow.Cells["formid"].Value));
                                frmio.ShowDialog();
                                break;
                        }
                    }
                }
            }
        }
    }
}