using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;

namespace LXMS
{
    class PrintService
    {
        // Declare the PrintDocument object.
        //创建一个PrintDocument的实例
        //public enum PrintType { Txt = 0, Image = 1, Line = 2 };        
        private PrintDocument pdDocument =new PrintDocument();
        private Stream _streamToPrint;
        string _streamType;
        bool _printflag = false;
        IList<modPrintItem> _list = new List<modPrintItem>();        
        public PrintService()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            //将事件处理函数添加到PrintDocument的PrintPage中
            this.pdDocument.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);            
        }

        public bool PrintFlag
        {
            get { return _printflag; }
        }
        // This method will set properties on the PrintDialog object and
        // then display the dialog.
        public void StartPrint(Stream streamToPrint, string streamType, int copies)
        {
            _streamToPrint = streamToPrint;
            _streamType = streamType;
            // Allow the user to choose the page range he or she would
            // like to print.
            PrintDialog PrintDialog1 = new PrintDialog();//创建一个PrintDialog的实例。
            PrintDialog1.AllowSomePages = true;
            PrintDialog1.PrinterSettings.Copies = (short)copies;
            // Show the help button.
            PrintDialog1.ShowHelp = true;            
            // Set the Document property to the PrintDocument for 
            // which the PrintPage Event has been handled. To display the
            // dialog, either this property or the PrinterSettings property 
            // must be set 
            PrintDialog1.Document = pdDocument;//把PrintDialog的Document属性设为上面配置好的PrintDocument的实例

            //DialogResult result = PrintDialog1.ShowDialog();//调用PrintDialog的ShowDialog函数显示打印对话框

            //// If the result is OK then print the document.
            //if (result == DialogResult.OK)
            //{
                pdDocument.Print();//开始打印
                _printflag = true;
            //}
        }

        // This method will set properties on the PrintDialog object and
        // then display the dialog.
        public void StartPrint(IList<modPrintItem> list, int copies, int paper_width, int paper_height)
        {            
            _list = list;            
            // Allow the user to choose the page range he or she would
            // like to print.            
            pdDocument.PrinterSettings.Copies = (short)copies;
            pdDocument.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("Custum", paper_width, paper_height);
            pdDocument.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = 256;
            // Set the Document property to the PrintDocument for 
            // which the PrintPage Event has been handled. To display the
            // dialog, either this property or the PrinterSettings property 
            // must be set 
            //PrintDialog PrintDialog1 = new PrintDialog();//创建一个PrintDialog的实例。
            //PrintDialog1.AllowSomePages = true;
            //PrintDialog1.Document = pdDocument;//把PrintDialog的Document属性设为上面配置好的PrintDocument的实例

            // If the result is OK then print the document.
            //if (PrintDialog1.ShowDialog() == DialogResult.OK)
            //{
                pdDocument.Print();//开始打印
                _printflag = true;
            //}
        }

        public void StartPreview(IList<modPrintItem> list, int copies, int paper_width, int paper_height)
        {
            _list = list;
            _printflag = false;
            // Allow the user to choose the page range he or she would
            // like to print.            
            pdDocument.PrinterSettings.Copies = (short)copies;
            //pdDocument.DefaultPageSettings.PaperSize.RawKind = 256;
            pdDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", paper_width, paper_height);            
            pdDocument.DefaultPageSettings.PaperSize.RawKind = 256;
            
            PrintPreviewDialog PrintPreview = new PrintPreviewDialog();
            PrintPreview.Document = pdDocument;
           
            if (PrintPreview.ShowDialog() == DialogResult.OK)
            {
                //调用打印 
                pdDocument.Print();
                _printflag = true;
            }
        }
        
        // The PrintDialog will print the document
        // by handling the document’s PrintPage event.
        private void docToPrint_PrintPage(object sender, PrintPageEventArgs e)//设置打印机开始打印的事件处理函数
        {
            // Insert code to render the page here.
            // This code will be called when the control is drawn.

            // The following code will render a simple
            // message on the printed document   
            if (_list != null)
            {
                foreach (modPrintItem mod in _list)
                {
                    if (mod.iLeft == 0)
                        mod.iLeft = Convert.ToInt32((e.MarginBounds.Left) * 3 / 4);　 //左边距 
                    if (mod.iTop == 0)
                        mod.iTop = Convert.ToInt32(e.MarginBounds.Top * 2 / 3);　　　 //顶边距 
                    if (mod.iWidth == 0)
                        mod.iWidth = e.MarginBounds.Right - mod.iLeft;
                    float fontsize = 10;
                    if (mod.FontSize != 0)
                        fontsize = mod.FontSize;

                    string fontname = "宋体";
                    if (!string.IsNullOrEmpty(mod.FontName))
                        fontname = mod.FontName;

                    switch (mod.printType)
                    {
                        case "TXT":
                            System.Drawing.Font printFont = new System.Drawing.Font(fontname, fontsize, mod.LabelFontStyle);
                            e.Graphics.DrawString(mod.PrintText, printFont, System.Drawing.Brushes.Black, mod.iLeft, mod.iTop);
                            break;
                        case "IMAGE":
                            System.Drawing.Image image = mod.PrintImage;
                            int x = mod.iLeft;
                            int y = mod.iTop;
                            int width = 0;
                            if (mod.iWidth > 0)
                                width = mod.iWidth;
                            else
                                width = image.Width;
                            int height = 0;
                            if (mod.iHeight > 0)
                                height = mod.iHeight;
                            else
                                height = image.Height;

                            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
                            e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
                            break;
                        case "LINE":
                            e.Graphics.DrawLine(new Pen(Color.Black, mod.LineWeight), mod.iLeft, mod.iTop, mod.iLeft + mod.iWidth, mod.iTop);
                            break;
                        case "VLINE":
                            e.Graphics.DrawLine(new Pen(Color.Black, mod.LineWeight), mod.iLeft, mod.iTop, mod.iLeft, mod.iTop + mod.iHeight);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                float fontsize = 10;                
                string fontname = "宋体";
                FontStyle content_fontstyle = clsLxms.GetFontStyle("Regular");
                System.Drawing.Font printFont = new System.Drawing.Font(fontname, fontsize, content_fontstyle);
                e.Graphics.DrawString("_", printFont, System.Drawing.Brushes.Black, 12, 1);
            }
        }
    }

    [Serializable]
    public class modPrintItem
    {
        string _printtype;
        string _printtext;
        Image _image;
        int _left;
        int _top;
        int _width;
        int _height;
        string _fontname;
        float _fontsize;
        FontStyle _fontstyle;
        float _lineweight;

        public string printType
        {
            get { return _printtype; }
            set { _printtype = value; }
        }
        public string PrintText
        {
            get { return _printtext; }
            set { _printtext = value; }
        }
        public Image PrintImage
        {
            get { return _image; }
            set { _image = value; }
        }
        public int iLeft
        {
            get { return _left; }
            set { _left = value; }
        }
        public int iTop
        {
            get { return _top; }
            set { _top = value; }
        }
        public int iWidth
        {
            get { return _width; }
            set { _width = value; }
        }
        public int iHeight
        {
            get { return _height; }
            set { _height = value; }
        }
        public string FontName
        {
            get { return _fontname; }
            set { _fontname = value; }
        }
        public float FontSize
        {
            get { return _fontsize; }
            set { _fontsize = value; }
        }
        public FontStyle LabelFontStyle
        {
            get { return _fontstyle; }
            set { _fontstyle = value; }
        }
        public float LineWeight
        {
            get { return _lineweight == 0 ? 1 : _lineweight; }
            set { _lineweight = value; }
        }
    }
}

