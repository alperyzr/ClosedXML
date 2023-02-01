using ClosedXML.Excel;
using ClosedXML.Web.Helper.Attributes;
using System.Data;

namespace ClosedXML.Web.Extensions
{
    public static class ConvertToExcel
    {
        public static byte[] DataTableToExcel(DataTable data)
        {
            using (var workbook = new XLWorkbook())
            {

                IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet1");


                List<string> excelHeader = UpdateAllExcelHeader(data);


                for (int i = 1; i <= data.Columns.Count; i++)
                {
                    worksheet.Cell(1, i).Value = excelHeader.Count > 0 ? excelHeader[i - 1] : data.Columns[i - 1].ColumnName;
                    worksheet.Cell(1, i).Style.Font.Bold = true;
                    worksheet.Cell(1, i).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1);
                }

                for (int r = 0; r < data.Rows.Count; r++)
                {
                    for (int c = 1; c <= data.Columns.Count; c++)
                    {
                        var cellValue = data.Rows[r][c - 1].ToString();
                        worksheet.Cell(r + 2, c).Value = "\u200C" + data.Rows[r].Field<string>(c - 1);
                    }
                }
                //Excel exportta colonları kolon uzunluğu kadar otomatik genişletir
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
        }

        public static List<string> UpdateAllExcelHeader(DataTable table)
        {
            try
            {
                Dictionary<string, string> excelHeaders = new();

                List<string> columnNamelist = table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();

                Type typeName = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                    .First(x => x.Name == table.TableName);

                foreach (var prop in typeName.GetProperties())
                {
                    excelHeaders.Add(prop.Name, ((ExcelHeaderAttribute[])prop.GetCustomAttributes(typeof(ExcelHeaderAttribute), false)).AsEnumerable().Select(c => c.Name).FirstOrDefault() ?? prop.Name);
                }

                for (int i = 0; i < columnNamelist.Count; i++)
                {
                    var itemToChange = excelHeaders.FirstOrDefault(d => d.Key == columnNamelist[i]);
                    columnNamelist[i] = itemToChange.Value;

                }
                return columnNamelist;

            }
            catch (Exception ex)
            {

                return new List<string>();

            }

        }
    }
}
