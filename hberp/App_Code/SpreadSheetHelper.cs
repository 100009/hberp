using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.bridge;
using unoidl.com.sun.star.frame;



namespace OpenOffice
{
    public class SpreadSheetHelper : IDisposable
    {
        // __  private members  ___________________________________________

        private const String msDataSheetName = "Data";

        private unoidl.com.sun.star.uno.XComponentContext m_xContext;
        private unoidl.com.sun.star.lang.XMultiServiceFactory mxMSFactory;
        private unoidl.com.sun.star.sheet.XSpreadsheetDocument mxDocument;

        // ________________________________________________________________

        /// <summary>
        /// init document
        /// </summary>
        /// <param name="IsHidden">true is hidden docuement</param>
        public SpreadSheetHelper(bool IsHidden)
        {
            // Connect to a running office and get the service manager
            mxMSFactory = connect();
            // Create a new spreadsheet document
            mxDocument = initDocument("",IsHidden);
        }
        /// <summary>
        /// init document
        /// </summary>
        /// <param name="filepath">open a exist document</param>
        /// <param name="ishidden">true is hidden docuement</param>
        public SpreadSheetHelper(string filepath,bool ishidden)
        {
            // Connect to a running office and get the service manager
            mxMSFactory = connect();
            // get a  spreadsheet document
            mxDocument = initDocument(filepath,ishidden);
        }

        // __  helper methods  ____________________________________________

        /** Returns the service manager.
            @return  XMultiServiceFactory interface of the service manager. */

        public XMultiServiceFactory getServiceManager()
        {
            return mxMSFactory;
        }

        /** Returns the whole spreadsheet document.
            @return  XSpreadsheetDocument interface of the document. */

        public unoidl.com.sun.star.sheet.XSpreadsheetDocument getDocument()
        {
            return mxDocument;
        }

        
        /// <summary>
        /// Returns the spreadsheet with the specified index (0-based).
        /// </summary>
        /// <param name="nIndex">The index of the sheet</param>
        /// <returns>XSpreadsheet interface of the sheet.</returns>
        public unoidl.com.sun.star.sheet.XSpreadsheet getSpreadsheet(int nIndex)
        {
            // Collection of sheets
            unoidl.com.sun.star.sheet.XSpreadsheets xSheets =
                mxDocument.getSheets();

            unoidl.com.sun.star.container.XIndexAccess xSheetsIA =
                (unoidl.com.sun.star.container.XIndexAccess)xSheets;

            unoidl.com.sun.star.sheet.XSpreadsheet xSheet =
                (unoidl.com.sun.star.sheet.XSpreadsheet)
                  xSheetsIA.getByIndex(nIndex).Value;

            return xSheet;
        }

        /** Inserts a new empty spreadsheet with the specified name.
            @param aName  The name of the new sheet.
            @param nIndex  The insertion index.
            @return  The XSpreadsheet interface of the new sheet. */
        public unoidl.com.sun.star.sheet.XSpreadsheet insertSpreadsheet(
            String aName, short nIndex)
        {
            // Collection of sheets
            unoidl.com.sun.star.sheet.XSpreadsheets xSheets =
                mxDocument.getSheets();

            xSheets.insertNewByName(aName, nIndex);
            unoidl.com.sun.star.sheet.XSpreadsheet xSheet =
                (unoidl.com.sun.star.sheet.XSpreadsheet)
                  xSheets.getByName(aName).Value;

            return xSheet;
        }

        // ________________________________________________________________
        // Methods to fill values into cells.

        /** Writes a double value into a spreadsheet.
            @param xSheet  The XSpreadsheet interface of the spreadsheet.
            @param aCellName  The address of the cell (or a named range).
            @param fValue  The value to write into the cell. */
        
        public void setValue(
            unoidl.com.sun.star.sheet.XSpreadsheet xSheet,
            String aCellName,
            double fValue)
        {
            xSheet.getCellRangeByName(aCellName).getCellByPosition(
                0, 0).setValue(fValue);
        }

        /** Writes a formula into a spreadsheet.
            @param xSheet  The XSpreadsheet interface of the spreadsheet.
            @param aCellName  The address of the cell (or a named range).
            @param aFormula  The formula to write into the cell. */
        public void setFormula(
            unoidl.com.sun.star.sheet.XSpreadsheet xSheet,
            String aCellName,
            String aFormula)
        {
            xSheet.getCellRangeByName(aCellName).getCellByPosition(
                0, 0).setFormula(aFormula);
        }

