#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Syncfusion.XlsIO;

namespace EJ2CoreSampleBrowser.Pages.Excel
{
    public class EncryptAndDecrypt : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EncryptAndDecrypt(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult OnPost(string button, string saveOption, string password, IFormFile file)
        {
            string basePath = _hostingEnvironment.WebRootPath;
            if (button == null)
                return null;

            if (password == null)
                password = "PASSWORD";
            if (button == "Encrypt Document")
            {

                //New instance of XlsIO is created.[Equivalent to launching Microsoft Excel with no workbooks open].
                //The instantiation process consists of two steps.

                //Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();

                //Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                Stream inputStream = null;
                if (file != null)
                    inputStream = file.OpenReadStream();
                else
                    inputStream = new FileStream(basePath + @"/XlsIO/Encrypt.xlsx", FileMode.Open, FileAccess.Read);
                inputStream.Position = 0;

                // Opening the Existing Worksheet from a Workbook.
                IWorkbook workbook = application.Workbooks.Open(inputStream);

                //Encrypt the workbook with password.
                workbook.PasswordToOpen = password;
                string ContentType = string.Empty;
                string fileName = string.Empty;
                if (saveOption == "Xls")
                {
                    workbook.Version = ExcelVersion.Excel97to2003;
                    ContentType = "Application/vnd.ms-excel";
                    fileName = "EncryptedDocument.xls";
                }
                else
                {
                    workbook.Version = ExcelVersion.Xlsx;
                    ContentType = "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName = "EncryptedDocument.xlsx";
                }

                MemoryStream ms = new MemoryStream();
                workbook.SaveAs(ms);
                ms.Position = 0;

                // Close the workbook
                workbook.Close();
                excelEngine.Dispose();

                return File(ms, ContentType, fileName);
            }
            else
            {
                //New instance of XlsIO is created.[Equivalent to launching Microsoft Excel with no workbooks open].
                //The instantiation process consists of two steps.

                //Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();

                //Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;
                if (file != null && file.FileName.EndsWith(".xls"))
                    application.DefaultVersion = ExcelVersion.Excel97to2003;

                Stream inputStream = null;
                if (file != null)
                    inputStream = file.OpenReadStream();
                else
                    inputStream = new FileStream(basePath + @"/XlsIO/EncryptedWorkbook.xlsx", FileMode.Open, FileAccess.Read);
                inputStream.Position = 0;

                // Opening the encrypted Workbook.
                IWorkbook workbook = application.Workbooks.Open(inputStream, ExcelParseOptions.Default, true, password);

                //Modify the decrypted document.
                workbook.Worksheets[0].Range["B2"].Text = "Demo for workbook decryption with Essential XlsIO";
                workbook.Worksheets[0].Range["B5"].Text = "This document is decrypted using password '" + password + "'.";

                workbook.PasswordToOpen = "";
                string ContentType = string.Empty;
                string fileName = string.Empty;
                if (saveOption == "Xls")
                {
                    workbook.Version = ExcelVersion.Excel97to2003;
                    ContentType = "Application/vnd.ms-excel";
                    fileName = "DecryptedDocument.xls";
                }
                else
                {
                    workbook.Version = ExcelVersion.Xlsx;
                    ContentType = "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName = "DecryptedDocument.xlsx";
                }

                MemoryStream ms = new MemoryStream();
                workbook.SaveAs(ms);
                ms.Position = 0;

                // Close the workbook
                workbook.Close();
                excelEngine.Dispose();

                return File(ms, ContentType, fileName);
            }

        }

    }
}
