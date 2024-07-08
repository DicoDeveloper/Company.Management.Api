namespace Company.Management.Application.Core.Pagination;

public class Paginated
{
    public int Page { get; set; } = 1;

    public int Size { get; set; } = 100;

    public bool IsValid => Page > 0 && Size > 0;
}