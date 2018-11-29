using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class MsgLabel : Label
    {
        PointF p;
        Font _ft = new Font("宋体", 12);
        string _temp = string.Empty;
        string _msg="无信息";
        Color _backcolor = Color.DarkGreen;
        Color _forecolor = Color.Yellow;
        INIClass ini = new INIClass(Util.INI_FILE);
        public MsgLabel()
        {
            InitializeComponent();            
        }

        public void InitLanguage()
        {
            for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
            {
                contextMenuStrip1.Items[i].Text = clsTranslate.TranslateString(contextMenuStrip1.Items[i].Text);
            }
        }

        public string Message
        {
            get { return _msg; }
            set { _msg = value; }
        }

        public int Speed
        {
            set { 
                if(value>0)
                    timer1.Interval = 1000 / value; 
                else
                    timer1.Interval = 200; 
            }
        }

        public void GetSetting()
        {
            if (this.Tag != null)
            {
                string bkcolor = ini.IniReadValue(this.Tag.ToString(), "BACKCOLOR");
                if (!string.IsNullOrEmpty(bkcolor))
                {
                    _backcolor = Color.FromName(bkcolor);
                    this.BackColor = _backcolor;
                }

                string forecolor = ini.IniReadValue(this.Tag.ToString(), "FORECOLOR");
                if (!string.IsNullOrEmpty(forecolor))
                    _forecolor = Color.FromName(forecolor);

                string fontsize = ini.IniReadValue(this.Tag.ToString(), "FONTSIZE");
                if (!string.IsNullOrEmpty(fontsize))
                    _ft = new Font("宋体", Convert.ToInt32(fontsize));

                string speed = ini.IniReadValue(this.Tag.ToString(), "SPEED");
                if (!string.IsNullOrEmpty(bkcolor))
                    Speed = Convert.ToInt32(speed);
            }
        }
        private void ShowText()
        {
            //Brush brush = Brushes.Yellow; 
            SolidBrush brush = new SolidBrush(_forecolor);
            Graphics g = this.CreateGraphics();
            SizeF s = new SizeF();
            s = g.MeasureString(_msg, _ft);//测量文字长度     
            g.Clear(_backcolor);//清除背景   

            if (string.IsNullOrEmpty(_temp) || _temp != _msg)//文字改变时,重新显示     
            {
                p = new PointF(this.Size.Width, 0);
                _temp = _msg;
            }
            else
                p = new PointF(p.X - 10, 0);//每次偏移10     
            if (p.X <= -s.Width)
                p = new PointF(this.Size.Width, 0);
            g.DrawString(_msg, _ft, brush, p);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowText();
        }

        private void MsgLabel_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void MsgLabel_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void toolSetting_Click(object sender, EventArgs e)
        {
            frmMsgLabelSetting frm = new frmMsgLabelSetting(this.Tag.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
                GetSetting();
        }

        private void toolHide_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Visible = false;            
        }

        private void toolView_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_msg, "View", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }  
    }
}
