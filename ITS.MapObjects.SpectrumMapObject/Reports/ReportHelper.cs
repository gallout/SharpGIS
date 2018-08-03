using System;
using System.Collections.Generic;
using System.Diagnostics;
using ITS.Common.RtfWriter;
using HorizontalAlignment = ITS.Common.RtfWriter.HorizontalAlignment;

namespace ITS.MapObjects.SpectrumMapObject.Reports
{
    public class ReportHelper
    {
        #region Методы для работы с отчетом

        /// <summary>
        /// Сохраняет отчет в файл RTF.
        /// </summary>
        /// <param name="pathToRtfFile">Путь к файлу.</param>
        /// <param name="rtfDocument"></param>
        public static void Save(string pathToRtfFile, RtfDocument rtfDocument)
        {
            if (rtfDocument == null) throw new Exception("Отчет для сохранения не был сгенерирован.");
            rtfDocument.Save(pathToRtfFile);
        }

        /// <summary>
        /// Открывает файл во внешнем редакторе RTF.
        /// </summary>
        /// <param name="pathToRtfFile">Путь к файлу.</param>
        public static void Open(string pathToRtfFile)
        {
            var p = new Process { StartInfo = { FileName = pathToRtfFile } };
            p.Start();
        }
        #endregion
    }

    public class RTFWorkingProvider
    {
        public static void AddTextToCell(int row, int col, RtfTable table, string text)
        {
            var paragraph = table.Cell(row, col).AddParagraph();
            paragraph.Text = text;
        }

        public static void AddTextToDocument(RtfDocument rtfDocument, float fontSize, HorizontalAlignment alignment,
            string text)
        {
            var paragraph = rtfDocument.AddParagraph();
            paragraph.HorizontalAlignment = alignment;
            paragraph.AddCharFormat().FontSize = fontSize;
            paragraph.Text = text;
        }

        public static void AddEmptyLine(RtfDocument rtfDocument, float fontSize, HorizontalAlignment alignment)
        {
            var paragraph = rtfDocument.AddParagraph();
            paragraph.HorizontalAlignment = alignment;
            paragraph.AddCharFormat().FontSize = fontSize;
        }

        public static void AligmentColumn(RtfTable table, int column, int row1, int row2, HorizontalAlignment alignment, int fontSize = 12)
        {
            try
            {
                for (var i = row1; i <= row2; i++)
                {
                    table.Cell(i, column).HorizontalAlignment = alignment;
                    table.Cell(i, column).DefaultCharFormat.FontSize = fontSize;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DocumentMargins(RtfDocument doc, float top, float left, float bottom, float right)
        {
            // Границы документа:
            const float smToPt = 28.34646f;
            doc.Margins[Direction.Top] = top * smToPt;
            doc.Margins[Direction.Left] = left * smToPt;
            doc.Margins[Direction.Bottom] = bottom * smToPt;
            doc.Margins[Direction.Right] = right * smToPt;
        }
    }
}
