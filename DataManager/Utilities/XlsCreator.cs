using ClosedXML.Excel;

namespace Nipper.DataManager.Utilities;

public static class XlsCreator
{
    private static readonly SettingsManager settingsManager = new();
    public static void CreateXlsxFile(Dictionary<string, string> nipResultDict)
    {
        using var workbook = new XLWorkbook();
        {
            var worksheet = workbook.AddWorksheet("Nipy");
            int currentLine = 1;
            foreach(var record in nipResultDict)
            {
                worksheet.Cell($"A{currentLine}").Value = record.Key;
                worksheet.Cell($"B{currentLine}").Value = record.Value;
                currentLine++;
            }
            workbook.SaveAs(settingsManager.GetXlsSavePath() + "\\" + DateTime.UtcNow.ToString().Replace(":", "-") + ".xlsx");
        };
    }
}
