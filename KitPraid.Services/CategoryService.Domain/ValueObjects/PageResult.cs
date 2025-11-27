namespace CategoryService.Domain.ValueObjects;

public class PageResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int Page {get;set;}
    public int Size {get;set;}
    private int TotalPages => (int)Math.Ceiling(TotalCount / (double)Size);
    
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;

    public PageResult(IEnumerable<T> items, int totalCount, int page, int size)
    {
        Items = items;
        TotalCount = totalCount;
        Page = page;
        Size = size;
    }
}