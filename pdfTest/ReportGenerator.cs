using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc;
using MigraDoc.DocumentObjectModel.Shapes;

namespace pdfTest
{
    static class ReportGenerator
    {
        private static Document doc;
        private static Section secMain;
        private static List<Table> tables = new List<Table>();

        private static ParagraphFormat parFormatHeader = new ParagraphFormat();
        private static ParagraphFormat tbFormatCaption = new ParagraphFormat();
        private static ParagraphFormat tbFormatDescription = new ParagraphFormat();
        private static ParagraphFormat tbFormatColNames = new ParagraphFormat();
        private static ParagraphFormat tbFormatText = new ParagraphFormat();

        private static string reportName;

        private static void Formats()
        {
            //header format
            parFormatHeader.Font.Name = "Arial";
            parFormatHeader.Font.Bold = true;
            parFormatHeader.Font.Color = Colors.Navy;
            parFormatHeader.Font.Size = "0.85cm";

            //Table formats
            //Caption
            tbFormatCaption.Font.Name = "Tahoma";
            tbFormatCaption.Font.Bold= true;
            tbFormatCaption.Font.Color = Colors.White;
            tbFormatCaption.Font.Size = "0.5cm";
            //Description
            tbFormatDescription.Font.Name = "Tahoma";
            tbFormatDescription.Font.Bold = true;
            tbFormatDescription.Font.Italic = true;
            tbFormatDescription.Font.Color = Colors.White;
            tbFormatDescription.Font.Size = "0.35cm";
            //ColNames
            tbFormatColNames.Font.Name = "Tahoma";
            tbFormatColNames.Font.Bold = true;
            tbFormatColNames.Font.Color = Colors.White;
            tbFormatColNames.Font.Size = "0.35cm";
            //Text
            tbFormatText.Font.Name = "Tahoma";
            tbFormatText.Font.Bold = false;
            tbFormatText.Font.Color = Colors.Black;
            tbFormatText.Font.Size = "7";


        }
        public static void NewReport(string reportname)
        {
            reportName = reportname;
            Formats();
            doc = new Document();
            tables.Clear();
            secMain = doc.AddSection();
            secMain.PageSetup.TopMargin = "4.5cm";
            secMain.PageSetup.BottomMargin = "2cm";
            secMain.PageSetup.LeftMargin = "2.5cm";
            secMain.PageSetup.RightMargin = "2cm";

            Table tbHeader = secMain.Headers.Primary.AddTable();
            tbHeader.AddColumn(Unit.FromMillimeter(100));
            tbHeader.AddColumn(Unit.FromMillimeter(65));
            Row rowHeader = tbHeader.AddRow();
            rowHeader.VerticalAlignment = VerticalAlignment.Center;

            Cell cellTextHeader = rowHeader.Cells[0];
            Cell cellImgHeader = rowHeader.Cells[1];

            //text header            
            Paragraph parHeaderText = cellTextHeader.AddParagraph();
            parHeaderText.AddText("Autorizacija korisnika");
            parHeaderText.Format = parFormatHeader.Clone();

            //image/logo header
            System.Drawing.Image tempImg = Properties.Resources.logo;
            tempImg.Save(@"logo.jpg");
            Image headerPic = cellImgHeader.AddImage(@"logo.jpg");
            //System.IO.File.Delete(@"logo.jpg");
            headerPic.Height = "2cm";
            headerPic.LockAspectRatio = true;

            rowHeader.Borders.Bottom.Width = 1;
            rowHeader.Borders.Bottom.Color= Colors.Black;
            rowHeader.Borders.Bottom.Style = BorderStyle.Single;
            rowHeader.Borders.Bottom.Visible = true;


            //Table tbHeader = secMain.AddTable();
            //tbHeader.AddColumn(Unit.FromMillimeter(100));
            //tbHeader.AddColumn(Unit.FromMillimeter(75));
            //Row rowHeader = tbHeader.AddRow();
            //rowHeader.TopPadding = "-1cm";
            //rowHeader.VerticalAlignment = VerticalAlignment.Center;
            //tbHeader.SetEdge(0, 0, 2, 1, Edge.Bottom, MigraDoc.DocumentObjectModel.BorderStyle.Single, 1.5, Colors.Black);

            //Cell cellTextHeader = rowHeader.Cells[0];
            //Cell cellImgHeader = rowHeader.Cells[1];



            ////text header            
            //Paragraph parHeaderText = cellTextHeader.AddParagraph();
            //parHeaderText.AddText("Autorizacija korisnika");
            //parHeaderText.Format = parFormatHeader.Clone();

            ////image/logo header
            //Image headerPic = cellImgHeader.AddImage("logo.jpg");
            //headerPic.Height = "2.5cm";
            //headerPic.LockAspectRatio = true;



            //footer
            //secMain.Footers.Primary.Format.Borders.Top.Visible = true;
            //secMain.Footers.Primary.Format.Borders.Top.Width = 0;

            Table tbFooter = secMain.Footers.Primary.AddTable();
            //tbFooter.Format.Shading.Color = Colors.AliceBlue;
            //tbFooter.Borders.Width = 0;
            tbFooter.Borders.Top.Visible = true;
            tbFooter.Borders.Top.Color = Colors.Black;
            tbFooter.Borders.Top.Width = 1;

            Column colDateTime = tbFooter.AddColumn("75mm");
            Column colPgNum = tbFooter.AddColumn("15mm");
            Column colDocName = tbFooter.AddColumn("75mm");

            Row rowFooter = tbFooter.AddRow();
            rowFooter.Format.Font.Size = 9;

            Paragraph parDateTime = rowFooter[colDateTime.Index].AddParagraph();
            parDateTime.AddText("Vrijeme: ");
            parDateTime.AddDateField("HH:mm dd.MM.yyyy");
            parDateTime.Format.Alignment = ParagraphAlignment.Left;

            Paragraph parPgNum = rowFooter[colPgNum.Index].AddParagraph();
            parPgNum.AddPageField();
            parPgNum.AddText("/");
            parPgNum.AddNumPagesField();
            parPgNum.Format.Alignment = ParagraphAlignment.Center;

            Paragraph parDocName = rowFooter[colDocName.Index].AddParagraph();
            parDocName.AddText(reportName);
            parDocName.Format.Alignment = ParagraphAlignment.Right;

            //Paragraph parFooter = secMain.Footers.Primary.AddParagraph();
            //parFooter.Format.Font.Size = 9;
            //parFooter.Format.Alignment = ParagraphAlignment.Left;


            //parFooter.AddText("Vrijeme: ");
            //parFooter.AddDateField("HH:mm dd.MM.yyyy");
            //parFooter.Format.AddTabStop("8cm");
            //parFooter.AddTab();
            //parFooter.AddPageField();
            //parFooter.AddText("/");
            //parFooter.AddNumPagesField();
            ////parFooter.AddTab();
            ////parFooter.AddTab();
            ////parFooter.AddTab();
            ////parFooter.AddText(reportName);

            //TextFrame tf = secMain.Footers.Primary.AddTextFrame();
            //tf.Width = "5cm";
            //tf.Height = "0.5cm";
            //tf.MarginTop = "0cm";
            //tf.Top = "0cm";
            //tf.Left = "12cm";
            //tf.AddParagraph("text").Format.Alignment = ParagraphAlignment.Right;
            //tf.FillFormat.Color = Colors.AliceBlue;
            ////parFooter.AddText("01");



        }

