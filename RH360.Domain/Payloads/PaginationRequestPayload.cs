namespace RH360.Domain.Payloads
{
    public class PaginationRequestPayload
    {
        public int Page { get; set; } = 1;
        public string OrderBy { get; set; } = "id";
        public string? OrderDirection { get; set; } = "asc";
        public int PageSize { get; set; } = 10;
        public int Skip => (Page - 1) * PageSize;
        public int Take => PageSize;
    }

}
