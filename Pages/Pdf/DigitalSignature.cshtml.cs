#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.AspNetCore.Mvc.RazorPages;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;

namespace EJ2CoreSampleBrowser.Pages.Pdf;

public class DigitalSignature : PageModel
{
    public void OnGet()
    {
        
    }

    public string lab;
    private readonly IWebHostEnvironment _hostingEnvironment;
    public DigitalSignature(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public ActionResult OnPost(string Browser, string password, string Reason, string Location,
        string Contact, string RadioButtonList2, string NewPDF, string submit, string Cryptographic,
        string Digest_Algorithm, IFormFile pdfdocument, IFormFile certificate)
    {
        string basePath = _hostingEnvironment.WebRootPath;
        string dataPath = string.Empty;
        dataPath = basePath + @"/PDF/";

        if (submit == "Sign PDF")
        {
            if (pdfdocument != null && pdfdocument.Length > 0 && certificate != null && certificate.Length > 0 &&
                certificate.FileName.Contains(".pfx") && password != null && Location != null && Reason != null &&
                Contact != null)
            {
                PdfLoadedDocument ldoc = new PdfLoadedDocument(pdfdocument.OpenReadStream());
                PdfCertificate pdfCert = new PdfCertificate(certificate.OpenReadStream(), password);
                FileStream jpgFile = new FileStream(dataPath + "logo.png", FileMode.Open, FileAccess.Read,
                    FileShare.ReadWrite);
                PdfBitmap bmp = new PdfBitmap(jpgFile);
                PdfPageBase page = ldoc.Pages[0];
                PdfSignature signature = new PdfSignature(ldoc, page, pdfCert, "Signature");
                signature.Bounds = new RectangleF(new PointF(20, 20), new SizeF(240, 70));
                signature.ContactInfo = Contact;
                signature.LocationInfo = Location;
                signature.Reason = Reason;
                SetCryptographicStandard(Cryptographic, signature);
                SetDigest_Algorithm(Digest_Algorithm, signature);
                PdfGraphics graphics = signature.Appearance.Normal.Graphics;
                graphics.DrawImage(bmp, 0, 0);
                MemoryStream stream = new MemoryStream();
                ldoc.Save(stream);
                stream.Position = 0;
                ldoc.Close(true);

                //Download the PDF document in the browser.
                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
                fileStreamResult.FileDownloadName = "SignedPDF.pdf";
                return fileStreamResult;
            }
            else
            {
                lab = "NOTE: Fill all fields and then create PDF";
                // return View();
                return null;
            }
        }
        else
        {
            //Read the PFX file
            FileStream pfxFile =
                new FileStream(dataPath + "PDF.pfx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();
            PdfSolidBrush brush = new PdfSolidBrush(Color.Black);
            PdfPen pen = new PdfPen(brush, 0.2f);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Courier, 12, PdfFontStyle.Regular);
            PdfCertificate pdfCert = new PdfCertificate(pfxFile, "password123");
            PdfSignature signature = new PdfSignature(page, pdfCert, "Signature");
            FileStream jpgFile =
                new FileStream(dataPath + "logo.png", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            PdfBitmap bmp = new PdfBitmap(jpgFile);
            signature.Bounds = new RectangleF(new PointF(20, 20), new SizeF(240, 110));
            signature.ContactInfo = "johndoe@owned.us";
            signature.LocationInfo = "Honolulu, Hawaii";
            signature.Reason = "I am author of this document.";
            SetCryptographicStandard(Cryptographic, signature);
            SetDigest_Algorithm(Digest_Algorithm, signature);
            if (RadioButtonList2 == "Standard")
                signature.Certificated = false;
            else
                signature.Certificated = true;
            PdfGraphics graphics = signature.Appearance.Normal.Graphics;

            string validto = " Valid To: " + signature.Certificate.ValidTo.ToString();
            string validfrom = " Valid From: " + signature.Certificate.ValidFrom.ToString();

            graphics.DrawImage(bmp, 0, 0);

            doc.Pages[0].Graphics.DrawString(validfrom, font, pen, brush, 20, 90);
            doc.Pages[0].Graphics.DrawString(validto, font, pen, brush, 20, 110);

            // Save the pdf document to the Stream.
            MemoryStream stream = new MemoryStream();

            doc.Save(stream);

            //Close document
            doc.Close(true);

            stream.Position = 0;

            // Download the PDF document in the browser.
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            fileStreamResult.FileDownloadName = "SignedPDF.pdf";
            return fileStreamResult;
        }
    }
    private void SetCryptographicStandard(string cryptographic, PdfSignature signature)
    {
        if (cryptographic != null)
        {
            if (cryptographic == "CAdES")
                signature.Settings.CryptographicStandard = CryptographicStandard.CADES;
            else
                signature.Settings.CryptographicStandard = CryptographicStandard.CMS;
        }
            
    }
    private void SetDigest_Algorithm(string digest_Algorithm, PdfSignature signature)
    {
        if (digest_Algorithm != null)
        {
            switch (digest_Algorithm)
            {
                case "SHA1":
                    signature.Settings.DigestAlgorithm = DigestAlgorithm.SHA1;
                    break;
                case "SHA384":
                    signature.Settings.DigestAlgorithm = DigestAlgorithm.SHA384;
                    break;
                case "SHA512":
                    signature.Settings.DigestAlgorithm = DigestAlgorithm.SHA512;
                    break;
                case "RIPEMD160":
                    signature.Settings.DigestAlgorithm = DigestAlgorithm.RIPEMD160;
                    break;
                default:
                    signature.Settings.DigestAlgorithm = DigestAlgorithm.SHA256;
                    break;
            }
        }
    }
}