        public static void CreateNewTable(string grpDomain, string tableName, string tableDescription)
        {
            Table tb = new Table();
            tb.Tag = grpDomain + "\\" + tableName;

            tb.KeepTogether = true;
            tb.Borders.Width = 1.5;
            tb.Borders.Color = Colors.White;
            tb.Borders.Style = BorderStyle.Single;
            tb.Borders.Visible = true;

            Column colNum = tb.AddColumn(Unit.FromMillimeter(5));
            Column colDispName = tb.AddColumn(Unit.FromMillimeter(32));
            Column colUsrName = tb.AddColumn(Unit.FromMillimeter(25));
            Column colEmail = tb.AddColumn(Unit.FromMillimeter(55));
            //Column colCountry = tb.AddColumn(Unit.FromMillimeter(22));
            Column colDep = tb.AddColumn(Unit.FromMillimeter(31));
            Column colUsrEnabled = tb.AddColumn(Unit.FromMillimeter(17));

            //caption and desc
            Row rowCaption = tb.AddRow();
            rowCaption.Shading.Color = Color.Parse("#1160AD");
            Cell cellCaption = rowCaption[0];
            cellCaption.MergeRight = tb.Columns.Count-1;

            Paragraph parCaption = cellCaption.AddParagraph(tableName + "(" + grpDomain + ")");
            Paragraph parDesc = cellCaption.AddParagraph(tableDescription);
            parCaption.Format = tbFormatCaption.Clone();
            parDesc.Format = tbFormatDescription.Clone();
            

            //colNames
            Row rowColNames = tb.AddRow();
            rowColNames.Format = tbFormatColNames.Clone();
            rowColNames.Format.Alignment = ParagraphAlignment.Center;
            rowColNames.VerticalAlignment = VerticalAlignment.Center;
            rowColNames.Height = "0.6cm";
            rowColNames.Shading.Color = Color.Parse("#199f86");
            rowColNames.HeadingFormat = true;

            Cell cellNum = rowColNames[colNum.Index];
            Cell cellDispName = rowColNames[colDispName.Index];
            Cell cellUserName = rowColNames[colUsrName.Index];
            Cell cellEmail = rowColNames[colEmail.Index];
            //Cell cellCountry = rowColNames[colCountry.Index];
            Cell cellDep = rowColNames[colDep.Index];
            Cell cellUsrEnabled = rowColNames[colUsrEnabled.Index];

            //cellNum.MergeRight = 1;
            Paragraph parColNum = cellNum.AddParagraph("#");
            Paragraph parDispName = cellDispName.AddParagraph("Naziv korisnika");
            Paragraph parUsername = cellUserName.AddParagraph("ID korisnika");
            Paragraph parEmail = cellEmail.AddParagraph("Email adresa");
            //Paragraph parCountry = cellCountry.AddParagraph("Država");
            Paragraph parDep = cellDep.AddParagraph("Odjel");
            Paragraph parUsrEnabled = cellUsrEnabled.AddParagraph("Status");

            tables.Add(tb);
        }

