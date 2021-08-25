using iTextSharp.text;
using System.IO;

namespace PDFToImage
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var imagePath in args)
                {
                    if (!string.IsNullOrWhiteSpace(imagePath))
                    {
                        ConvertImageToPdf(imagePath);
                    }
                }
            }
        }

        public static void ConvertImageToPdf(string srcFilename)
        {

            using var ms = new System.IO.MemoryStream();
            var image = iTextSharp.text.Image.GetInstance(srcFilename);
            Rectangle pagesize = new Rectangle(image.ScaledWidth, image.ScaledHeight);
            var document = new iTextSharp.text.Document(pagesize);
            iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms);
            image.SetAbsolutePosition(0, 0);
            document.Open();
            document.Add(image);
            document.Close();

            File.WriteAllBytes(Path.ChangeExtension(srcFilename, ".pdf"), ms.ToArray());

        }

    }
}
