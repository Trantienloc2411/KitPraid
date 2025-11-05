namespace ProductService.Domain.ValueObjects;

public class PageRequest
{
    public int Page { get; }
    public int Size { get; }
    public int Skip => (Page - 1) * Size;

    public PageRequest(int page, int size)
    {
        if (page < 1) throw new ArgumentOutOfRangeException("Page must be greater than zero");
        if (size < 1) throw new ArgumentOutOfRangeException("Size must be greater than zero");
        
        Page = page;
        Size = size;
    }
}