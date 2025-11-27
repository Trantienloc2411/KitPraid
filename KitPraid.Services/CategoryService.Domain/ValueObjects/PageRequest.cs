namespace CategoryService.Domain.ValueObjects;

public class PageRequest
{
    public int Page { get; }
    public int Size { get; } = 10;

    public int Skip => (Page - 1) * Size;

    public PageRequest(int page, int size)
    {
        if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
        if (size < 1) throw new ArgumentOutOfRangeException(nameof(size));

        Page = page;
        Size = size;
    }
}