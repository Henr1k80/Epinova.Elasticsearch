using Alloy.Models.Pages;
using Epinova.ElasticSearch.Core.EPiServer;
using EPiServer.Core;
using System.Reflection;

namespace Alloy.Models.ViewModels
{
    public class PageViewModel<T> : IPageViewModel<T> where T : SitePageData
    {
        public PageViewModel(T currentPage)
        {
            CurrentPage = currentPage;
        }

        public T CurrentPage { get; private set; }
        public LayoutModel Layout { get; set; }
        public IContent Section { get; set; }

        public ContentSearchResult<StandardPage> SearchResults { get; set; }
        public string Query => SearchResults?
            .GetType()?
            .GetProperty("Query", BindingFlags.NonPublic | BindingFlags.Instance)?
            .GetValue(SearchResults)?
            .ToString();
        public SearchModel SearchModel { get; set; }
    }

    public static class PageViewModel
    {
        /// <summary>
        /// Returns a PageViewModel of type <typeparam name="T"/>.
        /// </summary>
        /// <remarks>
        /// Convenience method for creating PageViewModels without having to specify the type as methods can use type inference while constructors cannot.
        /// </remarks>
        public static PageViewModel<T> Create<T>(T page) where T : SitePageData
        {
            return new PageViewModel<T>(page);
        }
    }
}
