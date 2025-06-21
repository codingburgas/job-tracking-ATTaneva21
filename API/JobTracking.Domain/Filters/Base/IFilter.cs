using JobTracking.Domain.Enums;

namespace JobTracking.Domain.Filters.Base
{
    public interface IFilter
    {
        int Page { get; set; }
        int PageSize { get; set; }
        string? SortBy { get; set; }
        SortOrderEnum? SortDirection { get; set; }
    }
}