        /** Writes a date with standard date format into a spreadsheet.
            @param xSheet  The XSpreadsheet interface of the spreadsheet.
            @param aCellName  The address of the cell (or a named range).
            @param nDay  The day of the date.
            @param nMonth  The month of the date.
            @param nYear  The year of the date. */
        public void setDate(
            unoidl.com.sun.star.sheet.XSpreadsheet xSheet,
            String aCellName,
            int nDay, int nMonth, int nYear)
        {
            // Set the date value.
            unoidl.com.sun.star.table.XCell xCell =
                xSheet.getCellRangeByName(aCellName).getCellByPosition(0, 0);
            String aDateStr = nMonth + "/" + nDay + "/" + nYear;
            xCell.setFormula(aDateStr);

            // Set standard date format.
            unoidl.com.sun.star.util.XNumberFormatsSupplier xFormatsSupplier =
                (unoidl.com.sun.star.util.XNumberFormatsSupplier)getDocument();
            unoidl.com.sun.star.util.XNumberFormatTypes xFormatTypes =
                (unoidl.com.sun.star.util.XNumberFormatTypes)
                  xFormatsSupplier.getNumberFormats();
            int nFormat = xFormatTypes.getStandardFormat(
                unoidl.com.sun.star.util.NumberFormat.DATE,
                new unoidl.com.sun.star.lang.Locale());

            unoidl.com.sun.star.beans.XPropertySet xPropSet =
                (unoidl.com.sun.star.beans.XPropertySet)xCell;
            xPropSet.setPropertyValue(
                "NumberFormat",
                new uno.Any((Int32)nFormat));
        }
        /// <summary>
        /// Insert a TEXT CELL using the XText interface
        /// </summary>
        /// <param name="xSheet"></param>
        /// <param name="nColumn"></param>
        /// <param name="nRow"></param>
        /// <param name="strText"></param>
        public void InsertTextCell(unoidl.com.sun.star.sheet.XSpreadsheet xSheet, int nColumn, int nRow, string strText)
        {
            unoidl.com.sun.star.table.XCell xCell = xSheet.getCellByPosition(nColumn, nRow);
            unoidl.com.sun.star.text.XText xCellText =
                (unoidl.com.sun.star.text.XText)xCell;
            unoidl.com.sun.star.text.XTextCursor xTextCursor =
                xCellText.createTextCursor();
            xCellText.insertString(xTextCursor, strText, false);
        }
        /// <summary>
        /// MergeCell
        /// </summary>
        /// <param name="xSheet"></param>
        /// <param name="aRange"></param>
        public void MergeCell(unoidl.com.sun.star.sheet.XSpreadsheet xSheet, String aRange)
        {
            unoidl.com.sun.star.table.XCellRange xCellRange = xSheet.getCellRangeByName(aRange);
            unoidl.com.sun.star.util.XMergeable xMerge =
                (unoidl.com.sun.star.util.XMergeable)xCellRange;
            xMerge.merge(true);
        }
        /// <summary>
        /// set Cell BackColor
        /// </summary>
        /// <param name="xSheet"></param>
        /// <param name="aRange"></param>
        /// <param name="color"></param>
        public void setCellBackColor(unoidl.com.sun.star.sheet.XSpreadsheet xSheet, String aRange, int color)
        {
            unoidl.com.sun.star.beans.XPropertySet xPropSet = null;
            unoidl.com.sun.star.table.XCellRange xCellRange = null;

            // draw back color
            xCellRange = xSheet.getCellRangeByName(aRange);
            xPropSet = (unoidl.com.sun.star.beans.XPropertySet)xCellRange;
            xPropSet.setPropertyValue(
                "CellBackColor", new uno.Any((Int32)color));
        }
        /// <summary>
        /// draw border color
        /// </summary>
        /// <param name="xSheet"></param>
        /// <param name="aRange"></param>
        /// <param name="width"></param>
        /// <param name="color"></param>
        public void setBorderColor(unoidl.com.sun.star.sheet.XSpreadsheet xSheet, String aRange, short width, int color)
        {
            unoidl.com.sun.star.beans.XPropertySet xPropSet = null;
            unoidl.com.sun.star.table.XCellRange xCellRange = null;

            // draw border
            xCellRange = xSheet.getCellRangeByName(aRange);
            xPropSet = (unoidl.com.sun.star.beans.XPropertySet)xCellRange;
            unoidl.com.sun.star.table.BorderLine aLine =
                new unoidl.com.sun.star.table.BorderLine();
            aLine.Color = color ;
            aLine.InnerLineWidth = aLine.LineDistance = 0;
            aLine.OuterLineWidth = width;

            unoidl.com.sun.star.table.TableBorder aBorder =
                new unoidl.com.sun.star.table.TableBorder();
            aBorder.TopLine = aBorder.BottomLine = aBorder.LeftLine =
                aBorder.RightLine = aLine;
            aBorder.IsTopLineValid = aBorder.IsBottomLineValid = true;
            aBorder.IsLeftLineValid = aBorder.IsRightLineValid = true;
            xPropSet.setPropertyValue(
                "TableBorder",
                new uno.Any(
                    typeof(unoidl.com.sun.star.table.TableBorder), aBorder));
        }

