namespace SportsStore.Models.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public bool IsNextPage { get { return ItemsPerPage * CurrentPage < TotalItems; } }
        public bool IsPreviousPage { get { return CurrentPage > 1; } }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}