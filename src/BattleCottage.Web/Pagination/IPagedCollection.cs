using BattleCottage.Web.Pagination;

namespace BattleCottage.Core.Pagination
{
    public interface IPagedCollection<T>
    {
        string BuildUrl(int pageDelta);

        string GetNextUrl();

        string GetPreviousUrl();

        PageResult<T> Result { get; set; }
    }
}
