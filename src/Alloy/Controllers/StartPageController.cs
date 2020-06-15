using Alloy.Models.Pages;
using Alloy.Models.ViewModels;
using Epinova.ElasticSearch.Core.Contracts;
using Epinova.ElasticSearch.Core.EPiServer.Extensions;
using System;
using System.Web.Mvc;

namespace Alloy.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        private readonly IElasticSearchService _elasticSearchService;

        public StartPageController(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public ActionResult Index(StartPage currentPage, SearchModel searchModel)
        {
            var model = PageViewModel.Create<StartPage>(currentPage);
            model.SearchModel = searchModel;

            if (!String.IsNullOrEmpty(searchModel.SearchText))
            {
                var query = _elasticSearchService
                    .Search<StandardPage>(searchModel.SearchText);

                if (searchModel.OptionTrack)
                    query = query.Track();
                if (searchModel.OptionBestBet)
                    query = query.UseBestBets();
                if (searchModel.Boost == nameof(StandardPage.Name))
                    query = query.Boost(x => x.Name, 10);
                if (searchModel.Boost == nameof(StandardPage.MainBody))
                    query = query.Boost(x => x.MainBody, 10);
                if (searchModel.FilterKey != null && searchModel.FilterValue != null)
                    query = query.Filter(searchModel.FilterKey, searchModel.FilterValue);

                var results = query
                    .FacetsFor(m => m.PageTypeName)
                    .GetContentResults(
                        requirePageTemplate: false,
                        ignoreFilters: false,
                        providerNames: Array.Empty<string>(),
                        enableHighlighting: searchModel.OptionHighlighting,
                        enableDidYouMean: searchModel.OptionDidYouMean);

                model.SearchResults = results;
            }

            searchModel.SearchText = searchModel.SearchText ?? "alloy";

            return View(model);
        }
    }
}
