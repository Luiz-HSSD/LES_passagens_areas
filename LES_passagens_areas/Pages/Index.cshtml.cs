using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LES_passagens_areas.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet(string cod)
        {
            if(!string.IsNullOrEmpty(cod))
            HttpContext.Session.SetObjectAsJson("login", null);
           
        }
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        [HttpPost]
        public IActionResult OnPost(int id)
        {
            FileStreamResult rr;
            //using ()
            {
                var stream = new MemoryStream();
                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 30f);
                iTextSharp.text.pdf.PdfWriter ww = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, stream);
                doc.Open();
                doc.Add(new iTextSharp.text.Chunk("john vitor meu amigo"));
                doc.Add(new iTextSharp.text.Chunk(""));
                iTextSharp.text.Table tb = new iTextSharp.text.Table(3);
                tb.AddCell(new iTextSharp.text.Cell("asdf"));
                tb.AddCell(new iTextSharp.text.Cell("asdf"));
                tb.AddCell(new iTextSharp.text.Cell("asdf"));

                tb.AddCell(new iTextSharp.text.Cell("asdf"));
                tb.AddCell(new iTextSharp.text.Cell("asdf"));
                tb.AddCell(new iTextSharp.text.Cell("asdf"));
                doc.Add(tb);
                doc.Close();
                return File(stream.ToArray(), "application/pdf");
            }
           
        }
    }
}
