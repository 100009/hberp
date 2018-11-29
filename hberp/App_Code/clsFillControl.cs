using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    class FillControl
    {
        #region FillListBox
        public static void FillListBox(ListBox box, string listdata, char delimiter, bool keepoldlist)
        {
            if (!keepoldlist)
                box.Items.Clear();
            if (string.IsNullOrEmpty(listdata)) return;

            string[] arr = listdata.Split(delimiter);
            for (int i = 0; i < arr.Length; i++)
            {
                box.Items.Add(arr[i]);
            }
        }

        public static void FillListBox(ListBox box, ArrayList arr, bool keepoldlist)
        {
            if (!keepoldlist)
                box.Items.Clear();
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    box.Items.Add(arr[i]);
                }
            }
        }
        #endregion

        #region FillComboBox
        public static void FillComboBox(ComboBox box, string listdata, char delimiter, bool keepoldlist)
        {
            if (!keepoldlist)
                box.Items.Clear();
            if (string.IsNullOrEmpty(listdata)) return;

            string[] arr = listdata.Split(delimiter);
            for (int i = 0; i < arr.Length; i++)
            {
                box.Items.Add(arr[i]);
            }
        }

        public static void FillComboBox(ComboBox box, ArrayList arr, bool keepoldlist)
        {
            if (!keepoldlist)
                box.Items.Clear();
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    box.Items.Add(arr[i]);
                }
            }
        }
        #endregion

        #region FillCredenceType
        public static void FillCredenceType(ComboBox box)
        {            
            box.Items.Clear();
            box.Items.Add("一般凭证");
            box.Items.Add("销售凭证");
            box.Items.Add("设计加工凭证");
            box.Items.Add("采购凭证");            
            box.Items.Add("仓库进出");
            box.Items.Add("收款凭证");
            box.Items.Add("付款凭证");
            box.Items.Add("其它应收凭证");
            box.Items.Add("其它应付凭证");
            box.Items.Add("生产凭证");
            box.Items.Add("支票承兑");
            box.Items.Add("固定资产增加");
            box.Items.Add("固定资产维修");
            box.Items.Add("固定资产报废");
            box.Items.Add("资产折旧");
            box.Items.Add("零库清理");
            box.SelectedIndex = 0;            
        }

        public static void FillCredenceType(ListBox box)
        {
            box.Items.Clear();
            box.Items.Add("一般凭证");
            box.Items.Add("销售凭证");
            box.Items.Add("采购凭证");
            box.Items.Add("购销一体凭证");
            box.Items.Add("仓库进出");
            box.Items.Add("收款凭证");
            box.Items.Add("付款凭证");
            box.Items.Add("其它应收凭证");
            box.Items.Add("其它应付凭证");
            box.Items.Add("生产凭证");
            box.Items.Add("支票承兑");
            box.Items.Add("固定资产增加");
            box.Items.Add("固定资产维修");
            box.Items.Add("固定资产报废");
            box.Items.Add("资产折旧");
        }
        #endregion

        #region AccBookType
        private static IList<modStatus> GetAccBookType(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }

            list.Add(new modStatus("0", "应收帐款"));
            list.Add(new modStatus("1", "应付帐款"));
            list.Add(new modStatus("2", "其它应收款"));
            list.Add(new modStatus("3", "其它应付款"));
            list.Add(new modStatus("5", "现金银行"));
            return list;
        }

        public static void FillAccBookType(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetAccBookType(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillAccBookType(ListBox box)
        {
            IList<modStatus> list = GetAccBookType(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region AccExpenseType
        private static IList<modStatus> GetAccExpenseType(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }

            list.Add(new modStatus("913530", "管理费用"));
            list.Add(new modStatus("913535", "销售费用"));
            list.Add(new modStatus("913540", "财务费用"));
            return list;
        }

        public static void FillAccExpenseType(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetAccExpenseType(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillAccExpenseType(ListBox box)
        {
            IList<modStatus> list = GetAccExpenseType(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillStatus
        private static IList<modStatus> GetStatus(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            if (Util.language == "0")
            {
                modStatus modp = new modStatus("0", "Disable");
                list.Add(modp);
                modStatus mods = new modStatus("1", "Enable");
                list.Add(mods);
            }
            else
            {
                modStatus modp = new modStatus("0", "禁用");
                list.Add(modp);
                modStatus mods = new modStatus("1", "有效");
                list.Add(mods);
            }
            return list;
        }

        public static void FillStatus(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetStatus(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            if (allitem)
                box.SelectedIndex = 0;
            else
                box.SelectedIndex = -1;
        }

        public static void FillStatus(ListBox box)
        {
            IList<modStatus> list = GetStatus(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillYesNo
        private static IList<modStatus> GetYesNo(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            if (Util.language == "0")
            {
                modStatus modp = new modStatus("0", "No");
                list.Add(modp);
                modStatus mods = new modStatus("1", "Yes");
                list.Add(mods);
            }
            else
            {
                modStatus modp = new modStatus("0", "否");
                list.Add(modp);
                modStatus mods = new modStatus("1", "是");
                list.Add(mods);
            }
            return list;
        }

        public static void FillYesNo(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetYesNo(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            if (allitem)
                box.SelectedIndex = 0;
            else
                box.SelectedIndex = -1;
        }

        public static void FillYesNo(ListBox box)
        {
            IList<modStatus> list = GetYesNo(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion
        
        #region FillShipmentTemplete
        private static IList<modStatus> GetShipmentTemplete(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("SalesShipment", "普通送货格式");
            list.Add(modp);            
            return list;
        }

        public static void FillShipmentTemplete(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetShipmentTemplete(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillShipmentTemplete(ListBox box)
        {
            IList<modStatus> list = GetShipmentTemplete(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillPayStatus
        private static IList<modStatus> GetPayStatus(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("0", "未付款");
            list.Add(modp);
            modStatus mods = new modStatus("1", "已付款");
            list.Add(mods);
            return list;
        }

        public static void FillPayStatus(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetPayStatus(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillPayStatus(ListBox box)
        {
            IList<modStatus> list = GetPayStatus(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillReceiveStatus
        private static IList<modStatus> GetReceiveStatus(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("0", "未收款");
            list.Add(modp);
            modStatus mods = new modStatus("1", "已收款");
            list.Add(mods);
            return list;
        }

        public static void FillReceiveStatus(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetReceiveStatus(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillReceiveStatus(ListBox box)
        {
            IList<modStatus> list = GetReceiveStatus(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillDepreMethod
        private static IList<modStatus> GetDepreMethod(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus mod1 = new modStatus("平均年限法", "平均年限法");
            list.Add(mod1);
            modStatus mod2 = new modStatus("双倍余额递减法", "双倍余额递减法");
            list.Add(mod2);
            modStatus mod3 = new modStatus("工作量法", "工作量法");
            list.Add(mod3);
            return list;
        }

        public static void FillDepreMethod(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetDepreMethod(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillDepreMethod(ListBox box)
        {
            IList<modStatus> list = GetPayStatus(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillOtherReceivableType
        private static IList<modStatus> GetOtherReceivableType(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("1", "其它应收增加");
            list.Add(modp);
            modStatus mods = new modStatus("-1", "其它应收减少");
            list.Add(mods);
            return list;
        }

        public static void FillOtherReceivableType(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetOtherReceivableType(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillOtherReceivableType(ListBox box)
        {
            IList<modStatus> list = GetOtherReceivableType(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillOtherPayableType
        private static IList<modStatus> GetOtherPayableType(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("1", "其它应付增加");
            list.Add(modp);
            modStatus mods = new modStatus("-1", "其它应付减少");
            list.Add(mods);
            return list;
        }

        public static void FillOtherPayableType(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetOtherPayableType(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillOtherPayableType(ListBox box)
        {
            IList<modStatus> list = GetOtherPayableType(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillCheckStatus
        private static IList<modStatus> GetCheckStatus(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("0", "未承兑");
            list.Add(modp);
            modStatus mods = new modStatus("1", "已承兑");
            list.Add(mods);
            return list;
        }

        public static void FillCheckStatus(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetCheckStatus(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillCheckStatus(ListBox box)
        {
            IList<modStatus> list = GetCheckStatus(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillCheckSubject
        public static IList<modStatus> GetCheckSubject(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("1075", "应收票据");
            list.Add(modp);
            modStatus mods = new modStatus("5125", "应付票据");
            list.Add(mods);
            return list;
        }

        public static void FillCheckSubject(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetCheckSubject(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillCheckSubject(ListBox box)
        {
            IList<modStatus> list = GetCheckSubject(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillInvoiceStatus
        private static IList<modStatus> GetInvoiceStatus(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("0", "不开票");
            list.Add(modp);
            modStatus mods = new modStatus("1", "未开票");
            list.Add(mods);
            modStatus modw = new modStatus("2", "已开票");
            list.Add(modw);
            return list;
        }

        public static void FillInvoiceStatus(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetInvoiceStatus(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillInvoiceStatus(ListBox box)
        {
            IList<modStatus> list = GetInvoiceStatus(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillShipType
        private static IList<modStatus> GetShipType(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("送货单", "送货单");
            list.Add(modp);
            modStatus modc = new modStatus("收营单", "收营单");
            list.Add(modc);
            modStatus mods = new modStatus("样品单", "样品单");
            list.Add(mods);
            modStatus modr = new modStatus("退货单", "退货单");
            list.Add(modr);
            return list;
        }

        public static void FillShipType(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetShipType(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillShipType(ListBox box)
        {
            IList<modStatus> list = GetShipType(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillDesignType
        private static IList<modStatus> GetDesignType(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("设计服务", "设计服务");
            list.Add(modp);
            modStatus mods = new modStatus("来料加工", "来料加工");
            list.Add(mods);
            modStatus modr = new modStatus("设计服务退货", "设计服务退货");
            list.Add(modr);
            modStatus modt = new modStatus("来料加工退货", "来料加工退货");
            list.Add(modt);
            return list;
        }

        public static void FillDesignType(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetDesignType(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillDesignType(ListBox box)
        {
            IList<modStatus> list = GetShipType(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion
        
        #region FillPurchaseType
        private static IList<modStatus> GetPurchaseType(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("采购收货", "采购收货");
            list.Add(modp);
            modStatus mods = new modStatus("采购退货", "采购退货");
            list.Add(mods);
            return list;
        }

        public static void FillPurchaseType(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetPurchaseType(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillPurchaseType(ListBox box)
        {
            IList<modStatus> list = GetPurchaseType(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillPurchaseSalesType
        private static IList<modStatus> GetPurchaseSalesType(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("购销送货", "购销送货");
            list.Add(modp);
            modStatus modw = new modStatus("购销退货", "购销退货");
            list.Add(modw);            
            return list;
        }

        public static void FillPurchaseSalesType(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetPurchaseSalesType(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillPurchaseSalesType(ListBox box)
        {
            IList<modStatus> list = GetPurchaseSalesType(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillProductionType
        private static IList<modStatus> GetProductionType(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("内部单", "内部单");
            list.Add(modp);
            modStatus mods = new modStatus("外发单", "外发单");
            list.Add(mods);
            return list;
        }

        public static void FillProductionType(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetProductionType(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillProductionType(ListBox box)
        {
            IList<modStatus> list = GetProductionType(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion
        
        #region FillInoutFlag
        private static IList<modStatus> GetInoutFlag(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            modStatus modp = new modStatus("-1", clsTranslate.TranslateString("OUTPUT"));
            list.Add(modp);
            modStatus mods = new modStatus("1", clsTranslate.TranslateString("INPUT"));
            list.Add(mods);
            return list;
        }

        public static void FillInoutFlag(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetInoutFlag(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            if (allitem)
                box.SelectedIndex = 0;
            else
                box.SelectedIndex = -1;
        }

        public static void FillInoutFlag(ListBox box)
        {
            IList<modStatus> list = GetInoutFlag(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillPeriodList
        public static void FillPeriodList(ComboBox box)
        {
            dalAccPeriodList dal = new dalAccPeriodList();
            BindingCollection<modAccPeriodList> list = dal.GetIList(out Util.emsg);
            if (list != null)
            {                
                box.ValueMember = "AccName";
                box.DisplayMember = "AccName";
                box.DataSource = list;
                foreach (modAccPeriodList mod in list)
                {
                    if (mod.LockFlag == 0)
                    {
                        box.SelectedValue = mod.AccName;
                        break;
                    }
                }
            }
            else
            {
                box.DataSource = null;
            }
        }
		public static void FillEndPeriodList(ComboBox box, string startAccName)
		{
			dalAccPeriodList dal = new dalAccPeriodList();
			BindingCollection<modAccPeriodList> list = dal.GetEndList(startAccName, out Util.emsg);
			if (list != null)
			{
				box.ValueMember = "AccName";
				box.DisplayMember = "AccName";
				box.DataSource = list;
				foreach (modAccPeriodList mod in list)
				{
					if (mod.LockFlag == 0)
					{
						box.SelectedValue = mod.AccName;
						break;
					}
				}
			}
			else
			{
				box.DataSource = null;
			}
		}

		public static void FillPeriodList(ListBox box)
        {
            dalAccPeriodList dal = new dalAccPeriodList();
            BindingCollection<modAccPeriodList> list = dal.GetIList(out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "AccName";
                box.DisplayMember = "AccName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion
        
        #region FillRoleList
        public static void FillRoleList(ComboBox box, bool allitem)
        {
            dalRoleList dal = new dalRoleList();
            BindingCollection<modRoleList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                if (allitem)
                {
                    modRoleList mod = new modRoleList();
                    mod.RoleId = "ALL";
                    mod.RoleDesc = "ALL";
                    mod.Status = 1;
                    mod.UpdateUser = Util.UserId;
                    list.Insert(0, mod);
                }

                box.ValueMember = "RoleId";
                box.DisplayMember = "RoleDesc";
                box.DataSource = list;
                //if (list.Count > 0)
                //    box.SelectedIndex = 0;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillRoleList(ListBox box)
        {
            dalRoleList dal = new dalRoleList();
            BindingCollection<modRoleList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "RoleId";
                box.DisplayMember = "RoleDesc";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillUserList
        public static void FillUserList(ComboBox box, bool allitem)
        {
            dalUserList dal = new dalUserList();
            BindingCollection<modUserList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                if (allitem)
                {
                    modUserList mod = new modUserList("ALL", "ALL", 1, "ALL", "", Util.UserId, DateTime.Now, "");
                    list.Insert(0, mod);
                }
                box.DataSource = list;
                box.ValueMember = "UserId";
                box.DisplayMember = "UserName";
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillUserList(ListBox box, string deptid)
        {
            dalUserList dal = new dalUserList();
            BindingCollection<modUserList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                
                box.ValueMember = "UserId";
                box.DisplayMember = "UserName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion
        
        #region FillProductType
        public static void FillProductType(ComboBox box, bool allitem, bool newitem)
        {
            dalProductTypeList dal = new dalProductTypeList();
            BindingCollection<modProductTypeList> list = dal.GetIList(true, out Util.emsg);
            if (newitem)
            {
                modProductTypeList mod = new modProductTypeList("New...", 1, Util.UserId, DateTime.Now);
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modProductTypeList mod = new modProductTypeList("ALL", 1, Util.UserId, DateTime.Now);
                    list.Insert(0, mod);
                }

                box.ValueMember = "ProductType";
                box.DisplayMember = "ProductType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillProductType(ListBox box)
        {
            dalProductTypeList dal = new dalProductTypeList();
            BindingCollection<modProductTypeList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "ProductType";
                box.DisplayMember = "ProductType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillProductList
        public static void FillProductList(ComboBox box, string producttypelist, bool allitem)
        {
            dalProductList dal = new dalProductList();
            BindingCollection<modProductList> list = dal.GetIList(producttypelist, string.Empty, out Util.emsg);
            if (list != null)
            {
                if (allitem)
                {
                    modProductList mod = new modProductList();
                    mod.ProductId = "ALL";
                    mod.ProductName = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "ProductId";
                box.DisplayMember = "ProductName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillProductList(ListBox box, string producttypelist)
        {
            dalProductList dal = new dalProductList();
            BindingCollection<modProductList> list = dal.GetIList(producttypelist, string.Empty, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "ProuctionId";
                box.DisplayMember = "ProuctionName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillBrandList
        private static IList<modStatus> GetBrandList(bool allitem)
        {
            IList<modStatus> list = new List<modStatus>();
            if (allitem)
            {
                modStatus mod = new modStatus("ALL", "ALL");
                list.Add(mod);
            }
            dalProductList dal=new dalProductList();
            ArrayList arr = dal.GetBrandList(out Util.emsg);
            if (arr != null && arr.Count > 0)
            {
                for(int i=0;i<arr.Count;i++)
                {
                    modStatus moditem = new modStatus(arr[i].ToString(), arr[i].ToString());
                    list.Add(moditem);
                }
            }
            return list;
        }

        public static void FillBrandList(ComboBox box, bool allitem)
        {
            IList<modStatus> list = GetBrandList(allitem);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
            box.SelectedIndex = 0;
        }

        public static void FillBrandList(ListBox box)
        {
            IList<modStatus> list = GetPayStatus(false);
            box.DataSource = list;
            box.ValueMember = "Code";
            box.DisplayMember = "Name";
        }
        #endregion

        #region FillCustomerType
        public static void FillCustomerType(ComboBox box, bool allitem, bool newitem)
        {
            dalCustomerType dal = new dalCustomerType();
            BindingCollection<modCustomerType> list = dal.GetIList(true, out Util.emsg);
            if (newitem)
            {
                modCustomerType mod = new modCustomerType("New...", 1, Util.UserId, DateTime.Now);
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modCustomerType mod = new modCustomerType("ALL", 1, Util.UserId, DateTime.Now);
                    list.Insert(0, mod);
                }
                
                box.ValueMember = "CustType";
                box.DisplayMember = "CustType";
                box.DataSource = list;
            }
            else
            {                
                box.DataSource = list;
            }
        }

        public static void FillCustomerType(ListBox box)
        {
            dalCustomerType dal = new dalCustomerType();
            BindingCollection<modCustomerType> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "CustType";
                box.DisplayMember = "CustType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillVendorType
        public static void FillVendorType(ComboBox box, bool allitem, bool newitem)
        {
            dalVendorType dal = new dalVendorType();
            BindingCollection<modVendorType> list = dal.GetIList(true, out Util.emsg);
            if (newitem)
            {
                modVendorType mod = new modVendorType("New...", 1, Util.UserId, DateTime.Now);
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modVendorType mod = new modVendorType("ALL", 1, Util.UserId, DateTime.Now);
                    list.Insert(0, mod);
                }

                box.ValueMember = "VendorType";
                box.DisplayMember = "VendorType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillVendorType(ListBox box)
        {
            dalVendorType dal = new dalVendorType();
            BindingCollection<modVendorType> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "VendorType";
                box.DisplayMember = "VendorType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillCustomerLevel
        public static void FillCustomerLevel(ComboBox box, bool allitem)
        {
            dalCustomerLevel dal = new dalCustomerLevel();
            BindingCollection<modCustomerLevel> list = new BindingCollection<modCustomerLevel>();
            list = dal.GetIList("and status=1", out Util.emsg);
            if (list != null)
            {
                if (allitem)
                {
                    modCustomerLevel mod = new modCustomerLevel();
                    mod.CustLevel = "ALL";
                    mod.Description = "ALL";
                    mod.Status = 1;
                    list.Insert(0, mod);
                }
                else
                {
                    modCustomerLevel mod = new modCustomerLevel();
                    mod.CustLevel = "";
                    mod.Description = "";
                    mod.Status = 1;
                    list.Insert(0, mod);
                }

                box.ValueMember = "CustLevel";
                box.DisplayMember = "Description";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillCustomerLevel(ListBox box)
        {
            dalCustomerLevel dal = new dalCustomerLevel();
            BindingCollection<modCustomerLevel> list = new BindingCollection<modCustomerLevel>();
            list = dal.GetIList("and status=1", out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "CustLevel";
                box.DisplayMember = "Description";
                box.DataSource = list;
                box.SelectedItems.Clear();
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillCustomerList
        public static void FillCustomerList(ComboBox box, bool allitem)
        {
            dalCustomerList dal = new dalCustomerList();
            BindingCollection<modCustomerList> list=new BindingCollection<modCustomerList>();
            list = dal.GetIList(string.Empty, string.Empty, false, Util.UserId, out Util.emsg);
            if (list != null)
            {
                if (allitem)
                {
                    modCustomerList mod = new modCustomerList();
                    mod.CustId = "ALL";
                    mod.CustName = "ALL";
                    mod.Status = 1;
                    list.Insert(0, mod);
                }

                box.ValueMember = "CustId";
                box.DisplayMember = "CustName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillCustomerList(ListBox box)
        {
            dalCustomerList dal = new dalCustomerList();
            BindingCollection<modCustomerList> list = new BindingCollection<modCustomerList>();
            list = dal.GetIList(string.Empty, string.Empty, false, Util.UserId, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "CustId";
                box.DisplayMember = "CustName";
                box.DataSource = list;
                box.SelectedItems.Clear();
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillCustomerScoreRule
        public static void FillCustomerScoreRule(ComboBox box, string traceflaglist, bool allitem)
        {
            dalCustomerScoreRule dal = new dalCustomerScoreRule();
            BindingCollection<modCustomerScoreRule> list = dal.GetIList(traceflaglist, out Util.emsg);
            if (list != null)
            {
                if (allitem)
                {
                    modCustomerScoreRule mod = new modCustomerScoreRule();
                    mod.ActionCode = "ALL";
                    mod.ActionType = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "ActionCode";
                box.DisplayMember = "ActionType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillCustomerScoreRule(ListBox box, string traceflaglist)
        {
            dalCustomerScoreRule dal = new dalCustomerScoreRule();
            BindingCollection<modCustomerScoreRule> list = dal.GetIList(traceflaglist, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "ActionCode";
                box.DisplayMember = "ActionType";
                box.DataSource = list;
                box.SelectedItems.Clear();
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillVendorList
        public static void FillVendorList(ComboBox box, string statuslist, string vendortype, bool allitem)
        {
            dalVendorList dal = new dalVendorList();
            BindingCollection<modVendorList> list = dal.GetIList(statuslist, vendortype, out Util.emsg);            
            if (list != null)
            {
                if (allitem)
                {
                    modVendorList mod = new modVendorList();
                    mod.VendorName = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "VendorName";
                box.DisplayMember = "VendorName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillVendorList(ListBox box, string statuslist, string vendortype)
        {
            dalVendorList dal = new dalVendorList();
            BindingCollection<modVendorList> list = dal.GetIList(statuslist, vendortype, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "VendorName";
                box.DisplayMember = "VendorName";
                box.DataSource = list;
                box.SelectedItems.Clear();
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillAdminDutyList
        public static void FillAdminDutyList(ComboBox box, bool allitem, bool newitem)
        {
            dalAdminDutyList dal = new dalAdminDutyList();
            BindingCollection<modAdminDutyList> list = dal.GetIList(true, out Util.emsg);
            if (newitem)
            {
                modAdminDutyList mod = new modAdminDutyList("New...", 1, Util.UserId, DateTime.Now);
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAdminDutyList mod = new modAdminDutyList("ALL", 1, Util.UserId, DateTime.Now);
                    list.Insert(0, mod);
                }

                box.ValueMember = "Duty";
                box.DisplayMember = "Duty";
                box.DataSource = list;
                //if (list.Count > 0)
                //    box.SelectedIndex = 0;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillDutyList(ListBox box)
        {
            dalAdminDutyList dal = new dalAdminDutyList();
            BindingCollection<modAdminDutyList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "Duty";
                box.DisplayMember = "Duty";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillAdminDegreeList
        public static void FillAdminDegreeList(ComboBox box, bool allitem, bool newitem)
        {
            dalAdminDegreeList dal = new dalAdminDegreeList();
            BindingCollection<modAdminDegreeList> list = dal.GetIList(true, out Util.emsg);
            if (newitem)
            {
                modAdminDegreeList mod = new modAdminDegreeList("New...", 1, Util.UserId, DateTime.Now);
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAdminDegreeList mod = new modAdminDegreeList("ALL", 1, Util.UserId, DateTime.Now);
                    list.Insert(0, mod);
                }

                box.ValueMember = "EduDegree";
                box.DisplayMember = "EduDegree";
                box.DataSource = list;
                //if (list.Count > 0)
                //    box.SelectedIndex = 0;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillAdminDegreeList(ListBox box)
        {
            dalAdminDegreeList dal = new dalAdminDegreeList();
            BindingCollection<modAdminDegreeList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "EduDegree";
                box.DisplayMember = "EduDegree";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillAdminDeptList
        public static void FillAdminDeptList(ComboBox box, bool allitem, bool newitem)
        {
            dalAdminDeptList dal = new dalAdminDeptList();
            BindingCollection<modAdminDeptList> list = dal.GetIList(true, out Util.emsg);
            if (newitem)
            {
                modAdminDeptList mod = new modAdminDeptList("New...", string.Empty, 1, Util.UserId, DateTime.Now);
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAdminDeptList mod = new modAdminDeptList("ALL", "", 1, Util.UserId, DateTime.Now);
                    list.Insert(0, mod);
                }

                box.ValueMember = "DeptId";
                box.DisplayMember = "DeptId";
                box.DataSource = list;
                //if (list.Count > 0)
                //    box.SelectedIndex = 0;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillAdminDeptList(ListBox box)
        {
            dalAdminDeptList dal = new dalAdminDeptList();
            BindingCollection<modAdminDeptList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "DeptId";
                box.DisplayMember = "DeptId";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillEmployeeList
        public static void FillEmployeeList(ComboBox box, bool allitem, bool newitem)
        {
            dalAdminEmployeeList dal = new dalAdminEmployeeList();
            BindingCollection<modAdminEmployeeList> list = dal.GetIList(true, string.Empty, out Util.emsg);
            if (newitem)
            {
                modAdminEmployeeList mod = new modAdminEmployeeList();
                mod.EmployeeId = "New...";
                mod.EmployeeName = "New...";
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAdminEmployeeList mod = new modAdminEmployeeList();
                    mod.EmployeeId = "ALL";
                    mod.EmployeeName = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "EmployeeId";
                box.DisplayMember = "EmployeeName";
                box.DataSource = list;
                //if (list.Count > 0)
                //    box.SelectedIndex = 0;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillEmployeeList(ListBox box)
        {
            dalAdminDutyList dal = new dalAdminDutyList();
            BindingCollection<modAdminDutyList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "EmpolyeeId";
                box.DisplayMember = "EmpolyeeName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillUnitList
        public static void FillUnitList(ComboBox box, bool allitem, bool newitem)
        {
            dalUnitList dal = new dalUnitList();
            BindingCollection<modUnitList> list = dal.GetIList(true, out Util.emsg);
            if (newitem)
            {
                modUnitList mod = new modUnitList("New...", 1, Util.UserId, DateTime.Now);
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modUnitList mod = new modUnitList("ALL", 1, Util.UserId, DateTime.Now);
                    list.Insert(0, mod);
                }

                box.ValueMember = "UnitNo";
                box.DisplayMember = "UnitNo";
                box.DataSource = list;
                //if (list.Count > 0)
                //    box.SelectedIndex = 0;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillUnitList(ListBox box)
        {
            dalUnitList dal = new dalUnitList();
            BindingCollection<modUnitList> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "UnitNo";
                box.DisplayMember = "UnitNo";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillWarehouseList
        public static void FillWarehouseList(ComboBox box, bool allitem)
        {
            dalWarehouseList dal = new dalWarehouseList();
            BindingCollection<modWarehouseList> list = dal.GetIList(true, out Util.emsg);           
            if (list != null)
            {
                if (allitem)
                {
                    modWarehouseList mod = new modWarehouseList();
                    mod.WarehouseId = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "WarehouseId";
                box.DisplayMember = "WarehouseId";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillWarehouseList(ListBox box)
        {
            dalWarehouseList dal = new dalWarehouseList();
            BindingCollection<modWarehouseList> list = dal.GetIList(true, out Util.emsg);            
            if (list != null)
            {
                box.ValueMember = "WarehouseId";
                box.DisplayMember = "WarehouseId";
                box.DataSource = list;
                box.SelectedItems.Clear();
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillWarehouseInoutType
        public static void FillWarehouseInoutType(ComboBox box, int? inout_flag, bool allitem, bool newitem)
        {
            dalWarehouseInoutType dal = new dalWarehouseInoutType();
            BindingCollection<modWarehouseInoutType> list = new BindingCollection<modWarehouseInoutType>();
            if(inout_flag!=-1 && inout_flag != 1)
                list = dal.GetIList(out Util.emsg);
            else
                list = dal.GetIList(inout_flag, out Util.emsg);
            if (newitem)
            {
                modWarehouseInoutType mod = new modWarehouseInoutType("New...", 1, 1, Util.UserId, string.Empty);
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modWarehouseInoutType mod = new modWarehouseInoutType();
                    mod.InoutType = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "InoutType";
                box.DisplayMember = "InoutType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillWarehouseInoutType(ListBox box, int? inout_flag)
        {
            dalWarehouseInoutType dal = new dalWarehouseInoutType();
            BindingCollection<modWarehouseInoutType> list = new BindingCollection<modWarehouseInoutType>();
            if (inout_flag != -1 && inout_flag != 1)
                list = dal.GetIList(out Util.emsg);
            else
                list = dal.GetIList(inout_flag, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "InoutType";
                box.DisplayMember = "InvOutType";
                box.DataSource = list;
                box.SelectedItems.Clear();
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillCurrency
        public static void FillCurrency(ComboBox box, bool allitem, bool newitem)
        {
            dalAccCurrencyList dal = new dalAccCurrencyList();
            BindingCollection<modAccCurrencyList> list = dal.GetIList(out Util.emsg);
            if (newitem)
            {
                modAccCurrencyList mod = new modAccCurrencyList("New...", 1, 0, Util.UserId, DateTime.Now);
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAccCurrencyList mod = new modAccCurrencyList();
                    mod.Currency = "ALL";
                    mod.ExchangeRate = 1;
                    list.Insert(0, mod);
                }

                box.ValueMember = "Currency";
                box.DisplayMember = "Currency";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillCurrency(ListBox box)
        {
            dalAccCurrencyList dal = new dalAccCurrencyList();
            BindingCollection<modAccCurrencyList> list = dal.GetIList(out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "Currency";
                box.DisplayMember = "Currency";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion
        
        #region FillBalanceStyle
        public static void FillBalanceStyle(ComboBox box, bool allitem, bool newitem)
        {
            dalAccBalanceStyle dal = new dalAccBalanceStyle();
            BindingCollection<modAccBalanceStyle> list = dal.GetIList(out Util.emsg);
            if (newitem)
            {
                modAccBalanceStyle mod = new modAccBalanceStyle();
                mod.BalanceStyle = "New...";
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAccBalanceStyle mod = new modAccBalanceStyle();
                    mod.BalanceStyle = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "BalanceStyle";
                box.DisplayMember = "BalanceStyle";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillBalanceStyle(ListBox box)
        {
            dalAccBalanceStyle dal = new dalAccBalanceStyle();
            BindingCollection<modAccBalanceStyle> list = dal.GetIList(out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "BalanceStyle";
                box.DisplayMember = "BalanceStyle";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillBankList
        public static void FillBankList(ComboBox box, bool allitem, bool newitem)
        {
            dalAccBankList dal = new dalAccBankList();
            BindingCollection<modAccBankList> list = dal.GetIList(out Util.emsg);
            if (newitem)
            {
                modAccBankList mod = new modAccBankList();
                mod.BankName = "New...";
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAccBankList mod = new modAccBankList();
                    mod.BankName = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "BankName";
                box.DisplayMember = "BankName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillBankList(ListBox box)
        {
            dalAccBankList dal = new dalAccBankList();
            BindingCollection<modAccBankList> list = dal.GetIList(out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "BankName";
                box.DisplayMember = "BankName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillBankAccount
        public static void FillBankAccount(ComboBox box, bool allitem, bool newitem)
        {
            dalAccBankAccount dal = new dalAccBankAccount();
            BindingCollection<modAccBankAccount> list = dal.GetIList(out Util.emsg);
            if (newitem)
            {
                modAccBankAccount mod = new modAccBankAccount();
                mod.AccountNo = "New...";
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAccBankAccount mod = new modAccBankAccount();
                    mod.AccountNo = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "AccountNo";
                box.DisplayMember = "AccountNo";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillBankAccount(ListBox box)
        {
            dalAccBankAccount dal = new dalAccBankAccount();
            BindingCollection<modAccBankAccount> list = dal.GetIList(out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "AccountNo";
                box.DisplayMember = "AccountNo";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillCheckType
        public static void FillCheckType(ComboBox box, bool allitem, bool newitem)
        {
            dalAccCheckType dal = new dalAccCheckType();
            BindingCollection<modAccCheckType> list = dal.GetIList(out Util.emsg);
            if (newitem)
            {
                modAccCheckType mod = new modAccCheckType();
                mod.CheckType = "New...";
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAccCheckType mod = new modAccCheckType();
                    mod.CheckType = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "CheckType";
                box.DisplayMember = "CheckType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillCheckType(ListBox box)
        {
            dalAccCheckType dal = new dalAccCheckType();
            BindingCollection<modAccCheckType> list = dal.GetIList(out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "CheckType";
                box.DisplayMember = "CheckType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillCommonDigestType
        public static void FillCommonDigestType(ComboBox box, bool allitem, bool newitem)
        {
            dalAccCommonDigestType dal = new dalAccCommonDigestType();
            BindingCollection<modAccCommonDigestType> list = dal.GetIList(out Util.emsg);
            if (newitem)
            {
                modAccCommonDigestType mod = new modAccCommonDigestType();
                mod.DigestType = "New...";
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAccCommonDigestType mod = new modAccCommonDigestType();
                    mod.DigestType = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "DigestType";
                box.DisplayMember = "DigestType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillCommonDigestType(ListBox box)
        {
            dalAccCommonDigestType dal = new dalAccCommonDigestType();
            BindingCollection<modAccCommonDigestType> list = dal.GetIList(out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "DigestType";
                box.DisplayMember = "DigestType";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillCommonDigest
        public static void FillCommonDigest(ComboBox box, string digesttypelist, bool allitem)
        {
            dalAccCommonDigest dal = new dalAccCommonDigest();
            BindingCollection<modAccCommonDigest> list = dal.GetIList(digesttypelist, out Util.emsg);
            if (list != null)
            {
                if (allitem)
                {
                    modAccCommonDigest mod = new modAccCommonDigest();
                    mod.DigestType = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "Digest";
                box.DisplayMember = "Digest";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillCommonDigest(ListBox box, string digesttypelist)
        {
            dalAccCommonDigest dal = new dalAccCommonDigest();
            BindingCollection<modAccCommonDigest> list = dal.GetIList(digesttypelist, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "Digest";
                box.DisplayMember = "Digest";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillCredenceWord
        public static void FillCredenceWord(ComboBox box, bool allitem, bool newitem)
        {
            dalAccCredenceWord dal = new dalAccCredenceWord();
            BindingCollection<modAccCredenceWord> list = dal.GetIList(out Util.emsg);
            if (newitem)
            {
                modAccCredenceWord mod = new modAccCredenceWord();
                mod.CredenceWord = "New...";
                list.Add(mod);
            }
            if (list != null)
            {
                if (allitem)
                {
                    modAccCredenceWord mod = new modAccCredenceWord();
                    mod.CredenceWord = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "CredenceWord";
                box.DisplayMember = "CredenceWord";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillCredenceWord(ListBox box)
        {
            dalAccCredenceWord dal = new dalAccCredenceWord();
            BindingCollection<modAccCredenceWord> list = dal.GetIList(out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "CredenceWord";
                box.DisplayMember = "CredenceWord";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillOtherReceivableObject
        public static void FillOtherReceivableObject(ComboBox box, bool allitem)
        {
            dalOtherReceivableObject dal = new dalOtherReceivableObject();
            BindingCollection<modOtherReceivableObject> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                if (allitem)
                {
                    modOtherReceivableObject mod = new modOtherReceivableObject();
                    mod.ObjectName = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "ObjectName";
                box.DisplayMember = "ObjectName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillOtherReceivableObject(ListBox box)
        {
            dalOtherReceivableObject dal = new dalOtherReceivableObject();
            BindingCollection<modOtherReceivableObject> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "ObjectName";
                box.DisplayMember = "ObjectName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion

        #region FillOtherPayableObject
        public static void FillOtherPayableObject(ComboBox box, bool allitem)
        {
            dalOtherPayableObject dal = new dalOtherPayableObject();
            BindingCollection<modOtherPayableObject> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                if (allitem)
                {
                    modOtherPayableObject mod = new modOtherPayableObject();
                    mod.ObjectName = "ALL";
                    list.Insert(0, mod);
                }

                box.ValueMember = "ObjectName";
                box.DisplayMember = "ObjectName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }

        public static void FillOtherPayableObject(ListBox box)
        {
            dalOtherPayableObject dal = new dalOtherPayableObject();
            BindingCollection<modOtherPayableObject> list = dal.GetIList(true, out Util.emsg);
            if (list != null)
            {
                box.ValueMember = "ObjectName";
                box.DisplayMember = "ObjectName";
                box.DataSource = list;
            }
            else
            {
                box.DataSource = null;
            }
        }
        #endregion
    }

    [Serializable]
    public class modStatus
    {
        string _code;
        string _name;

        public modStatus() { }

        public modStatus(string code, string name)
        {
            _code = code;
            _name = name;
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
