namespace ProductService.Domain.ValueObjects;

public class PageResult<T>
{
    public IEnumerable<T> Items { get; set; }
    private int TotalCount { get; set; }
    private int Page {get;set;}
    private int Size {get;set;}
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