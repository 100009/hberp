using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LXMS
{
    public partial class PictureBoxEx : PictureBox
    {
        Color _transparentcolor = Color.Magenta;
        public PictureBoxEx()
        {
            InitializeComponent();
            clsTranslate.TranslateMenu(contextMenuStrip1);            
        }

        private void toolView_Click(object sender, EventArgs e)
        {
            frmViewImage frm = new frmViewImage(this.ImageLocation);
            frm.ShowDialog();
        }

        private void toolSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SaveFileDialog sfd = new SaveFileDialog();
                RWReg reg = new RWReg("CURRENT_USER");
                sfd.InitialDirectory = reg.GetRegValue(@"\software\microsoft\windows\currentversion\explorer\shell folders", "Desktop", string.Empty);
                sfd.FileName = this.ImageLocation.Substring(this.ImageLocation.LastIndexOf("\\") + 1);
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        //if (MessageBox.Show(clsTranslate.TranslateString("This file is exist,do you want to override it?"), clsTranslate.TranslateString("Information"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                        File.Delete(sfd.FileName);
                    }
                    File.Copy(this.ImageLocation, sfd.FileName);
                    MessageBox.Show(clsTranslate.TranslateString("File save success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public Color TransparentColor
        {
            get { return _transparentcolor; }
            set { _transparentcolor = value; }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {            
            if (this.Image != null)
            {
                Bitmap img = this.Image as Bitmap;
                img.MakeTransparent(_transparentcolor);
            }
            //this.BackColor = frmOptions.BACKCOLOR;
            base.OnPaint(pe);
        }
    }
}
