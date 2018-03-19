using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;

namespace Eindwerk2018.PdfStuff
{
    public class PdfUtil
    {
        public Paragraph AddParagragh(string ParagraphText)
        {
            Paragraph p = new Paragraph();
            p.SpacingBefore = 10f;
            p.FirstLineIndent = 10f;
            p.SpacingAfter = 15f;

            iTextSharp.text.Font f = new iTextSharp.text.Font();
            p.Font.SetFamily("Courier");
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.Font.Size = 13f;
            p.Font.SetColor(0, 0, 0);
            p.Add(ParagraphText);
            return p;
        }
        public Chunk AddParagraphHeader(string headingText)
        {
            Chunk ch = new Chunk(headingText);
            ch.Font.Size = 16f;
            ch.Font.SetStyle("bold");
            ch.Font.SetColor(0, 0, 0);
            return ch;
        }
    }
}