        /** Draws a colored border around the range and writes the headline
            in the first cell.
        
            @param xSheet  The XSpreadsheet interface of the spreadsheet.
            @param aRange  The address of the cell range (or a named range).
            @param aHeadline  The headline text. */
        public void prepareRange(
            unoidl.com.sun.star.sheet.XSpreadsheet xSheet,
            String aRange, String aHeadline)
        {
            unoidl.com.sun.star.beans.XPropertySet xPropSet = null;
            unoidl.com.sun.star.table.XCellRange xCellRange = null;

            // draw border
            xCellRange = xSheet.getCellRangeByName(aRange);
            xPropSet = (unoidl.com.sun.star.beans.XPropertySet)xCellRange;
            unoidl.com.sun.star.table.BorderLine aLine =
                new unoidl.com.sun.star.table.BorderLine();
            aLine.Color = 0x99CCFF;
            aLine.InnerLineWidth = aLine.LineDistance = 0;
            aLine.OuterLineWidth = 100;
            
            unoidl.com.sun.star.table.TableBorder aBorder =
                new unoidl.com.sun.star.table.TableBorder();
            aBorder.TopLine = aBorder.BottomLine = aBorder.LeftLine =
                aBorder.RightLine = aLine;
            aBorder.IsTopLineValid = aBorder.IsBottomLineValid = true;
            aBorder.IsLeftLineValid = aBorder.IsRightLineValid = true;
            xPropSet.setPropertyValue(
                "TableBorder",
                new uno.Any(
                    typeof(unoidl.com.sun.star.table.TableBorder), aBorder));
            
                // draw headline
                unoidl.com.sun.star.sheet.XCellRangeAddressable xAddr =
                    (unoidl.com.sun.star.sheet.XCellRangeAddressable)xCellRange;
                unoidl.com.sun.star.table.CellRangeAddress aAddr =
                    xAddr.getRangeAddress();

                xCellRange = xSheet.getCellRangeByPosition(
                    aAddr.StartColumn,
                    aAddr.StartRow, aAddr.EndColumn, aAddr.StartRow);

                xPropSet = (unoidl.com.sun.star.beans.XPropertySet)xCellRange;
                xPropSet.setPropertyValue(
                    "CellBackColor", new uno.Any((Int32)0x99CCFF));
                // write headline
                unoidl.com.sun.star.table.XCell xCell =
                    xCellRange.getCellByPosition(0, 0);
                xCell.setFormula(aHeadline);
                xPropSet = (unoidl.com.sun.star.beans.XPropertySet)xCell;
                xPropSet.setPropertyValue(
                    "CharColor", new uno.Any((Int32)0x003399));
                xPropSet.setPropertyValue(
                    "CharWeight",
                    new uno.Any((Single)unoidl.com.sun.star.awt.FontWeight.BOLD));
            
        }

        // ________________________________________________________________
        // Methods to create cell addresses and range addresses.

