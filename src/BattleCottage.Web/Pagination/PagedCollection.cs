using BattleCottage.Web.Pagination;
using System.Text.RegularExpressions;

namespace BattleCottage.Core.Pagination
{
    public class PagedCollection<T> : IPagedCollection<T>
    {
        private readonly string _pagePattern = @"page=\d+";
        private readonly string _pageSizePattern = @"pageSize=\d+";
        private HttpRequest HttpRequest { get; set; }

        public ICollection<T> Values { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public PageResult<T> Result { get; set; }

        public PagedCollection(ICollection<T>? values, int? page, int? pageSize, HttpRequest request)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            HttpRequest = request;

            PageSize =
                pageSize != null && pageSize > 0 && pageSize <= PageSettings.MaxPageSize
                    ? pageSize.Value
                    : PageSettings.MaxPageSize;

            TotalPages = (int)Math.Ceiling(values.Count / (double)PageSize);

            if (page != null && page <= TotalPages && page > 0)
            {
                Page = page.Value;
            }
            else
            {
                throw new ArgumentException("Invalid page.");
            }

            Values = values.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

            Result = new PageResult<T>();

            if (Page + 1 <= TotalPages)
            {
                Result.Next = GetNextUrl();
            }

            if (Page - 1 > 0)
            {
                Result.Previous = GetPreviousUrl();
            }

            Result.Results = Values;
        }

        private void UpdateQueryString(ref string queryString, string key, int value, string pattern)
        {
            if (queryString.Contains($"{key}="))
            {
                queryString = Regex.Replace(queryString, pattern, $"{key}={value}");
            }
            else
            {
                if (!queryString.EndsWith("?") && !queryString.EndsWith("&"))
                    queryString += "&";
                queryString += $"{key}={value}";
            }
        }

        public string BuildUrl(int pageDelta)
        {
            if (HttpRequest == null)
            {
                throw new ArgumentNullException(nameof(HttpRequest));
            }

            string host = HttpRequest.Host.Value;
            string? path = HttpRequest.Path.Value;
            string scheme = HttpRequest.Scheme;
            string? queryString = HttpRequest.QueryString.Value;

            queryString ??= "?";

            UpdateQueryString(ref queryString, "page", Page + pageDelta, _pagePattern);
            UpdateQueryString(ref queryString, "pageSize", PageSize, _pageSizePattern);

            return $"{scheme}://{host}{path ?? ""}{queryString}";
        }

        public string GetNextUrl() => BuildUrl(1);

        public string GetPreviousUrl() => BuildUrl(-1);
    }
}
