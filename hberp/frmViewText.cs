using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class frmViewText : Form
    {
        //public static string strText;

        public frmViewText(string P_Text)
        {
            InitializeComponent();
            txtView.Text = P_Text;            
            //txtView.Focus();
        }

        public frmViewText(string title,string P_Text)
        {
            InitializeComponent();
            this.Text = title;
            txtView.Text = P_Text;
            //txtView.Focus();
        }

        private void frmViewText_Load(object sender, EventArgs e)
        {

        }

        private void frmViewText_Shown(object sender, EventArgs e)
        {
            txtView.SelectionLength = 0;
        }        
    }
}