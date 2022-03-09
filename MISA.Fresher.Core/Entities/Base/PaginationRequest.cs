namespace MISA.Fresher.Core.Entities.Base
{
    /// <summary>
    /// Pagination
    /// createdBy NHHai 25/2/2022
    /// </summary>
    public class PaginationRequest
    {
        // vị trí trang
        public int PageNumber { get; set; }

        // kích thước trang
        public int PageSize { get; set; }

        // chuỗi filter
        public string FilterText { get; set; }

    }
}
