#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EJ2CoreSampleBrowser.Models
{
    public class DateTimeFormats
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public List<DateTimeFormats> GetDateTimeFormatsWithId()
        {
            return new List<DateTimeFormats>
        {
            new DateTimeFormats { Id = "format1", Text = "dd-MMM-yy hh:mm a" },
            new DateTimeFormats { Id = "format2", Text = "yyyy-MM-dd HH:mm" }
        };
        }

        public List<string> GetDateTimeInputFormats()
        {
            return new List<string>
        {
            "MM.dd.yyyy hh:mm a", "yyyy-MM-dd HH:mm", "dd MMM yyyy HH:mm",
            "MMM/dd/yyyy hh:mm tt", "yyyy/MM/dd hh:mm tt", "dd-MM-yyyy HH:mm",
            "MM/dd/yyyy hh:mm tt", "yyyy.MM.dd HH:mm"
        };
        }
    }

}