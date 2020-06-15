using Alloy.Models.Pages;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Alloy.Models.ViewModels
{
    public class SearchModel
    {
        public string SearchText { get; set; }
        public string Boost { get; set; }
        public bool OptionTrack { get; set; }
        public bool OptionBestBet { get; set; }
        public bool OptionDidYouMean { get; set; }
        public bool OptionHighlighting { get; set; }
        public string FilterKey { get; set; }
        public string FilterValue { get; set; }

        public IEnumerable<SelectListItem> BoostFields { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "" },
            new SelectListItem { Text = nameof(StandardPage.Name) },
            new SelectListItem { Text = nameof(StandardPage.MainBody) }
        };
    }
}
