using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using LXMS.Model;

namespace LXMS
{
    public partial class frmResult : Form
    {
        int _timer = 0;
        int _waitsecond = 0;
        public frmResult()
        {
            InitializeComponent();
        }

        private void frmSuccess_Load(object sender, EventArgs e)
        {
            DBGrid.AllowUserToAddRows = false;
            DBGrid.Visible = false;
        }

        public void InitViewList<T>(string title, IList<T> list, string result)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Text = title;
            lblResult.Text = result;
            if (result.CompareTo("Success") == 0)
            {
                _waitsecond = 1;
                lblResult.ForeColor = Color.YellowGreen;
            }
            else
            {
                _waitsecond = 15;
                lblResult.ForeColor = Color.Red;
            }
            if (list.Count > 0)
            {
                //panel1.Visible = false;                
                string msg = string.Empty;                
                if (!string.IsNullOrEmpty(msg))
                {
                    txtInfo.Text = msg;
                    panel1.Visible = true;
                    timer1.Enabled = false;
                    lblTimer.Visible = false;
                }
                else
                {
                    _waitsecond = 1;
                    panel1.Visible = false;
                    lblTimer.Text = _waitsecond.ToString();
                    lblTimer.Visible = true;
                    timer1.Enabled = true;
                }
            }
            else
            {
                //panel1.Visible = false;
                if (result.CompareTo("Success") == 0)
                    _waitsecond = 1;

                lblTimer.Text = _waitsecond.ToString();
                lblTimer.Visible = true;
                timer1.Enabled = true;                
            }
            btnClose.Focus();
            this.Cursor = Cursors.Default;
        }

        public void InitText(string title, string content, string result)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Text = title;
            lblResult.Text = result;
            if (result.CompareTo("Success") == 0)
            {
                _waitsecond = 3;
                lblResult.ForeColor = Color.YellowGreen;
            }
            else
            {
                _waitsecond = 15;
                lblResult.ForeColor = Color.Red;
            }
            if (!string.IsNullOrEmpty(content))
            {                
                this.Text = title;
                panel1.Visible = true;
                txtInfo.Text = content;
                txtInfo.BringToFront();
                txtInfo.Dock = DockStyle.Fill;
            }
            else
            {
                panel1.Visible = false;
                if (result.CompareTo("Success") == 0)            
                    _waitsecond = 1;                              
            }
            lblTimer.Text = _waitsecond.ToString();
            timer1.Enabled = true;
            btnClose.Focus();
            this.Cursor = Cursors.Default;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _timer++;

            lblTimer.Text = (_waitsecond - _timer).ToString();
            if (_waitsecond - _timer <= 0)
                this.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
