using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.CSharp;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.bridge;
using unoidl.com.sun.star.frame;
using OpenOffice;

namespace LXMS
{
    public class clsExport
    {
        private string _title;
        private string _filename;
        private int _exportType;
        private DataGridView _exportGrid;
        private MSFlexGridLib.MSFlexGrid _flexGrid;
        private ListView _listview;
        private int _gridType = 0;
        public clsExport(string title, string filename, int expType,DataGridView exportGrid)
        {
            _filename = filename;
            _title = title;
            _exportType = expType;
            _exportGrid = exportGrid;
            _gridType = 0;
        }

        public clsExport(string title, string filename, int expType, MSFlexGridLib.MSFlexGrid exportGrid)
        {
            _filename = filename;
            _title = title;
            _exportType = expType;
            _flexGrid = exportGrid;
            _gridType = 1;
        }

        public clsExport(string title, string filename, int expType, ListView listview)
        {
            _filename = filename;
            _title = title;
            _exportType = expType;
            _listview = listview;
            _gridType = 2;
        }

        public static string GetExportFilePath(int expType)
        {
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.Reset();
            string ext = string.Empty;
            switch (expType)
            {
                case (0):
                case (4):
                    ext = "xls";
                    break;
                case (1):
                    ext = "ods";
                    break;
                case (2):
                    ext = "txt";
                    break;
                case (3):
                    ext = "pdf";
                    break;
                default:
                    break;
            }            
            string path = Path.GetTempPath();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string timestr = DateTime.Now.ToLongTimeString().Replace(":", "");
            string filename = @path + timestr + "." + ext;
            return filename;            
        }

