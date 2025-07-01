using JobTracking.Domain.Enums;

namespace JobTracking.Domain.Filters;

public class BaseFilter<T>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? SortBy { get; set; }
    public SortOrderEnum? SortDirection { get; set; }
    public T? Filter { get; set; }
}