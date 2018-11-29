using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LXMS
{
    public partial class frmViewImage : Form
    {
        //string _imagepath = string.Empty;
        public frmViewImage(string imagepath)
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
            if (File.Exists(imagepath))
            {
                pictureBox1.ImageLocation = imagepath;
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                //pictureBox1.Refresh();
                Image img = Image.FromFile(imagepath);
                this.Width = img.Width +pictureBox1.Margin.Left+pictureBox1.Margin.Right + 10;
                this.Height = img.Height + pictureBox1.Margin.Top + pictureBox1.Margin.Bottom + 30;
                pictureBox1.Refresh();
                img.Dispose();
                
            }
            else
                pictureBox1.ImageLocation = string.Empty;
            this.Text = imagepath;
        }

        private void frmViewImage_Load(object sender, EventArgs e)
        {
                       
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void autoSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void centerImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
