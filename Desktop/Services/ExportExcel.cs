using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using ClosedXML.Excel;

namespace Desktop.Services
{
    public record ProductBatchesToExport(ProductBatch ProductBatch);

    public static class ExportExcel
    {
        public static void ExportBatches(string Path)
        {
            var book = new XLWorkbook();
            var sheet = book.AddWorksheet("Name");

            sheet.Cell(1, 1).Value = "";

            sheet.Columns().AdjustToContents();

            book.SaveAs(Path);
        }
    }
}