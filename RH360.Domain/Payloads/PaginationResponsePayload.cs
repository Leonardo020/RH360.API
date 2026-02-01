namespace RH360.Domain.Payloads
{
    public class PaginationResponsePayload<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PaginationResponsePayload() { }

        public PaginationResponsePayload(IEnumerable<T> items, int totalItems, int page, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            Page = page;
            PageSize = pageSize;
        }
    }
}
