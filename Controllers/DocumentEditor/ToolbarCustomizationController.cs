#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EJ2CoreSampleBrowser.Controllers
{
    public partial class DocumentEditorController : Controller
    {
        public IActionResult ToolbarCustomization()
        {
            List<object> exportItems = new List<object>();
            exportItems.Add(new { text = "Syncfusion Document Text (*.sfdt)", id = "sfdt" });
            exportItems.Add(new { text = "Word Document (*.docx)", id = "word" });
            exportItems.Add(new { text = "Word Template (*.dotx)", id = "dotx" });
            exportItems.Add(new { text = "Plain Text (*.txt)", id = "txt" });
            ViewBag.ExportItems = exportItems;
            ViewBag.toolbarItems = new string[] {"New", "Open", "Undo", "Redo","Comments", "Image","Table", "Hyperlink","Bookmark", "TableOfContents", "Header",  "Footer",
        "PageSetup", "PageNumber","Break","Find","LocalClipboard", "RestrictEditing" };
            return View();
        }
    }
}