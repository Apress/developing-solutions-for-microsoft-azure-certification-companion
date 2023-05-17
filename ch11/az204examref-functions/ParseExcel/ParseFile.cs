using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using az204examref_functions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace AZ204ExamRefSimpleFunctionApp.ParseExcel
{
    public class ParseFile
    {
        public static List<SampleDataItem> ParseDataFile(MemoryStream fileStreamInMemory)
        {
            fileStreamInMemory.Position = 0;

            var sampleData = new List<SampleDataItem>();

            string text;
            int index;
            string columnRef;
            bool success;

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fileStreamInMemory, false))
            {
                var workbookPart = spreadsheetDocument.WorkbookPart;
                var worksheetPart = workbookPart.WorksheetParts.First();
                var sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                var sst = sstpart.SharedStringTable;
                var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                foreach (Row r in sheetData.Elements<Row>())
                {
                    if (r.RowIndex == 1)
                    {
                        //skip columns row
                        continue;
                    }

                    var sdi = new SampleDataItem();

                    foreach (Cell c in r.Elements<Cell>())
                    {
                        columnRef = c?.CellReference?.Value?.Substring(0, 1) ?? "";

                        string dataString = c?.CellValue?.Text ?? string.Empty;

                        if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
                        {
                            var isIndex = int.TryParse(c?.CellValue?.Text, out int ssid);
                            if (!isIndex) continue;
                            dataString = sst.ChildElements[ssid].InnerText;
                        }

                        switch (columnRef)
                        {
                            case "A":
                                //ID
                                var isId = int.TryParse(dataString, out int id);
                                if (isId && id > 0)
                                {
                                    sdi.Id = id;
                                }
                                break;
                            case "B":
                                //Title
                                sdi.Title = dataString;
                                break;
                            case "C":
                                //Rating
                                sdi.Rating = dataString;
                                break;
                            case "D":
                                //Type
                                sdi.Type = dataString;
                                break;
                        }
                    }

                    if (sdi.Id > 0)
                    {
                        sampleData.Add(sdi);
                    }
                }
            }

            return sampleData;
        }
    }
}
