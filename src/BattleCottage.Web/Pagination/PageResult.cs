namespace BattleCottage.Web.Pagination
{
    public class PageResult<T>
    {
        public string? Next { get; set; }

        public string? Previous { get; set; }

        public ICollection<T>? Results { get; set; }
    }
}
