using LpgConsumptionCostCalculator.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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

        public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName, string sortField, QueryOptions queryOptions)
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

        private static object BuildSortIcon(bool isCurrentSortField, QueryOptions queryOptions)
        {
            string sortIcon = "fa-sort";
            if (isCurrentSortField)
            {
                sortIcon += (queryOptions.SortOrder == SortOrder.descending) ? "-asc" : "-desc";
            }
            return string.Format("<i class=\"{0} {1}\"></i>", "fa", sortIcon);
        }

        public static MvcHtmlString BuildNextPreviousLinks(this HtmlHelper htmlHelper, QueryOptions queryOptions, string actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format(
                "<div class=\"pagination\">" +
                "   <nav aria-label=\"...\">" +
                "       <ul class=\"pagination pagination-sm\">" +
                "           <li class=\"page-item {0}\">{1}</li>" +
                "           <li class=\"page-item {2}\">{3}</li>" +
                "       </ul>" +
                "   </nav>" +
                "</div>"
                ,
                IsPreviousDisabled(queryOptions),
                BuildPreviousLink(urlHelper, queryOptions, actionName),
                IsNextDisabled(queryOptions),
                BuildNextLink(urlHelper, queryOptions, actionName)
                ));
        }
        private static string IsPreviousDisabled(QueryOptions queryOptions)
        {
            return (queryOptions.CurrentPage == 1) ? "disabled" : string.Empty;
        }
        private static string IsNextDisabled(QueryOptions queryOptions)
        {
            return (queryOptions.CurrentPage == queryOptions.TotalPages) ? "disabled" : string.Empty;
        }
        private static string BuildPreviousLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
        {
            return string.Format(
                "<a class=\"text-dark page-link\" href=\"{0}\">Prev</a>",
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage - 1,
                    PageSize = queryOptions.PageSize
                }));
        }
        private static string BuildNextLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
        {
            return string.Format(
                "<a class=\"text-dark page-link\" href=\"{0}\">Next</a>",
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage + 1,
                    PageSize = queryOptions.PageSize
                }));
        }

        public static MvcHtmlString BuildPaginationDetails(this HtmlHelper htmlHelper, QueryOptions queryOptions, string actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format(
                "<div class=\"navbar\">" +
                "   <span class=\"page-list\">" +
                "       <span class=\"btn-group\">" +
                "           <button class=\"btn btn-secondary btn-sm dropdown-toggle\" data-toggle=\"dropdown\">" +
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
                "           </div>" +
                "       </span>" +
                "   </span>" +
                "</div>"
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
                queryOptions.PageSize
            ));
        }

    }
}