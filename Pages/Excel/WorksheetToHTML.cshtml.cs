#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;

namespace EJ2CoreSampleBrowser.Pages.Excel
{
    public class WorksheetToHTML : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public WorksheetToHTML(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult OnPost(string button, string saveOption)
        {
            string basePath = _hostingEnvironment.WebRootPath;

            if (button == null)
                return null;
            else if (button == "Input Template")
            {
                Stream ms = new FileStream(basePath + @"/XlsIO/NorthwindTemplate.xlsx", FileMode.Open, FileAccess.Read);
                return File(ms, "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Template.xlsx");
            }
            else
            {
                // The instantiation process consists of two steps.
                // Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();

                // Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;

                //Read file to memory stream
                Stream file = new FileStream(basePath + @"/XlsIO/NorthwindTemplate.xlsx", FileMode.Open, FileAccess.Read);

                // An existing workbook is opened.
                IWorkbook workbook = application.Workbooks.Open(file);

                // The first worksheet object in the worksheets collection is accessed.
                IWorksheet worksheet = workbook.Worksheets[0];

                //Create a memory stream to store the generated image.
                Stream stream = new MemoryStream();
                FileStreamResult fileStreamResult = null;

                try
                {
                    //Convert Workbook to HTML file.
                    if (saveOption == "Workbook")
                    {
                        workbook.SaveAsHtml(stream);
                    }
                    //Convert Worksheet to HTML file.
                    else
                    {
                        worksheet.SaveAsHtml(stream);
                    }
                    stream.Position = 0;
                    fileStreamResult = new FileStreamResult(stream, "text/html");
                    return fileStreamResult;
                }
                catch (Exception)
                { }
                finally
                {
                    workbook.Close();
                    excelEngine.Dispose();
                }
            }
            return null;
        }
    }
}
