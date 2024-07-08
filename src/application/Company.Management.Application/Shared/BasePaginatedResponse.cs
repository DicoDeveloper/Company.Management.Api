namespace Company.Management.Application.Shared;

public abstract class BasePaginatedResponse<TData>(
    IEnumerable<TData> data,
    int currentPage,
    int totalItems,
    int totalPages
    )
{
    public IEnumerable<TData> Data { get; set; } = data;
    public int CurrentPage { get; set; } = currentPage;
    public int TotalItems { get; set; } = totalItems;
    public int TotalPages { get; set; } = totalPages;
}