        public static void AddMember(string usrDomain, string tableName, string dispName,
            string usrName, string usrEmail, string usrDep,string usrEnabled)
        {
            if (usrDomain == null) usrDomain = "?";
            if (dispName == null) dispName = "";
            if (usrName == null) usrName = "";
            if (usrEmail == null) usrEmail = "";
            //if (usrCountry == null) usrCountry = "";
            if (usrDep == null) usrDep = "";
            if (usrEnabled == null) usrEnabled = "?";
            Table tb = tables.Where(x => x.Tag.ToString() == usrDomain+"\\"+tableName).First();
            Row rowMbr = tb.AddRow();
            rowMbr.Borders.Top.Width = 0;
            rowMbr.Borders.Bottom.Width = 0;
            //rowMbr.Format = tbFormatText.Clone();
            rowMbr.Format.Font.Size = "7";
            if (tb.Rows.Count % 2 != 0) rowMbr.Shading.Color = Colors.LightGray;
            else rowMbr.Shading.Color = Colors.White;
            rowMbr.Height = "0.44cm";
            rowMbr.VerticalAlignment = VerticalAlignment.Center;

            Cell cellNum = rowMbr[0];
            Cell cellDispName = rowMbr[1];
            Cell cellUserName = rowMbr[2];
            Cell cellEmail = rowMbr[3];
            //Cell cellCountry = rowMbr[4];
            Cell cellDep = rowMbr[4];
            Cell cellUsrEnabled = rowMbr[5];

            cellNum.AddParagraph((tb.Rows.Count-2).ToString());
            

            cellNum.Format.Alignment = ParagraphAlignment.Center;
            //cellNum.Format.RightIndent = 8;
            cellNum.Format.Font.Bold = false;

            cellDispName.AddParagraph(dispName);
            cellDispName.Format.Alignment = ParagraphAlignment.Center;
            //cellDispName.Format.LeftIndent = 5;

            cellUserName.AddParagraph(usrName);
            cellUserName.Format.Alignment = ParagraphAlignment.Center;


            int MAXCHARCOUNT_EMAIL = 35;
            if (usrEmail.Length>0 && usrEmail.Length > MAXCHARCOUNT_EMAIL)
            {
                string[] mailDivAt = usrEmail.Split('@');
                string[] mailNameDivDot = mailDivAt[0].Split('.');

                List<string> lines = new List<string>();
                foreach (string s in mailNameDivDot)
                {
                    if (lines.Count == 0)
                    {
                        lines.Add(s);
                        continue;
                    }
                    if (lines.Last().Length + s.Length > MAXCHARCOUNT_EMAIL)
                        lines.Add("." + s);
                    else
                        lines[lines.Count - 1] += "." + s;
                }

                if (lines.Last().Length + mailDivAt[1].Length > MAXCHARCOUNT_EMAIL)
                    lines.Add("@" + mailDivAt[1]);
                else
                    lines[lines.Count - 1] += "@" + mailDivAt[1];
                usrEmail = string.Join("\n", lines.ToArray());
            }

            cellEmail.AddParagraph(usrEmail);
            cellEmail.Format.Alignment = ParagraphAlignment.Center;

            //cellCountry.AddParagraph(usrCountry);
            //cellCountry.Format.Alignment = ParagraphAlignment.Center;

            cellDep.AddParagraph(usrDep);
            cellDep.Format.Alignment = ParagraphAlignment.Center;

            cellUsrEnabled.AddParagraph(usrEnabled);
            cellUsrEnabled.Format.Alignment = ParagraphAlignment.Center;

            

        }
        private static void GenerateTables()
        {
            foreach (Table tb in tables)
            {
                secMain.Add(tb);
                secMain.AddParagraph().Format.SpaceAfter = "1cm";
            }
        }