        /** Creates a unoidl.com.sun.star.table.CellAddress and initializes it
            with the given range.
            @param xSheet  The XSpreadsheet interface of the spreadsheet.
            @param aCell  The address of the cell (or a named cell). */
        public unoidl.com.sun.star.table.CellAddress createCellAddress(
            unoidl.com.sun.star.sheet.XSpreadsheet xSheet,
            String aCell)
        {
            unoidl.com.sun.star.sheet.XCellAddressable xAddr =
            (unoidl.com.sun.star.sheet.XCellAddressable)
                xSheet.getCellRangeByName(aCell).getCellByPosition(0, 0);
            return xAddr.getCellAddress();
        }

        /** Creates a unoidl.com.sun.star.table.CellRangeAddress and initializes
            it with the given range.
            @param xSheet  The XSpreadsheet interface of the spreadsheet.
            @param aRange  The address of the cell range (or a named range). */
        public unoidl.com.sun.star.table.CellRangeAddress createCellRangeAddress(
            unoidl.com.sun.star.sheet.XSpreadsheet xSheet, String aRange)
        {
            unoidl.com.sun.star.sheet.XCellRangeAddressable xAddr =
                (unoidl.com.sun.star.sheet.XCellRangeAddressable)
                xSheet.getCellRangeByName(aRange);
            return xAddr.getRangeAddress();
        }

        // ________________________________________________________________
        // Methods to convert cell addresses and range addresses to strings.

        /** Returns the text address of the cell.
            @param nColumn  The column index.
            @param nRow  The row index.
            @return  A string containing the cell address. */
        public String getCellAddressString(int nColumn, int nRow)
        {
            String aStr = "";
            if (nColumn > 25)
                aStr += (char)('A' + nColumn / 26 - 1);
            aStr += (char)('A' + nColumn % 26);
            aStr += (nRow + 1);
            return aStr;
        }

        /** Returns the text address of the cell range.
            @param aCellRange  The cell range address.
            @return  A string containing the cell range address. */
        public String getCellRangeAddressString(unoidl.com.sun.star.table.CellRangeAddress aCellRange)
        {
            return
                getCellAddressString(aCellRange.StartColumn, aCellRange.StartRow)
                + ":"
                + getCellAddressString(aCellRange.EndColumn, aCellRange.EndRow);
        }

        /// <summary>
        /// Returns the text address of the cell range
        /// </summary>
        /// <param name="xCellRange">The XSheetCellRange interface of the cell range.</param>
        /// <param name="bWithSheet">true = Include sheet name.</param>
        /// <returns>A string containing the cell range address list.</returns>
        public String getCellRangeAddressString(unoidl.com.sun.star.sheet.XSheetCellRange xCellRange, bool bWithSheet)
        {
            String aStr = "";
            if (bWithSheet)
            {
                unoidl.com.sun.star.sheet.XSpreadsheet xSheet =
                    xCellRange.getSpreadsheet();
                unoidl.com.sun.star.container.XNamed xNamed =
                    (unoidl.com.sun.star.container.XNamed)xSheet;
                aStr += xNamed.getName() + ".";
            }
            unoidl.com.sun.star.sheet.XCellRangeAddressable xAddr =
                (unoidl.com.sun.star.sheet.XCellRangeAddressable)xCellRange;
            aStr += getCellRangeAddressString(xAddr.getRangeAddress());
            return aStr;
        }

        /// <summary>
        /// Returns a list of addresses of all cell ranges contained in thecollection.
        /// </summary>
        /// <param name="unoidl.com.sun.star.container.XIndexAccess">The XIndexAccess interface of the collection.</param>
        /// <returns>A string containing the cell range address list.</returns>
        public String getCellRangeListString(unoidl.com.sun.star.container.XIndexAccess xRangesIA)
        {
            String aStr = "";
            int nCount = xRangesIA.getCount();
            for (int nIndex = 0; nIndex < nCount; ++nIndex)
            {
                if (nIndex > 0)
                    aStr += " ";
                uno.Any aRangeObj = xRangesIA.getByIndex(nIndex);
                unoidl.com.sun.star.sheet.XSheetCellRange xCellRange =
                    (unoidl.com.sun.star.sheet.XSheetCellRange)aRangeObj.Value;
                aStr += getCellRangeAddressString(xCellRange, false);
            }
            return aStr;
        }

