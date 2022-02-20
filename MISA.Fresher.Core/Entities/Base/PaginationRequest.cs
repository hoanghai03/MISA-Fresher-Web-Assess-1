namespace MISA.Fresher.Core.Entities.Base
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string FilterText { get; set; }

    }
}