        public static void AddSignature (string authorName)
        {
            Paragraph parSign = secMain.AddParagraph();
            parSign.Format.SpaceBefore = "5cm";
            parSign.AddText("Izradio: ");
            for (int i = authorName.Length; i < 30; i++) authorName += " ";
            parSign.AddFormattedText(authorName, TextFormat.Underline | TextFormat.Bold);
            //parSign.AddText("Izradio: ______________________________");
            parSign.AddLineBreak();
            parSign.AddLineBreak();
            parSign.AddText("Potpis: ______________________________");
            parSign.AddLineBreak();
            parSign.AddLineBreak();
            parSign.AddDateField("dd.MM.yyyy");
            parSign.Format.Alignment = ParagraphAlignment.Right;
            parSign.Format.RightIndent = 10;
            parSign.Format.Font.Size = 12;

        }
        public static void GenerateReport()
        {
            GenerateReport(null);
        }
            public static void GenerateReport(string reportAuthor)
        {
            GenerateTables();
            if (reportAuthor != null && reportAuthor.Length > 0)
                AddSignature(reportAuthor);

            MigraDoc.Rendering.PdfDocumentRenderer pdfRenderer = new MigraDoc.Rendering.PdfDocumentRenderer(false);

            pdfRenderer.Document = doc;
            pdfRenderer.RenderDocument();

            string filename = reportName + ".pdf";
            pdfRenderer.PdfDocument.Save(filename);
        }


        public static void main()
        {
            //Document document = new Document();
            //Section section = document.AddSection();
            //Table tb = section.AddTable();
            //tb.AddColumn(Unit.FromMillimeter(100));
            //tb.AddColumn(Unit.FromMillimeter(70));
            //tb.AddRow();
            //tb.Rows[0].VerticalAlignment = VerticalAlignment.Center;
            //tb.SetEdge(0, 0, 2, 1, Edge.Bottom, MigraDoc.DocumentObjectModel.BorderStyle.Single, 1.5, Colors.Black);
            //Image headerPic = tb.Rows[0].Cells[1].AddImage("logo.jpg");
            //headerPic.Height = "2.5cm";
            //headerPic.LockAspectRatio = true;

            //Paragraph headerTxt = tb.Rows[0].Cells[0].AddParagraph();
            //headerTxt.AddText("User Group Membership Report");
            //headerTxt.Format.Font.Color = Colors.Navy;
            //headerTxt.Format.Font.Bold = true;
            //headerTxt.Format.Font.Size = "0.85cm";
            ////tb.Rows[0].Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            //section.AddParagraph("Hello World");



            //Paragraph par = section.AddParagraph("oj ej");
            //par.Format.Font.Color = Color.FromCmyk(100, 100, 0, 0);




            //MigraDoc.Rendering.PdfDocumentRenderer pdfRenderer = new MigraDoc.Rendering.PdfDocumentRenderer(false);

            //pdfRenderer.Document = document;
            //pdfRenderer.RenderDocument();

            //string filename = "doc.pdf";
            //pdfRenderer.PdfDocument.Save(filename);

        }
    }
}