        public bool ExportGrid(out string emsg)
        {
            try
            {
                emsg = string.Empty;
                if (File.Exists(_filename))
                {
                    File.Delete(_filename);
                }
                bool ret;
                switch (_exportType)
                {
                    case (0):    //Excel
                        ret = ExportToExcel();
                        if (ret == true)
                        {
                            Process.Start(_filename);
                        }
                        break;
                    case (1):    //Open Office
                        ret = ExportToCsv();
                        if (ret == true)
                        {
                            Process.Start(_filename);
                        }
                        break;
                    case (2):    //Txt file
                        ret = ExportToTxt();
                        if (ret == true)
                        {
                            Process.Start(_filename);
                        }
                        break;
                    case (3):    //PDF
                        ret = ExportToPdf();
                        if (ret == true)
                        {
                            Process.Start(_filename);
                        }                                        
                        break;
                    default:
                        ret = false;
                        break;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                //throw;
                emsg = ex.Message.ToString();
                return false;
            }
        }

        private bool ExportToTxt()
        {
            try
            {
                string temp = string.Empty;
                string temp2 = string.Empty;
                if (_gridType == 0)
                {
                    int iCol = _exportGrid.CurrentCell.ColumnIndex;
                    for (int iRow = 0; iRow < _exportGrid.RowCount; iRow++)
                    {
                        if (_exportGrid.Rows[iRow].Cells[iCol].Value != null)
                        {
                            if (string.IsNullOrEmpty(temp))
                            {
                                temp = _exportGrid.Rows[iRow].Cells[iCol].Value.ToString();
                                temp2 = _exportGrid.Rows[iRow].Cells[iCol].Value.ToString();
                            }
                            else
                            {
                                temp += "\r\n" + _exportGrid.Rows[iRow].Cells[iCol].Value.ToString();
                                temp2 += "," + _exportGrid.Rows[iRow].Cells[iCol].Value.ToString();
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(temp))
                            {
                                temp = "";
                            }
                            else
                            {
                                temp += "\r\n";
                            }
                        }
                    }
                }
                else if (_gridType == 1)
                {
                    for (int iRow = 0; iRow < _flexGrid.Rows; iRow++)
                    {
                        if (!string.IsNullOrEmpty(_flexGrid.get_TextMatrix(iRow, _flexGrid.Col)))
                        {
                            if (string.IsNullOrEmpty(temp))
                            {
                                temp = _flexGrid.get_TextMatrix(iRow, _flexGrid.Col);
                                temp2 = _flexGrid.get_TextMatrix(iRow, _flexGrid.Col);
                            }
                            else
                            {
                                temp += "\r\n" + _flexGrid.get_TextMatrix(iRow, _flexGrid.Col);
                                temp2 += "," + _flexGrid.get_TextMatrix(iRow, _flexGrid.Col);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(temp))
                            {
                                temp = "";
                            }
                            else
                            {
                                temp += ",";
                            }
                        }
                    }
                }
                else
                {
                    for (int iRow = 0; iRow < _listview.Items.Count; iRow++)
                    {
                        if (!string.IsNullOrEmpty(_listview.Items[iRow].Text))
                        {
                            if (string.IsNullOrEmpty(temp))
                            {
                                temp = _listview.Items[iRow].Text;
                                temp2 = _listview.Items[iRow].Text;
                            }
                            else
                            {
                                temp += "," + _listview.Items[iRow].Text;
                                temp2 += "," + _listview.Items[iRow].Text;
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(temp))
                            {
                                temp = "";
                            }
                            else
                            {
                                temp += ",";
                            }
                        }
                    }
                }
                StreamWriter sw = new StreamWriter(_filename, false, Encoding.UTF8);
                sw.Write(temp2 + "\r\n\r\n" + temp);  //这里可以用 你的a代替
                sw.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            finally
            {                
                //_exportGrid.Dispose();
            }
        }

        private bool ExportToPdf()
        {            
            Document dc = new Document();
            try
            {
                FileStream fs = new FileStream(_filename, FileMode.Create);
                PdfWriter writer = PdfWriter.getInstance(dc, fs);
                dc.Open();

                dc.Add(new Paragraph(DateTime.Now.ToString()));
                dc.Add(new Paragraph(_title));
                dc.Add(new Paragraph(""));

                PdfPTable table;
                if (_gridType == 0)
                {
                    table = new PdfPTable(_exportGrid.Rows.Count);
                    for (int i = 0; i < _exportGrid.Columns.Count; i++)
                    {
                        table.addCell(new Phrase(_exportGrid.Columns[i].HeaderText));
                    }
                    for (int i = 0; i < _exportGrid.Rows.Count; i++)
                    {
                        for (int j = 0; j < _exportGrid.Columns.Count; j++)
                        {
                            if (_exportGrid.Rows[j].Cells[j].Value != null)
                                table.addCell(new Phrase(_exportGrid.Rows[i].Cells[j].Value.ToString()));
                            else
                                table.addCell(new Phrase(string.Empty));
                        }
                    }
                }
                else if (_gridType == 1)
                {
                    table = new PdfPTable(_flexGrid.Rows);                    
                    for (int i = 0; i < _flexGrid.Rows; i++)
                    {
                        for (int j = 0; j < _flexGrid.Cols; j++)
                        {
                            table.addCell(new Phrase(_flexGrid.get_TextMatrix(i,j)));
                        }
                    }
                }
                else
                {
                    float[] wids = new float[_listview.Columns.Count];
                    for (int i = 0; i < _listview.Columns.Count; i++)
                        wids[i] = _listview.Columns[i].Width;
                    table = new PdfPTable(wids);
                    for (int i = 0; i < _listview.Items.Count; i++)
                    {
                        table.addCell(new Phrase(_listview.Items[i].Text));
                        for (int j = 0; j < _flexGrid.Cols; j++)
                        {
                            table.addCell(new Phrase(_listview.Items[i].SubItems[j].Text));
                        }
                    }
                }
                dc.Add(table);
                dc.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            finally
            {
                dc.Close();
                //_exportGrid.Dispose();
            }
        }
    
        public bool ExportToExcel()
        {                  
            if(File.Exists(_filename))
                File.Delete(_filename);
                
            Excel.Application m_objExcel = new Excel.Application();
            m_objExcel.DisplayAlerts = false;
            Excel.Workbooks m_objBooks = m_objExcel.Workbooks;

            Excel.Workbook m_objBook = (Excel.Workbook)m_objBooks.Add(Type.Missing);
            Excel.Sheets sm_objSheets = (Excel.Sheets)m_objBook.Worksheets;

            Excel.Worksheet m_objSheet = (Excel.Worksheet)sm_objSheets.get_Item(1);

            int colindex = 0;
            for (int j = 0; j < _exportGrid.ColumnCount; j++)
            {
                if (_exportGrid.Columns[j].Visible)
                {
                    colindex++;
                    m_objSheet.Cells[1, colindex] = _exportGrid.Columns[j].HeaderText;
                }
            }
                        
            if (_exportGrid.SelectedRows.Count == 0)
            {
                for (int i = 0; i < _exportGrid.RowCount; i++)
                {
                    colindex = 0;
                    for (int j = 0; j < _exportGrid.ColumnCount; j++)
                    {
                        if (_exportGrid.Columns[j].Visible)
                        {
                            colindex++;
                            if (_exportGrid.Rows[i].Cells[j].Value != null)
                                m_objSheet.Cells[i + 2, colindex] = _exportGrid.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _exportGrid.SelectedRows.Count; i++)
                {
                    colindex = 0;
                    for (int j = 0; j < _exportGrid.ColumnCount; j++)
                    {                        
                        if (_exportGrid.Columns[j].Visible)
                        {
                            colindex++;
                            if (_exportGrid.Rows[i].Cells[j].Value != null)
                            {
                                m_objSheet.Cells[i + 2, colindex] = _exportGrid.SelectedRows[i].Cells[j].Value.ToString();
                            }
                        }
                    }
                }
            }

            m_objSheet.Activate();
            System.Windows.Forms.Application.DoEvents();
            m_objExcel.DisplayAlerts = false;
            m_objExcel.AlertBeforeOverwriting = false;
            //保存工作簿                 
            m_objBook.SaveAs(_filename);
            m_objBook.Close();
            return true;
        }

        private bool ExportToCsv()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = _filename;
            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.Default);
            string columnTitle = "";
            try
            {
                sw.WriteLine(_title + "\r\n");
                if (_gridType == 0)
                {
                    //写入列标题 
                    for (int i = 0; i < _exportGrid.ColumnCount; i++)
                    {
                        if (_exportGrid.Columns[i].Visible)
                        {
                            if (!string.IsNullOrEmpty(columnTitle))
                            {
                                columnTitle += "|";
                            }
                            columnTitle += _exportGrid.Columns[i].HeaderText;
                        }                        
                    }
                    sw.WriteLine(columnTitle);                    
                    //写入列内容 
                    for (int j = 0; j < _exportGrid.Rows.Count; j++)
                    {
                        if (_exportGrid.Rows[j].Visible)
                        {
                            string columnValue = string.Empty;
                            for (int k = 0; k < _exportGrid.Columns.Count; k++)
                            {
                                if (_exportGrid.Columns[k].Visible)
                                {
                                    if (!string.IsNullOrEmpty(columnValue))
                                    {
                                        columnValue += "|";
                                    }
                                    if (_exportGrid.Rows[j].Cells[k].Value != null)
                                        columnValue += _exportGrid.Rows[j].Cells[k].Value.ToString();
                                }
                            }
                            sw.WriteLine(columnValue);
                        }
                    }
                }
                else if (_gridType == 1)
                {
                    for (int i = 0; i < _flexGrid.Rows; i++)
                    {
                        string columnValue = string.Empty;
                        for (int j = 0; j < _flexGrid.Cols; j++)
                        {
                            if (j > 0)
                            {
                                columnValue += "|";
                            }
                            columnValue += _flexGrid.get_TextMatrix(i, j);
                        }
                        sw.WriteLine(columnValue);                        
                    }
                }
                else
                {
                    for (int i = 0; i < _listview.Items.Count; i++)
                    {
                        string columnValue = _listview.Items[i].Text;
                        for (int j = 1; j < _listview.Columns.Count; j++)
                        {
                            columnValue += "|" + _listview.Items[i].SubItems[j].Text;
                        }
                        sw.WriteLine(columnValue);                        
                    }
                }
                sw.Close();
                myStream.Close();

            }
            catch
            {
                return false;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
            return true;

        }

        private bool ExportOOo()
        {
            try
            {
                SpreadSheetHelper helper = new SpreadSheetHelper(true);

                //Samples for service sheet.SheetCell
                unoidl.com.sun.star.sheet.XSpreadsheet xSheet = helper.getSpreadsheet(0);
                unoidl.com.sun.star.table.XCell xCell = null;

                //xCell = xSheet.getCellByPosition(1, 1);
                //// --- Insert two text paragraphs into the cell. ---
                //unoidl.com.sun.star.text.XText tText = (unoidl.com.sun.star.text.XText)xCell;
                //unoidl.com.sun.star.text.XTextCursor tTextCursor =tText.createTextCursor();
                //tText.insertString(tTextCursor, _title + "\r\n", false);

                if (_gridType == 0)
                {
                    for (int j = 0; j < _exportGrid.Columns.Count; j++)
                    {
                        xCell = xSheet.getCellByPosition(j + 1, 2);

                        // --- Insert two text paragraphs into the cell. ---
                        unoidl.com.sun.star.text.XText xText = (unoidl.com.sun.star.text.XText)xCell;
                        unoidl.com.sun.star.text.XTextCursor xTextCursor = xText.createTextCursor();

                        xText.insertString(xTextCursor, _exportGrid.Columns[j].HeaderText, false);
                    }
                    // --- Get cell B3 by position - (column, row) ---
                    for (int i = 0; i < _exportGrid.Rows.Count; i++)
                    {
                        if (_exportGrid.Rows[i].Visible)
                        {
                            for (int j = 0; j < _exportGrid.Columns.Count; j++)
                            {
                                xCell = xSheet.getCellByPosition(j + 1, i + 3);
                                unoidl.com.sun.star.text.XText xText = (unoidl.com.sun.star.text.XText)xCell;
                                unoidl.com.sun.star.text.XTextCursor xTextCursor = xText.createTextCursor();
                                xText.insertString(xTextCursor, _exportGrid.Rows[i].Cells[j].Value.ToString(), false);
                            }
                        }
                    }
                }
                else if (_gridType == 1)
                {
                    for (int i = 0; i < _flexGrid.Rows; i++)
                    {
                        for (int j = 0; j < _flexGrid.Cols; j++)
                        {
                            xCell = xSheet.getCellByPosition(j + 1, i + 2);
                            unoidl.com.sun.star.text.XText xText = (unoidl.com.sun.star.text.XText)xCell;
                            unoidl.com.sun.star.text.XTextCursor xTextCursor = xText.createTextCursor();
                            xText.insertString(xTextCursor, _flexGrid.get_TextMatrix(i, j), false);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _listview.Items.Count; i++)
                    {                        
                        xCell = xSheet.getCellByPosition(1, i + 2);
                        unoidl.com.sun.star.text.XText xText = (unoidl.com.sun.star.text.XText)xCell;
                        unoidl.com.sun.star.text.XTextCursor xTextCursor = xText.createTextCursor();
                        xText.insertString(xTextCursor, _listview.Items[i].Text, false);
                        for (int j = 1; j < _flexGrid.Cols; j++)
                        {
                            xCell = xSheet.getCellByPosition(j + 1, i + 2);
                            xText = (unoidl.com.sun.star.text.XText)xCell;
                            xTextCursor = xText.createTextCursor();
                            xText.insertString(xTextCursor, _listview.Items[i].SubItems[j].Text, false);
                        }
                    }
                }
                helper.storeDocComponent(_filename);
                helper.closeDocCompant();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            finally
            {
                //_exportGrid.Dispose();
            }
        }
                
        public static void ExportByTemplete(IList<modExcelRangeData> list, string sheetname)
        {
            try
            {
                string filename = clsLxms.GetParameterValue("EXCEL_TEMPLETE_FILE");
                if(!File.Exists(filename))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Can not find the templete file") + ": \r\n" + filename, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                string destfilename = GetExportFilePath(0);
                if (File.Exists(destfilename))
                    File.Delete(destfilename);
                Util.retValue1 = destfilename;
                File.Copy(filename, destfilename);
                Excel.Application m_objExcel = new Excel.Application();
                m_objExcel.DisplayAlerts = false;
                Excel.Workbooks m_objBooks = m_objExcel.Workbooks;
                m_objBooks.Open(destfilename, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Excel.Workbook m_objBook = (Excel.Workbook)m_objBooks.get_Item(1);
                Excel.Sheets sm_objSheets = (Excel.Sheets)m_objBook.Worksheets;
                
                Excel.Worksheet m_objSheet = (Excel.Worksheet)sm_objSheets.get_Item(sheetname);
                for (int i = sm_objSheets.Count; i >= 1 ; i--)
                {
                    Excel.Worksheet m_Sheet = sm_objSheets.get_Item(i);
                    if (m_Sheet.Name.ToLower().CompareTo(sheetname.ToLower()) != 0)
                        m_Sheet.Delete();
                }
                Excel.Range range;                
                foreach (modExcelRangeData mod in list)
                {
                    range = (Excel.Range)m_objSheet.get_Range(mod.CellBegin, mod.CellEnd);
                    if (!mod.Is_Pic)
                        range.Value = mod.CellValue;
                    else
                    {
                        if (!string.IsNullOrEmpty(mod.CellValue) && File.Exists(mod.CellValue))
                        {
                            System.Drawing.Image im = System.Drawing.Image.FromFile(mod.CellValue);

                            double iLeft = range.Left + 1;
                            double iTop = range.Top + 1;
                            double iWidth = range.Width - 2;
                            double iHeight = range.Height - 2;
                            if (im.Width <= range.Width)
                            {
                                iWidth = im.Width;
                                iLeft = range.Left + (range.Width - im.Width) / 2;
                            }

                            if (im.Height <= range.Height)
                            {
                                iHeight = im.Height;
                                iTop = range.Top + (range.Height - im.Height) / 2;
                            }
                            im.Dispose();
                            m_objSheet.Activate();
                            range.Select();
                            m_objSheet.Shapes.AddPicture(mod.CellValue, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)iLeft, (float)iTop, (float)iWidth, (float)iHeight);
                        }
                    }                    
                }
                m_objSheet.Activate();
                m_objExcel.Visible = true;                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }            
        }

        /// <summary>
        /// 模板导出Excel
        /// 可分页导出.
        /// </summary>
        /// <param name="sheetname">sheetname</param>
        /// <param name="startrow">startrow</param>
        /// <param name="rows">rows</param>
        /// <param name="cols">cols</param>
        /// <param name="copies">copies</param>
        /// <param name="returnfile">returnfile</param>
        public static void ExportByTemplate(IList<modExcelRangeData> list, string sheetname, int startrow, int rows, int cols, int copies)
        {            
            try
            {
                if (list == null || list.Count == 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("No data!") , clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
				string filename;
#if DEBUG
				filename = @"C:\HongBiao\templates.xls";
#else
				filename = clsLxms.GetParameterValue("EXCEL_TEMPLETE_FILE");
#endif
                if (!File.Exists(filename))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Can not find the templete file") + ": \r\n" + filename, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                string destfile = GetExportFilePath(0);
                if(File.Exists(destfile))
                    File.Delete(destfile);
                Util.retValue1 = destfile;
                File.Copy(filename, destfile);
                Excel.Application m_objExcel = new Excel.Application();
                m_objExcel.DisplayAlerts = false;
                Excel.Workbooks m_objBooks = m_objExcel.Workbooks;
                m_objBooks.Open(destfile, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Excel.Workbook m_objBook = (Excel.Workbook)m_objBooks.get_Item(1);
                Excel.Sheets sm_objSheets = (Excel.Sheets)m_objBook.Worksheets;

                Excel.Worksheet m_objSheet = (Excel.Worksheet)sm_objSheets.get_Item(sheetname);
                for (int i = sm_objSheets.Count; i >= 1; i--)
                {
                    Excel.Worksheet m_Sheet = sm_objSheets.get_Item(i);
                    if (m_Sheet.Name.ToLower().CompareTo(sheetname.ToLower()) != 0)
                        m_Sheet.Delete();
                }

                //Excel.Range templaterange = m_objSheet.Range(m_objSheet.Cells[startrow, 1], m_objSheet.Cells[startrow + rows - 1, cols]);
                Excel.Range templaterange = m_objSheet.Range[m_objSheet.Cells[startrow, 1], m_objSheet.Cells[startrow + rows - 1, cols]];
                for (int i = 1; i < copies; i++)
                {
                    Excel.Range targetrange = (Excel.Range)m_objSheet.Range[m_objSheet.Cells[rows * i + 1, 1], m_objSheet.Cells[rows * (i + 1), cols]];
                    templaterange.Copy(targetrange);
                    for (int k = 0; k < rows; k++)
                    {
                        m_objSheet.Rows[rows * i + 1 + k].RowHeight = m_objSheet.Rows[startrow + k].RowHeight;
                    }
                }
                
                Excel.Range range;
                foreach (modExcelRangeData mod in list)
                {
                    range = (Excel.Range)m_objSheet.get_Range(mod.CellBegin, mod.CellEnd);
                    if (!mod.Is_Pic)
                        range.Value = mod.CellValue;
                    else
                    {
                        if (!string.IsNullOrEmpty(mod.CellValue) && File.Exists(mod.CellValue))
                        {
                            System.Drawing.Image im = System.Drawing.Image.FromFile(mod.CellValue);

                            double iLeft = range.Left + 1;
                            double iTop = range.Top + 1;
                            double iWidth = range.Width - 2;
                            double iHeight = range.Height - 2;
                            if (im.Width <= range.Width)
                            {
                                iWidth = im.Width;
                                iLeft = range.Left + (range.Width - im.Width) / 2;
                            }

                            if (im.Height <= range.Height)
                            {
                                iHeight = im.Height;
                                iTop = range.Top + (range.Height - im.Height) / 2;
                            }
                            im.Dispose();
                            m_objSheet.Activate();
                            range.Select();
                            m_objSheet.Shapes.AddPicture(mod.CellValue, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)iLeft, (float)iTop, (float)iWidth, (float)iHeight);
                        }
                    }
                }
                
                m_objSheet.Activate();
                System.Windows.Forms.Application.DoEvents();  
                m_objExcel.DisplayAlerts = false;
                m_objExcel.AlertBeforeOverwriting = false;
                //保存工作簿 
                m_objExcel.Visible = true; 
                return;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }    
        }
    }

    [Serializable]
    public class modExcelRangeData
    {
        string _cellvalue;
        string _cellbegin;
        string _cellend;
        bool _ispic;

        public modExcelRangeData() { }

        public modExcelRangeData(string cellvalue, string cellbegin, string cellend) 
        {
            _cellvalue = cellvalue;
            _cellbegin = cellbegin;
            _cellend = cellend;
            _ispic = false;
        }

        public modExcelRangeData(string cellvalue, string cellbegin, string cellend, bool is_pic)
        {
            _cellvalue = cellvalue;
            _cellbegin = cellbegin;
            _cellend = cellend;
            _ispic = is_pic;
        }

        public string CellValue
        {
            get { return _cellvalue; }
            set { _cellvalue = value; }
        }
        public string CellBegin
        {
            get { return _cellbegin; }
            set { _cellbegin = value; }
        }
        public string CellEnd
        {
            get { return _cellend; }
            set { _cellend = value; }
        }
        public bool Is_Pic
        {
            get { return _ispic; }
            set { _ispic = value; }
        }
    }
}
