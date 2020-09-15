using LpgConsumptionCostCalculator.Data.Enums;
using LpgConsumptionCostCalculator.Data.QueryOptions;
using LpgConsumptionCostCalculator.Web.Content.Resources;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            return new HtmlString(JsonConvert.SerializeObject(model, settings));
        }

        public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName, string sortField, TableQueryOptions queryOptions)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var isCurrentSortField = queryOptions.SortField == sortField;

            return new MvcHtmlString(string.Format("<a href=\"{0}\" class = \"text-dark font-weight-bold\">{1} {2}</a>", 
                urlHelper.Action(actionName, 
                new
                {
                    SortField = sortField,
                    SortOrder = (isCurrentSortField && queryOptions.SortOrder == SortOrder.ascending ? SortOrder.descending : SortOrder.ascending)
                }),
                fieldName,
                BuildSortIcon(isCurrentSortField, queryOptions)));
        }

        private static object BuildSortIcon(bool isCurrentSortField, TableQueryOptions queryOptions)
        {
            string sortIcon = "fa-sort";
            if (isCurrentSortField)
            {
                sortIcon += (queryOptions.SortOrder == SortOrder.descending) ? "-asc" : "-desc";
            }
            return string.Format("<i class=\"{0} {1}\"></i>", "fa", sortIcon);
        }

        public static MvcHtmlString BuildNextPreviousLinks(this HtmlHelper htmlHelper, TableQueryOptions queryOptions, string actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format(
                "<div class=\"navbar\">" +
                "   <nav aria-label=\"...\">" +
                "       <ul class=\"pagination pagination-sm\">" +
                "           <li class=\"page-item {0}\">{1}</li>" +
                "               {4}" +
                "           <li class=\"page-item {2}\">{3}</li>" +
                "       </ul>" +
                "   </nav>" +
                "</div>"
                ,
                IsPreviousDisabled(queryOptions),
                BuildPreviousLink(urlHelper, queryOptions, actionName),
                IsNextDisabled(queryOptions),
                BuildNextLink(urlHelper, queryOptions, actionName),
                BuildPageNumbers(urlHelper, queryOptions, actionName)
                ));
        }
        private static string IsPreviousDisabled(TableQueryOptions queryOptions)
        {
            return (queryOptions.CurrentPage == 1) ? "disabled" : string.Empty;
        }
        private static string IsNextDisabled(TableQueryOptions queryOptions)
        {
            return (queryOptions.CurrentPage == queryOptions.TotalPages) ? "disabled" : string.Empty;
        }
        private static string BuildPreviousLink(UrlHelper urlHelper, TableQueryOptions queryOptions, string actionName)
        {
            return string.Format(
                "<a class=\"page-link\" href=\"{0}\">{1}</a>",
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage - 1,
                    PageSize = queryOptions.PageSize
                }), RGlobal.Previous);
        }
        private static string BuildNextLink(UrlHelper urlHelper, TableQueryOptions queryOptions, string actionName)
        {
            return string.Format(
                "<a class=\"page-link\" href=\"{0}\">{1}</a>",
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage + 1,
                    PageSize = queryOptions.PageSize
                }), RGlobal.Next);
        }

        private static string BuildPageNumbers(UrlHelper urlHelper, TableQueryOptions queryOptions, string actionName)
        {
            string paginationPagesButtons = string.Empty;
            for (int i = 1; i <= queryOptions.TotalPages; i++)
            {
                paginationPagesButtons = paginationPagesButtons 
                    + string.Format(
                "<li class=\"page-item {2}\">" +
                "   <a class=\"page-link\" href=\"{0}\">{1}</a>" +
                "</li>",
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = i,
                    PageSize = queryOptions.PageSize
                }),
                i,
                (i == queryOptions.CurrentPage) ? "active" : string.Empty);
            }
            return paginationPagesButtons;                
        }

        public static MvcHtmlString BuildPaginationDetails(this HtmlHelper htmlHelper, TableQueryOptions queryOptions, string actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format(
                "       <div>{5} " +
                "           <button class=\"btn btn-light btn-sm dropdown-toggle\" data-toggle=\"dropdown\">" +
                "                <span class=\"page-size\">" +
                "                   {4}" + 
                "                </span>" +
                "                <span class=\"caret\">" +
                "                </span>" +
                "           </button>" +
                "           <div class=\"dropdown-menu\">" +
                "               <a class=\"dropdown-item\" href=\"{0}\">10</a>" +
                "               <a class=\"dropdown-item\" href=\"{1}\">25</a>" +
                "               <a class=\"dropdown-item\" href=\"{2}\">50</a>" +
                "               <a class=\"dropdown-item\" href=\"{3}\">100</a>" +
                "           </div> {6}" +
                "       </div>"
                ,
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage,
                    PageSize = 10
                }),
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage,
                    PageSize = 25
                }),
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage,
                    PageSize = 50
                }),
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage,
                    PageSize = 100
                }),
                queryOptions.PageSize,
                RGlobal.Show,
                RGlobal.Entries
            ));
        }
        public static MvcHtmlString BuildChartTimeSelection(this HtmlHelper htmlHelper, ChartQueryOptions chartQueryOptions, string actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format(
                "<div class=\"dropdown\">" +
                "   <a class=\"dropdown-toggle d-sm-inline-block btn btn-sm btn-secondary shadow-sm\" " +
                "   style=\"width: 170px\" href=\"#\" role=\"button\" id=\"dropdownMenuLink\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">" +
                "       <i class=\"fa fa-clock-o fa-sm text-white-50\"></i> {10}" +
                "   </a>" + 
                "   <div class=\"dropdown-menu dropdown-menu-right shadow animated--fade-in\" aria-labelledby=\"dropdownMenuLink\">" +
                "       <a class=\"dropdown-item\" href=\"{0}\">{5}</a>" +
                "       <a class=\"dropdown-item\" href=\"{1}\">{6}</a>" +
                "       <a class=\"dropdown-item\" href=\"{2}\">{7}</a>" +
                "       <a class=\"dropdown-item\" href=\"{3}\">{8}</a>" +
                "       <a class=\"dropdown-item\" href=\"{4}\">{9}</a>" +
                "   </div>" +
                "</div>",
                urlHelper.Action(actionName, new
                {
                    startingTimeRange = DateTime.Now.AddMonths(-1)
                }),
                urlHelper.Action(actionName, new
                {
                    startingTimeRange = DateTime.Now.AddMonths(-3)
                }),
                urlHelper.Action(actionName, new
                {
                    startingTimeRange = DateTime.Now.AddMonths(-6)
                }),
                urlHelper.Action(actionName, new
                {
                    startingTimeRange = DateTime.Now.AddMonths(-12)
                }),
                urlHelper.Action(actionName, new
                {
                    startingTimeRange = DateTime.MinValue
                }),
                RGlobal.LastMonth,
                RGlobal.Last3Months,
                RGlobal.Last6Months,
                RGlobal.LastYear,
                RGlobal.All,
                RGlobal.SelectTimeRange
            ));
        }

    }
}