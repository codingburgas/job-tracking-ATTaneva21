using JobTracking.Domain.Enums;

public class BaseFilter<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortOrderEnum? SortDirection { get; set; }
    public T? Filter { get; set; } 
}