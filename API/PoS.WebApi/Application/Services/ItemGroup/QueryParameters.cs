namespace PoS.WebApi.Application.Services.ItemGroup;

public class QueryParameters
{
    //Pagination
    const int maxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 20;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > maxPageSize ? maxPageSize : value; }
    }

    //Search params
    public string Search { get; set; } = string.Empty;
}
