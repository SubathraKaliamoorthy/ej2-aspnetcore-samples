#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.AspNetCore.Mvc.RazorPages;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace EJ2CoreSampleBrowser.Pages.Word;
public partial class WordToTXT : PageModel
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    public WordToTXT(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }
    public void OnGet()
    {

    }
    public string Message { get; set; }
    public ActionResult OnPost(string button)
    {
        if (button == null)
            return null;
            //return View();
        Stream stream = GetWordDocumentForTXTConversion();

        if (stream != null)
        {
            try
            {
                string output = (Request.Form.Files != null && Request.Form.Files.Count != 0) ? Path.GetFileNameWithoutExtension(Request.Form.Files[0].FileName) : "WordtoTXT";

                //Opens an existing document from file system through constructor of WordDocument class
                using (WordDocument document = new WordDocument(stream, FormatType.Automatic))
                {
                    FormatType type = FormatType.Txt;
                    string filename = output + ".txt";
                    string contenttype = "text/plain";

                    //Saves the Word document to MemoryStream
                    MemoryStream outputStream = new MemoryStream();
                    document.Save(outputStream, type);
                    document.Close();
                    outputStream.Position = 0;
                    return File(outputStream, contenttype, filename);
                }
            }
            catch
            {
                Message = string.Format("The input document could not be processed completely, Could you please email the document to support@syncfusion.com for troubleshooting.");
                return null;
            }
        }
        return null;
        //return View();
    }

    /// <summary>
    /// Gets Word document for TXT Conversion
    /// </summary>
    /// <returns></returns>
    private Stream GetWordDocumentForTXTConversion()
    {
        if (Request.Form.Files != null && Request.Form.Files.Count != 0)
        {
            // Gets the extension from file.
            string extension = Path.GetExtension(Request.Form.Files[0].FileName).ToLower();

            // Compares extension with supported extensions.
            if (extension == ".doc" || extension == ".docx")
            {
                MemoryStream stream = new MemoryStream();
                Request.Form.Files[0].CopyTo(stream);
                return stream;
            }
            else
            {
                Message = string.Format("Please choose Word format document to convert to TXT");
                return null;
            }
        }
        else
        {
            //Opens an existing document from stream through constructor of `WordDocument` class
            FileStream fileStreamPath = new FileStream(_hostingEnvironment.WebRootPath + @"/Word/WordToTXT.docx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return fileStreamPath;
        }
    }
}