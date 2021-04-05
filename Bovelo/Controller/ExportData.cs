using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
namespace Bovelo
{
    public static class ExportData
    {
        public static void CreateInvoice(string client, List<string> column, List<List<String>> Data)
        {
            string date = DateTime.Now.ToString();
            date = date.Replace('/', '_');
            date = date.Replace(' ', '_');
            date = date.Replace(':', '_');
            string path = Environment.CurrentDirectory;
            PdfWriter writer = new PdfWriter(@"../../facture/" + client + "_" + date + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Header
            Paragraph header = new Paragraph("Bovelo")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            // New line
            Paragraph newline = new Paragraph(new Text("\n"));
            document.Add(newline);
            document.Add(header);
            // Add sub-header
            Paragraph subheader = new Paragraph(client)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(15);
            document.Add(subheader);
            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);
            // Add paragraph1
            Paragraph paragraph1 = new Paragraph("Récapitulatif de commande " + date.Substring(0, 8));
            paragraph1.SetTextAlignment(TextAlignment.CENTER);
            document.Add(paragraph1);
            document.Add(ls);
            document.Add(newline);

            // Table
            Table table = new Table(column.Count(), false);

            foreach (var elem in column)
            {
                Cell cell = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.GRAY)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetWidth(100)
                    .Add(new Paragraph(elem));
                table.AddCell(cell);
            }
            int price = 0;
            foreach (var item in Data)
            {
                foreach (var value in item)
                {
                    Cell cell = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(value));
                    table.AddCell(cell);
                    if (value == item[item.Count() - 1])
                    {
                        price += Int32.Parse(value);
                    }
                }
            }
            document.Add(table);
            //Total
            document.Add(newline);
            document.Add(ls);
            document.Add(newline);
            Paragraph paragraph2 = new Paragraph("Total : " + price);
            paragraph2.SetTextAlignment(TextAlignment.RIGHT);
            document.Add(paragraph2);
            // Page numbers
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(i + "/" + n)),
                   559, 806, i, TextAlignment.RIGHT,
                   VerticalAlignment.TOP, 0);
            }

            // Close document
            document.Close();
        }
        //put DataBaseBackup method here
    }
}
