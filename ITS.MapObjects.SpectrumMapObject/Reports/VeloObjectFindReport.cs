using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Common.RtfWriter;
using ITS.Core.Spectrum.Domain;
using VerticalAlignment = ITS.Common.RtfWriter.VerticalAlignment;

namespace ITS.MapObjects.SpectrumMapObject.Reports
{
    public static class VeloObjectFindReport
    {
        /// <summary>
        /// Размер шрифта заголовка.
        /// </summary>
        private static float titleFontSize = 12;

        /// <summary>
        /// Отчет из плагина
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="someobjs"></param>
        public static void ReportMake(string fileName, IList<VeloObject> someobjs,
            float sectionFontSize = 8.5f, PaperOrientation paperOrientation = PaperOrientation.Portrait)
        {
            var doc = new RtfDocument(PaperSize.A4, paperOrientation, Lcid.Russian);
            RTFWorkingProvider.DocumentMargins(doc,
                paperOrientation == PaperOrientation.Landscape ? 2.5f : 1.5f,
                paperOrientation == PaperOrientation.Landscape ? 1.5f : 2.5f,
                1.5f, 1.5f);

            CreateDocumentTitle(doc);
            CreateDataTable(doc, someobjs, sectionFontSize);
            doc.Save(fileName);
        }

        private static void CreateDocumentTitle(RtfDocument document)
        {
            RTFWorkingProvider.AddTextToDocument(document, titleFontSize, HorizontalAlignment.Center, "Сводная ведомость");
            RTFWorkingProvider.AddEmptyLine(document, titleFontSize, HorizontalAlignment.Left);
            RTFWorkingProvider.AddEmptyLine(document, titleFontSize, HorizontalAlignment.Left);
        }

        #region Jтчет для сводной ведомости плагина

        private static void CreateDataTable(RtfDocument document, IList<VeloObject> roadrepairs, float fontSize = 8.5f)
        {
            var table = document.AddTable(roadrepairs.Count + 2, 10); // 3-заголовок
            table.SetInnerBorder(BorderStyle.Single, 0.5f);
            table.SetOuterBorder(BorderStyle.Single, 1.5f);

            for (var i = 0; i < table.RowCount; i++)
            {
                for (var j = 0; j < table.ColCount; j++)
                {
                    table.Cell(i, j).HorizontalAlignment = HorizontalAlignment.Center;
                    table.Cell(i, j).VerticalAlignment = VerticalAlignment.Middle;
                    table.Cell(i, j).DefaultCharFormat.Font = document.CreateFont("Times New Roman");
                    table.Cell(i, j).DefaultCharFormat.AnsiFont = document.CreateFont("Times New Roman");
                    table.Cell(i, j).DefaultCharFormat.FontSize = fontSize;
                }
                table.SetRowKeepInSamePage(i, true);
            }

            table.HorizontalAlignment = HorizontalAlignment.Distributed;

            for (var col = 0; col < table.ColCount; col++)
            {
                table.SetOuterBorder(col, 0, 1, table.RowCount, BorderStyle.Single, 1.5f);
            }

            old_CreateTableTitle(table);
            old_FillDataTable(table, roadrepairs);
        }

        private static void old_CreateTableTitle(RtfTable table)
        {
            for (var j = 0; j < table.ColCount; j++)
            {
                for (var i = 0; i < 2; i++)
                {
                    table.Cell(i, j).DefaultCharFormat.FontStyle.AddStyle(FontStyleFlag.Bold);
                }
            }
            RTFWorkingProvider.AddTextToCell(0, 0, table, "№ п/п");
            RTFWorkingProvider.AddTextToCell(0, 1, table, "Идентификатор");
            RTFWorkingProvider.AddTextToCell(0, 2, table, "Тип велопарковки");
            RTFWorkingProvider.AddTextToCell(0, 3, table, "Вид велопарковки");
            RTFWorkingProvider.AddTextToCell(0, 4, table, "Статус");
            RTFWorkingProvider.AddTextToCell(0, 5, table, "Длина, м");
            RTFWorkingProvider.AddTextToCell(0, 6, table, "Ширина, м");
            RTFWorkingProvider.AddTextToCell(0, 7, table, "Количество секций");
            RTFWorkingProvider.AddTextToCell(0, 8, table, "Дата установки");
            RTFWorkingProvider.AddTextToCell(0, 9, table, "Дата обслуживания");


            for (var i = 0; i < table.ColCount; i++)
            {
                RTFWorkingProvider.AddTextToCell(1, i, table, (i + 1).ToString());
            }

        }

        private static void old_FillDataTable(RtfTable table, IList<VeloObject> someobjs)
        {
            var i = 2;
            foreach (var so in someobjs)
            {
                RTFWorkingProvider.AddTextToCell(i, 0, table, (i - 1).ToString());
                RTFWorkingProvider.AddTextToCell(i, 1, table, so.ID.ToString());
                RTFWorkingProvider.AddTextToCell(i, 2, table, so.TypeAsString);
                RTFWorkingProvider.AddTextToCell(i, 3, table, so.ViewAsString);
                RTFWorkingProvider.AddTextToCell(i, 4, table, so.StatusAsString);
                RTFWorkingProvider.AddTextToCell(i, 5, table, so.VeloLength.ToString());
                RTFWorkingProvider.AddTextToCell(i, 6, table, so.VeloWidth.ToString());
                RTFWorkingProvider.AddTextToCell(i, 7, table, so.VeloSection.ToString());
                RTFWorkingProvider.AddTextToCell(i, 8, table, so.DataCheck?.ToShortDateString() ?? "");
                RTFWorkingProvider.AddTextToCell(i, 9, table, so.DataSet?.ToShortDateString() ?? "");
               
                i++;
            }
        }

        #endregion
    }
}
