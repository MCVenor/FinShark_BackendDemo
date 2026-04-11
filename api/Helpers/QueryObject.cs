using System.Runtime.CompilerServices;

namespace api.Helpers
{
    public class QueryObject
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string SortBy { get; set; } = string.Empty;
        public bool IsDecsending { get; set; } = false;
    }
}
