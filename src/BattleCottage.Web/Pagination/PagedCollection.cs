using System.Text.RegularExpressions;

namespace BattleCottage.Core.Pagination
{
    public class PagedCollection<T> : IPagedCollection<T>
    {
        private readonly string _pagePattern = @"page=\d+";
        private readonly string _pageSizePattern = @"pageSize=\d+";

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

            Results = values.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
        }

        public ICollection<T> Results { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int NextPageNumber { get; private set; }
        public int PreviousPageNumber { get; private set; }
        private HttpRequest HttpRequest { get; set; }

        public string GetNextUrl()
        {
            if (HttpRequest != null)
            {
                string host = HttpRequest.Host.Value;
                string? path = HttpRequest.Path.Value;
                string scheme = HttpRequest.Scheme;
                string? queryString = HttpRequest.QueryString.Value;

                queryString ??= "?";

                if (queryString.Contains("page="))
                {
                    queryString = Regex.Replace(queryString, _pagePattern, $"page={Page + 1}");
                }
                else
                {

                    if (!queryString.EndsWith("?") && !queryString.EndsWith("&")) queryString += "&";
                    queryString += $"page={Page + 1}";
                }

                if (queryString.Contains("pageSize="))
                {
                    queryString = Regex.Replace(queryString, _pageSizePattern, $"pageSize={PageSize}");
                }
                else
                {
                    if (!queryString.EndsWith("?") && !queryString.EndsWith("&")) queryString += "&";
                    queryString += $"pageSize={PageSize}";
                }

                string url = $"{scheme}://{host}{(path ?? "")}{queryString}";

                return url;
            }

            throw new ArgumentNullException(nameof(HttpRequest));
        }

        public string GetPreviousUrl()
        {
            if (HttpRequest != null)
            {
                string host = HttpRequest.Host.Value;
                string? path = HttpRequest.Path.Value;
                string scheme = HttpRequest.Scheme;
                string? queryString = HttpRequest.QueryString.Value;

                queryString ??= "?";

                if (queryString.Contains("page="))
                {
                    queryString = Regex.Replace(queryString, _pagePattern, $"page={Page - 1}");
                }
                else
                {
                    if (!queryString.EndsWith("?") && !queryString.EndsWith("&")) queryString += "&";
                    queryString += $"page={Page - 1}";
                }

                if (queryString.Contains("pageSize="))
                {
                    queryString = Regex.Replace(queryString, _pageSizePattern, $"pageSize={PageSize}");
                }
                else
                {
                    if (!queryString.EndsWith("?") && !queryString.EndsWith("&")) queryString += "&";
                    queryString += $"pageSize={PageSize}";
                }

                string url = $"{scheme}://{host}{(path ?? "")}{queryString}";

                return url;
            }

            throw new ArgumentNullException(nameof(HttpRequest));
        }
    }
}
