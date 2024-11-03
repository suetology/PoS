using System;

namespace PoS.WebApi.Application.Services.Discount
{
    public class QueryParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 20;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        // Filter properties
        public string Name { get; set; } = string.Empty;
        public decimal? Value { get; set; } // Nullable to allow filtering only if provided
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
