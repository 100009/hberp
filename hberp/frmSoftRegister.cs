using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class frmSoftRegister : Form
    {
        public frmSoftRegister()
        {
            InitializeComponent();
        }

        private void frmSoftRegister_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Generate();
        }

        private void Generate()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                txtHostCode.Text = Util.HOST_CODE;
                txtHostCode.Tag = Util.Encrypt(Util.HOST_CODE, Util.PWD_MASK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (txtHostCode.Tag == null)
                {
                    MessageBox.Show("请先生成机器码！", "注册提示");
                }
                if (!string.IsNullOrEmpty(txtRegisterCode.Text.Trim()))
                {
                    if (txtHostCode.Tag.ToString().CompareTo(txtRegisterCode.Text.Trim()) == 0)
                    {
                        Microsoft.Win32.RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("software", true).CreateSubKey("HBERP");
                        retkey.SetValue(txtHostCode.Text.Trim(), txtRegisterCode.Text.Trim());
                        Util.SOFT_REGISTER = true;                        
                        Util.REGISTER_CODE = txtRegisterCode.Text.Trim();
                        MessageBox.Show("注册成功！", "注册提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("注册码输入错误！", "注册提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("请输入注册码！", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnUnregister_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Win32.RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("software", true).CreateSubKey("HBERP");
                retkey.DeleteValue(txtHostCode.Text.Trim());
                Util.SOFT_REGISTER = false;                
                Util.REGISTER_CODE = string.Empty;
                MessageBox.Show("反注册成功！", "注册提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
