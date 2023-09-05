namespace BattleCottage.Core.Pagination
{
    public interface IPagedCollection<T>
    {
        string GetNextUrl();

        string GetPreviousUrl();
    }
}
