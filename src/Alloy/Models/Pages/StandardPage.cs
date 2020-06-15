using Epinova.ElasticSearch.Core.Attributes;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Alloy.Models.Pages
{
    /// <summary>
    /// Used for the pages mainly consisting of manually created content such as text, images, and blocks
    /// </summary>
    [SiteContentType(GUID = "9CCC8A41-5C8C-4BE0-8E73-520FF3DE8267")]
    [SiteImageUrl(Global.StaticGraphicsFolderPath + "page-type-thumbnail-standard.png")]
    public class StandardPage : SitePageData
    {
        [DidYouMeanSource]
        public override string Name { get => base.Name; set => base.Name = value; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 310)]
        [CultureSpecific]
        [DidYouMeanSource]
        public virtual XhtmlString MainBody { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        public virtual ContentArea MainContentArea { get; set; }
    }
}