        /// <summary>
        /// 连接到OpenOffice.org来获取服务管理器的引用
        /// </summary>
        /// <returns>服务管理器，用来初始化文档</returns>
        private XMultiServiceFactory connect()
        {

            m_xContext = uno.util.Bootstrap.bootstrap();

            return (XMultiServiceFactory)m_xContext.getServiceManager();
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// 初始化一个文档
        /// </summary>
        /// <returns>该文档的XSpreadsheetDocument接口</returns>
        private unoidl.com.sun.star.sheet.XSpreadsheetDocument initDocument(string filepath,bool IsHidden)
        {
            XComponentLoader aLoader = (XComponentLoader)
                mxMSFactory.createInstance("com.sun.star.frame.Desktop");
            XComponent xComponent = null;
            unoidl.com.sun.star.beans.PropertyValue[] myArgs=new unoidl.com.sun.star.beans.PropertyValue[1];
            myArgs[0] = new unoidl.com.sun.star.beans.PropertyValue();
            myArgs[0].Name = "Hidden";
            myArgs[0].Value =new uno.Any(IsHidden) ;
            if (filepath == "")
            {
                xComponent = aLoader.loadComponentFromURL(
                "private:factory/scalc", "_blank", 0,
                myArgs);
            }
            else
            {
                xComponent = aLoader.loadComponentFromURL(
                this.PathConverter(filepath), "_blank", 0,
                myArgs);

            }

            return (unoidl.com.sun.star.sheet.XSpreadsheetDocument)xComponent;
        }

        /** Returns the XCellSeries interface of a cell range.
        @param xSheet  The spreadsheet containing the cell range.
        @param aRange  The address of the cell range.
        @return  The XCellSeries interface. */
        public unoidl.com.sun.star.sheet.XCellSeries getCellSeries(
                unoidl.com.sun.star.sheet.XSpreadsheet xSheet, String aRange)
        {
            return (unoidl.com.sun.star.sheet.XCellSeries)
                xSheet.getCellRangeByName(aRange);
        }

        /// <summary>
        /// getCellByPosition
        /// </summary>
        /// <param name="xSheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public unoidl.com.sun.star.table.XCell getCellByPosition(unoidl.com.sun.star.sheet.XSpreadsheet xSheet,int col,int row)
        {
            return xSheet.getCellByPosition(col, row);
        }

        /** Inserts a cell range address into a cell range container and prints
            a message.
            @param xContainer  unoidl.com.sun.star.sheet.XSheetCellRangeContainer
                               interface of the container.
            @param nSheet  Index of sheet of the range.
            @param nStartCol  Index of first column of the range.
            @param nStartRow  Index of first row of the range.
            @param nEndCol  Index of last column of the range.
            @param nEndRow  Index of last row of the range.
            @param bMerge  Determines whether the new range should be merged
                           with the existing ranges.
        */
        public void insertRange(
                unoidl.com.sun.star.sheet.XSheetCellRangeContainer xContainer,
                int nSheet, int nStartCol, int nStartRow, int nEndCol, int nEndRow,
                bool bMerge)
        {
            unoidl.com.sun.star.table.CellRangeAddress aAddress =
                new unoidl.com.sun.star.table.CellRangeAddress();
            aAddress.Sheet = (short)nSheet;
            aAddress.StartColumn = nStartCol;
            aAddress.StartRow = nStartRow;
            aAddress.EndColumn = nEndCol;
            aAddress.EndRow = nEndRow;
            xContainer.addRangeAddress(aAddress, bMerge);
            //Console.WriteLine(
            //    "Inserting " + getCellRangeAddressString(aAddress)
            //    + " " + (bMerge ? "   with" : "without") + " merge,"
            //    + " resulting list: " + xContainer.getRangeAddressesAsString());
        }

        /** Store a document, using the StarDraw 5.0 Filter */
        public void storeDocComponent(String storeUrl)
        {

            unoidl.com.sun.star.frame.XStorable xStorable = mxDocument as XStorable;
            xStorable.storeAsURL(PathConverter(storeUrl), new unoidl.com.sun.star.beans.PropertyValue[] { });
        }

        public void closeDocCompant()
        {
            unoidl.com.sun.star.util.XCloseable xCloseable = mxDocument as unoidl.com.sun.star.util.XCloseable;
            xCloseable.close(true);
        }
        /// <summary>
        /// OOo路径转换
        /// </summary>
        /// <param name="file">windows 原始路径</param>
        /// <returns>OOo的路径格式</returns>
        public string PathConverter(string file)
        {
            file = file.Replace(@"\", "/");
            return "file:///" + file;
        }

        public void InsertGraphic(string imgpath)
        {
            Object dispatchHelper = mxMSFactory.createInstance("com.sun.star.frame.DispatchHelper");
            XDispatchHelper dispatcher = dispatchHelper as XDispatchHelper;
            XModel xModel = mxDocument as XModel;
            XFrame xFrame = xModel.getCurrentController().getFrame();
            XDispatchProvider xDispatchProvider = xFrame as XDispatchProvider;
            unoidl.com.sun.star.beans.PropertyValue[] MyProp = new unoidl.com.sun.star.beans.PropertyValue[4];
            MyProp[0] = new unoidl.com.sun.star.beans.PropertyValue(); 
            MyProp[0].Name = "FileName";
            MyProp[0].Value =new uno.Any(PathConverter(imgpath));
            MyProp[1] = new unoidl.com.sun.star.beans.PropertyValue(); 
            MyProp[1].Name = "FilterName";
            MyProp[1].Value = new uno.Any("<Tous les formats>");
            MyProp[2] = new unoidl.com.sun.star.beans.PropertyValue(); 
            MyProp[2].Name = "AsLink";
            MyProp[2].Value = new uno.Any(false);
            MyProp[3] = new unoidl.com.sun.star.beans.PropertyValue(); 
            MyProp[3].Name = "Style";
            MyProp[3].Value =new uno.Any("Image");
            dispatcher.executeDispatch(xDispatchProvider, ".uno:InsertGraphic", "", 0, MyProp);
        }

        /** Activates a scenario.
        @param xSheet           The XSpreadsheet interface of the spreadsheet.
        @param aScenarioName    The name of the scenario. */
        public void showScenario(
                unoidl.com.sun.star.sheet.XSpreadsheet xSheet,
                String aScenarioName)
        {
            // get the scenario set
            unoidl.com.sun.star.sheet.XScenariosSupplier xScenSupp =
                (unoidl.com.sun.star.sheet.XScenariosSupplier)xSheet;
            unoidl.com.sun.star.sheet.XScenarios xScenarios =
                xScenSupp.getScenarios();

            // get the scenario and activate it
            uno.Any aScenarioObj = xScenarios.getByName(aScenarioName);
            unoidl.com.sun.star.sheet.XScenario xScenario =
                (unoidl.com.sun.star.sheet.XScenario)aScenarioObj.Value;
            xScenario.apply();

        }

        /** Inserts a scenario containing one cell range into a sheet and
        applies the value array.
        @param xSheet           The XSpreadsheet interface of the spreadsheet.
        @param aRange           The range address for the scenario.
        @param aValueArray      The array of cell contents.
        @param aScenarioName    The name of the new scenario.
        @param aScenarioComment The user comment for the scenario. */
        public  void insertScenario(
                unoidl.com.sun.star.sheet.XSpreadsheet xSheet,
                String aRange,
                uno.Any[][] aValueArray,
                String aScenarioName,
                String aScenarioComment)
        {
            // get the cell range with the given address
            unoidl.com.sun.star.table.XCellRange xCellRange =
                xSheet.getCellRangeByName(aRange);

            // create the range address sequence
            unoidl.com.sun.star.sheet.XCellRangeAddressable xAddr =
                (unoidl.com.sun.star.sheet.XCellRangeAddressable)xCellRange;
            unoidl.com.sun.star.table.CellRangeAddress[] aRangesSeq =
                new unoidl.com.sun.star.table.CellRangeAddress[1];
            aRangesSeq[0] = xAddr.getRangeAddress();

            // create the scenario
            unoidl.com.sun.star.sheet.XScenariosSupplier xScenSupp =
                (unoidl.com.sun.star.sheet.XScenariosSupplier)xSheet;
            unoidl.com.sun.star.sheet.XScenarios xScenarios =
                xScenSupp.getScenarios();
            xScenarios.addNewByName(aScenarioName, aRangesSeq, aScenarioComment);

            // insert the values into the range
            unoidl.com.sun.star.sheet.XCellRangeData xData =
                (unoidl.com.sun.star.sheet.XCellRangeData)xCellRange;
            xData.setDataArray(aValueArray);
        }
    }
}
