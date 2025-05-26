namespace Tutorial9.Domain.Models;

public class PaginatedList<T> where T : class
{
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
    
    public int TotalPages { get; set; }

    public List<T> Items { get; set; } = [];
}