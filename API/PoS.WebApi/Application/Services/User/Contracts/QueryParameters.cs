using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.User;

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

    // Filtering by role
    public Role? Role { get; set; }

    //Sorting params
    public static readonly HashSet<string> AllowedSortFields = new HashSet<string>
    {
        "name",
        "surname",
        "username",
        "email",
        "dateOfEmployment",
        "role"
    };

    private string _orderBy = "name";

    public string OrderBy
    {
        get { return _orderBy; }
        set
        {
            if (AllowedSortFields.Contains(value.ToLower()))
            {
                _orderBy = value;
            }
            else
            {
                _orderBy = "name";
            }
        }
    }
    public bool OrderAsc { get; set; } = true;
}
