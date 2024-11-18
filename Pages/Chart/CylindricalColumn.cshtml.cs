#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EJ2CoreSampleBrowser.Pages.Chart
{
    public class CylindricalColumnModel : PageModel
    {
        public List<CylindricalColumnChartData> ChartPoints { get; set; }
        public void OnGet()
        {
            ChartPoints = new List<CylindricalColumnChartData>
            {
                new CylindricalColumnChartData { Country = "China", GoldMedal = 26, MappingName = "China" },
                new CylindricalColumnChartData { Country = "Australia", GoldMedal = 8, MappingName = "Australia" },
                new CylindricalColumnChartData { Country = "Germany", GoldMedal = 17, MappingName = "Germany" },
                new CylindricalColumnChartData { Country = "Spain", GoldMedal = 7, MappingName = "Spain" },
                new CylindricalColumnChartData { Country = "Japan", GoldMedal = 12, MappingName = "Japan" },
                new CylindricalColumnChartData { Country = "USA", GoldMedal = 46, MappingName = "United States" }
            };
        }
    }
    public class CylindricalColumnChartData
    {
        public string Country;
        public double GoldMedal;
        public string MappingName;
    }
}
