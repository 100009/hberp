using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    class clsTranslate
    {
        private static StringBuilder _sb = new StringBuilder();
        public static void InitLanguage(Form frm)
        {
            frm.BackColor = frmOptions.BACKCOLOR;
            if (Util.language == "0")
                return;

            _sb.Clear();
            frm.Text = TranslateString(frm.Text);
            string[] titles = Properties.Resources.Language.Replace("\n", "").Replace("\r", "").Split(';');
            if (titles != null)
            {
                for (int i = 0; i < titles.Length; i++)
                {
                    string[] title = titles[i].Split('-');
                    if (title.Length == 2)
                    {
                        string oldtitle, newtitle;
                        oldtitle = title[0].Replace("\r\n", "").ToLower();
                        newtitle = title[1].Replace("\r\n", "");
                        foreach (Control tb in frm.Controls)
                        {
                            switch (tb.GetType().ToString())
                            {
                                case "System.Windows.Forms.TextBox":
                                    ((TextBox)tb).ShortcutsEnabled = true;
                                    break;
                                case "System.Windows.Forms.ComboBox":
                                    break;
                                case "System.Windows.Forms.MenuStrip":
                                    MenuStrip ms = (MenuStrip)tb;
                                    for (int k = 0; k < ms.Items.Count; k++)
                                    {
                                        ToolStripMenuItem menuitem = (ToolStripMenuItem)ms.Items[k];
                                        if (menuitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                                        {
                                            menuitem.Text = newtitle;
                                            break;
                                        }
                                        else
                                            SetSubMenu((ToolStripMenuItem)ms.Items[k], oldtitle, newtitle);
                                    }
                                    break;
                                case "LXMS.ToolStripEx":
                                case "System.Windows.Forms.ToolStrip":
                                    ToolStrip ts = (ToolStrip)tb;
                                    string controltype = string.Empty;
                                    for (int k = 0; k < ts.Items.Count; k++)
                                    {
                                        ToolStripItem toolitem = (ToolStripItem)ts.Items[k];
                                        if (toolitem.GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                                        {
                                            if (toolitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                                            {
                                                toolitem.Text = newtitle;
                                                break;
                                            }
                                            else if (toolitem.GetType().ToString() == "System.Windows.Forms.ToolStripDropDownButton")
                                            {
                                                ToolStripDropDownButton tsb = (ToolStripDropDownButton)toolitem;
                                                for (int m = 0; m < tsb.DropDownItems.Count; m++)
                                                {
                                                    ToolStripDropDownItem tsbitem = (ToolStripDropDownItem)tsb.DropDownItems[m];
                                                    if (tsbitem.GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                                                    {
                                                        if (tsbitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                                                        {
                                                            tsbitem.Text = newtitle;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (toolitem.GetType().ToString() == "System.Windows.Forms.ToolStripSplitButton")
                                            {
                                                ToolStripSplitButton tsb = (ToolStripSplitButton)toolitem;
                                                for (int m = 0; m < tsb.DropDownItems.Count; m++)
                                                {
                                                    if (tsb.DropDownItems[m].GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                                                    {
                                                        ToolStripMenuItem tsbitem = (ToolStripMenuItem)tsb.DropDownItems[m];
                                                        SetSubMenu((ToolStripMenuItem)tsbitem, oldtitle, newtitle);
                                                        if (tsbitem.GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                                                        {
                                                            if (tsbitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                                                            {
                                                                tsbitem.Text = newtitle;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "LXMS.TabControlEx":
                                case "System.Windows.Forms.TabControl":
                                    TabControl tc = (TabControl)tb;
                                    foreach (TabPage tp in tc.Controls)
                                    {
                                        _sb.Append(tp.Text.ToString() + "-" + oldtitle + "\r\n");
                                        if (tp.Text.ToLower() == oldtitle)
                                        {
                                            tp.Text = newtitle;
                                            break;
                                        }
                                        foreach (Control ct in tp.Controls)
                                        {
                                            SetSubControl(ct, oldtitle, newtitle);
                                        }
                                    }
                                    break;
                                case "System.Windows.Forms.SplitContainer":
                                    SplitContainer sc = (SplitContainer)tb;
                                    SetSubControl(sc.Panel1, oldtitle, newtitle);
                                    SetSubControl(sc.Panel2, oldtitle, newtitle);
                                    break;
                                default:
                                    SetSubControl(tb, oldtitle, newtitle);
                                    break;
                            }
                        }
                        System.Reflection.FieldInfo[] fieldInfo = frm.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        for (int m = 0; m < fieldInfo.Length; m++)
                        {
                            switch (fieldInfo[m].FieldType.Name)
                            {
                                case "ContextMenuStrip":
                                    ContextMenuStrip cms = (ContextMenuStrip)fieldInfo[m].GetValue(frm);
                                    for (int k = 0; k < cms.Items.Count; k++)
                                    {
                                        if (cms.Items[k].GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                                        {
                                            ToolStripMenuItem menuitem = (ToolStripMenuItem)cms.Items[k];
                                            if (menuitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                                            {
                                                menuitem.Text = newtitle;
                                                break;
                                            }
                                            else
                                                SetSubMenu((ToolStripMenuItem)cms.Items[k], oldtitle, newtitle);
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            string retstr = _sb.ToString();
        }

        public static void SetSubControl(Control tb, string oldtitle, string newtitle)
        {
            switch (tb.GetType().ToString())
            {
                case "System.Windows.Forms.TextBox":
                    ((TextBox)tb).ShortcutsEnabled = true;
                    break;
                case "System.Windows.Forms.ComboBox":
                    break;
                case "System.Windows.Forms.MenuStrip":
                    MenuStrip ms = (MenuStrip)tb;
                    for (int k = 0; k < ms.Items.Count; k++)
                    {
                        ToolStripMenuItem menuitem = (ToolStripMenuItem)ms.Items[k];
                        if (menuitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                        {
                            menuitem.Text = newtitle;
                            break;
                        }
                        else
                            SetSubMenu((ToolStripMenuItem)ms.Items[k], oldtitle, newtitle);
                    }
                    break;
                case "LXMS.ToolStripEx":
                case "System.Windows.Forms.ToolStrip":
                    ToolStrip ts = (ToolStrip)tb;
                    for (int k = 0; k < ts.Items.Count; k++)
                    {
                        ToolStripItem toolitem = (ToolStripItem)ts.Items[k];
                        if (toolitem.GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                        {
                            if (toolitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                            {
                                toolitem.Text = newtitle;
                                break;
                            }
                            else if (toolitem.GetType().ToString() == "System.Windows.Forms.ToolStripDropDownButton")
                            {
                                ToolStripDropDownButton tsb = (ToolStripDropDownButton)toolitem;
                                for (int m = 0; m < tsb.DropDownItems.Count; m++)
                                {
                                    if (tsb.DropDownItems[m].GetType().ToString() == "System.Windows.Forms.ToolStripControlHost")
                                        continue;
                                    ToolStripDropDownItem tsbitem = (ToolStripDropDownItem)tsb.DropDownItems[m];
                                    if (tsbitem.GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                                    {
                                        if (tsbitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                                        {
                                            tsbitem.Text = newtitle;
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (toolitem.GetType().ToString() == "System.Windows.Forms.ToolStripSplitButton")
                            {
                                ToolStripSplitButton tsb = (ToolStripSplitButton)toolitem;
                                for (int m = 0; m < tsb.DropDownItems.Count; m++)
                                {
                                    ToolStripMenuItem tsbitem = (ToolStripMenuItem)tsb.DropDownItems[m];
                                    if (tsbitem.GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                                    {
                                        if (tsbitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                                        {
                                            tsbitem.Text = newtitle;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "LXMS.TabControlEx":
                case "System.Windows.Forms.TabControl":
                    TabControl tc = (TabControl)tb;
                    foreach (TabPage tp in tc.Controls)
                    {
                        if (tp.Text.ToLower() == oldtitle)
                        {
                            tp.Text = newtitle;
                            //break;
                        }
                        foreach (Control ct in tp.Controls)
                        {
                            SetSubControl(ct, oldtitle, newtitle);
                        }
                    }
                    break;
                case "System.Windows.Forms.SplitContainer":
                    SplitContainer sc = (SplitContainer)tb;
                    SetSubControl(sc.Panel1, oldtitle, newtitle);
                    SetSubControl(sc.Panel2, oldtitle, newtitle);
                    break;
                default:
                    if (!string.IsNullOrEmpty(tb.Text) && tb.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                    {
                        tb.Text = newtitle;
                    }
                    if (tb.Controls.Count > 0)
                    {
                        foreach (Control ch in tb.Controls)
                        {
                            SetSubControl(ch, oldtitle, newtitle);
                        }
                    }
                    break;
            }

            System.Reflection.FieldInfo[] fieldInfo = tb.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            for (int m = 0; m < fieldInfo.Length; m++)
            {
                switch (fieldInfo[m].FieldType.Name)
                {
                    case "ContextMenuStrip":
                        ContextMenuStrip cms = (ContextMenuStrip)fieldInfo[m].GetValue(tb);
                        for (int k = 0; k < cms.Items.Count; k++)
                        {
                            if (cms.Items[k].GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                            {
                                ToolStripMenuItem menuitem = (ToolStripMenuItem)cms.Items[k];
                                if (menuitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                                {
                                    menuitem.Text = newtitle;
                                    break;
                                }
                                else
                                    SetSubMenu((ToolStripMenuItem)cms.Items[k], oldtitle, newtitle);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public static string TranslateString(string oldstr)
        {
            string[] titles = Properties.Resources.Language.Split(';');
            if (titles != null)
            {
                for (int i = 0; i < titles.Length; i++)
                {
                    string[] title = titles[i].Split('-');
                    if (title.Length == 2)
                    {
                        string oldtitle, newtitle;
                        if (Util.language == "1")
                        {
                            oldtitle = title[0].Replace("\r\n", "");
                            newtitle = title[1].Replace("\r\n", "");
                        }
                        else
                        {
                            oldtitle = title[1].Replace("\r\n", "");
                            newtitle = title[0].Replace("\r\n", "");
                        }
                        if (oldstr.Trim().Replace(" ", "").Replace("_", "").ToLower() == oldtitle.Trim().Replace(" ", "").Replace("_", "").ToLower())
                            return newtitle;
                    }
                }
            }
            return oldstr;
        }

        public static void TranslateDataGridView(DataGridView dbgrid)
        {
            if (dbgrid.Columns.Count == 0) return;

            string[] titles = Properties.Resources.Language.Split(';');
            if (titles != null)
            {
                for (int k = 0; k < dbgrid.Columns.Count; k++)
                {
                    string oldstr = dbgrid.Columns[k].HeaderText;
                    for (int i = 0; i < titles.Length; i++)
                    {
                        string[] title = titles[i].Split('-');
                        if (title.Length == 2)
                        {
                            string oldtitle, newtitle;
                            if (Util.language == "1")
                            {
                                oldtitle = title[0].Replace("\r\n", "");
                                newtitle = title[1].Replace("\r\n", "");
                            }
                            else
                            {
                                oldtitle = title[1].Replace("\r\n", "");
                                newtitle = title[0].Replace("\r\n", "");
                            }
                            if (oldstr.Trim().Replace(" ", "").Replace("_", "").ToLower() == oldtitle.Replace(" ", "").Replace("_", "").Trim().ToLower())
                            {
                                dbgrid.Columns[k].HeaderText = newtitle;
                                break;
                            }
                        }
                    }
                }
            }
            return;
        }

        public static void TranslateMenu(ContextMenuStrip menu)
        {
            if (menu.Items.Count == 0) return;

            string[] titles = Properties.Resources.Language.Split(';');
            if (titles != null)
            {
                for (int i = 0; i < titles.Length; i++)
                {
                    string[] title = titles[i].Split('-');
                    if (title.Length == 2)
                    {
                        string oldtitle, newtitle;
                        if (Util.language == "1")
                        {
                            oldtitle = title[0].Replace("\r\n", "");
                            newtitle = title[1].Replace("\r\n", "");
                        }
                        else
                        {
                            oldtitle = title[1].Replace("\r\n", "");
                            newtitle = title[0].Replace("\r\n", "");
                        }
                        for (int k = 0; k < menu.Items.Count; k++)
                        {
                            if (menu.Items[k].GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                            {
                                ToolStripMenuItem tsm = (ToolStripMenuItem)menu.Items[k];
                                if (tsm.Text.ToLower().CompareTo(oldtitle.ToLower()) == 0)
                                {
                                    tsm.Text = newtitle;
                                    break;
                                }
                                else if (tsm.DropDownItems.Count > 0)
                                {
                                    SetSubMenu(tsm, oldtitle, newtitle);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SetSubMenu(ToolStripMenuItem tsmItem, string oldtitle, string newtitle)
        {
            int i;
            if (tsmItem.DropDownItems.Count > 0)
            {
                for (i = 0; i < tsmItem.DropDownItems.Count; i++)
                {
                    if (tsmItem.DropDownItems[i].GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                    {
                        ToolStripMenuItem subitem = (ToolStripMenuItem)tsmItem.DropDownItems[i];
                        if (subitem.Name != null)
                        {
                            if (subitem.Text.Trim().ToLower().CompareTo(oldtitle) == 0)
                            {
                                subitem.Text = newtitle;
                                return;
                            }
                            else if (subitem.DropDownItems.Count > 0)
                            {
                                SetSubMenu(subitem, oldtitle, newtitle);
                            }
                        }
                    }
                }
            }
        }
    }
}